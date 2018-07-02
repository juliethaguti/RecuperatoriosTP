using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public static class GuardaString
    {
        public static bool Guardar(this string texto, string archivo)
        {
            bool retorno = false;
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = folder + Path.DirectorySeparatorChar + archivo;
            //FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            StreamWriter sw = null;
 
            try
            {
                sw = new StreamWriter(filePath, true);
                sw.Write(texto);

                retorno = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sw.Close();
            }
            
            return retorno;
        }
    }
}
