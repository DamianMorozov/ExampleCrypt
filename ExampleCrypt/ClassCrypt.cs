using System;
using System.Security.Cryptography;

namespace ExampleCrypt
{
	/// <summary>
	/// Singleton ClassCrypt.
	/// </summary>
	public sealed class ClassCrypt
	{
		#region Singleton Lazy<T>

		private static readonly Lazy<ClassCrypt> _instance = new Lazy<ClassCrypt>(() => new ClassCrypt());

		public static ClassCrypt Instance { get { return _instance.Value; } }

		public ClassCrypt() { }

		#endregion

		#region Public instances

		/// <summary>
		/// Instance of System.Security.Cryptography.Aes
		/// </summary>
		public Aes Aes { get; private set; } = Aes.Create();

		/// <summary>
		/// Instance of System.Security.Cryptography.Rijndael
		/// </summary>
		public Rijndael Rijndael { get; private set; } = Rijndael.Create();

		/// <summary>
		/// Instance of ClassCryptRijndael.
		/// </summary>
		public ClassCryptRijndael AlgorithmRijndael { get; private set; } = ClassCryptRijndael.Instance;

		/// <summary>
		/// Instance of ClassCryptAes.
		/// </summary>
		public ClassCryptAes AlgorithmAes { get; private set; } = ClassCryptAes.Instance; 
		
		#endregion

		#region Public methods

		/// <summary>
		/// Present array of bytes how HEX string
		/// </summary>
		/// <param name="byteArray">Length array of bytes must be more than 0</param>
		/// <param name="allowDash">Allow using "-"</param>
		/// <returns></returns>
		public string ByteArrayAsHexString(byte[] byteArray, bool allowDash)
		{
			string result = string.Empty;
			if (byteArray.Length > 0)
			{
				foreach (char letter in byteArray)
				{
					int value = Convert.ToInt32(letter);
					result += allowDash ? $"{value:X2}-" : $"{value:X2}";
				}
				if (result.EndsWith("-"))
					result = result.TrimEnd('-');
			}
			return result;
		}

		/// <summary>
		/// Present HEX string how array of bytes
		/// </summary>
		/// <param name="str">Any string, except "<null>"</param>
		/// <returns></returns>
		public byte[] HexStringAsByteArray(string str, bool haveDash)
		{
			byte[] result = new byte[0];
			if (str != null)
			{
				if (!string.IsNullOrEmpty(str))
				{
					for (var i = 0; i < str.Length; i += 2)
					{
						Array.Resize(ref result, result.Length + 1);
						result[result.Length - 1] = Convert.ToByte(str.Substring(i, 2), 16);
						if (haveDash)
							i++;
					}
				}
			}
			return result;
		} 
		
		#endregion
	}
}
