using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using DCC;
using Newtonsoft.Json;

namespace DccGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dsc = X509Certificate2.CreateFromPemFile("dgc_cert.pem", "dgc_key.pem");

            var encoder = new GreenCertificateEncoder(dsc);

            var data = File.ReadAllText(args.FirstOrDefault() ?? "dcc.json", Encoding.UTF8);

            var dgc = JsonConvert.DeserializeObject<DgCertificate>(data);

            var cwt = new CWT
            {
                DGCv1 = dgc
            };

            var hcert = encoder.Encode(cwt);

            Console.WriteLine($"HCert: |{hcert}|");
            Console.WriteLine(hcert);
            Console.Read();
        }
    }
}
