#
# KeyVaultPreparation.ps1
#

# Paramaeters
$subscriptionId=''
$location = 'West europe'
$resourceGroupName='keyvaultrg1'
$vaultName='titodevkeyvault'


# Login and set subscription

Login-AzureRmAccount 

# Get-AzureRmSubscription # To view list of subscriptions associated with the account
Set-AzureRmContext -SubscriptionId $subscriptionId
# Get-AzureRmContext # To verify which subscription has been selected
New-AzureRmResourceGroup –Name $resourceGroupName –Location $location
New-AzureRmKeyVault -VaultName $vaultName -ResourceGroupName $resourceGroupName -Location $location -SKU 'Premium'

<#
#Operation - Add sample secret to Key Vault 
$secretvalue1 = ConvertTo-SecureString 'devsecret value 1' -AsPlainText -Force
$secret = Set-AzureKeyVaultSecret -VaultName $vaultName -Name 'DevSecret1' -SecretValue $secretvalue1
$secret.Id
#>