using System;
using System.Numerics;

internal static class DiffieHellman
{
    public static BigInteger PrivateKey(BigInteger primeP)
    {
        var random = new Random();
        var buffer = primeP.ToByteArray();

        var privateKey = primeP;
        while (privateKey >= primeP)
        {
            random.NextBytes(buffer);
            buffer[^1] &= 0x7F;
            privateKey = new BigInteger(buffer);
        }

        return privateKey;
    }

    public static BigInteger PublicKey(BigInteger primeP, BigInteger primeG, BigInteger privateKey) => BigInteger.ModPow(primeG, privateKey, primeP);

    public static BigInteger Secret(BigInteger primeP, BigInteger publicKey, BigInteger privateKey) => BigInteger.ModPow(publicKey, privateKey, primeP);
}
