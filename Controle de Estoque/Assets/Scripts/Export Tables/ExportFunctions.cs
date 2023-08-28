using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Assets.Scripts.Misc;

public class ExportFunctions
    {
        public static JObject LoadJsonFromFile(string saveFile)
    {
        string path = Path.Combine(Application.persistentDataPath, saveFile + ConstStrings.extension);
        if (!File.Exists(path))
        {
            return new JObject();
        }

        using (var textReader = File.OpenText(path))
        {
            using (var reader = new JsonTextReader(textReader))
            {
                reader.FloatParseHandling = FloatParseHandling.Double;

                return JObject.Load(reader);
            }
        }
    }
       
    }
