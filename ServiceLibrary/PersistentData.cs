using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ServiceLibrary.Model;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    internal class PersistentData
    {
        private string _powerShellScriptFilesDescriptionFilePath = String.Empty;

        internal PersistentData(string powerShellScriptFilesDescriptionFilePath)
        {
            _powerShellScriptFilesDescriptionFilePath = powerShellScriptFilesDescriptionFilePath;
        }

        internal List<PowerShellScript> RegisteredPowerShellScripts
        {
            //serializes/deserializes info regarding the power shell script files that should be exposed by this service
            get {
                // deserialize JSON directly from a file
                using (var file = File.OpenText(_powerShellScriptFilesDescriptionFilePath))
                {
                    var serializer = new JsonSerializer();
                    var registeredPowerShellScripts = (List<PowerShellScript>)serializer
                        .Deserialize(file, typeof(List<PowerShellScript>));

                    var registeredPowerShellScriptsInAlphabeticalOrder =
                        registeredPowerShellScripts.OrderBy(s => s.Name).ToList();

                    return registeredPowerShellScriptsInAlphabeticalOrder;
                }
            }
            set
            {
                using (StreamWriter sw = new StreamWriter(_powerShellScriptFilesDescriptionFilePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, value);
                }
            }
        }

        internal List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters()
        {
            var scripts = RegisteredPowerShellScripts
                .Select(script => new PowerShellScript_NameAndDescriptionAndParametersWithDescription
                {
                    Name = script.Name,
                    FileNameWithoutPath = Path.GetFileName(script.File),
                    Description = script.Description,
                    Parameters = script.Parameters.Select(p => new ParameterDescription
                    {
                        Description = p.Description,
                        Name = p.Name
                    }).ToList()
                }).ToList();
            return scripts;
        }

        public PowerShellScript GetPowerShellScriptBy(string name)
        {
            var foundScript = this.RegisteredPowerShellScripts
                .Where(n => n.Name == name)
                .Single();
            return foundScript;
        }
    }
}
