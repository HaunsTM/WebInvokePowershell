using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PowerShellService.Interfaces
{
    [ServiceContract]
    public interface IPowerShellService
    {
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetNamesForRegisteredPowerShellScripts")]
        List<string> GetNamesForRegisteredPowerShellScripts();

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetRegisteredPowerShellScriptPrototype?powerShellScriptName={powerShellScriptName}")]
        Model.PowerShellScript GetRegisteredPowerShellScriptPrototype(string powerShellScriptName);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "InvokePowerShellScript?powerShellScriptName={powerShellScriptName}")]
        void InvokePowerShellScript(string powerShellScriptName);
    }
}
