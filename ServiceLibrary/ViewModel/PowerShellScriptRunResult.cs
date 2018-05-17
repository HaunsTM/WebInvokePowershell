using System.Runtime.Serialization;

namespace ServiceLibrary.ViewModel
{
    [DataContract]
    public class PowerShellScriptRunResult
    {
        public PowerShellScriptRunResult(string output, string errors)
        {
            Output = output;
            Errors = errors;
        }
        [DataMember]
        public string Output { get; private set; }
        [DataMember]
        public string Errors { get; private set; }
    }
}
