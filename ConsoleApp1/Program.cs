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
            string json = File.ReadAllText("wifiModuleParameters.json");
            Dictionary<string, dynamic> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            string[] sad = jsonDict.Keys.ToArray();
            for(int i = 0; i < sad.Length; i++)
            {
                Trace.WriteLine(sad[i]);
            }
            dynamic k; 
            jsonDict.TryGetValue("Redpie", out k);
            string innerJson = @k.ToString();
            Dictionary<string, string> inner = JsonConvert.DeserializeObject<Dictionary<string, string>>(innerJson);
            String p;
            inner.TryGetValue("suffix_String", out p);
            //Trace.WriteLine(p);
            
        }

        static void Main(string[] args)
        {
            loadJSON();
        }
    }
}
