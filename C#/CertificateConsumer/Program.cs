using System;
using System.Security.Cryptography.X509Certificates;

namespace CertificateConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            X509Store store = new X509Store("MY", StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection certificates = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection filteredCertificates = (X509Certificate2Collection)certificates.Find(X509FindType.FindBySubjectName, "appOAuth", false);

            Console.WriteLine();
            Console.WriteLine("->> Showing all the certificates on the Machine:");
            foreach (var certificate in filteredCertificates)
            {
                byte[] rawdata = certificate.RawData;
                Console.WriteLine();
                Console.WriteLine("Content Type: {0}",X509Certificate2.GetCertContentType(rawdata));
                Console.WriteLine("Friendly Name: {0}",certificate.FriendlyName);
                Console.WriteLine("Subject: {0}",certificate.Subject);
                Console.WriteLine("Subject: {0}",certificate.SubjectName.Oid);
                Console.WriteLine("Certificate Verified?: {0}",certificate.Verify());
                Console.WriteLine("Simple Name: {0}",certificate.GetNameInfo(X509NameType.SimpleName, true));
                Console.WriteLine("Signature Algorithm: {0}",certificate.SignatureAlgorithm.FriendlyName);
                Console.WriteLine("Certificate Archived?: {0}",certificate.Archived);
                Console.WriteLine("Length of Raw Data: {0}",certificate.RawData.Length);

                if(certificate.HasPrivateKey)
                {
                    Console.WriteLine("Private Key: {0}",certificate.PrivateKey.ToXmlString(false));
                    Console.WriteLine("Public Key: {0}",certificate.PublicKey.Key.ToXmlString(false));
                }
            }

            store.Close();
        }
    }
}
