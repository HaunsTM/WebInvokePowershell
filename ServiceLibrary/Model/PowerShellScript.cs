using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceLibrary.Model
{
    [DataContract]
    public class PowerShellScript
    {
        [DataMember]
        public string File { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<PowershellScriptParameter> Parameters { get; set; }
    }
}