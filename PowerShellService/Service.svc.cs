using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using PowerShellService.Interfaces;
using ServiceLibrary;
using ServiceLibrary.ViewModel;

namespace PowerShellService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {

        private ICommander _commander = null;

        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Service()
        {
            _commander = new Commander();
        }

        private void SetResponseHttpStatus(HttpStatusCode statusCode, string statusDescription = "")
        {
            var context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = statusCode;
            context.OutgoingResponse.StatusDescription = Regex.Replace(statusDescription, @"\r\n?|\n", "");
        }

        // Add more operations here and mark them with [OperationContract]

        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> IService.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters()
        {
            try
            {
                return _commander.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();
            }
            catch (Exception ex)
            {
                var message = $"Couldn't return GetRegisteredPowerShellScriptPrototype. Reason: {ex.Message}";
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: message);
                log.Error(message, ex);
            }
            return null;
        }

        string IService.InvokePowerShellScript(string powerShellScriptName, string args)
        {
            return "";
        }
    }
}
