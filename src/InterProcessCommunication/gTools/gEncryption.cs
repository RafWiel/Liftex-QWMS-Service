using System;
using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Text;

namespace InterProcessCommunication.gTools
{
	/// <summary>
	/// Summary description for Encryption.
	/// </summary>
	public class gEnc
	{
        public static string GetHash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            SHA256 sha = SHA256.Create();
            StringBuilder result = new StringBuilder();

            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes("2gW6gG" + text + "a9sH2T"));

            for (int i = 0; i < data.Length; i++)
                result.Append(data[i].ToString("x2"));

            return result.ToString();
        }

		public static string Encrypt(string clearText) 
		{
            if (string.IsNullOrEmpty(clearText))
                return string.Empty;

			try
			{
				byte[] clearBytes		= System.Text.Encoding.Unicode.GetBytes(clearText); 
				PasswordDeriveBytes pdb = new PasswordDeriveBytes("3g7Rj8W66ffXd5", new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				byte[] encryptedData	= Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16)); 

				return Convert.ToBase64String(encryptedData); 
			}
			catch(Exception ex)
			{
				gLog.Error("encrypt: " + ex.Message);

				return clearText;
			}
		}

		public static string Decrypt(string cipherText) 
		{
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

			try
			{
				byte[] cipherBytes		= Convert.FromBase64String(cipherText); 
               	PasswordDeriveBytes pdb = new PasswordDeriveBytes("3g7Rj8W66ffXd5", new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 
				byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16)); 

				return System.Text.Encoding.Unicode.GetString(decryptedData); 
			}
			catch(Exception ex)
			{
				gLog.Error("decrypt: " + ex.Message);

				return cipherText;
			}
		}

        public static string Obfuscate(string text)
        {
            char[] arr = new char[text.Length];
            arr = text.ToCharArray();
            Obfuscate(ref arr, text.Length);

            return new string(arr);
        }

        static public string MakePassword(string st)
        {
            int[] num = {6, 4, 8};
            
            st = ReplaceChars(st);
            st = InverseByBase(st, num[0]);
            st = InverseByBase(st, num[1]);
            st = InverseByBase(st, num[2]);

            StringBuilder sb = new StringBuilder();
            foreach (char ch in st)
                sb.Append(ChangeChar(ch, num));
            
            return sb.ToString();
        }

		#region private methods

		private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV) 
		{ 
			MemoryStream ms = new MemoryStream(); 
			Rijndael alg	= Rijndael.Create(); 

			alg.Key	= Key; 
			alg.IV	= IV; 

			CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write); 

			cs.Write(clearData, 0, clearData.Length); 
			cs.Close(); 

			byte[] encryptedData = ms.ToArray();

			return encryptedData; 
		} 

		private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV) 
		{ 
			MemoryStream ms = new MemoryStream(); 
			Rijndael alg	= Rijndael.Create(); 

			alg.Key = Key; 
			alg.IV	= IV; 

			CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write); 

			cs.Write(cipherData, 0, cipherData.Length); 
			cs.Close(); 

			byte[] decryptedData = ms.ToArray(); 

			return decryptedData; 
		}

        private static void Obfuscate(ref char[] buffer, int bufferSize)
        {
            int offset = 0;
            string key = "Ta27sdGoinEeowYUifbWQiorVgboNZMirgJrFF";
            int l = key.Length;
            for (int i = 0; i < bufferSize; i++)
            {
                buffer[i] ^= (char)((int)key[offset % l] + 48);
                offset++;
            }
        }

        private static string ReplaceChars(string st)
        {
            int newPlace;
            char ch;
            for (int i = 0; i < st.Length; i++)
            {
                newPlace = i * Convert.ToUInt16(st[i]);
                newPlace = newPlace % st.Length;
                ch = st[i];
                st = st.Remove(i, 1);
                st = st.Insert(newPlace, ch.ToString());
            }

            return st;
        }

        private static string InverseByBase(string st, int moveBase)
        {
            StringBuilder sb = new StringBuilder();
            
            int c;
            for (int i = 0; i < st.Length; i += moveBase)
            {
                if (i + moveBase > st.Length - 1)
                    c = st.Length - i;
                else
                    c = moveBase;

                sb.Append(InverseString(st.Substring(i, c)));
            }

            return sb.ToString();
        }

        private static char ChangeChar(char ch, int[] enCode)
        {
            ch = char.ToUpper(ch);
            if (ch >= 'A' && ch <= 'H')
                return Convert.ToChar(Convert.ToInt16(ch) + 2 * enCode[0]);
            else if (ch >= 'I' && ch <= 'P')
                return Convert.ToChar(Convert.ToInt16(ch) - enCode[2]);
            else if (ch >= 'Q' && ch <= 'Z')
                return Convert.ToChar(Convert.ToInt16(ch) - enCode[1]);
            else if (ch >= '0' && ch <= '4')
                return Convert.ToChar(Convert.ToInt16(ch) + 5);
            else if (ch >= '5' && ch <= '9')
                return Convert.ToChar(Convert.ToInt16(ch) - 5);
            else
                return '0';
        }

        private static string InverseString(string st)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
                sb.Append(st[i]);

            return sb.ToString();
        }

		#endregion
	}
}
