using System.Collections.Generic;

namespace Devon4Net.Infrastructure.Common.Options.Devon
{
    public class Clientcertificates
    {
        public List<string> Whitelist { get; set; }
        public bool DisableClientCertificateCheck { get; set; }
    }
}