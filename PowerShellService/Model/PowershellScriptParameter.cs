﻿namespace PowerShellService.Model
{
    public class PowershellScriptParameter : IPowershellScriptParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserProvidedValue { get; set; }
    }
}