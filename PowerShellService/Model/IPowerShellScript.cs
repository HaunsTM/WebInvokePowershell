using System.Collections.Generic;

namespace PowerShellService.Model
{
    public interface IPowerShellScript
    {
        string PowerShellFile { get; set; }
        List<PowershellScriptParameter> Parameters { get; set; }
    }
}