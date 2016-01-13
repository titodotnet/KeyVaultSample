#
# KeyVaultsecretOps.ps1
#
$vaultName='titodevkeyvault'
$secretvalue1 = ConvertTo-SecureString 'devsecret value 1' -AsPlainText -Force
$secret = Set-AzureKeyVaultSecret -VaultName $vaultName -Name 'DevSecret1' -SecretValue $secretvalue1
$secret.Id