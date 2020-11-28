$reason = $args[0]
if($reason -eq "PullRequest")
{
    $files=$(git diff HEAD HEAD~ --name-only)
    $paths = $env:PATHS -split ' '
            
    $shouldBuild = $false
    foreach ($path in $paths) 
    { 
        if( $files -like $path) 
        {
            $shouldBuild = $true
            break
        } 
    }

    if ($shouldBuild)
    {
        echo "Build will execute"
        Write-Host "##vso[task.setvariable variable=shouldBuild;isOutput=true]Yes"
    }
    else
    {
        echo "Build will not execute"
        Write-Host "##vso[task.setvariable variable=shouldBuild;isOutput=true]No"
    }
}
else
{
    echo "Build will execute"
    Write-Host "##vso[task.setvariable variable=shouldBuild;isOutput=true]Yes"
}