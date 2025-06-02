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
    public class AD_OrigenSismo
    {
        public static OrigenDeGeneracion agregarOrigen(int idOrigen)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            OrigenDeGeneracion listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM origen_generacion WHERE id = @idOrigen";

                cmd.Parameters.AddWithValue("@idOrigen", idOrigen);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new OrigenDeGeneracion();

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.Descripcion = fila["descripcion"].ToString();
                    listaResultados.Nombre = fila["nombre"].ToString();
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
