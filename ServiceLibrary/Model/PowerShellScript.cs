using System.Collections.Generic;

namespace ServiceLibrary.Model
{
    internal class PowerShellScript : IPowerShellScript
    {
        public string File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<IPowershellScriptParameter> Parameters { get; set; }
    }
}