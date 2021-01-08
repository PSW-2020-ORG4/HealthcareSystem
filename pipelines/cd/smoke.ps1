Param(
    [Parameter(Mandatory=$true)]
    $uri,
    [Parameter(Mandatory=$true)]
    $code
)

For ($i=0; $i -lt 10; $i++) {
    Write-Output "---------------------------------------"
    Write-Output ("Attepmt #" + ($i + 1))
    Write-Output "---------------------------------------"
    $response = Invoke-WebRequest -Uri $uri -SkipHttpErrorCheck
    $response
    Write-Output ""
    if ($response.StatusCode -eq $code){
        Write-Output "Smoke test successful."
        Exit
    }
    Start-Sleep -s 10
}

Write-Output "Smoke test failed."
Exit 1