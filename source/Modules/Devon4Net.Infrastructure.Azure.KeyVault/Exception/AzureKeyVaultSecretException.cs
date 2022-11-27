using System.Runtime.Serialization;

namespace Devon4Net.Infrastructure.Azure.KeyVault.Exception
{
    [Serializable]
    public class AzureKeyVaultSecretException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretException"/> class.
        /// </summary>
        public AzureKeyVaultSecretException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public AzureKeyVaultSecretException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AzureKeyVaultSecretException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureKeyVaultSecretException"/> class.
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected AzureKeyVaultSecretException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
