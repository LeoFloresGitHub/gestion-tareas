
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoLoginLeo.Recursos
{
    public class Utilidad
    {

        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder(); // Para concatenar sin +
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                foreach(byte b in result)
                {
                    sb.Append(b.ToString("x2")); //Para formatearse en valor hexadecimal
                }
            }
            return sb.ToString();
        }
    }
}
