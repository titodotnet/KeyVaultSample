using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KeyVaultSampleApp1
{
    /// <summary>    
    /// This class uses Microsoft.KeyVault.Client library to call into Key Vault and retrieve a secret.
    /// 
    /// Authentication when calling Key Vault is done through the configured X509 ceritifcate.
    /// </summary>
    public static class KeyVaultAccessor
    {
        private static KeyVaultClient keyVaultClient;
        private static ClientAssertionCertificate assertionCert;

        static KeyVaultAccessor()
        {
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetAccessToken));
            var clientAssertionCertPfx = CertificateHelper.FindCertificateByThumbprint(CloudConfigurationManager.GetSetting(Constants.KeyVaultAuthCertThumbprint));
            var client_id = CloudConfigurationManager.GetSetting(Constants.KeyVaultAuthClientId);
            assertionCert = new ClientAssertionCertificate(client_id, clientAssertionCertPfx);
        }

        /// <summary>
        /// Get a secret from Key Vault
        /// </summary>
        /// <param name="secretId">ID of the secret</param>
        /// <returns>secret value</returns>
        public static async Task<string> GetSecret(string secretId)
        {
            var secret = await keyVaultClient.GetSecretAsync(secretId).ConfigureAwait(false);
            return secret.Value;
        }

        /// <summary>
        /// Get a secret from Key Vault
        /// </summary>
        /// <param name="vaultUri">vault uri</param>
        /// <param name="secretName">secret name</param>
        /// <returns>secret value</returns>
        public static async Task<string> GetSecret(string vaultUri,string secretName)
        {
            var secret = await keyVaultClient.GetSecretAsync(vaultUri, secretName).ConfigureAwait(false);
            return secret.Value;
        }

        /// <summary>
        /// Authentication callback that gets a token using the X509 certificate
        /// </summary>
        /// <param name="authority">Address of the authority</param>
        /// <param name="resource">Identifier of the target resource that is the recipient of the requested token</param>
        /// <param name="scope">Scope</param>
        /// <returns></returns>
        public static async Task<string> GetAccessToken(string authority, string resource, string scope)
        {
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

            var result = await context.AcquireTokenAsync(resource, assertionCert);

            // In real case implementaation the above async call has been converted to sync call (as specified below) to avoid the token refresh issue.
            //var result = context.AcquireTokenAsync(resource, assertionCert);

            return result.AccessToken;
        }

    }
}