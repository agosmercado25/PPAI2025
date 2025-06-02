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
    public class AD_MuestraSismica
    {
        public static List<MuestraSismica> agregarMuestras(int idSerieTemporal)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<MuestraSismica> listaResultados = new List<MuestraSismica>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM muestra_sismica WHERE id_serie_temporal = @idSerieTemporal";

                cmd.Parameters.AddWithValue("@idSerieTemporal", idSerieTemporal);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    MuestraSismica nuevaMuestra = new MuestraSismica();

                    nuevaMuestra.Id = Convert.ToInt32(fila["id"]);
                    nuevaMuestra.FechaHoraMuestra = Convert.ToDateTime(fila["fecha_hora_muestra"]);

                    int idMuestra = Convert.ToInt32(fila["id"]);
                    nuevaMuestra.DetalleMuestraSismica = obtenerDetalleMuestra(idMuestra);

                    listaResultados.Add(nuevaMuestra);
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


        private static List<DetalleMuestraSismica> obtenerDetalleMuestra(int idDetalle)
        {
            return AD_DetalleMuestraSismica.agregarDetalle(idDetalle);
        }
    }
}
