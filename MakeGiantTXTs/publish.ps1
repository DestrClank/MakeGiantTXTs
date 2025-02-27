# Définir les configurations de publication
$publishConfigurations = @(
    @{ Runtime = "win-x64"; Output = "./publish/win-x64" },
    @{ Runtime = "win-arm64"; Output = "./publish/win-arm64" },
    @{ Runtime = "linux-x64"; Output = "./publish/linux-x64" },
    @{ Runtime = "linux-arm64"; Output = "./publish/linux-arm64" },
    @{ Runtime = "osx-x64"; Output = "./publish/osx-x64" },
    @{ Runtime = "osx-arm64"; Output = "./publish/osx-arm64" }
)

# Publier pour chaque configuration
foreach ($config in $publishConfigurations) {
    $runtime = $config.Runtime
    $output = $config.Output

    Write-Host "Publishing for $runtime..."
    dotnet publish -c Release -r $runtime --self-contained -p:PublishSingleFile=true -p:PublishTrimmed=true -o $output
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to publish for $runtime" -ForegroundColor Red
        exit $LASTEXITCODE
    }
}

Write-Host "Publishing completed successfully!" -ForegroundColor Green
