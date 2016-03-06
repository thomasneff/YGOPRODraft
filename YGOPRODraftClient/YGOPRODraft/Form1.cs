using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Lidgren.Network;
using System.Reflection;

namespace YGOPRODraft
{

    

    public partial class Form1: Form
    {

        public Dictionary<Panel, int> scroll_val_map;

        int small_card_height = 60;
        int small_card_spacing = 10;
        int SIGNATURE_SCROLL_VAL = -133789929;
        String server_ip_text = "";
        struct settingsStructJSONClient
        {
            public String ygopro_path;
            public String ServerIP;
        }

        public Form1()
        {
            
            m_ygopro_ids = new List<int>();
            InitializeComponent();

            panelExtraDeck.MouseWheel += new MouseEventHandler(scroll_in_panel);
            panelMainDeck.MouseWheel += new MouseEventHandler(scroll_in_panel);
            panelDraftCards.MouseWheel += new MouseEventHandler(scroll_in_panel);
            panelSideDeck.MouseWheel += new MouseEventHandler(scroll_in_panel);
            // card_modify_mutex = new Mutex();
            scroll_val_map = new Dictionary<Panel, int>();
            extra_deck = new List<YGOPROCard>();

                main_deck = new List<YGOPROCard>();
            
      
                side_deck = new List<YGOPROCard>();
        
                draft_cards = new List<YGOPROCard>();
            scroll_val_map[panelDraftCards] = 0;
            scroll_val_map[panelMainDeck] = 0;
            scroll_val_map[panelSideDeck] = 0;
            scroll_val_map[panelExtraDeck] = 0;
            cardShowPanel.BackgroundImageLayout = ImageLayout.Stretch;
            readSettingsJSON();
        }

        private static Object card_modify_mutex = new Object();
        List<int> m_ygopro_ids;
        String ygopro_db_path = "";
        int monsters = 0;
        int spells = 0;
        int traps = 0;
     
        List<YGOPROCard> main_deck;
        List<YGOPROCard> extra_deck;
        List<YGOPROCard> draft_cards;
        List<YGOPROCard> side_deck;
        NetClient server_conn;
        Thread m_receiveThread;
  
        NetConnection netcon;

        public void sendMSG(String MSG)
        {
            var message = server_conn.CreateMessage();
            message.Write(MSG);

            server_conn.SendMessage(message, netcon, NetDeliveryMethod.ReliableOrdered);
        }


        public void sendCardIds(String MSG)
        {
            var message = server_conn.CreateMessage();
            message.Write(MSG);
            String json = "";
           
                json = JsonConvert.SerializeObject(m_ygopro_ids);
            
            message.Write(json);
            server_conn.SendMessage(message, netcon, NetDeliveryMethod.ReliableOrdered);
        }
       // Bitmap draftPanelBuffer = null;
        public void renderDraftCards(YGOPROCard focused_card, Panel draw_panel, List<YGOPROCard> cards, int xMouse, int yMouse)
        {
            if (cards == null)
                return;

            if (panelDraftCards.InvokeRequired)
            {
                this.BeginInvoke(new Action<YGOPROCard, Panel, List<YGOPROCard>, int, int>(renderDraftCards), new object[] {  focused_card,  draw_panel,  cards,  xMouse,  yMouse });
                return;
            }

            //if (cards.Count == 0)
            //     return;
            lock (card_modify_mutex)
            {
                int width = draw_panel.Width;
            int height = draw_panel.Height;
           // if (draftPanelBuffer == null)
                Bitmap draftPanelBuffer = new Bitmap(width, height);
            int spacing = small_card_spacing;
            int card_width = 40;
            int card_height = small_card_height;
            int num_per_row = width / (card_width + spacing);
            Graphics gr = Graphics.FromImage(draftPanelBuffer);
            int x = spacing;
            int y = 0;
                int max_y = cards.Count / num_per_row * (card_height + spacing) + card_height;
               
            //calc y offset
            if(scroll_val_map[draw_panel] != SIGNATURE_SCROLL_VAL)
                {


                    if (scroll_val_map[draw_panel] < 0)
                    {
                        //cards scrolling up, neg delta
                        y += scroll_val_map[draw_panel];
                        if (max_y < height)
                        {
                            y = spacing;
                            scroll_val_map[draw_panel] = 0;
                        }
                        else if (y < height - max_y)
                        {
                            y = height - max_y;
                            scroll_val_map[draw_panel] = height - max_y;
                        }

                        //  scroll_val_map[draw_panel] = SIGNATURE_SCROLL_VAL;
                    }

                    else if (scroll_val_map[draw_panel] > 0)
                    {
                        //cards scrolling down. -> pos delta
                        y += scroll_val_map[draw_panel];
                        if (max_y < height)
                        {
                            y = spacing;
                            scroll_val_map[draw_panel] = 0;
                        }
                        else if (y > spacing)
                        {
                            y = spacing;
                            scroll_val_map[draw_panel] = y;
                        }

                        //  scroll_val_map[draw_panel] = SIGNATURE_SCROLL_VAL;
                    }
                    else
                    {
                        y = spacing;
                    }

                }



                gr.Clear(Color.DarkGray);
            
            
                foreach(YGOPROCard card in cards)
                {
                    if (card.isInRect(xMouse, yMouse))
                        focused_card = card;

                    if (card != focused_card)
                    {
                        card.m_drawing_rect = new Rectangle(x, y, card_width, card_height);
                        gr.DrawImage(card.m_card_image, card.m_drawing_rect);
                    }
                    else
                    {
                        card.m_drawing_rect = new Rectangle(x - spacing / 2, y - spacing / 2, card_width + spacing, card_height + spacing);
                        gr.DrawImage(card.m_card_image, card.m_drawing_rect);
                    }

                    x += card_width + spacing;
                    if (x + card_width > width)
                    {
                        x = spacing;
                        y += card_height + spacing;
                    }

               
                }
                draw_panel.BackgroundImage = draftPanelBuffer;
                draw_panel.Invalidate();
                gr.Dispose();
            }
            UpdateCardDetailedInfo(focused_card);
        }


        public void getDraftCardsFromIds()
        {
            lock (card_modify_mutex)
            {
                    foreach (int id in m_ygopro_ids)
                {
                
                        draft_cards.Add(YGOPROCard.QueryFromID(id, ygopro_db_path));
                
                
                }
            }
        }
        bool drafting = false;
        public void receiveThreadStart()
        {
            var config = new NetPeerConfiguration("YGOPRODraft");
         //   config.EnableUPnP = true;
            server_conn = new NetClient(config);
            
            server_conn.Start();
            netcon = server_conn.Connect(host: server_ip_text, port: 12345);
            
            while (true)
            {
                server_conn.MessageReceivedEvent.WaitOne();

                //we got a msg.
                //for startup, we need every client to send a "CONNECT" string.
                NetIncomingMessage msg;
              
                while ((msg = server_conn.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.Data:
                            {
                                String cmd = msg.ReadString();
                                if(cmd.Equals("DRAFTCARDS"))
                                {
                                    if(draft_cards != null)

                                        lock (card_modify_mutex)
                                        {
                                            draft_cards = new List<YGOPROCard>();
                                        
                                    String json = msg.ReadString();
                                    printDebugConsole("JSON GOT! " + json);

                                    m_ygopro_ids = new List<int>();
                                    m_ygopro_ids = JsonConvert.DeserializeObject<List<int>>(json);
                                        }
                                    getDraftCardsFromIds();
                                   if(draft_cards.Count != 0)
                                        renderDraftCards(draft_cards[0], panelDraftCards, draft_cards, -1, -1);
                                    else
                                        renderDraftCards(null, panelDraftCards, draft_cards, -1, -1);
                                    printDebugConsole(m_ygopro_ids);
                                }
                                else if(cmd.Equals("QUITDRAFTING"))
                                {
                                    //TODO: save deck. we're done.
                                    finishDraft();
                                    drafting = false;
                                    server_conn.Disconnect("BYE");
                                    return;
                                }
                            }
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            // handle connection status messages
                                break;
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            Console.WriteLine(msg.ReadString());
                            break;
                        default:
                            Console.WriteLine("Unhandled type: " + msg.MessageType);
                            break;
                    }


                    server_conn.Recycle(msg);
                }
            }

            m_receiveThread.Suspend();
        }

        void finishDraft()
        {

            if (panelDraftCards.InvokeRequired)
            {
                this.BeginInvoke(new Action(finishDraft), new object[] { });
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Drafted Deck!";
            String trimmed = ygopro_db_path.TrimEnd("cards.cdb".ToCharArray());
            sfd.FileName =  "myDraftDeck.ydk";
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = ygopro_db_path + "\\deck\\";

            sfd.ShowDialog();

            String file_path = sfd.FileName;

            List<String> lines = new List<string>();
            lines.Add("#created by YGOPRODraft");
            lines.Add("#main");

            foreach (YGOPROCard card in main_deck)
                lines.Add(card.m_ygopro_id.ToString());

            lines.Add("#extra");
            foreach (YGOPROCard card in extra_deck)
                lines.Add(card.m_ygopro_id.ToString());

            lines.Add("#side");
            foreach (YGOPROCard card in side_deck)
                lines.Add(card.m_ygopro_id.ToString());

            File.WriteAllLines(file_path, lines.ToArray());
        }

        void printDebugConsole(String line)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(printDebugConsole), new object[] { line });
                return;
            }
            DebugConsole.AppendText(line + Environment.NewLine);

            DebugConsole.ScrollToCaret();
        }

        void printDebugConsole(List<String> lines)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<List<String>>(printDebugConsole), new object[] { lines });
                return;
            }
            foreach (String line in lines)
                DebugConsole.AppendText(line + Environment.NewLine);
            DebugConsole.ScrollToCaret();
        }
        void printDebugConsole(List<int> lines)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<List<int>>(printDebugConsole), new object[] { lines });
                return;
            }
            foreach (int line in lines)
                DebugConsole.AppendText(line.ToString() + Environment.NewLine);
            DebugConsole.ScrollToCaret();
        }


        private void FindYGOPRODBBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.ShowDialog();

                ygopro_db_path = ofd.FileName;

            }
            catch
            {

            }
        }

   

        private void startServerButton_Click(object sender, EventArgs e)
        {
            
        }

        private void ConfigurePackBtn_Click(object sender, EventArgs e)
        {
            if (!drafting)
            {

                lock (card_modify_mutex)
                {
                    m_ygopro_ids = new List<int>();
                    extra_deck = new List<YGOPROCard>();
                }
                lock (card_modify_mutex)
                {
                    main_deck = new List<YGOPROCard>();
                }
                lock (card_modify_mutex)
                {
                    side_deck = new List<YGOPROCard>();
                }
                lock (card_modify_mutex)
                {
                    draft_cards = new List<YGOPROCard>();
                }
                monsters = 0;
                spells = 0;
                traps = 0;
                m_receiveThread = new Thread(receiveThreadStart);
                m_receiveThread.Start();
            }
   

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!drafting)
            {
                
                lock(card_modify_mutex)
                {
                    m_ygopro_ids = new List<int>();
                    extra_deck = new List<YGOPROCard>();
                }
                lock(card_modify_mutex)
                {
                    main_deck = new List<YGOPROCard>();
                }
                lock(card_modify_mutex)
                {
                    side_deck = new List<YGOPROCard>();
                }
                lock(card_modify_mutex)
                {
                    draft_cards = new List<YGOPROCard>();
                }
            }
            drafting = true;
            sendMSG("READY");
        }

        private void readSettingsJSON()
        {
            settingsStructJSONClient set;
            String json = "";
            try
            {
                json = File.ReadAllText("ygoprodraft.settings");
            }
            catch
            {
                return;
            }


            set = JsonConvert.DeserializeObject<settingsStructJSONClient>(json);

            ygopro_db_path = set.ygopro_path;
            server_ip_text = set.ServerIP;
            textBoxIP.Text = server_ip_text;
            labelPath.Text = "" + ygopro_db_path;
        }

        private void saveSettingsJSON()
        {
            settingsStructJSONClient set;
            set.ServerIP = server_ip_text;
            set.ygopro_path = ygopro_db_path;

            String json = JsonConvert.SerializeObject(set, Formatting.Indented);
            File.WriteAllText("ygoprodraft.settings", json);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(server_conn != null)
               server_conn.Disconnect("cya");

           
            saveSettingsJSON();
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO JUST A TEST PLZ REMOVE
           // m_ygopro_ids.RemoveAt(new Random().Next(m_ygopro_ids.Count));
           // sendCardIds("CONTINUEDRAFT");
        }

     

        public void UpdateCardDetailedInfo(YGOPROCard my_card)
        {
            if (my_card == null)
                return;
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<YGOPROCard>(UpdateCardDetailedInfo), new object[] { my_card });
                return;
            }
            cardShowPanel.BackgroundImage = my_card.m_card_image;
            //cardShowPanel.Size = my_card.m_card_image.Size;
            cardShowPanel.Refresh();
            labelCardAtk.Text = "ATK: " + my_card.m_card_atk;
            labelCardAttr.Text = "Attibute: " + my_card.m_card_attr;
            labelCardDef.Text = "DEF: " + my_card.m_card_def;
            labelCardDescription.Text = my_card.m_card_desc;
            labelCardLevel.Text = "Level " + my_card.m_card_level;
            labelCardName.Text = my_card.m_card_name;
            labelCardRace.Text = my_card.m_card_race;
            labelCardType.Text = my_card.m_card_type;
        }


        private void FindYGOPRODBBtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.ShowDialog();

                ygopro_db_path = ofd.FileName;
                labelPath.Text = "" + ygopro_db_path;
            }
            catch
            {

            }

           // YGOPROCard my_card = YGOPROCard.QueryFromID(1546123, ygopro_db_path);
           
        }

        private void panelDraftCards_MouseMove(object sender, MouseEventArgs e)
        {
            Panel s_panel = (Panel)sender;
            
            if(s_panel == panelDraftCards)
            {
                renderDraftCards(null, s_panel, draft_cards, e.X, e.Y);
            }
            else if(s_panel == panelMainDeck)
            {
                renderDraftCards(null, s_panel, main_deck, e.X, e.Y);
            }
            else if (s_panel == panelExtraDeck)
            {
                renderDraftCards(null, s_panel, extra_deck, e.X, e.Y);
            }
            else if(s_panel == panelSideDeck)
            {
                renderDraftCards(null, s_panel, side_deck, e.X, e.Y);
            }

        }

        private void panelDraftCards_Paint(object sender, PaintEventArgs e)
        {
            
        }


        private void scroll_in_panel(object sender, MouseEventArgs e)
        {

            //if I want to include user32.dll, this should be easy. dont want to yet.
            Panel s_panel = (Panel)sender;

            scroll_val_map[s_panel] = scroll_val_map[s_panel] + e.Delta/2;


            


                if (s_panel == panelDraftCards)
            {
                    renderDraftCards(null, s_panel, draft_cards, e.X, e.Y);
            }
            else if (s_panel == panelMainDeck)
            {
                renderDraftCards(null, s_panel, main_deck, e.X, e.Y);
            }
            else if (s_panel == panelExtraDeck)
            {
                renderDraftCards(null, s_panel, extra_deck, e.X, e.Y);
            }
            else if (s_panel == panelSideDeck)
            {
                renderDraftCards(null, s_panel, side_deck, e.X, e.Y);
            }
        }

        private void panelDraftCards_MouseEnter(object sender, EventArgs e)
        {
            // labelCardDescription.Focus();
            ((Panel)(sender)).Focus();
        }

        private void panelMainDeck_Enter(object sender, EventArgs e)
        {
            ((Panel)(sender)).Focus();
        }

        private void panelExtraDeck_MouseEnter(object sender, EventArgs e)
        {
            //  labelCardDescription.Focus();
            ((Panel)(sender)).Focus();
        }

        private void panelMainDeck_MouseEnter(object sender, EventArgs e)
        {
            //  labelCardDescription.Focus();
            ((Panel)(sender)).Focus();
        }


        void switchCardToSide(int x, int y, bool main)
        {
            lock(card_modify_mutex)
            {
                YGOPROCard chosen_card = null;
                if(main)
                {
                    foreach (YGOPROCard card in main_deck)
                    {
                        if (card.isInRect(x, y))
                        {
                            chosen_card = card;
                            break;
                        }
                    }

                }
                 else
                {
                    foreach (YGOPROCard card in extra_deck)
                    {
                        if (card.isInRect(x, y))
                        {
                            chosen_card = card;
                            break;
                        }
                    }
                }
                if (chosen_card == null)
                    return;
                chosen_card.m_drawing_rect = new Rectangle();

                if (main)
                {
                    main_deck.Remove(chosen_card);
                    side_deck.Add(chosen_card);
                    renderDraftCards(null, panelMainDeck, main_deck, -1, -1);
                    renderDraftCards(chosen_card, panelSideDeck, side_deck, -1, -1);
                }
                else
                {
                    extra_deck.Remove(chosen_card);
                    side_deck.Add(chosen_card);
                    renderDraftCards(null, panelMainDeck, main_deck, -1, -1);
                    renderDraftCards(chosen_card, panelSideDeck, side_deck, -1, -1);
                }
            }
         
        }

        void switchCardToMain(int x, int y)
        {
            lock (card_modify_mutex)
            {
                YGOPROCard chosen_card = null;
                foreach (YGOPROCard card in side_deck)
                {
                    if (card.isInRect(x, y))
                    {
                        chosen_card = card;
                        break;
                    }
                }
                if (chosen_card == null)
                    return;
                chosen_card.m_drawing_rect = new Rectangle();




                if (!chosen_card.m_card_extra)
                {
                    side_deck.Remove(chosen_card);
                    main_deck.Add(chosen_card);
                    renderDraftCards(null, panelSideDeck, side_deck, -1, -1);
                    renderDraftCards(chosen_card, panelMainDeck, main_deck, -1, -1);
                }
                else
                {
                    side_deck.Remove(chosen_card);
                    extra_deck.Add(chosen_card);
                    renderDraftCards(null, panelSideDeck, side_deck, -1, -1);
                    renderDraftCards(chosen_card, panelMainDeck, main_deck, -1, -1);

                }
            }
        }

        void updateCardCountLabel()
        {
            if (labelNumMTS.InvokeRequired)
            {
                this.BeginInvoke(new Action(updateCardCountLabel), new object[] { });
                return;
            }

            labelNumMTS.Text = "Monsters: " + monsters + ", Spells: " + spells + ", Traps: " + traps + ", Sum: " + (monsters + spells + traps).ToString();
        }

        void selectCardForDeck(int x, int y, bool left_mouse_button)
        {
            lock (card_modify_mutex)
            {
                YGOPROCard chosen_card = null;
                foreach (YGOPROCard card in draft_cards)
                {
                    if (card.isInRect(x, y))
                    {
                        chosen_card = card;
                        break;
                    }
                }
                if (chosen_card == null)
                    return;
                chosen_card.m_drawing_rect = new Rectangle();

                if (!left_mouse_button)
                {
                    side_deck.Add(chosen_card);
                    renderDraftCards(chosen_card, panelSideDeck, side_deck, -1, -1);
                }
                else
                {
                    if (chosen_card.m_card_extra)
                    {

                        extra_deck.Add(chosen_card);
                        renderDraftCards(chosen_card, panelExtraDeck, extra_deck, -1, -1);

                    }
                    else
                    {
                        main_deck.Add(chosen_card);
                        renderDraftCards(chosen_card, panelMainDeck, main_deck, -1, -1);
                    }
                }

                draft_cards.Remove(chosen_card);

                if((chosen_card.m_raw_type & 0x01) != 0)
                {
                    //monster
                    monsters++;
                }
                else if ((chosen_card.m_raw_type & 0x02) != 0)
                {
                    spells++;
                }
                else if((chosen_card.m_raw_type & 0x04) != 0)
                {
                    traps++;
                }

                updateCardCountLabel();
                    m_ygopro_ids.Remove(chosen_card.m_ygopro_id);
                // m_ygopro_ids.Remove(chosen_card.)
                sendCardIds("CONTINUEDRAFT");

               
                draft_cards.Clear();
                m_ygopro_ids.Clear();
            }
            

        }


        private void panelDraftCards_MouseClick(object sender, MouseEventArgs e)
        {   
            selectCardForDeck(e.X, e.Y, e.Button == MouseButtons.Left);
        }

        private void panelMainDeck_MouseClick(object sender, MouseEventArgs e)
        {
            switchCardToSide(e.X, e.Y, ((Panel)sender) == panelMainDeck);
        }

        private void panelSideDeck_MouseClick(object sender, MouseEventArgs e)
        {
            switchCardToMain(e.X, e.Y);
        }

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {
            server_ip_text = textBoxIP.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            finishDraft();
        }
    }
}
