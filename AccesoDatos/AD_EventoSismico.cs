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
    public class AD_EventoSismico
    {
        public static List<EventoSismico> BuscarTodosEventosSismicos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<EventoSismico> listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM evento_sismico";

                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                listaResultados = new List<EventoSismico>();

                foreach (DataRow fila in tabla.Rows)
                {
                    EventoSismico nuevoEvento = new EventoSismico();

                    nuevoEvento.Id = Convert.ToInt32(fila["id"]);
                    nuevoEvento.FechaOcurrencia = Convert.ToDateTime(fila["fecha_hora_ocurrencia"]);
                    nuevoEvento.FechaHoraFin = Convert.ToDateTime(fila["fecha_hora_fin"]);
                    nuevoEvento.LatitudEpicentro = Convert.ToSingle(fila["latitud_epicentro"]);
                    nuevoEvento.LongitudEpicentro = Convert.ToSingle(fila["longitud_epicentro"]);
                    nuevoEvento.LatitudHipocentro = Convert.ToSingle(fila["latitud_hipocentro"]);
                    nuevoEvento.LongitudHipocentro = Convert.ToSingle(fila["longitud_hipocentro"]);
                    nuevoEvento.ValorMagnitud = Convert.ToSingle(fila["valor_magnitud"]);

                    int idEvento = Convert.ToInt32(fila["id"]);

                    int idClasificacion = Convert.ToInt32(fila["id_clasificacion_sismo"]);
                    nuevoEvento.Clasificacion = obtenerClasificacionSismo(idClasificacion);

                    int idMagnitud = Convert.ToInt32(fila["id_magnitud_richter"]);
                    nuevoEvento.Magnitud = obtenerMagnitudSismo(idMagnitud);

                    int idOrigen = Convert.ToInt32(fila["id_origen_generacion"]);
                    nuevoEvento.Origen = obtenerOrigenSismo(idOrigen);

                    int idAlcance = Convert.ToInt32(fila["id_alcance_sismo"]);
                    nuevoEvento.Alcance = obtenerAlcanceSismo(idAlcance);

                    nuevoEvento.CambioEstado = obtenerCambiosEstado(idEvento);

                    nuevoEvento.SerieTemporal = obtenerSeriesTemporales(idEvento);

                    listaResultados.Add(nuevoEvento);
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

        private static ClasificacionSismo obtenerClasificacionSismo(int idClasificacion)
        {
            return AD_ClasificacionSismo.agregarClasificacion(idClasificacion);
        }

        private static MagnitudRichter obtenerMagnitudSismo(int idMagnitud)
        {
            return AD_MagnitudSismo.agregarMagnitud(idMagnitud);
        }

        private static OrigenDeGeneracion obtenerOrigenSismo(int idOrigen)
        {
            return AD_OrigenSismo.agregarOrigen(idOrigen);
        }

        private static AlcanceSismo obtenerAlcanceSismo(int idAlcance)
        {
            return AD_AlcanceSismo.agregarAlcance(idAlcance);
        }

        private static List<CambioEstado> obtenerCambiosEstado (int idEvento)
        {
            return AD_CambioEstado.agregarCambiosEstado(idEvento);
        }

        private static List<SerieTemporal> obtenerSeriesTemporales(int idEvento)
        {
            return AD_SerieTemporal.agregarSeries(idEvento);
        }
    }
}
