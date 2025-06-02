using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PPAI2025.Entidades;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PPAI2025.AccesoDatos
{
    public class AD_AlcanceSismo
    {
        public static AlcanceSismo agregarAlcance(int idAlcance)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            AlcanceSismo listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM alcance_sismo WHERE id = @idAlcance";

                cmd.Parameters.AddWithValue("@idAlcance", idAlcance);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new AlcanceSismo();

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.Nombre = fila["nombre"].ToString();
                    listaResultados.Descripcion = fila["descripcion"].ToString();
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return listaResultados;

        }
    }
}

