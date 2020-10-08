using AuthService.Domain.Entities;
using AuthService.Domain.Settings;
using System;
using System.Collections.Generic;

namespace AuthService.Domain.UnitTests.Util
{
    public static class TokenFactory
    {
        /*
         * Before we test the program, first we manually create the access token using this data:
         * JWT payload data:
         * {
         *   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "5f7238dd776f88c5c55f2457",
         *   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "Albert Brucelee",
         *   "nbf": 1577836800,
         *   "exp": 1893456000,
         *   "iss": "MakanKuy.AuthService",
         *   "aud": "MakanKuy.AccessTokenHandler"
         * }
         * 
         * Private key: MIIEpgIBAAKCAQEAyZp+j76yf5SQRNonrnSTTbITQRb3XAdw94n/SmS3TCsGFgzH/rX6gkDue1c0fQSaXXS0pn0A0QNQIcy0yLpE+JdgL71Xmbu4ZcadFlFOLi3gF7K9K8uzcDzy5pm8yoZBuRaIOFAhp7J/B+xF5TWHvboQGEHe2U17wx234L9tUHsv/pVCczyXUvsHl+H0NlKKQqF4Evlmq9PXdLX8V2IFkbmhztaublrwegrCmvQHbgJ7j2nL4rgyKNDTsETaOZOoBbHICVZxDoWBlFw3M4wCe6Bs4FKpNk1/D3YbePydWrhSgC7EMeG33IVPkaUgmsz/0g+zkZrtzHyTNJ7IGmfJ3QIDAQABAoIBAQCPAJabsgH9e00mebRCDBDcwN7lgbvuPJ0GCY3boDtgvPfxNhm69CyArjw7oyzpLRWuCvWFTxAAMqBpiUIHTBFWYFHrKxxzQPPYxZxx0zRoj0Pwq2mCIlji2WjW5+BBrB/8gR4ZC8YDpRp0bUVbA7CIhO2bt0Wy0EJksbTXqBGLTsP7Vw4P5YMpQgykjjM7MVJx8YmNPzejutgJB8FqEEW3V744XB/FHbtNA7ew1jzxvWWzsqc8XU+l2GQIPjkkdMfTEng/8JFBet8GMRy+rBw0hF4aHyADDmfyIdNYyWPqlwQgCdtHLSpdJlUBse9rk4bUjcAo6O7TDZZ95yNxQwLFAoGBAOEsSIM5XjpWyUgcqtoJngieipTuU/eXDBjMQXR6RxWGevn/Giyqd6kCY+d3ZBMcHQ2j01F6EvmNgii7/TsIJ66cTRT3uXUhAWGuMg/uZnMUymEQzLtSurl9qNacBoWx/WAhajdzDE6Alsz1LrGnR4eAO4mIBRG/qY2KBr7K8sH7AoGBAOU0KbntvOLouembG030OR7VnPpbIgdMUgIYronCUkF/s9WGGJIf+iniuP9WnKCCoWqh0hoWUzN3xV1R2hTnt7b/xOMTajyeBeioqw3bXBLkc4NTFQaWI2ZeV0lD6h88mjLd7C0B7W4EJzKRaRsk6ca3NTg8c8da7TctJfx6ILQHAoGBANC/8hWdmKqzDHWLBiWPJvBMsqMxc6ykXrWans/yEHZ3LwkXI9fmzXpk2eObfq5ssM6VY3I0nuS1+MX0yeXxQICTLK7Unh1lVNeO26CDXn0v+BMWtQawwqT2RxF2omFNyl1VfRgc03rvoV4vq3NNZnXLPubYsAJtUi4CmRBGK9oDAoGBAM6nLmvt9t0beewvJfouFYZAkS2FF3Q/Er3DJTMd2m4lxq3hHqw5WqODQMsvez6ZKRJsXnOY52FDPta54wfwOEst5oXaTnHjBG7WDIwM2MJL6f5g3Vc37SjyLH7pVeDeEiWEw9l8oGcOJY1JX9vSd1jsfHZ8wuLej3ytH0+5iVznAoGBANZ5Y0+X1XEJg18LjttTLZbHfHYPczfpmi90gu6lPuYBM+jss6mAXCnOzhTMdLE5UJOd8hlGcaPVwuJpU/R2IEGPz5TQgRXlzC8bFJ+g6hB+39e/Zx6K1cRVUAyh2ViQoz1MsuBj1Aqd1XANMkVV+aSKfuXz45nyDOByIEh7OnJq
         * 
         * the generated access token is:
         * eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjVmNzIzOGU4Nzc2Zjg4YzVjNTVmMjQ1OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBbGJlcnQgQnJ1Y2VsZWUiLCJuYmYiOjE2MDE3ODQxNTgsImV4cCI6MTYzMjg4ODE1OCwiaXNzIjoiTWFrYW5LdXkuQXV0aFNlcnZpY2UiLCJhdWQiOiJNYWthbkt1eS5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.EKztbAMuEA1TP9NbgRI5XfT70_ROKnRbLPCfPACbiV3_RhnbM-76XodWVSOeNzpXul4PJj2aCoSmtY9Lw-dvtUUxzR5s_cDolfXFMDIrc4qs2bFVur-FK6_5Y5ay6ga4t-PBtVwSQQKvOsSV2v6EmfowBpLjrP3NweuCu4rejn0Kts3SVVAgDryL3UTJwBqEktNZGO7khe2IsnfUe17uXLLE-Pm2LZ5nWVOxybSzdjCyyVM8p8_2mSqM0wJqC0l-AIarB_RDDpd3u5lBo9AyEedDUDMe8Alg9T540XWxMacjb-1ZoQAJKqQQ2nSJXk-772W3sBwFUkVLNO2MUe4lJQ
         * 
         * In order for our program to generate the same access token,
         * we input the same payload data to our program.
         * 
         * "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" is for user id
         * "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" is for user full name
         * nbf is jwt not before in seconds (count from 1970-01-01 00:00:00)
         * exp is jwt expires in seconds (count from 1970-01-01 00:00:00)
         * iss is jwt issuer
         * aud is jwt audience
         * 
         * */

        private static readonly DateTime JwtStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] {
                    new AccessTokenSettings
                    {
                        PrivateKey = "MIIEpgIBAAKCAQEAyZp+j76yf5SQRNonrnSTTbITQRb3XAdw94n/SmS3TCsGFgzH/rX6gkDue1c0fQSaXXS0pn0A0QNQIcy0yLpE+JdgL71Xmbu4ZcadFlFOLi3gF7K9K8uzcDzy5pm8yoZBuRaIOFAhp7J/B+xF5TWHvboQGEHe2U17wx234L9tUHsv/pVCczyXUvsHl+H0NlKKQqF4Evlmq9PXdLX8V2IFkbmhztaublrwegrCmvQHbgJ7j2nL4rgyKNDTsETaOZOoBbHICVZxDoWBlFw3M4wCe6Bs4FKpNk1/D3YbePydWrhSgC7EMeG33IVPkaUgmsz/0g+zkZrtzHyTNJ7IGmfJ3QIDAQABAoIBAQCPAJabsgH9e00mebRCDBDcwN7lgbvuPJ0GCY3boDtgvPfxNhm69CyArjw7oyzpLRWuCvWFTxAAMqBpiUIHTBFWYFHrKxxzQPPYxZxx0zRoj0Pwq2mCIlji2WjW5+BBrB/8gR4ZC8YDpRp0bUVbA7CIhO2bt0Wy0EJksbTXqBGLTsP7Vw4P5YMpQgykjjM7MVJx8YmNPzejutgJB8FqEEW3V744XB/FHbtNA7ew1jzxvWWzsqc8XU+l2GQIPjkkdMfTEng/8JFBet8GMRy+rBw0hF4aHyADDmfyIdNYyWPqlwQgCdtHLSpdJlUBse9rk4bUjcAo6O7TDZZ95yNxQwLFAoGBAOEsSIM5XjpWyUgcqtoJngieipTuU/eXDBjMQXR6RxWGevn/Giyqd6kCY+d3ZBMcHQ2j01F6EvmNgii7/TsIJ66cTRT3uXUhAWGuMg/uZnMUymEQzLtSurl9qNacBoWx/WAhajdzDE6Alsz1LrGnR4eAO4mIBRG/qY2KBr7K8sH7AoGBAOU0KbntvOLouembG030OR7VnPpbIgdMUgIYronCUkF/s9WGGJIf+iniuP9WnKCCoWqh0hoWUzN3xV1R2hTnt7b/xOMTajyeBeioqw3bXBLkc4NTFQaWI2ZeV0lD6h88mjLd7C0B7W4EJzKRaRsk6ca3NTg8c8da7TctJfx6ILQHAoGBANC/8hWdmKqzDHWLBiWPJvBMsqMxc6ykXrWans/yEHZ3LwkXI9fmzXpk2eObfq5ssM6VY3I0nuS1+MX0yeXxQICTLK7Unh1lVNeO26CDXn0v+BMWtQawwqT2RxF2omFNyl1VfRgc03rvoV4vq3NNZnXLPubYsAJtUi4CmRBGK9oDAoGBAM6nLmvt9t0beewvJfouFYZAkS2FF3Q/Er3DJTMd2m4lxq3hHqw5WqODQMsvez6ZKRJsXnOY52FDPta54wfwOEst5oXaTnHjBG7WDIwM2MJL6f5g3Vc37SjyLH7pVeDeEiWEw9l8oGcOJY1JX9vSd1jsfHZ8wuLej3ytH0+5iVznAoGBANZ5Y0+X1XEJg18LjttTLZbHfHYPczfpmi90gu6lPuYBM+jss6mAXCnOzhTMdLE5UJOd8hlGcaPVwuJpU/R2IEGPz5TQgRXlzC8bFJ+g6hB+39e/Zx6K1cRVUAyh2ViQoz1MsuBj1Aqd1XANMkVV+aSKfuXz45nyDOByIEh7OnJq",
                        Issuer = "MakanKuy.AuthService",
                        Audience = "MakanKuy.AccessTokenHandler",
                        NotBefore = JwtStartDateTime.AddSeconds(1577836800),  //2020-01-01 00:00:00
                        Expires = JwtStartDateTime.AddSeconds(1893456000), //2030-01-01 00:00:00
                        ExpiresIn = 311040000 //10 years
                    },
                    User.Load(
                        id: "5f7238dd776f88c5c55f2457",
                        firstName: "Albert",
                        lastName: "Brucelee",
                        passwordHash: "test123",
                        email: "test@gmail.com",
                        isEmailVerified: false,
                        lastLoginUsingEmail: DateTime.UtcNow,
                        phone: "+6281234567891",
                        isPhoneVerified: false,
                        lastLoginUsingPhone: DateTime.UtcNow,
                        signUpDate: DateTime.UtcNow,
                        claims: null
                    ),
                    "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjVmNzIzOGRkNzc2Zjg4YzVjNTVmMjQ1NyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBbGJlcnQgQnJ1Y2VsZWUiLCJuYmYiOjE1Nzc4MzY4MDAsImV4cCI6MTg5MzQ1NjAwMCwiaXNzIjoiTWFrYW5LdXkuQXV0aFNlcnZpY2UiLCJhdWQiOiJNYWthbkt1eS5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.aal_VuP-3bzIvSMJ_l_KHl3tb1v4NqMniVz4wRePAotD_7a3Ea1pAfqHFWz6V_HjcaGJXpvoTtFcWetBwnGXt0EsV9CkhHIERBaJ6ANYWKyhmMt0tW6l6Bl641_mz-qtVKYjoYOWkxiIpfH2JerkhyLN_gen77LbZKE_KoyHZPo530oH3aqzhs5pXITaUbtfMI23JuOil9B6WqXcctbNE9MMv0WpjHQ8kFC9ve1ho2wg6tcpzOBg3HcZd0ahhucq1mQd882L9vhHoY8sPdXqNOAQgzcuLWTsgZj1ucPVb4tpC9RP6zvzNKZSf5hBWsIdV5-dH3KH9vjaDVeCybg4Mg"
                }
            };
        }

        /*
         * we use settings custom class, so we can mock the expires and the notBefore datetime
         * */
        private class AccessTokenSettings : IAccessTokenSettings
        {
            public string PrivateKey { get; set; }

            public string Issuer { get; set; }

            public string Audience { get; set; }

            public uint ExpiresIn { get; set; }

            public DateTime Expires { get; set; }

            public DateTime NotBefore { get; set; }

            public void ValidateAttributes() { }
        }
    }
}
