using System;
using System.Security.Cryptography;

namespace AuthService.GeneratorUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateRsaKeyPair();
        }

        /// <summary>
        /// Generate RSA Key Pair (Private and Public Key)
        /// example key pair generated:
        /// private key: MIIEpgIBAAKCAQEAyZp+j76yf5SQRNonrnSTTbITQRb3XAdw94n/SmS3TCsGFgzH/rX6gkDue1c0fQSaXXS0pn0A0QNQIcy0yLpE+JdgL71Xmbu4ZcadFlFOLi3gF7K9K8uzcDzy5pm8yoZBuRaIOFAhp7J/B+xF5TWHvboQGEHe2U17wx234L9tUHsv/pVCczyXUvsHl+H0NlKKQqF4Evlmq9PXdLX8V2IFkbmhztaublrwegrCmvQHbgJ7j2nL4rgyKNDTsETaOZOoBbHICVZxDoWBlFw3M4wCe6Bs4FKpNk1/D3YbePydWrhSgC7EMeG33IVPkaUgmsz/0g+zkZrtzHyTNJ7IGmfJ3QIDAQABAoIBAQCPAJabsgH9e00mebRCDBDcwN7lgbvuPJ0GCY3boDtgvPfxNhm69CyArjw7oyzpLRWuCvWFTxAAMqBpiUIHTBFWYFHrKxxzQPPYxZxx0zRoj0Pwq2mCIlji2WjW5+BBrB/8gR4ZC8YDpRp0bUVbA7CIhO2bt0Wy0EJksbTXqBGLTsP7Vw4P5YMpQgykjjM7MVJx8YmNPzejutgJB8FqEEW3V744XB/FHbtNA7ew1jzxvWWzsqc8XU+l2GQIPjkkdMfTEng/8JFBet8GMRy+rBw0hF4aHyADDmfyIdNYyWPqlwQgCdtHLSpdJlUBse9rk4bUjcAo6O7TDZZ95yNxQwLFAoGBAOEsSIM5XjpWyUgcqtoJngieipTuU/eXDBjMQXR6RxWGevn/Giyqd6kCY+d3ZBMcHQ2j01F6EvmNgii7/TsIJ66cTRT3uXUhAWGuMg/uZnMUymEQzLtSurl9qNacBoWx/WAhajdzDE6Alsz1LrGnR4eAO4mIBRG/qY2KBr7K8sH7AoGBAOU0KbntvOLouembG030OR7VnPpbIgdMUgIYronCUkF/s9WGGJIf+iniuP9WnKCCoWqh0hoWUzN3xV1R2hTnt7b/xOMTajyeBeioqw3bXBLkc4NTFQaWI2ZeV0lD6h88mjLd7C0B7W4EJzKRaRsk6ca3NTg8c8da7TctJfx6ILQHAoGBANC/8hWdmKqzDHWLBiWPJvBMsqMxc6ykXrWans/yEHZ3LwkXI9fmzXpk2eObfq5ssM6VY3I0nuS1+MX0yeXxQICTLK7Unh1lVNeO26CDXn0v+BMWtQawwqT2RxF2omFNyl1VfRgc03rvoV4vq3NNZnXLPubYsAJtUi4CmRBGK9oDAoGBAM6nLmvt9t0beewvJfouFYZAkS2FF3Q/Er3DJTMd2m4lxq3hHqw5WqODQMsvez6ZKRJsXnOY52FDPta54wfwOEst5oXaTnHjBG7WDIwM2MJL6f5g3Vc37SjyLH7pVeDeEiWEw9l8oGcOJY1JX9vSd1jsfHZ8wuLej3ytH0+5iVznAoGBANZ5Y0+X1XEJg18LjttTLZbHfHYPczfpmi90gu6lPuYBM+jss6mAXCnOzhTMdLE5UJOd8hlGcaPVwuJpU/R2IEGPz5TQgRXlzC8bFJ+g6hB+39e/Zx6K1cRVUAyh2ViQoz1MsuBj1Aqd1XANMkVV+aSKfuXz45nyDOByIEh7OnJq
        /// public key: MIIBCgKCAQEAyZp+j76yf5SQRNonrnSTTbITQRb3XAdw94n/SmS3TCsGFgzH/rX6gkDue1c0fQSaXXS0pn0A0QNQIcy0yLpE+JdgL71Xmbu4ZcadFlFOLi3gF7K9K8uzcDzy5pm8yoZBuRaIOFAhp7J/B+xF5TWHvboQGEHe2U17wx234L9tUHsv/pVCczyXUvsHl+H0NlKKQqF4Evlmq9PXdLX8V2IFkbmhztaublrwegrCmvQHbgJ7j2nL4rgyKNDTsETaOZOoBbHICVZxDoWBlFw3M4wCe6Bs4FKpNk1/D3YbePydWrhSgC7EMeG33IVPkaUgmsz/0g+zkZrtzHyTNJ7IGmfJ3QIDAQAB
        /// </summary>
        static void GenerateRsaKeyPair()
        {
            using RSA rsa = RSA.Create();
            Console.WriteLine("##### Generate RSA Key Pair #####");
            Console.WriteLine($"-----Private key-----{Environment.NewLine}{Convert.ToBase64String(rsa.ExportRSAPrivateKey())}{Environment.NewLine}");
            Console.WriteLine($"-----Public key-----{Environment.NewLine}{Convert.ToBase64String(rsa.ExportRSAPublicKey())}");
        }
    }
}
