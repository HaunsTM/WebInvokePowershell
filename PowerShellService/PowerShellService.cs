//Here is the once-per-application setup information

using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using PowerShellService.Interfaces;
using PowerShellService.Model;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace PowerShellService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class PowerShellService : ServiceBase, IPowerShellService
    {
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

        List<Model.PowerShellScript> IPowerShellService.GetRegisteredPowerShellScripts()
        {
            try
            {
                var test = new Model.PowerShellScript
                {
                    PowerShellFile = "sdfhg",
                    Parameters = new List<PowershellScriptParameter>
                    {
                        new PowershellScriptParameter
                        {
                            Description = "Namn"
                        }
                    }
                };
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(test);

                var deserializedResult = serializer.Deserialize<List<Model.PowerShellScript>>(serializedResult);
                // Write that JSON to txt file,  
                System.IO.File.WriteAllText("" + "output.json", serializedResult);

                this.SetResponseHttpStatus(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: $"Couldn't return GetRegisteredPowerShellScripts. Reason: {ex.Message}");
                log.Error("Couldn't return GetRegisteredPowerShellScripts", ex);
            }
            return null;
        }

        string IPowerShellService.InvokePowerShellScript(string powerShellScriptName)
        {
            try
            {
                this.SetResponseHttpStatus(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: $"Couldn't invoke InvokePowerShellScript. Reason: {ex.Message}");
                log.Error($"Couldn't invoke InvokePowerShellScript", ex);
            }
            return "";
        }
    }
}
