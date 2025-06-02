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
    public class AD_SerieTemporal
    {
        public static List<SerieTemporal> agregarSeries(int idEvento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<SerieTemporal> listaResultados = new List<SerieTemporal>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM serie_temporal WHERE id_evento = @idEvento";

                cmd.Parameters.AddWithValue("@idEvento", idEvento);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    SerieTemporal nuevaSerie = new SerieTemporal();

                    nuevaSerie.Id = Convert.ToInt32(fila["id"]);
                    nuevaSerie.CondicionAlarma = fila["condicion_alarma"].ToString();
                    nuevaSerie.FechaHoraRegistro = Convert.ToDateTime(fila["fecha_hora_registro"]);
                    nuevaSerie.FechaHoraInicioRegistroMuestras = Convert.ToDateTime(fila["fecha_hora_inicio_muestras"]);
                    nuevaSerie.FrecuenciaMuestreo = fila["frecuencia_muestras"].ToString();

                    int idSerieTemporal = Convert.ToInt32(fila["id"]);
                    int idSismografo = Convert.ToInt32(fila["id_sismografo"]);

                    nuevaSerie.Muestras = obtenerMuestrasSismicas(idSerieTemporal);
                    nuevaSerie.Sismografo = obtenerSismografo(idSismografo);

                    listaResultados.Add(nuevaSerie);
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

        internal static List<SerieTemporal> BuscarTodasLasSeriesTemporales()
        {
            throw new NotImplementedException();
        }

        private static List<MuestraSismica> obtenerMuestrasSismicas(int idSerieTemporal)
        {
            return AD_MuestraSismica.agregarMuestras (idSerieTemporal);
        }

        private static Sismografo obtenerSismografo(int idSismografo)
        {
            return AD_Sismografo.agregarSismografo (idSismografo);
        }
    }
}
