namespace PowerShellService.Model
{
    public interface IPowershellScriptParameter
    {
        string Description { get; set; }
        string UserProvidedValue { get; set; }
    }
}