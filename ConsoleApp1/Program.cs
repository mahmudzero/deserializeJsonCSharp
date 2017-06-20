using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void loadJSON()
        {

            List<Wifimodel> fileList = new List<Wifimodel>();
            string json = File.ReadAllText("wifiModuleParameters.json");
            Dictionary<string, dynamic> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            dynamic k;
            jsonDict.TryGetValue("Redpie", out k);
            string innerJson = @k.ToString();
            Dictionary<string, string> inner = JsonConvert.DeserializeObject<Dictionary<string, string>>(innerJson);
            String p;
            inner.TryGetValue("suffix_String", out p);
            Trace.WriteLine(p);
            
        }

        static void Main(string[] args)
        {
            loadJSON();
        }
    }

    class Wifimodel
    {
        public string name;
        public string suffix_String;
        public string set_Operation_Mode;

        public Wifimodel(string suffix_String, string set_Operation_Mode)
        {
            this.suffix_String = suffix_String;
            this.set_Operation_Mode = set_Operation_Mode;
            Console.WriteLine(this.suffix_String);
            Trace.WriteLine(this.suffix_String);
            Trace.WriteLine("now op mode");
            Console.WriteLine(this.set_Operation_Mode);
            Trace.WriteLine(this.set_Operation_Mode);
        }
    }
}
