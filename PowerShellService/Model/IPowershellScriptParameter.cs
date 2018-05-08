namespace PowerShellService.Model
{
    public interface IPowershellScriptParameter
    {
        string Name { get; set; }
        string Description { get; set; }
        string UserProvidedValue { get; set; }
    }
}