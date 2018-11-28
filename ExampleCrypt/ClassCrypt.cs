using System;

namespace ExampleCrypt
{
	/// <summary>
	/// Singleton ClassCrypt
	/// </summary>
	public sealed class ClassCrypt
	{
		#region Singleton Lazy<T>

		private static readonly Lazy<ClassCrypt> _instance = new Lazy<ClassCrypt>(() => new ClassCrypt());

		public static ClassCrypt Instance { get { return _instance.Value; } }

		public ClassCrypt() { }

		#endregion

		public ClassCryptRijndael AlgorithmRijndael { get; private set; } = ClassCryptRijndael.Instance;

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
					result += allowDash ? $"{value:X}-" : $"{value:X}";
				}
				if (result.EndsWith("-"))
					result = result.TrimEnd('-');
			}
			else
				result = @"<null>";
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
				if (!string.IsNullOrEmpty(str) && str != @"<null>")
				{
					for (var i = 0; i < str.Length; i += 2)
					{
						var iValue = Convert.ToInt32(Convert.ToString(str[i] + Convert.ToString(str[i + 1])), 16);
						Array.Resize(ref result, result.Length + 1);
						result[result.Length - 1] = (byte)iValue;
						if (haveDash)
							i++;
					}
				}
			}
			return result;
		}
	}
}
