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

namespace YGOPRODraft
{


  
    public partial class Form1: Form
    {
        public class NCComparer : IComparer<NetConnection>
        {
            public int Compare(NetConnection x, NetConnection y)
            {
                // TODO: Handle x or y being null, or them not having names
                return (int)(x.RemoteUniqueIdentifier - y.RemoteUniqueIdentifier);
            }
        }
        struct RarityDistribution
        {

            public double rare;
            public double super_rare;
            public double ultra_rare;
            public double secret_rare;
        }
        struct settingsStructJSONServer
        {
            public String ygopro_path;
            public String packs_path;
        }
        public class BoosterPool
        {
            public List<int> commons;
            public List<int> rares;
            public List<int> super_rares;
            public List<int> ultra_rares;
            public List<int> secret_rares;
            public String path;
        }

        List<BoosterPool> m_booster_pools;

        int m_numCardsPerBooster = 7;
        int m_numRaresPerBooster = 1;
        int m_numCardsPerDraftRound = 5;
        int m_numCardsPerDeck = 45;
        int m_cardCounter = 0;
        int m_packIdx = 0;
        bool m_pullOnlyOne = false;

        List<String> json_filenames;
      
  
        struct YGOJSONStruct
       {
           public String rarity;
           public String set_number;
           public String name;
       }
        String m_pack_path = "";
        RarityDistribution m_dist;
        public Form1()
        {
            m_total_card_names = new List<String>();
            m_ygopro_ids = new List<int>();
            client_card_ids = new SortedDictionary<NetConnection, List<int>>(new NCComparer());
            m_booster_pools = new List<BoosterPool>();
           
            InitializeComponent();
            readSettingsJSON();
        }

        List<String> extractRarityList(List<YGOJSONStruct> json_list, String rarity)
        {
            List<String> list = new List<String>();

            foreach(YGOJSONStruct ygo in json_list)
            {
                YGOJSONStruct ygo_var = ygo;
                if (ygo_var.rarity.Contains("\n"))
                    ygo_var.rarity = ygo_var.rarity.Split('\n')[0];

                if(ygo_var.rarity.ToLower().Equals(rarity))
                {
                    list.Add(ygo.name);
                }
            }

            return list;
        }


        List<String> extractRarityListContains(List<YGOJSONStruct> json_list, String rarity)
        {
            List<String> list = new List<String>();

            foreach (YGOJSONStruct ygo in json_list)
            {
                if (ygo.rarity.ToLower().Contains(rarity))
                {
                    list.Add(ygo.name);
                }
            }

            return list;
        }


        List<string> m_total_card_names;

        List<int> m_ygopro_ids;
        String ygopro_db_path = "";

        List<int> query_ygopro_ids(List<string> card_names)
        {
            List<int> ids = new List<int>();
            try
            {




                using (SQLiteConnection connect = new SQLiteConnection("Data Source=" + ygopro_db_path))
                {
                    connect.Open();
                    foreach (String name in card_names)
                    {
                        String new_name = name.Replace("'", "''");
                        using (SQLiteCommand fmd = connect.CreateCommand())
                        {
                            fmd.CommandText = "SELECT id from texts where name is '" + new_name + "'";
                            fmd.CommandType = CommandType.Text;
                            
                            SQLiteDataReader r = fmd.ExecuteReader();
                            try
                            { 
                                if (r.Read())
                                {
                                    int a = r.GetInt32(0);
                                    ids.Add(a);

                                }
                                else
                                {
                                    fmd.CommandText = "SELECT id from texts where name like '%" + new_name + "%'";
                                    fmd.CommandType = CommandType.Text;

                                    r = fmd.ExecuteReader();
                                    if (r.Read())
                                    {
                                        int a = r.GetInt32(0);
                                        ids.Add(a);

                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                    }


                }
            }
            catch
            {

            }

            return ids;
        }
        Random rng = new Random();
        void addRandomCardFromPool(List<int> dest, List<int> src, List<int> all_cards, bool remove_from_pool)
        {
           
            if (src.Count == 0)
                return;
            int idx = rng.Next(src.Count);
            dest.Add(src[idx]);
            if (remove_from_pool)
            {
                if(src != all_cards)
                    all_cards.Remove(src[idx]);
                src.RemoveAt(idx);     
            }
        }


        List<int> pullFromBoosterPoolWithRarityDistribution(BoosterPool pool, bool remove_from_pool, RarityDistribution dist, int number_of_cards, int number_of_rares_or_better)
        {
            List<int> ret_list = new List<int>();
            //rarity calc
            
            
            //define probability ranges for convenience
            double rare_bound = m_dist.rare;
            double super_rare_bound = rare_bound + m_dist.super_rare;
            double ultra_rare_bound = super_rare_bound + m_dist.ultra_rare;
            double secret_rare_bound = ultra_rare_bound + m_dist.secret_rare; ;
            List<int> all_cards = new List<int>();
            all_cards.AddRange(pool.commons);
            all_cards.AddRange(pool.rares);
            all_cards.AddRange(pool.super_rares);
            all_cards.AddRange(pool.ultra_rares);
            all_cards.AddRange(pool.secret_rares);
            for (int i = 0; i < number_of_rares_or_better; i++)
            {
                double rng_val = rng.NextDouble();

                //don't care about ultimate/ghost whatever

                if (rng_val <= rare_bound)
                {
                    //rarity = "Rare";
                    //get random card out of rares
                    if (pool.rares.Count == 0)
                    {
                        //card_names.Add(commons[rng.Next(commons.Count)]);
                        //TODO: what if there are no commons? cant fall back. maybe just use a random one.
                        //ret_list.Add(all_cards[rng.Next(all_cards.Count)]);
                        addRandomCardFromPool(ret_list, all_cards, all_cards, remove_from_pool);
                        //card_names.Add(json_list[rng.Next(json_list.Count)].name);
                    }
                    else
                    {
                        //card_names.Add(rares[rng.Next(rares.Count)]);
                        addRandomCardFromPool(ret_list, pool.rares, all_cards, remove_from_pool);
                    }
                }
                else if (rng_val > rare_bound && rng_val <= super_rare_bound)
                {
                    // rarity = "Super Rare";
                    if (pool.super_rares.Count == 0)
                    {
                        if (pool.rares.Count == 0)
                        {
                            addRandomCardFromPool(ret_list, pool.commons, all_cards, remove_from_pool);
                        }
                        else
                        {
                            addRandomCardFromPool(ret_list, pool.rares, all_cards, remove_from_pool);
                        }

                    }
                    else
                    {
                        addRandomCardFromPool(ret_list, pool.super_rares, all_cards, remove_from_pool);
                    }
                }
                else if (rng_val > super_rare_bound && rng_val <= ultra_rare_bound)
                {
                    //rarity = "Ultra Rare";
                    if (pool.ultra_rares.Count == 0)
                    {
                        if (pool.super_rares.Count == 0)
                        {
                            if (pool.rares.Count == 0)
                            {
                                addRandomCardFromPool(ret_list, pool.commons, all_cards, remove_from_pool);
                            }
                            else
                            {
                                addRandomCardFromPool(ret_list, pool.rares, all_cards, remove_from_pool);
                            }

                        }
                        else
                        {
                            addRandomCardFromPool(ret_list, pool.super_rares, all_cards, remove_from_pool);
                        }
                    }
                    else
                    {
                        addRandomCardFromPool(ret_list, pool.ultra_rares, all_cards, remove_from_pool);
                    }
                }
                else
                {
                    // rarity = "Secret Rare";
                    if (pool.secret_rares.Count == 0)
                    {
                        if (pool.ultra_rares.Count == 0)
                        {
                            if (pool.super_rares.Count == 0)
                            {
                                if (pool.rares.Count == 0)
                                {
                                    addRandomCardFromPool(ret_list, pool.commons, all_cards, remove_from_pool);
                                }
                                else
                                {
                                    addRandomCardFromPool(ret_list, pool.rares, all_cards, remove_from_pool);
                                }

                            }
                            else
                            {
                                addRandomCardFromPool(ret_list, pool.super_rares, all_cards, remove_from_pool);
                            }
                        }
                        else
                        {
                            addRandomCardFromPool(ret_list, pool.ultra_rares, all_cards, remove_from_pool);
                        }

                    }
                    else
                    {
                        addRandomCardFromPool(ret_list, pool.secret_rares, all_cards, remove_from_pool);
                    }

                }


            }

            for (int i = 0; i < number_of_cards - number_of_rares_or_better; i++)
            {
                //rest is commons
                if (pool.commons.Count != 0)
                    addRandomCardFromPool(ret_list, pool.commons, all_cards, remove_from_pool);
                else
                    addRandomCardFromPool(ret_list, all_cards, all_cards, remove_from_pool);
            }



            return ret_list;
        }
        private void readSettingsJSON()
        {
            settingsStructJSONServer set;
            String json = "";
            try
            {
                json = File.ReadAllText("ygoprodraft.settings");
            }
            catch
            {
                return;
            }


            set = JsonConvert.DeserializeObject<settingsStructJSONServer>(json);

            ygopro_db_path = set.ygopro_path;
            labelPath.Text = ygopro_db_path;
            m_pack_path = set.packs_path;
            try { 
            AddJSONPacksToList(m_pack_path);
            }
            catch
            {

            }
           // server_ip_text = set.ServerIP;
           //  textBoxIP.Text = server_ip_text;
           //  labelPath.Text = "" + ygopro_db_path;
        }

        private void saveSettingsJSON()
        {
            settingsStructJSONServer set;
            set.packs_path = m_pack_path;
            set.ygopro_path = ygopro_db_path;

            String json = JsonConvert.SerializeObject(set, Formatting.Indented);
            File.WriteAllText("ygoprodraft.settings", json);
        }


        BoosterPool getBoosterPoolFromJSON(String JSONFileName)
        {
            BoosterPool new_pool = new BoosterPool();
            new_pool.path = JSONFileName;
            //first, get all respective names


            String json = System.IO.File.ReadAllText(JSONFileName);


            //TODO: read JSON
            List<YGOJSONStruct> json_list = JsonConvert.DeserializeObject<List<YGOJSONStruct>>(json);

            List<String> rares = extractRarityList(json_list, "rare");
            List<String> super_rares = extractRarityList(json_list, "super rare");
            List<String> commons = extractRarityList(json_list, "common");
            List<String> ultra_rares = extractRarityList(json_list, "ultra rare");
            List<String> secret_rares = extractRarityList(json_list, "secret rare");

            //next get BoosterPool for these names from ygopro database
           
            new_pool.commons = query_ygopro_ids(commons);
            new_pool.rares = query_ygopro_ids(rares);
            new_pool.super_rares = query_ygopro_ids(super_rares);
            new_pool.ultra_rares = query_ygopro_ids(ultra_rares);
            new_pool.secret_rares = query_ygopro_ids(secret_rares);



            return new_pool;
        }

        List<String> pullFromJSON(String JSONFileName, RarityDistribution dist, int number_of_cards, int number_of_rares_or_better)
        {
            List<String> card_names = new List<String>();

            String json = System.IO.File.ReadAllText(JSONFileName);


            //TODO: read JSON
            List<YGOJSONStruct> json_list = JsonConvert.DeserializeObject<List<YGOJSONStruct>>(json);

            //get rng
            rng = new Random();
            String rarity = "";
            List<String> rares = extractRarityList(json_list, "rare");
            List<String> super_rares = extractRarityList(json_list, "super rare");
            List<String> commons = extractRarityList(json_list, "common");
            List<String> ultra_rares = extractRarityList(json_list, "ultra rare");
            List<String> secret_rares = extractRarityList(json_list, "secret rare");

            //define probability ranges for convenience
            double rare_bound = m_dist.rare;
            double super_rare_bound = rare_bound + m_dist.super_rare;
            double ultra_rare_bound = super_rare_bound + m_dist.ultra_rare;
            double secret_rare_bound = ultra_rare_bound + m_dist.secret_rare; ;

            for(int i = 0; i < number_of_rares_or_better; i++)
            {
                double rng_val = rng.NextDouble();

                //don't care about ultimate/ghost whatever

                if(rng_val <= rare_bound)
                {
                    rarity = "Rare";
                    //get random card out of rares
                    if (rares.Count == 0)
                    {
                        //card_names.Add(commons[rng.Next(commons.Count)]);
                        //TODO: what if there are no commons? cant fall back. maybe just use a random one.
                        card_names.Add(json_list[rng.Next(json_list.Count)].name);
                    }
                    else
                    {
                        card_names.Add(rares[rng.Next(rares.Count)]);
                    }
                }
                else if(rng_val > rare_bound && rng_val <= super_rare_bound)
                {
                    rarity = "Super Rare";
                    if (super_rares.Count == 0)
                    {
                        if (rares.Count == 0)
                        {
                            card_names.Add(commons[rng.Next(commons.Count)]);
                        }
                        else
                        {
                            card_names.Add(rares[rng.Next(rares.Count)]);
                        }

                    }
                    else
                    {
                        card_names.Add(super_rares[rng.Next(super_rares.Count)]);
                    }
                }
                else if (rng_val > super_rare_bound && rng_val <= ultra_rare_bound)
                {
                    rarity = "Ultra Rare";
                    if (ultra_rares.Count == 0)
                    {
                        if (super_rares.Count == 0)
                        {
                            if (rares.Count == 0)
                            {
                                card_names.Add(commons[rng.Next(commons.Count)]);
                            }
                            else
                            {
                                card_names.Add(rares[rng.Next(rares.Count)]);
                            }

                        }
                        else
                        {
                            card_names.Add(super_rares[rng.Next(super_rares.Count)]);
                        }
                    }
                    else
                    {
                        card_names.Add(ultra_rares[rng.Next(ultra_rares.Count)]);
                    }
                }
                else
                {
                    rarity = "Secret Rare";
                    if(secret_rares.Count == 0)
                    {
                        if(ultra_rares.Count == 0)
                        {
                            if(super_rares.Count == 0)
                            {
                                if(rares.Count == 0)
                                {
                                    card_names.Add(commons[rng.Next(commons.Count)]);
                                }
                                else
                                {
                                    card_names.Add(rares[rng.Next(rares.Count)]);
                                }
                                
                            }
                            else
                            {
                                card_names.Add(super_rares[rng.Next(super_rares.Count)]);
                            }
                        }
                        else
                        {
                            card_names.Add(ultra_rares[rng.Next(ultra_rares.Count)]);
                        }
                        
                    }
                    else
                    {
                        card_names.Add(secret_rares[rng.Next(secret_rares.Count)]);
                    }
                    
                }


            }
            
            for(int i = 0; i < number_of_cards - number_of_rares_or_better; i++)
            {
                //rest is commons
                if(commons.Count != 0)
                  card_names.Add(commons[rng.Next(commons.Count)]);
                else
                  card_names.Add(json_list[rng.Next(json_list.Count)].name);
            }


            return card_names;
        }
        List<NetConnection> clients;
        List<long> ready_clients;
        List<long> cd_clients;
        Thread m_receiveThread;
        bool drafting = false;
        bool swapping_and_sending_draft_cards = false;
        SortedDictionary<NetConnection, List<int>> client_card_ids;
        public void beginDraft()
        {
            //for each client, draft congigured set of cards
            m_booster_pools = new List<BoosterPool>();
            List<String> json_paths = ListChosenPacks.Items.OfType<String>().ToList();
            //  m_total_card_names = new List<String>();
            foreach (String JSONFileName in json_paths)
            {
                String full_path = Directory.GetCurrentDirectory() + "\\jsons\\" + JSONFileName;
                m_booster_pools.Add(getBoosterPoolFromJSON(full_path));
               
            }
            foreach (NetConnection nc in clients)
            {
                
                client_card_ids[nc] = new List<int>();
                m_dist.ultra_rare = 1.0f / 6.0f;
                m_dist.super_rare = 1.0f / 4.0f;
                m_dist.secret_rare = 1.0f / 12.0f;
                m_dist.rare = 1 - m_dist.secret_rare - m_dist.super_rare - m_dist.ultra_rare;

                //setup BoosterPools
           
                // 
             //   printDebugConsole("Pulled the following cards: ");
               //  printDebugConsole(m_total_card_names);

                //TODO: some cards are not in ygopro database... rip
           /*     client_card_ids[nc] = query_ygopro_ids(m_total_card_names);

                printDebugConsole("Found " + m_ygopro_ids.Count + " matches in ygopro database!");
                printDebugConsole(m_ygopro_ids);


                //send card ids to each client

                var message = nc.Peer.CreateMessage();
                message.Write("DRAFTCARDS");
                var bin = new BinaryFormatter();
                var ser = JsonConvert.SerializeObject(client_card_ids[nc]);
                message.Write(ser);
                server.SendMessage(message, nc, NetDeliveryMethod.ReliableOrdered);*/
            }




        }

        bool m_remove_from_pool = false;

        public void pullCardsFromBoosterPoolsAndSend()
        {
            foreach (NetConnection nc in clients)
            {

                client_card_ids[nc] = new List<int>();
                m_dist.ultra_rare = 1.0f / 6.0f;
                m_dist.super_rare = 1.0f / 4.0f;
                m_dist.secret_rare = 1.0f / 12.0f;
                m_dist.rare = 1 - m_dist.secret_rare - m_dist.super_rare - m_dist.ultra_rare;
                //TODO: can also use a changing counter so booster pools are switched after every draftroun
                for(int idx = 0; idx < m_booster_pools.Count; idx++)
                {

                    if (m_pullOnlyOne && m_packIdx != idx)
                        continue;

                    BoosterPool bp = m_booster_pools[idx];
                    int num_cards = bp.commons.Count + bp.rares.Count + bp.secret_rares.Count + bp.super_rares.Count + bp.ultra_rares.Count;
                    if (num_cards - m_numCardsPerBooster < 0)
                    {
                        m_booster_pools[idx] = getBoosterPoolFromJSON(bp.path);
                        bp = m_booster_pools[idx];
                    }
                    num_cards = bp.commons.Count + bp.rares.Count + bp.secret_rares.Count + bp.super_rares.Count + bp.ultra_rares.Count;
                    client_card_ids[nc].AddRange(pullFromBoosterPoolWithRarityDistribution(bp, m_remove_from_pool, m_dist, m_numCardsPerBooster, m_numRaresPerBooster));

                    printDebugConsole("Pool: " + bp.path + " | " + num_cards + " Cards left.");

                }
         
               // printDebugConsole("Found " + m_ygopro_ids.Count + " matches in ygopro database!");
               // printDebugConsole(m_ygopro_ids);


                //send card ids to each client

                var message = nc.Peer.CreateMessage();
                message.Write("DRAFTCARDS");
                var bin = new BinaryFormatter();
                var ser = JsonConvert.SerializeObject(client_card_ids[nc]);
                message.Write(ser);
                server.SendMessage(message, nc, NetDeliveryMethod.ReliableOrdered);

            }
        }
        public void swapClientCardsAndSendBack()
        {
            List<int> client_1_cards = new List<int>();


            for (int client_idx = 0; client_idx < clients.Count - 1; client_idx++)
            {
                //diagram:
                //c1 <- c2 <- c3 <- ... <- cn <- c1
                //just store c1 list and every last client
                NetConnection nc1 = clients[client_idx];
                var message1 = nc1.Peer.CreateMessage();
                message1.Write("DRAFTCARDS");
                var bin1 = new BinaryFormatter();
                var ser1 = JsonConvert.SerializeObject(client_card_ids[clients[client_idx + 1]]);
                message1.Write(ser1);
                server.SendMessage(message1, nc1, NetDeliveryMethod.ReliableOrdered);

           
            }
            NetConnection nc = clients[clients.Count - 1];
            var message = nc.Peer.CreateMessage();
            message.Write("DRAFTCARDS");
            var bin = new BinaryFormatter();
            var ser = JsonConvert.SerializeObject(client_card_ids[clients[0]]);
            message.Write(ser);
            server.SendMessage(message, nc, NetDeliveryMethod.ReliableOrdered);

           
            swapping_and_sending_draft_cards = false;
        }

        public NetServer server; 
        public void receiveThreadStart()
        {
            var config = new NetPeerConfiguration("YGOPRODraft")
            { Port = 12345 };
           // config.EnableUPnP = true;
            server = new NetServer(config);
            server.Start();
          //  server.UPnP.ForwardPort(12345, "YGOPRODraft");
            clients = new List<NetConnection>();
            ready_clients = new List<long>();
            cd_clients = new List<long>();
            while(true)
            {
                server.MessageReceivedEvent.WaitOne();

                //we got a msg.
                //for startup, we need every client to send a "CONNECT" string.
                NetIncomingMessage msg;
          
                while ((msg = server.ReadMessage()) != null)
                {
                    printDebugConsole(msg.MessageType.ToString()); 
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.Data:
                            {
                                String cmd = msg.ReadString();
                                if(cmd.Equals("READY"))
                                {

                                    printDebugConsole("READY SET FROM " + msg.SenderConnection);
                                    if (drafting)
                                        break;
                                    if (!ready_clients.Contains(msg.SenderConnection.RemoteUniqueIdentifier))
                                    {
                                        ready_clients.Add(msg.SenderConnection.RemoteUniqueIdentifier);
                                    }

                                    if(clients.Count == ready_clients.Count)
                                    {
                                        drafting = true;
                                        beginDraft();
                                        pullCardsFromBoosterPoolsAndSend();
                                    }

                                }
                                else if (cmd.Equals("CONTINUEDRAFT"))
                                {

                                    printDebugConsole("CONTINUEDRAFT SET FROM " + msg.SenderConnection);
                                    String json = msg.ReadString();
                                  //  printDebugConsole("JSON GOT! " + json);

                                    client_card_ids[msg.SenderConnection] = new List<int>();
                                    client_card_ids[msg.SenderConnection] = JsonConvert.DeserializeObject<List<int>>(json);
                                    if (!cd_clients.Contains(msg.SenderConnection.RemoteUniqueIdentifier))
                                    {
                                        cd_clients.Add(msg.SenderConnection.RemoteUniqueIdentifier);
                                    }

                                    if (clients.Count == cd_clients.Count)
                                    {
                                        //check if any of the received lists contains nothing, we have to pull again in that case
                                        bool empty_list = false;
                                        foreach(NetConnection nc in clients)
                                        {
                                            if(client_card_ids[nc].Count == 0)
                                            {
                                                empty_list = true;
                                                break;
                                            }
                                        }
                                        
                                        if ((m_cardCounter+1)%(m_numCardsPerDraftRound) == 0 && m_cardCounter != 0 || empty_list)
                                        {
                                            if(m_cardCounter+1 >= m_numCardsPerDeck)
                                            {
                                               

                                                foreach (NetConnection nc in clients)
                                                {
                                                    var message = nc.Peer.CreateMessage();
                                                    message.Write("QUITDRAFTING");
                                        
                                                   
                                                    server.SendMessage(message, nc, NetDeliveryMethod.ReliableOrdered);
                                                    nc.Disconnect("cya!");

                                                }
                                                printDebugConsole("DRAFTING IS DONE, GLHF!");
                                                drafting = false;
                                                m_cardCounter = 0;
                                                swapping_and_sending_draft_cards = false;
                                                m_ygopro_ids.Clear();
                                                ready_clients.Clear();
                                                m_total_card_names.Clear();
                                                m_booster_pools.Clear();
                                                clients.Clear();
                                                cd_clients.Clear();
                                                client_card_ids.Clear();
                                            }
                                            else
                                            {
                                                m_packIdx++;
                                                if (m_packIdx == m_booster_pools.Count)
                                                    m_packIdx = 0;
                                                pullCardsFromBoosterPoolsAndSend();
                                                cd_clients.Clear();
                                                m_cardCounter++;
                                                break;
                                            }
                                            //beginDraft();
                                            
                                        }
                                        if(drafting)
                                        {
                                            swapping_and_sending_draft_cards = true;
                                            swapClientCardsAndSendBack();
                                            cd_clients.Clear();
                                            m_cardCounter++;
                                        }

                                        cd_clients.Clear();
                                        // beginDraft();
                                    }

                                }
                            }
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            // handle connection status messages
                            if(msg.SenderConnection.Status == NetConnectionStatus.Connected)
                            {
                                //NetPeer client = new NetClient(new NetPeerConfiguration())
                                clients.Add(msg.SenderConnection);
                                printDebugConsole("Connected " + msg.SenderConnection.ToString());
                            }
                            else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                            {
                                clients.Remove(msg.SenderConnection);
                                cd_clients.Remove(msg.SenderConnection.RemoteUniqueIdentifier);
                                ready_clients.Remove(msg.SenderConnection.RemoteUniqueIdentifier);
                                client_card_ids.Remove(msg.SenderConnection);
                                printDebugConsole("Disconnected " + msg.SenderConnection.ToString());
                                if (clients.Count == 0)
                                {

                                    m_cardCounter = 0;
                                    drafting = false;
                                    swapping_and_sending_draft_cards = false;
                                }
                            }
                                break;
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            Console.WriteLine(msg.ReadString());
                            break;
                        default:
                            Console.WriteLine("Unhandled type: " + msg.MessageType);
                            break;
                    }


                    server.Recycle(msg);
                }
            }

            m_receiveThread.Suspend();
        }
        void startServerListening()
        {

            //Just create a thread which blocks until a message arrives
            //for sending, there doesn't have to be a thread.
            m_receiveThread = new Thread(receiveThreadStart);

            m_receiveThread.Start();
            /*


            // Server side
            Stream stream = new NetworkStream(socket);
            var bin = new BinaryFormatter();
            bin.Serialize(stream, TaskManager());

            // Client side
            Stream stream = new NetworkStream(socket);
            var bin = new BinaryFormatter();
            var list = (List<string>)bin.Deserialize(stream);

            */
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

        private void readTestJSONbtn_Click(object sender, EventArgs e)
        {

            
        }

        private void AddJSONPacksToList(String folder_path)
        {
            //get all json files
            json_filenames = new List<String>();
            json_filenames.AddRange(Directory.GetFiles(folder_path));
            List<String> temp = new List<string>();
            foreach (String s in json_filenames)
            {
                String[] arr = s.Split("\\".ToCharArray());
                temp.Add(arr[arr.Length - 1]);
            }
            json_filenames = temp;
            ListChoosePacks.Items.Clear();
            ListChoosePacks.DataSource = json_filenames;
            // printDebugConsole("Found " + json_filenames.Count + " packs");
        }

        private void ConfigurePackBtn_Click(object sender, EventArgs e)
        {
            try
            {          
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.SelectedPath = Directory.GetCurrentDirectory() + "\\jsons";
                fbd.ShowDialog();

                String folder_path = fbd.SelectedPath;
                m_pack_path = folder_path;
                
                AddJSONPacksToList(folder_path);
                
            }
            catch
            {
                 
            }
            finally
            {

            }
            
        }

        private void ListChoosePacks_DoubleClick(object sender, EventArgs e)
        {
            ListChosenPacks.Items.Add(ListChoosePacks.SelectedItem);
            printDebugConsole("Added item to chosen packs: " + ListChoosePacks.SelectedItem.ToString());
        }

        private void FindYGOPRODBBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.ShowDialog();

                ygopro_db_path = ofd.FileName;
                labelPath.Text = ygopro_db_path;

            }
            catch
            {

            }
        }

        private void numCardsPerBooster_TextChanged(object sender, EventArgs e)
        {
            m_numCardsPerBooster = Convert.ToInt32(numCardsPerBooster.Text);
        }

        private void numRaresPerBooster_TextChanged(object sender, EventArgs e)
        {
            m_numRaresPerBooster = Convert.ToInt32(numRaresPerBooster.Text);
        }

        private void numCardsPerDraftRound_TextChanged(object sender, EventArgs e)
        {
            m_numCardsPerDraftRound = Convert.ToInt32(numCardsPerDraftRound.Text);
        }

        private void numCardsPerDeck_TextChanged(object sender, EventArgs e)
        {
            m_numCardsPerDeck = Convert.ToInt32(numCardsPerDeck.Text);
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            startServerListening();
        }

        private void chkRemovePool_CheckedChanged(object sender, EventArgs e)
        {
            m_remove_from_pool = chkRemovePool.Checked;
        }

        private void ListChoosePacks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkPullOnlyOne_CheckedChanged(object sender, EventArgs e)
        {
            m_pullOnlyOne = chkPullOnlyOne.Checked;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettingsJSON();
            Environment.Exit(0);
            m_receiveThread.Abort();
            
        }
    }
}
