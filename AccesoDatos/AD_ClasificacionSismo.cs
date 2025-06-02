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
    public class AD_ClasificacionSismo
    {
        public static ClasificacionSismo agregarClasificacion(int idClasificacion)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            ClasificacionSismo listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM clasificacion_sismo WHERE id = @idClasificacion";

                cmd.Parameters.AddWithValue("@idClasificacion", idClasificacion);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new ClasificacionSismo();

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.KmProfundidadDesde = Convert.ToSingle(fila["km_profundidad_desde"]);
                    listaResultados.KmProfundidadHasta = Convert.ToSingle(fila["km_profundidad_hasta"]);
                    listaResultados.Nombre = fila["nombre"].ToString(); 
                }


            }
            catch(Exception ex)
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
