# A ideia desse arquivo é possuir os scripts padrões da equipe que podem ser sobrescritos a qualquer momento
# Recomendamos manter personalizações do PowerShell no arquivo $PROFILE.CurrentUserAllHosts

# Iniciar o OhMyPosh e instalá-lo caso não esteja instalado
function Import-OhMyPosh() {
  $stopwatch =  [system.diagnostics.stopwatch]::StartNew()
  if(! (Get-Command oh-my-posh -ErrorAction SilentlyContinue)) {
    Write-Information "`n->> oh-my-posh is not installed. Installing it now with winget"
    curl -s https://ohmyposh.dev/install.sh | bash -s
  }
  
  if ( ! ($env:OhMyPoshFile) -and (Test-Path "$HOME/*.omp.json") ) {
    $OhMyPoshFile = (Get-ChildItem "$HOME/*.omp.json" | Select-Object -First 1).FullName
    Write-Information "`n->> Setting the OhMyPoshFile environment variable to $OhMyPoshFile"
    [System.Environment]::SetEnvironmentVariable("OhMyPoshFile", $OhMyPoshFile, "User")
    $env:OhMyPoshFile = $OhMyPoshFile
  }

  if ( $env:OhMyPoshFile ) {
    oh-my-posh init pwsh --config $env:OhMyPoshFile | Invoke-Expression
  } else {
    Write-Information "`n->> No OhMyPoshFile found. Starting without customizations"
    oh-my-posh init pwsh | Invoke-Expression
  } 

  $stopwatch.Stop(); Write-Verbose "`n-->> Importação do OhMyPosh demorou: $($stopwatch.ElapsedMilliseconds)"
}

# Ativar o histórico preditivo no PowerShell
try {
  Set-PSReadLineOption -PredictionSource History
  Set-PSReadLineOption -Colors @{ InlinePrediction = "#666699" }
  Set-PSReadLineKeyHandler -Chord "RightArrow" -Function ForwardWord
  Set-PSReadLineKeyHandler -Chord "End" -Function ForwardChar
}
catch {   
  Install-Module -Force -AcceptLicense PSReadLine
  Set-PSReadLineOption -PredictionSource History
  Set-PSReadLineOption -Colors @{ InlinePrediction = "#666699" }
  Set-PSReadLineKeyHandler -Chord "RightArrow" -Function ForwardWord
  Set-PSReadLineKeyHandler -Chord "End" -Function ForwardChar
}

# Ativado por último para fazer efeito
Import-OhMyPosh

