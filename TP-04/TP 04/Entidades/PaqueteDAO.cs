using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Entidades
{
    public static class PaqueteDAO
    {
        private static SqlCommand _comando;
        private static SqlConnection _conexion;

        #region Constructor
        static PaqueteDAO()
        {
            _comando = new SqlCommand();
            _conexion = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=correo-sp-2017;Integrated Security=True");
            PaqueteDAO._comando.CommandType = System.Data.CommandType.Text;
            PaqueteDAO._comando.Connection = PaqueteDAO._conexion;
        }
        #endregion

        #region Métodos
        public static bool Insertar(Paquete p)
        {
            string sql = "INSERT INTO Paquetes (direccionEntrega,trackingID,alumno) VALUES(";
            sql = sql + "'" + p.DireccionEntrega + "','" + p.TrackingID + "','" + "Juliet Gutierrez'" + ")";
            return EjecutarNonQuery(sql);
        }

        private static bool EjecutarNonQuery(string sql)
        {
            bool retorno = false;
            try
            {
                PaqueteDAO._comando.CommandText = sql;
                PaqueteDAO._conexion.Open();
                PaqueteDAO._comando.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception e)
            {
                retorno = false;
            }
            finally
            {
                if (retorno)
                {
                    PaqueteDAO._conexion.Close();
                }
            }
            return retorno;
        }
        #endregion
    }
}
