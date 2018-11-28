using System;
using System.IO;
using System.Security.Cryptography;

namespace ExampleCrypt
{
	/// <summary>
	/// Singleton ClassCryptRijndael
	/// </summary>
	public sealed class ClassCryptRijndael
	{
		#region Singleton Lazy<T>

		private static readonly Lazy<ClassCryptRijndael> _instance = new Lazy<ClassCryptRijndael>(() => new ClassCryptRijndael());

		public static ClassCryptRijndael Instance { get { return _instance.Value; } }

		public ClassCryptRijndael() { }

		#endregion

		public byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");
			byte[] encrypted;
			// Create an Rijndael object with the specified key and IV.
			using (Rijndael rijAlg = Rijndael.Create())
			{
				rijAlg.Key = Key;
				rijAlg.IV = IV;

				// Create an encryptor to perform the stream transform.
				ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{

							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream.
			return encrypted;
		}

		public string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an Rijndael object
			// with the specified key and IV.
			using (Rijndael rijAlg = Rijndael.Create())
			{
				rijAlg.Key = Key;
				rijAlg.IV = IV;

				// Create a decryptor to perform the stream transform.
				ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{

							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}

			}

			return plaintext;
		}
	}
}
