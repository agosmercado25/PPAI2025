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
    public class AD_EstacionSismologica
    {
        public static EstacionSismologica agregarEstacion(int idEstacion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            EstacionSismologica listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estacion_sismologica WHERE id_codigo_estacion = @idEstacion";

                cmd.Parameters.AddWithValue("@idEstacion", idEstacion);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new EstacionSismologica();

                    listaResultados.IdCodigoEstacion = Convert.ToInt32(fila["id_codigo_estacion"]);
                    listaResultados.DocumentoCertificacionAdq = fila["documento_certificado"].ToString();
                    listaResultados.FechaSolicitudCertificacion = Convert.ToDateTime(fila["fecha_solicitud_certificado"]);
                    listaResultados.Longitud = Convert.ToSingle(fila["longitud"]);
                    listaResultados.Latitud = Convert.ToSingle(fila["latitud"]);
                    listaResultados.Nombre = fila["nombre"].ToString();
                    listaResultados.NumeroCertificadoAdquisicion = Convert.ToInt32(fila["numero_certificado_adquisicion"]);
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
