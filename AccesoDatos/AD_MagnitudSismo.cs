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
    public class AD_MagnitudSismo
    {
        public static MagnitudRichter agregarMagnitud(int idMagnitud)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            MagnitudRichter listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM magnitud_richter WHERE id = @idMagnitud";

                cmd.Parameters.AddWithValue("@idMagnitud", idMagnitud);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new MagnitudRichter();

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.DescripcionMagnitud = fila["descripcion_magnitud"].ToString();
                    listaResultados.Numero = Convert.ToSingle(fila["numero"]);
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
