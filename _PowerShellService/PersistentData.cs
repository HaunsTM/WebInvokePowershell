using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PowerShellService
{
    public class PersistentData
    {

        public string CurrentProjectPath
        {
            get
            {
                var currrentBinOutput = Directory.GetCurrentDirectory();
                var projectPath = Directory.GetParent(currrentBinOutput).Parent.FullName + @"\";
                return projectPath;
            }
        }

        public string PowerShellScriptFilesDescriptionFilePath
        {
            get
            {
                return CurrentProjectPath + @"PowerShellScripts\PowerShellScriptFilesDescription.json";
            }
        }

        public List<Model.PowerShellScript> RegisteredPowerShellScripts
        {
            //serializes/deserializes info regarding the power shell script files that should be exposed by this service
            get {
                // deserialize JSON directly from a file
                using (var file = File.OpenText(PowerShellScriptFilesDescriptionFilePath))
                {
                    var serializer = new JsonSerializer();
                    var registeredPowerShellScripts = (List<Model.PowerShellScript>)serializer.Deserialize(file, typeof(List<Model.PowerShellScript>));

                    return registeredPowerShellScripts;
                }
            }
            set
            {
                using (StreamWriter sw = new StreamWriter(PowerShellScriptFilesDescriptionFilePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, value);
                }
            }
        }
    }
}
