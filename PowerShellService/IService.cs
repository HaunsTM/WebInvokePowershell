using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

using ServiceLibrary.ViewModel;

namespace PowerShellService.Interfaces
{
    [ServiceContract]
    public interface IService
    {
        //probably the only needed method
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters")]
        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();
        
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "InvokePowerShellScript")]
        string InvokePowerShellScript(string powerShellScriptName, string args);
    }
}
