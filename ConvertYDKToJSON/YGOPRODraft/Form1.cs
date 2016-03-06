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
        struct YGOJSONStruct
        {
            public String rarity;
            public String set_number;
            public String name;
        }
        public Dictionary<Panel, int> scroll_val_map;

        int small_card_height = 60;
        int small_card_spacing = 10;
        int SIGNATURE_SCROLL_VAL = -133789929;
        public Form1()
        {
            
            m_ygopro_ids = new List<int>();
            InitializeComponent();
            
            // card_modify_mutex = new Mutex();
 
  
        }

        private static Object card_modify_mutex = new Object();
        List<int> m_ygopro_ids;
        String ygopro_db_path = "";

       



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

        private YGOPROCard getFromIDAndSetRarity(String rarity, int id)
        {
            YGOPROCard card;

            card = YGOPROCard.QueryFromID(id, ygopro_db_path);
            card.m_rarity = rarity;
            return card;
        }

        private void ConfigurePackBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Choose .ydk (YGOPRO Deck) File to convert!";

            ofd.ShowDialog();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = ygopro_db_path;
            sfd.Title = "Save your resulting pack file!";
            sfd.ShowDialog();

            
            List<String> lines = new List<string>();
            lines.AddRange(File.ReadAllLines(ofd.FileName));
            List<YGOJSONStruct> output_json = new List<YGOJSONStruct>();
            String rarity = "Common";
            foreach(String line in lines)
            {
                String card = line.TrimEnd(new char[] { '\n', '\r', '\t', ' ' });
                YGOPROCard card_obj = null;
                if (line.Contains("!side"))
                {
                 
                    rarity = "Rare";
                    continue;
                }
                else if(line.Contains("#"))
                {
                    continue;
                }
                else
                {
                    int id = Int32.Parse(card);
                    card_obj = getFromIDAndSetRarity(rarity, id);
                }
                YGOJSONStruct output_json_struct = new YGOJSONStruct();
                output_json_struct.name = card_obj.m_card_name;
                output_json_struct.rarity = card_obj.m_rarity;
                output_json_struct.set_number = "Created-From-YDK-To-JSON";

                output_json.Add(output_json_struct);
            }

            String json = JsonConvert.SerializeObject(output_json, Formatting.Indented);

            File.WriteAllText(sfd.FileName, json);

        }

       

        private void FindYGOPRODBBtn_Click_1(object sender, EventArgs e)
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

           // YGOPROCard my_card = YGOPROCard.QueryFromID(1546123, ygopro_db_path);
           
        }

        
    }
}
