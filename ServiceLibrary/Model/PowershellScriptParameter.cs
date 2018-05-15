using System.Runtime.Serialization;

namespace ServiceLibrary.Model
{
    [DataContract]
    public class PowershellScriptParameter
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string UserProvidedValue { get; set; }
    }
}