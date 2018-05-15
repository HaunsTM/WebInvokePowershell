using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLibrary.Model;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    public class Commander: ICommander
    {
        private PersistentData _pD = null;
        private string _powerShellScriptFilesDescriptionFilePath = string.Empty;

        public Commander(string powerShellScriptFilesDescriptionFilePath)
        {
            _powerShellScriptFilesDescriptionFilePath = powerShellScriptFilesDescriptionFilePath;
        }

        private PersistentData PersistentDataProvider
        {
            get
            {
                if (_pD == null)
                {
                    _pD = new PersistentData(_powerShellScriptFilesDescriptionFilePath);
                }
                return _pD;
            }
        }

        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> ICommander.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters()
        {
            try
            {
                var scripts = PersistentDataProvider.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();
                return scripts;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't return GetRegisteredPowerShellScriptPrototype. Reason: {ex.Message}";
                throw new Exception(message, ex);
            }
        }

        string ICommander.InvokePowerShellScript(PowerShellScript powerShellScript)
        {
            try
            {
                var pSC = new PowerShellCommander();
                var scriptFile = PersistentDataProvider.GetPowerShellScriptBy(name: powerShellScript.Name);
                powerShellScript.Parameters.Select(x => new { x.Name + x.UserProvidedValue }).ToList().ToString()

                    /// bygg ihop strängen som ska köras
                return pSC.RunPowershellScript( scriptFile, args);

            }
            catch (Exception ex)
            {
                var message = $"Couldn't invoke power shell script. Reason: {ex.Message}";
                throw new Exception(message, ex);
            }
        }
    }
}
