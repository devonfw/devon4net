using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Azure.KeyVault.Exception
{
    [Serializable]
    public class AzureKeyVaultSecretNotFoundException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretNotFoundException"/> class.
        /// </summary>
        public AzureKeyVaultSecretNotFoundException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public AzureKeyVaultSecretNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretNotFoundException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AzureKeyVaultSecretNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretNotFoundException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AzureKeyVaultSecretNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
