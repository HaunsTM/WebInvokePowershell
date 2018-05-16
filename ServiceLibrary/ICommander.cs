using System.Collections.Generic;
using ServiceLibrary.Model;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    public interface ICommander
    {
        List<PowerShellScript_NameAndDescriptionAndParametersWithDescription> GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters();

        PowerShellScriptRunResult InvokePowerShellScript(PowerShellScript powerShellScript);
    }
}
