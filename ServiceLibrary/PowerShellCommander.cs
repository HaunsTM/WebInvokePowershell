using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using ServiceLibrary.ViewModel;

namespace ServiceLibrary
{
    internal class PowerShellCommander
    {
        internal PowerShellScriptRunResult RunPowerShellScript(string scriptFile, Dictionary<string, string> parameters = null)
        {
            try
            {
                var preparedParameters = string.Join(" ", parameters.Select(x => " " + "-" + x.Key + " \'" + x.Value + "\'").ToArray());
                var concatenatedArguments = $"{scriptFile} {preparedParameters}";

                //https://stackoverflow.com/questions/19514370/how-to-open-ps1-file-in-powershell-ise-in-c-sharp
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"powershell.exe";

                //provide powershell script full path
                startInfo.Arguments = concatenatedArguments;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                // execute script call start process
                process.Start();

                // get output information
                var output = process.StandardOutput.ReadToEnd();

                // catch error information
                var errors = process.StandardError.ReadToEnd();

                var result = new PowerShellScriptRunResult(output, errors);
                return result;
            }
            catch (Exception e)
            {
                var message = $"Couldn't execute PowerShell script {scriptFile}.";
                throw new Exception(message, e);
            }

        }
    }
}
