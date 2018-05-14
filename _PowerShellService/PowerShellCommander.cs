using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShellService
{
    public class PowerShellCommander
    {
        private static void RunPowershellScript(string scriptFile, List<string> parameters = null)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(scriptFile))
            {
                throw new ArgumentNullException("scriptFile");
            }

            var runspaceConfiguration = RunspaceConfiguration.Create();
            using (var runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration))
            {
                runspace.Open();
                var scriptInvoker = new RunspaceInvoke(runspace);
                scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");

                var pipeline = runspace.CreatePipeline();

                var scriptCommand = new Command(scriptFile);
                if (parameters != null)
                {
                    var commandParameters = new Collection<CommandParameter>();
                    foreach (string scriptParameter in parameters)
                    {
                        var commandParm = new CommandParameter(null, scriptParameter);
                        commandParameters.Add(commandParm);
                        scriptCommand.Parameters.Add(commandParm);
                    }

                }
                pipeline.Commands.Add(scriptCommand);
                var results = pipeline.Invoke();
                foreach (var psObject in results)
                {
                    
                }
            }
        }
    }
}
