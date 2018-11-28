using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace ExampleCrypt
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ClassCrypt _crypt = ClassCrypt.Instance;
		private Rijndael myRijndael = Rijndael.Create();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonCrypt_Click(object sender, RoutedEventArgs e)
		{
			fieldDecrypt.Clear();

			byte[] encrypted = _crypt.AlgorithmRijndael.EncryptStringToBytes(fieldCrypt.Text, myRijndael.Key, myRijndael.IV);
			fieldDecrypt.Text = _crypt.ByteArrayAsHexString(encrypted, true);
		}

		private void ButtonDecrypt_Click(object sender, RoutedEventArgs e)
		{
			fieldCrypt.Clear();
			
			fieldCrypt.Text = _crypt.AlgorithmRijndael.DecryptStringFromBytes(
				_crypt.HexStringAsByteArray(fieldDecrypt.Text, true), myRijndael.Key, myRijndael.IV);
		}
	}
}
