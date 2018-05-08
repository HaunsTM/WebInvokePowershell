using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

using PowerShellService.ViewModel;

namespace PowerShellService.Interfaces
{
    [ServiceContract]
    public interface IPowerShellService
    {
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetNamesForRegisteredPowerShellScripts")]
        List<PowerShellScript_NameAndDescription> GetNamesForRegisteredPowerShellScripts();

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetRegisteredPowerShellScriptPrototype?powerShellScriptName={powerShellScriptName}")]
        PowerShellScript_NameAndDescriptionAndParametersWithDescription GetRegisteredPowerShellScriptPrototype(string powerShellScriptName);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "InvokePowerShellScript?powerShellScriptName={powerShellScriptName}")]
        void InvokePowerShellScript(string powerShellScriptName, string[] param);
    }
}
