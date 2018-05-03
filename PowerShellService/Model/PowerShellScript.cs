using System.Collections.Generic;

namespace PowerShellService.Model
{
    public class PowerShellScript : IPowerShellScript
    {
        public string PowerShellFile { get; set; }
        public List<PowershellScriptParameter> Parameters { get; set; }
    }
}