using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace ConsoleApp1
{
    class JSONDeserializer
    {

        public JSONDeserializer()
        {
            initJSONDict();
        }

        private string filePath { get; set; }
        private Dictionary<string, dynamic> jsonDict;

        public string[] availableWiFiModules()
        {
            return jsonDict.Keys.ToArray();

        }

        public Dictionary<string, string> getModuleParameters(string moduleName)
        {
            dynamic moduleParametersrJsonDict;
            jsonDict.TryGetValue(moduleName, out moduleParametersrJsonDict);
            string moduleParamterJson = moduleParametersrJsonDict.ToString();
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(moduleParamterJson);
        }

        private void initJSONDict()
        {
            string json = File.ReadAllText(filePath);
            jsonDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            /*
                dynamic k;
                jsonDict.TryGetValue("Redpie", out k);
                string innerJson = @k.ToString();
                Dictionary<string, string> inner = JsonConvert.DeserializeObject<Dictionary<string, string>>(innerJson);
                String p;
                inner.TryGetValue("suffix_String", out p);
                Trace.WriteLine(p);
            */
        }
    }
}
