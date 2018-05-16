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
        private PowerShellCommander _pSC = null;

        private string _powerShellScriptFilesPath = string.Empty;
        private string _powerShellScriptFilesDescriptionFile = string.Empty;

        public Commander(string powerShellScriptFilesPath, string powerShellScriptFilesDescriptionFileName)
        {
            _powerShellScriptFilesPath = powerShellScriptFilesPath;
            _powerShellScriptFilesDescriptionFile = powerShellScriptFilesPath + powerShellScriptFilesDescriptionFileName;
        }

        private PersistentData PersistentDataProvider
        {
            get
            {
                if (_pD == null)
                {
                    _pD = new PersistentData(_powerShellScriptFilesDescriptionFile);
                }
                return _pD;
            }
        }

        private PowerShellCommander PowerShellCommanderProvider
        {
            get
            {
                if (_pSC == null)
                {
                    _pSC = new PowerShellCommander();
                }
                return _pSC;
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

        PowerShellScriptRunResult ICommander.InvokePowerShellScript(PowerShellScript powerShellScript)
        {
            var pSC = new PowerShellCommander();
            var scriptFile = _powerShellScriptFilesPath + PersistentDataProvider.GetPowerShellScriptBy(name: powerShellScript.Name).File;
            var parameters = powerShellScript.Parameters.ToDictionary(key => key.Name, value => value.UserProvidedValue);

            var result = PowerShellCommanderProvider.RunPowerShellScript(scriptFile, parameters);

            return result;
        }
    }
}
