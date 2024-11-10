using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CaseStudy.Core.Utilities.Helper
{
    public static class Func
    {
        public static Exception SonHata = (Exception)null;

        public static string ackToString(this object pText)
        {
            string str = "";

            SonHata = (Exception)null;

            try
            {
                str = !(pText is DBNull) && pText != null ? Convert.ToString(pText) : "";
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return str;
        }

        public static int ackToInt(this object pText)
        {
            int result = 0;

            SonHata = (Exception)null;

            try
            {
                int.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static long ackToLong(this object pText)
        {
            long result = 0;

            SonHata = (Exception)null;

            try
            {
                if (pText is DBNull || pText == null)
                {
                    result = 0L;
                }
                else
                {
                    string name = pText.GetType().Name;
                    if (name == "String" || name == "Char" || name == "DateTime")
                        long.TryParse(pText.ToString(), out result);
                    else
                        result = Convert.ToInt64(pText);
                }
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static double ackToDouble(this object pText)
        {
            double result = 0.0;

            SonHata = (Exception)null;

            try
            {
                double.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static float ackToFloat(this object pText)
        {
            float result = 0.0f;

            SonHata = (Exception)null;

            try
            {
                float.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static bool ackToBool(this object pText)
        {
            bool result = false;

            SonHata = (Exception)null;

            try
            {
                bool.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static byte ackToByte(this object pText)
        {
            byte result = 0;

            SonHata = (Exception)null;

            try
            {
                byte.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static Decimal ackToDecimal(this object pText)
        {
            Decimal result = 0M;

            SonHata = (Exception)null;

            try
            {
                Decimal.TryParse(pText.ToString(), out result);
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return result;
        }

        public static string ackTextDecryption(string EncryptedText)
        {
            string str = "";
            ushort num1 = 0;
            SonHata = (Exception)null;
            try
            {
                if (!(EncryptedText != ""))
                    return EncryptedText;
                int num2 = EncryptedText.Substring(0, EncryptedText.Length - 1).LastIndexOf("@");
                int num3 = char.ConvertToUtf32(EncryptedText.Substring(num2 + 1, 1), 0) - 32;
                if (EncryptedText[0] != '!' | num2 == 0 | num3 == 0)
                    return EncryptedText;
                try
                {
                    for (ushort index = 1; (int)index <= num2 - 2; ++index)
                    {
                        int num4 = num3 + (int)num1 % 6 + 32;
                        str += char.ConvertFromUtf32(char.ConvertToUtf32(EncryptedText.Substring((int)index, 1), 0) - num4);
                        ++num1;
                        if ((int)EncryptedText[(int)index + 1] == (int)Convert.ToChar(num4))
                            ++index;
                    }
                    return str.Length == num3 ? str : EncryptedText;
                }
                catch (Exception ex)
                {
                    SonHata = ex;
                    return EncryptedText;
                }
            }
            catch (Exception ex)
            {
                SonHata = ex;
                return EncryptedText;
            }
        }

        public static string ByteArrayToString(byte[] arr)
        {
            return "0x" + BitConverter.ToString(arr).Replace("-", String.Empty);
        }

        public static string SaveImageFile(string imgStr, string imgName, string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string imageName = imgName + ".jpg";
            string imgPath = Path.Combine(path, imageName);

            byte[] bytes = Convert.FromBase64String(imgStr);

            File.WriteAllBytes(imgPath, bytes);

            return imageName;
        }

        public static string GetRequest(string _url, string header1, string header1t)
        {
            string _result = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header1, header1t);

                    var response = client.GetAsync(_url).Result;
                    var responseContent = response.Content;

                    _result = responseContent.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                _result = "{\"ErrorCode\":999,\"ErrorMessage\":\"HATA 999 Unable_to_connect\",\"Status\":false}";
            }

            return _result;
        }

        public static string DtoToJson<T>(T filter)
        {
            string json = JsonSerializer.Serialize<T>(filter);

            return json;
        }

        public static string HashString(string text) 
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed()) 
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }

        public static string HashString<T>(string text, T filter)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (filter != null)
            {
                string json = JsonSerializer.Serialize<T>(filter);
                text += json;
            }
            
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                return hash;
            }
        }

        public static string ackToSqlReplace(this object pText)
        {
            string str = "";

            SonHata = (Exception)null;

            try
            {
                str = !(pText is DBNull) && pText != null ? Convert.ToString(pText) : "";
                str = str.Replace("'", " ");
            }
            catch (Exception ex)
            {
                SonHata = ex;
            }

            return str;
        }

        public static string AesEncryptString(string TokenSecurityKey, string Text)
        {
            byte[] Iv = new byte[16];
            byte[] Array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.Latin1.GetBytes(TokenSecurityKey);
                aes.IV = Iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(Text);
                        }

                        Array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(Array);
        }

        public static string AesDecryptString(string TokenSecurityKey, string Text)
        {
            byte[] Iv = new byte[16];
            byte[] Buffer = Convert.FromBase64String(Text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.Latin1.GetBytes(TokenSecurityKey);
                aes.IV = Iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(Buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
