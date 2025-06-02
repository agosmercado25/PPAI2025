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
    public class AD_Sismografo
    {
        public static Sismografo agregarSismografo(int idSismografo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Sismografo listaResultados = new Sismografo();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM sismografo WHERE id_sismografo = @idSismografo";

                cmd.Parameters.AddWithValue("@idSismografo", idSismografo);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if(tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];

                    listaResultados.Id = Convert.ToInt32(fila["id_sismografo"]);
                    listaResultados.FechaAdquisicion = Convert.ToDateTime(fila["fecha_adquisicion"]);
                    listaResultados.NroSerie = fila["nro_serie"].ToString();

                    int idEstacion = Convert.ToInt32(fila["id_estacion_sismologica"]);
                    listaResultados.EstacionSismologica = obtenerEstacionSismologica(idEstacion);
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

        private static EstacionSismologica obtenerEstacionSismologica(int idEstacion)
        {
            return AD_EstacionSismologica.agregarEstacion(idEstacion);
        }
    }
}
