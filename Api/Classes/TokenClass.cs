using System;
using System.Security.Cryptography;
using System.Configuration;
using System.Text;
using Database;
using Microsoft.Extensions.Configuration;

namespace Api.Classes;

public class TokenClass
{
    private static byte[] _secretKey;
    const string TOKENALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";

    public TokenClass()
    {
        // Getting the value of "SecretTokenKey" from "appsettings.json"
        var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfiguration config = configBuilder.Build();
        string secretTokenKey = config["TokenSecretKey"];

        _secretKey = Encoding.UTF8.GetBytes(secretTokenKey);
    }

    public string GenerateToken()
    {
        var rnd = new RNGCryptoServiceProvider();
        var tokenBytes = new byte[20];
        rnd.GetBytes(tokenBytes);
        var token =
            Enumerable
                .Range(0, 20)
                .Select(i => TOKENALPHABET[tokenBytes[i] % TOKENALPHABET.Length])
                .ToArray();

        return new String(token);
    }


    public bool ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        return DBUtils.CheckTokenExists(token);
    }
}