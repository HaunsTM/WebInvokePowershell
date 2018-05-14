﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        string ICommander.InvokePowerShellScript(string scriptFile, List<string> args)
        {
            try
            {
                var pSC = new PowerShellCommander();

                return pSC.RunPowershellScript(scriptFile, args);

            }
            catch (Exception ex)
            {
                var message = $"Couldn't invoke power shell script. Reason: {ex.Message}";
                throw new Exception(message, ex);
            }
        }
    }
}
