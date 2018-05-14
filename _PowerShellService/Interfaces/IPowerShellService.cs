using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

using PowerShellService.ViewModel;

namespace PowerShellService.Interfaces
{
    [ServiceContract]
    public interface IPowerShellService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters")]
        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "InvokePowerShellScript")]
        string InvokePowerShellScript(string powerShellScriptName, string args);
    }
}
