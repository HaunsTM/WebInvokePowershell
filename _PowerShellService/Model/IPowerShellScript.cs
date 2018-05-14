using System.Collections.Generic;

namespace PowerShellService.Model
{
    public interface IPowerShellScript
    {
        string File { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<PowershellScriptParameter> Parameters { get; set; }
    }
}