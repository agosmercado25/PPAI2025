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
    public class AD_DetalleMuestraSismica
    {
        public static List<DetalleMuestraSismica> agregarDetalle(int idMuestra)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<DetalleMuestraSismica> listaResultados = new List<DetalleMuestraSismica>();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM detalle_muestra_sismica WHERE id_muestra = @idMuestra";

                cmd.Parameters.AddWithValue("@idMuestra", idMuestra);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    DetalleMuestraSismica nuevoDetalle = new DetalleMuestraSismica();
                    nuevoDetalle.Id = Convert.ToInt32(fila["id"]);
                    nuevoDetalle.Valor = Convert.ToSingle(fila["valor"]);
                    
                    int idTipo = Convert.ToInt32(fila["id_tipo_de_dato"]);
                    nuevoDetalle.TipoDato = obtenerTipoDato(idTipo);


                    listaResultados.Add(nuevoDetalle);
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

        private static TipoDeDato obtenerTipoDato(int idTipo)
        {
            return AD_TipoDato.agregarTipoDato(idTipo);
        }
    }
}
