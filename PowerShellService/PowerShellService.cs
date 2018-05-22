using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using ServiceLibrary;
using ServiceLibrary.Model;
using ServiceLibrary.ViewModel;

namespace PowerShellService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PowerShellService : IPowerShellService
    {

        private ICommander _commander = null;

        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PowerShellService()
        {
            _commander = new Commander(
                powerShellScriptFilesPath: CurrentProjectPath + @"PowerShellScripts\", 
                powerShellScriptFilesDescriptionFileName: "PowerShellScriptFilesDefinitions.json");
        }

        private string CurrentProjectPath
        {
            get
            {
                var currrentBinOutput = Directory.GetCurrentDirectory();
                var projectPath = System.Web.HttpRuntime.BinDirectory;
                return projectPath;
            }
        }

        private string CurrentUser
        {
            get { return "ANONYMOUS user"; }
        }
        
        private void SetResponseHttpStatus(HttpStatusCode statusCode, string statusDescription = "")
        {
            var context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = statusCode;
            context.OutgoingResponse.StatusDescription = Regex.Replace(statusDescription, @"\r\n?|\n", "");
        }

        // Add more operations here and mark them with [OperationContract]

        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> IPowerShellService.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters()
        {
            try
            {
                log.Info($"{CurrentUser} is asking for a list of available PowerSHell scripts.");
                var scripts = _commander.GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();
                return scripts;
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: ex.Message);
                log.Error(ex.Message, ex);
            }
            return null;
        }

        PowerShellScriptRunResult IPowerShellService.InvokePowerShellScript(PowerShellScript powerShellScript)
        {

            try
            {
                log.Info($"{CurrentUser} is invoking PowerShell script #{powerShellScript.Name}# with arguments {powerShellScript.Parameters.Select(x => new {Name = x.Name, UserProvidedValue = x.UserProvidedValue}).ToList().ToString()}");
                return _commander.InvokePowerShellScript(powerShellScript);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(statusCode: HttpStatusCode.BadRequest, statusDescription: ex.Message);
                log.Error(ex.Message, ex);
            }
            return null;
        }
    }
}
