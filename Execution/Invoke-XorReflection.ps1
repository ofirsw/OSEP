function Invoke-XorReflection {
    param(
        [string]$inputFile,
        [string]$outputFile,
        [string]$functionName,
        [string]$xorKey = (Get-Random -Minimum 1 -Maximum 256 | Out-String).Trim()
    )
    $filebytes = [System.IO.File]::ReadAllBytes($inputFile)
    for($i=0; $i -lt $filebytes.count ; $i++)
    {
        $filebytes[$i] = $filebytes[$i] -bxor $xorKey
    }
    $b64 = [Convert]::ToBase64String($filebytes)
    Write-Host "Xor key is $xorKey"
    
    $scriptContent = @"
function $FunctionName {
    param (
        [string]`$Params
    )
    if (!`$fileassembly) {
        `$xorfilebytes = [Convert]::FromBase64String("$b64");
        for(`$i=0; `$i -lt `$xorfilebytes.count ; `$i++){ `$xorfilebytes[`$i] = `$xorfilebytes[`$i] -bxor $xorKey };
        `$fileassembly = [System.Reflection.Assembly]::Load(`$xorfilebytes)
    }
    `$fileassembly.EntryPoint.Invoke(`$null,(, `$Params.split()))

}
"@
    
    $scriptContent | Set-Content -Path "$outputfile"


}