using System.Collections.Generic;

namespace ServiceLibrary.ViewModel
{
    public class PowerShellScript_NameAndDescriptionAndParametersWithDescription
    {
        public string Name { get; set; }
        public string FileNameWithoutPath { get; set; }
        public string Description { get; set; }
        public List<ParameterDescription> Parameters { get; set; }
    }

    public class ParameterDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
