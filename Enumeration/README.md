Sharphound:
```
Invoke-BloodHound -CollectionMethods All -ZipFilename ofir -ZipPassword Aa123456 -OutputDirectory C:\Users\Public\
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
