using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgroMarketRD.Core.Helpers
{
    /// <summary>
    /// Clase helper para cifrado y descifrado de textos, usando el cifrado Rijndael (AES).
    /// 
    /// Author: Alix J. Javier
    /// Date: 14/04/2016
    /// 
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rijndael_key_schedule"/>
    /// <seealso cref="https://msdn.microsoft.com/en-us/library/system.security.cryptography.rijndaelmanaged.aspx"/>
    /// <seealso cref="http://www.codeproject.com/Tips/704372/How-to-Use-Rijndael-ManagedEncryption-with-Csharp"/>
    public class CryptoHelper
    {
        /// <summary>
        /// El valor de esta constante debe ser generado usando NewGuid para que sea un 
        /// valor único por aplicación dónde se use.
        /// </summary>
        internal const string InputKey = "f64e1b2d-1c6f-492b-b18b-be94b26e8dae";

        /// <summary>
        /// Debe cambiarse el valor de esta constante por el salt (sal)
        /// que se usará para generar el cifrado del texto que se quiera cifrar (o descifrar) con el método <c>Encrypt</c>
        /// </summary>
        /// <seealso cref="https://es.wikipedia.org/wiki/Sal_(criptograf%C3%ADa)"/>
        internal const string DEFAULT_SALT = "75198861-9795-4b72-91e2-5435e9de2865";

        /// <summary>
        /// Cifra un texto usando el cifrado Rijndael (AES) y una salt (sal).
        /// </summary>
        /// <param name="PlainText">Text a ser cifrado</param>
        /// <param name="SaltValue">Salt (sal) para generar clave aleatoria para el cifrado. Es opcional, sino se envia se tomará la de por defecto. 
        /// Debe usarse la misma al descifrar el texto.</param>
        /// <returns>Cadena de caracteres con el texto cifrado</returns>
        /// <seealso cref="https://en.wikipedia.org/wiki/Rijndael_key_schedule"/>
        /// <seealso cref="https://es.wikipedia.org/wiki/Sal_(criptograf%C3%ADa)"/>
        /// <exception cref="System.ArgumentException">Lanzada si no se enviaron los paramétros correctamente</exception>
        public static string Encrypt(string PlainText, string SaltValue = DEFAULT_SALT)
        {
            if (string.IsNullOrWhiteSpace(PlainText) || PlainText.Length <= 0)
                throw new ArgumentException("El texto a cifrar no puede estar en blanco.");

            if (string.IsNullOrWhiteSpace(SaltValue) || SaltValue.Length <= 0)
                throw new ArgumentException("SaltValue no puede estar en blanco, es necesario para generar la clave.");

            string encriptedText = string.Empty;

            using (var _aesAlgorithm = NewRijandaelManaged(SaltValue))
            {
                var _encryptor = _aesAlgorithm.CreateEncryptor(_aesAlgorithm.Key, _aesAlgorithm.IV);

                using (var _objMemStream = new MemoryStream())
                using (var _objCrypto = new CryptoStream(_objMemStream, _encryptor, CryptoStreamMode.Write))
                using (var _smWriter = new StreamWriter(_objCrypto))
                {
                    _smWriter.Write(PlainText);
                    _smWriter.Flush();
                    _objCrypto.FlushFinalBlock();

                    encriptedText = Convert.ToBase64String(_objMemStream.ToArray()); // Texto cifrado y encodeado en base64
                }

                return encriptedText;
            }
        }

        /// <summary>
        /// Descifra un texto cifrado previamente. IMPORTANTE: Se espera que este encodeado en base64, si
        /// se usó el método <c>Encrypt</c> para cifrar este texto entonces ya fue encodeado en base64.
        /// </summary>
        /// <param name="CipherText">Texto a ser descifrado</param>
        /// <param name="SaltValue">Salt (sal) para descifrar el texto, debe enviarse el mismo que se usó para cifrar el texto.</param>
        /// <returns>Cadena de caracteres con el texto descifrado</returns>
        /// <exception cref="System.ArgumentException">Lanzada si no se enviaron los paramétros correctamente</exception>
        public static string Decrypt(string CipherText, string SaltValue = DEFAULT_SALT)
        {
            if (string.IsNullOrWhiteSpace(CipherText) || CipherText.Length <= 0)
                throw new ArgumentException("CypherText es un parámetro requerido necesariamente.");

            if (string.IsNullOrWhiteSpace(SaltValue) || SaltValue.Length <= 0)
                throw new ArgumentException("SaltValue es un parámetro requerido, no puede estar en blanco.");

            string decryptedText = string.Empty;

            using (var _aesAlgorithm = NewRijandaelManaged(SaltValue))
            {
                var _decryptor = _aesAlgorithm.CreateDecryptor(_aesAlgorithm.Key, _aesAlgorithm.IV);
                var _cipher = Convert.FromBase64String(CipherText); // /!\ IMPORTANTE /!\ Se espera que la clave cifrada este encodeada en base64

                using (var _objMemDecrypt = new MemoryStream(_cipher))
                {
                    using (var _objCrypto = new CryptoStream(_objMemDecrypt, _decryptor, CryptoStreamMode.Read))
                    {
                        using (var smReader = new StreamReader(_objCrypto))
                        {
                            decryptedText = smReader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedText;
        }

        /// <summary>
        /// Singleton para la clase RijndaelManaged.
        /// </summary>
        /// <param name="SaltValue">Salt (sal) que se usará para generar una clave aleatorio de cifrado/descifrado.</param>
        /// <returns>Instancia de RijnadelManaged</returns>
        /// <exception cref="System.ArgumentException">Lanzada si no se enviaron los paramétros correctamente</exception>
        private static RijndaelManaged NewRijandaelManaged(string SaltValue)
        {
            if (string.IsNullOrWhiteSpace(SaltValue) || SaltValue.Length <= 0)
                throw new ArgumentException("SaltValue es un parámetro requerido, no puede estar en blanco.");

            var _saltBytes = Encoding.ASCII.GetBytes(SaltValue);
            var _key = new Rfc2898DeriveBytes(InputKey, _saltBytes);
            var _aesAlgorithm = new RijndaelManaged();

            _aesAlgorithm.Key = _key.GetBytes(_aesAlgorithm.KeySize / 8);
            _aesAlgorithm.IV = _key.GetBytes(_aesAlgorithm.BlockSize / 8);
            _aesAlgorithm.Padding = PaddingMode.PKCS7;

            return _aesAlgorithm;
        }
    }
}
