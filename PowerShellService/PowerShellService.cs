//Here is the once-per-application setup information

using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Linq;
using System.Data.Entity;
using System.Text.RegularExpressions;

using PowerShellService.Interfaces;
using PowerShellService.Model;
using PowerShellService.ViewModel;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace PowerShellService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class PowerShellService : ServiceBase, IPowerShellService
    {
        private PersistentData _pD = null;

        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void SetResponseHttpStatus(HttpStatusCode statusCode, string statusDescription = "")
        {
            var context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = statusCode;
            context.OutgoingResponse.StatusDescription = Regex.Replace(statusDescription, @"\r\n?|\n", "");

            context.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            if (context.IncomingRequest.Method == "OPTIONS")
            {
                context.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                context.OutgoingResponse.Headers.Add("Access-Control-Allow-Header", "Content-Type, Accept, SOAPAction");
                context.OutgoingResponse.Headers.Add("Access-Control-Allow-Credentials", "false");
            }
        }

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

        private void CreateDemoData()
        {
            var dataForSave = new List<Model.PowerShellScript>
            {
                new PowerShellScript
                {
                    File = @"c:\Powershell\UpdateRegister.ps1",
                    Name = "UpdateRegister",
                    Description = "A simple script to update a simple register.",
                    Parameters = new List<PowershellScriptParameter>
                    {
                        new PowershellScriptParameter
                        {
                            Description = "Name"
                        },
                        new PowershellScriptParameter
                        {
                            Description = "Favorite color"
                        }
                    }
                },
                new PowerShellScript
                {
                    File = @"c:\Powershell\UpdateMobileNumber.ps1",
                    Name = "UpdateMobileNumber",
                    Description = "When a user has got a new mobile phone number, this script will provide all necessary registers with new up to date data.",
                    Parameters = new List<PowershellScriptParameter>
                    {
                        new PowershellScriptParameter
                        {
                            Description = "Update mobile phone number"
                        }
                    }
                },
                new PowerShellScript
                {
                    File = @"c:\Powershell\ChangeAddresses.ps1",
                    Name = "ChangeAddresses",
                    Description = "This script will help users to change several types of addresses",
                    Parameters = new List<PowershellScriptParameter>
                    {
                        new PowershellScriptParameter
                        {
                            Description = "E-mail"
                        },
                        new PowershellScriptParameter
                        {
                            Description = "Office address"
                        },
                        new PowershellScriptParameter
                        {
                            Description = "Home address"
                        },
                        new PowershellScriptParameter
                        {
                            Description = "Office address"
                        }
                    }
                }
            };
            var pD = new PersistentData();
            pD.RegisteredPowerShellScripts = dataForSave;

            var dataReadBack = pD.RegisteredPowerShellScripts;
        }

        List<PowerShellScript_NameAndDescription> IPowerShellService.GetNamesForRegisteredPowerShellScripts()
        {
            try
            {
                //CreateDemoData();
                var registeredScripts = PersistentDataProvider.RegisteredPowerShellScripts;
                var registeredScripts_NameAndDescription = registeredScripts.Select(
                    script => 
                        new PowerShellScript_NameAndDescription {
                            Name = script.Name,
                            Description = script.Description }).ToList();

                this.SetResponseHttpStatus(HttpStatusCode.OK);
                return registeredScripts_NameAndDescription;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't return GetNamesForRegisteredPowerShellScripts. Reason: {ex.Message}";
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: message);
                log.Error(message, ex);
            }
            return null;
        }

        PowerShellScript_NameAndDescriptionAndParametersWithDescription IPowerShellService.GetRegisteredPowerShellScriptPrototype(string powerShellScriptName)
        {
            try
            {
                var registeredScripts = PersistentDataProvider.RegisteredPowerShellScripts;

                var currentScript = registeredScripts
                    .Where(script => script.Name == powerShellScriptName)
                    .Select(script => new PowerShellScript_NameAndDescriptionAndParametersWithDescription
                    {
                        Name = script.Name,
                        Description = script.Description,
                        Parameters = script.Parameters.Select(p => new ParameterDescription
                        {
                            Description = p.Description,
                            Name = p.Name
                        }).ToList()
                    }).First();
                this.SetResponseHttpStatus(HttpStatusCode.OK);
                return currentScript;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't return GetRegisteredPowerShellScriptPrototype. Reason: {ex.Message}";
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: message);
                log.Error(message, ex);
            }
            return null;
        }

        void IPowerShellService.InvokePowerShellScript(string powerShellScriptName, string[] param)
        {
            try
            {

                this.SetResponseHttpStatus(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                var message = $"Couldn't invoke InvokePowerShellScript. Reason: {ex.Message}";
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: message);
                log.Error(message, ex);
            }
        }
    }
}
