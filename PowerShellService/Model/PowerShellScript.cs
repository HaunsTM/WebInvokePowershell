using System.Collections.Generic;

namespace PowerShellService.Model
{
    public class PowerShellScript : IPowerShellScript
    {
        public string File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PowershellScriptParameter> Parameters { get; set; }
    }
}