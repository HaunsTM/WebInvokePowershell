<h1>WebInvokePowerShellBackendt</h1>

<h2>What is WebInvokePowerShell?</h2>
<p>WebInvokePowerShell is a web application which guides users to provide a powershell script file with arguments. When it has collected all data, the script is run.</p>
<p>This is the service part of an eco-system. There are separate backend and frontend. Backend is a C# WCF-application running on an IIS-instance, front is a SPA made with VueJS (running on another IIS-instance). Backend is responsible for the main powershell script performance.</p>
<p></p>

<h2>About the Service</h2>
<p>The service is a REST-ful C# WCF-application with CORS support.</p>
<p>The service is exposing two methods:
	<ul>
		<li><p><code>http://[BASE_URL]/PowerShellService.svc/GetRegisteredPowerShellScripts_NamesDescriptionsAndParameters</code>. This endpoint accepts [GET] and returns JSON, example:</p>
<pre>[
    {
        "Description": "This script will help users to change several types of addresses",
        "FileNameWithoutPath": "ChangeAddresses.ps1",
        "Name": "ChangeAddresses",
        "Parameters": [
            {
                "Description": "E-mail",
                "Name": null
            },
            {
                "Description": "Office address",
                "Name": null
            },
            {
                "Description": "Home address",
                "Name": null
            },
            {
                "Description": "Office address",
                "Name": null
            }
        ]
    },</pre></li>
		<li><code>http://[BASE_URL]/PowerShellService.svc/InvokePowerShellScript</code> This endpoint accepts [POST] and returns JSON, example:</li>
	</ul>
</p>