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
            UriTemplate = "GetRegisteredPowerShellScripts")]
        List<Model.PowerShellScript> GetRegisteredPowerShellScripts();

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "InvokePowerShellScript?powerShellScriptName={powerShellScriptName}")]
        string InvokePowerShellScript(string powerShellScriptName);
    }
}
