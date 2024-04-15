$OrganizationURL = "https://dev.azure.com"
$AzureDevOpsOrganization = "Organization"
$ProjectName = "Project"
$PersonalAccessToken = "8bea34f7-10d0-415a-95ce-3c3db8245610"
$Type = "Bug"
$headers = @{
  Authorization = "Basic " + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$PersonalAccessToken"))
  "Accept" = "application/json"
}

# Main script
$uri = "$OrganizationURL/$AzureDevOpsOrganization/$ProjectName/_apis/wit/workitems/`$$Type?api-version=7.0"

$body = @"
[
  {
    "op": "add",
    "path": "/fields/System.Title",
    "value": "Bug de teste via PowerShell"
  },
  {
    "op": "add",
    "path": "/fields/Microsoft.VSTS.TCM.ReproSteps",
    "value": "Passos para reproduzir o bug..."
  },
  {
    "op": "add",
    "path": "/fields/System.AreaPath",
    "value": "Project\\Area"
  }
]
"@

$response = Invoke-RestMethod -Uri $uri -Method Post -Headers $headers -Body $body -ContentType "application/json-patch+json"

# Output the response
Write-Output $response

