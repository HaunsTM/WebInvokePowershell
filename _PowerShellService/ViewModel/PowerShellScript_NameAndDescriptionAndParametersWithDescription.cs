using System.Collections.Generic;

namespace PowerShellService.ViewModel
{
    public class PowerShellScript_NameAndDescriptionAndParametersWithDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ParameterDescription> Parameters { get; set; }
    }

    public class ParameterDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
