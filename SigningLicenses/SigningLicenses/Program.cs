using System;
using System.IO;
using System.Security.Cryptography;

namespace SigningLicenses
{
    class Program
    {
        const string privateKeyFileName = "privatekey.xml";
        const string publicKeyFileName = "publickey.xml";
        const string licenseFileName = "License.txt";
        const string badLicenseFileName = "CorruptLicense.txt";

        static void Main(string[] args)
        {
            GenerateKeyPair();
            string signature = SignLicense(privateKeyFileName, licenseFileName);
            Console.WriteLine("Signed as: {0}", signature);

            bool doesMatch = VerifyLicense(publicKeyFileName, licenseFileName, signature);
            Console.WriteLine("This should be true: {0}", doesMatch);

            doesMatch = VerifyLicense(publicKeyFileName, badLicenseFileName, signature);
            Console.WriteLine("This should be false: {0}", doesMatch);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void GenerateKeyPair()
        {
            if (!File.Exists(privateKeyFileName) || !File.Exists(publicKeyFileName))
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                using (StreamWriter privateWriter = File.CreateText(privateKeyFileName))
                {
                    privateWriter.Write(provider.ToXmlString(true));
                }

                using (StreamWriter publicWriter = File.CreateText(publicKeyFileName))
                {
                    publicWriter.Write(provider.ToXmlString(false));
                }
            }
        }

        private static string SignLicense(string privateKeyFile, string licenseFile)
        {
            string keyXml = GetKeyXml(privateKeyFile);

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(keyXml);
            using (FileStream stream = File.OpenRead(licenseFile))
            {
                string signature = EncodeToBase64(provider.SignData(stream, new SHA1CryptoServiceProvider()));
                return signature;
            }
        }

        private static string GetKeyXml(string keyFile)
        {
            using (StreamReader reader = File.OpenText(keyFile))
            {
                return reader.ReadToEnd();
            }
        }

        private static bool VerifyLicense(string publicKeyFile, string licenseFile, string signature)
        {
            string keyXml = GetKeyXml(publicKeyFile);

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(keyXml);
            using (FileStream stream = File.OpenRead(licenseFile))
            {
                byte[] license = new byte[stream.Length];
                stream.Read(license, 0, (int)stream.Length);
                return provider.VerifyData(license, new SHA1CryptoServiceProvider(), DecodeFromBase64(signature));
            }
        }

        private static string EncodeToBase64(byte[] toEncode)
        {
            return Convert.ToBase64String(toEncode);
        }

        private static byte[] DecodeFromBase64(string toDecode)
        {
            return Convert.FromBase64String(toDecode);
        }
    }
}
