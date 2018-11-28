using System.Windows;

namespace ExampleCrypt
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ClassCrypt _crypt = ClassCrypt.Instance;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonCrypt_Click(object sender, RoutedEventArgs e)
		{
			fieldDecrypt.Clear();
			if (string.IsNullOrEmpty(fieldCrypt.Text))
				return;

			byte[] encrypted;
			switch (fieldAlgorithm.Items[fieldAlgorithm.SelectedIndex])
			{
				case "AES":
					encrypted = _crypt.AlgorithmAes.EncryptStringToBytes(fieldCrypt.Text, _crypt.Aes.Key, _crypt.Aes.IV);
					fieldDecrypt.Text = _crypt.ByteArrayAsHexString(encrypted, true);
					break;
				case "Rijndael":
					encrypted = _crypt.AlgorithmRijndael.EncryptStringToBytes(fieldCrypt.Text, _crypt.Rijndael.Key, _crypt.Rijndael.IV);
					fieldDecrypt.Text = _crypt.ByteArrayAsHexString(encrypted, true);
					break;
			}
		}

		private void ButtonDecrypt_Click(object sender, RoutedEventArgs e)
		{
			fieldCrypt.Clear();
			if (string.IsNullOrEmpty(fieldDecrypt.Text))
				return;

			switch (fieldAlgorithm.Items[fieldAlgorithm.SelectedIndex])
			{
				case "AES":
					fieldCrypt.Text = _crypt.AlgorithmAes.DecryptStringFromBytes(
						_crypt.HexStringAsByteArray(fieldDecrypt.Text, true), _crypt.Aes.Key, _crypt.Aes.IV);
					break;
				case "Rijndael":
					fieldCrypt.Text = _crypt.AlgorithmRijndael.DecryptStringFromBytes(
						_crypt.HexStringAsByteArray(fieldDecrypt.Text, true), _crypt.Rijndael.Key, _crypt.Rijndael.IV);
					break;
			}
		}
	}
}
