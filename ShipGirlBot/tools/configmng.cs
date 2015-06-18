using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonConfig;
using System.Security.Cryptography;

namespace tools
{
    public class configmng
    {
        static public string CFG_FILE_NAME = "cfg.json";
        static public string DISLIST_FILE_NAME = "dislist.json";
        static public string DISLISTEQUIP_FILE_NAME = "disequiplist.json";
        static private configmng mInstance;

        protected ConfigObject cf = null;

        protected Dictionary<string, string> dis_ship_list = new Dictionary<string, string>();
        protected Dictionary<string, string> dis_equip_list = new Dictionary<string, string>();
        public static configmng instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new configmng();
                }
                return mInstance;
            }
        }
        public configmng()
        {
            readcfg();
            loaddisshiplist();
            loaddisequiplist();
        }

        ~configmng()
        {
            savecfg();
            savedisshiplist();
            savedisequiplist();
        }

        public void readcfg()
        {
            cf = JsonConfig.Config.Default;
            if(System.IO.File.Exists(CFG_FILE_NAME))
            {
                cf.ApplyJson(System.IO.File.ReadAllText(CFG_FILE_NAME));
            }
        }

        public void savecfg()
        {
            string save_json_string = cf.ToString();
            if(save_json_string != null)
            {
                System.IO.File.WriteAllText(CFG_FILE_NAME, save_json_string, System.Text.Encoding.UTF8);
            }
        }

        public object getval(string key)
        {
            if (key != null && cf.ContainsKey(key) == true)
            {
                return cf[key];
            }
            else
            {
                return null;
            }
        }

        public void setval(string key,object val)
        {
            if(key != null)
            {
                cf[key] = val;
                savecfg();
            }
            return;
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }

            return byte2String;
        }
        
        public static string deviceUniqueIdentifier {
            get
            {



                return GetMD5(new HardwareInfo().GetMacAddress());
            }
        }

        public Dictionary<string, string> getDisShipList()
        {
            return dis_ship_list;
        }

        public void loaddisshiplist()
        {
            dis_ship_list = new Dictionary<string, string>();
            if (System.IO.File.Exists(DISLIST_FILE_NAME))
            {
	
	            string cf = System.IO.File.ReadAllText(DISLIST_FILE_NAME);

                dis_ship_list = new JsonFx.Json.JsonReader().Read<Dictionary<string, string>>(cf);
            }
        }

        public void savedisshiplist()
        {
            string content = "";
            content = new JsonFx.Json.JsonWriter().Write(dis_ship_list);
            if(content != "")
            {
                System.IO.File.WriteAllText(DISLIST_FILE_NAME, content);
            }
        }

        public bool adddisship(int shipcid, string name)
        {
            if(dis_ship_list.ContainsKey(shipcid.ToString()) == true)
            {
                return false;
            }
            dis_ship_list[shipcid.ToString()] = name;
            savedisshiplist();
            return true;
        }

        public bool removedisship(int shipcid)
        {
            if (dis_ship_list.ContainsKey(shipcid.ToString()) == true)
            {
                dis_ship_list.Remove(shipcid.ToString());
                savedisshiplist();
                return true;
            }
            
            return false;
        }



        public Dictionary<string, string> getDisEquipList()
        {
            return dis_equip_list;
        }

        public void loaddisequiplist()
        {
            dis_equip_list = new Dictionary<string, string>();
            if (System.IO.File.Exists(DISLISTEQUIP_FILE_NAME))
            {

                string cf = System.IO.File.ReadAllText(DISLISTEQUIP_FILE_NAME);

                dis_equip_list = new JsonFx.Json.JsonReader().Read<Dictionary<string, string>>(cf);
            }
        }

        public void savedisequiplist()
        {
            string content = "";
            content = new JsonFx.Json.JsonWriter().Write(dis_equip_list);
            if (content != "")
            {
                System.IO.File.WriteAllText(DISLISTEQUIP_FILE_NAME, content);
            }
        }

        public bool adddisequip(int equipcid, string name)
        {
            if (dis_equip_list.ContainsKey(equipcid.ToString()) == true)
            {
                return false;
            }
            dis_equip_list[equipcid.ToString()] = name;
            savedisequiplist();
            return true;
        }

        public bool removedisequip(int equipcid)
        {
            if (dis_equip_list.ContainsKey(equipcid.ToString()) == true)
            {
                dis_equip_list.Remove(equipcid.ToString());
                savedisequiplist();
                return true;
            }

            return false;
        }
    }
}
