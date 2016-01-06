#
# AzureAD.ps1
#
# Get-AzureSubscription
$subscriptionId=''
Select-AzureSubscription –SubscriptionId $subscriptionId
$vaultName='titodevkeyvault'
$certificateFilePath = "D:\cert\titodevcert2.cer"
$certificate = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2
$certificate.Import($certificateFilePath)
$rawCertificateData = $certificate.GetRawCertData()
$credential = [System.Convert]::ToBase64String($rawCertificateData)
$startDate= [System.DateTime]::Now
$endDate = $startDate.AddYears(1)
$adApplication = New-AzureRmADApplication -DisplayName "TitoKeyVaultADApp" `
  -HomePage "http://localhost:17412/" -IdentifierUris "http://localhost:17412/" `
  -KeyValue $credential -KeyType "AsymmetricX509Cert" -KeyUsage "Verify" `
  -StartDate $startDate -EndDate $endDate

$servicePrincipal = New-AzureRmADServicePrincipal -ApplicationId $adApplication.ApplicationId

Set-AzureRmKeyVaultAccessPolicy -VaultName $vaultName -ObjectId  $servicePrincipal.Id -PermissionsToSecrets get
$ServicePrincipal.ApplicationId #Outputs the ServicePrincipalName/AppPrincipalId
