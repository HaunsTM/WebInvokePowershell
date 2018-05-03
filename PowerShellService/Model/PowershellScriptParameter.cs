namespace PowerShellService.Model
{
    public class PowershellScriptParameter : IPowershellScriptParameter
    {
        public string Description { get; set; }
        public string UserProvidedValue { get; set; }
    }
}