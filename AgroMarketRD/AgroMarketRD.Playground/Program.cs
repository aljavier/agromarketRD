using AgroMarketRD.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroMarketRD.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Get Guid
                Console.WriteLine($"Guid >> {Guid.NewGuid().ToString()}");  // Input key para cryptohelper
                Console.WriteLine($"Otro Guid >> {Guid.NewGuid().ToString()}"); // SALT KEY para cryptohelper

                Console.Write("Password en plain text >> ");

                string _input = Console.ReadLine();
                string _cipher = CryptoHelper.Encrypt(_input);

                Console.WriteLine($"Cifrada >> {_cipher}");
                Console.WriteLine($"Descifrada >> {CryptoHelper.Decrypt(_cipher)}");
                #region Test login

                //using (AgroMarketWS.AgroMarketServiceClient ws = new AgroMarketWS.AgroMarketServiceClient())
                //{
                //    Console.WriteLine(">>>> TEST LOGIN <<<<");
                //    Console.Write("User: ");
                //    string userName = Console.ReadLine();
                //    Console.Write("\r\nPasswd: ");
                //    string passwd = Console.ReadLine();

                //    var _response = ws.SignIn(userName.Trim(), CryptoHelper.Encrypt(passwd.Trim()));
                //    StringBuilder sb = new StringBuilder();
                //    sb.AppendLine($"Error ==> Codigo: {_response.Error.Code} : Descripcion: {_response.Error.Description}");
                //    sb.AppendLine($"Token: {_response.Token} : UserName: {_response.UserName}");
                //    Console.WriteLine($"Response: {sb.ToString()}");
                //    Console.WriteLine(">>>>> END <<<<<<\r\n");
                //}
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
