using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    public class Commander: ICommander
    {
        private PersistentData _pD = null;

        private PersistentData PersistentDataProvider
        {
            get
            {
                if (_pD == null)
                {
                    _pD = new PersistentData();
                }
                return _pD;
            }
        }

        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> ICommander.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters()
        {
            try
            {
                var registeredScripts = PersistentDataProvider.RegisteredPowerShellScripts;

                var scripts = registeredScripts
                    .Select(script => new PowerShellScript_NameAndDescriptionAndParametersWithDescription
                    {
                        Name = script.Name,
                        Description = script.Description,
                        Parameters = script.Parameters.Select(p => new ParameterDescription
                        {
                            Description = p.Description,
                            Name = p.Name
                        }).ToList()
                    }).ToList();
                return scripts;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't return GetRegisteredPowerShellScriptPrototype. Reason: {ex.Message}";
                throw new Exception(message, ex);
            }
            return null;
        }

        string ICommander.InvokePowerShellScript(string powerShellScriptName, string args)
        {
            return "";
        }
    }
}
