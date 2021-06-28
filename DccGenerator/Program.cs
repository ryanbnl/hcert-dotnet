using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using DCC;

namespace DccGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dsc = X509Certificate2.CreateFromPemFile("dgc_cert.pem", "dgc_key.pem");

            var encoder = new GreenCertificateEncoder(dsc);

            var data = File.ReadAllText(args.FirstOrDefault() ?? "dcc.json", Encoding.UTF8);

            var hcert = encoder.Encode(new CWT
            {
                DGCv1 = JsonSerializer.Deserialize<DgCertificate>(data)
            });

            Console.WriteLine($"HCert: |{hcert}|");
            Console.WriteLine(hcert);
        }
    }
}
