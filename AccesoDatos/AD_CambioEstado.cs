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
    public class AD_CambioEstado
    {
        public static List<CambioEstado> agregarCambiosEstado(int idEvento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<CambioEstado> listaResultados = new List<CambioEstado>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM cambio_estado WHERE evento_id = @idEvento";

                cmd.Parameters.AddWithValue("@idEvento", idEvento);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    CambioEstado nuevoEstado = new CambioEstado();

                    nuevoEstado.Id = Convert.ToInt32(fila["id"]);
                    nuevoEstado.FechaHoraFin = fila["fecha_hora_fin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(fila["fecha_hora_fin"]);
                    nuevoEstado.FechaHoraInicio = Convert.ToDateTime(fila["fecha_hora_inicio"]);

                    int idEstado = Convert.ToInt32(fila["estado_id"]);
                    nuevoEstado.EstadoActual = obtenerEstadoSismo(idEstado);

                    nuevoEstado.IdEvento = Convert.ToInt32(fila["evento_id"]);

                    listaResultados.Add(nuevoEstado);
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

        private static Estado obtenerEstadoSismo(int idEstado)
        {
            return AD_Estado.agregarEstado(idEstado);
        }
    }
}

