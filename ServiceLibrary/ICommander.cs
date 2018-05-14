using System.Collections.Generic;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    public interface ICommander
    {
        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();

        string InvokePowerShellScript(string powerShellScriptName, string args);
    }
}
