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
    public class AD_TipoDato
    {
        public static TipoDeDato agregarTipoDato(int idTipo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            TipoDeDato listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM tipo_de_dato WHERE id = @idTipo";

                cmd.Parameters.AddWithValue("@idTipo", idTipo);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];
                    listaResultados = new TipoDeDato();

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.NombreUnidadMedida = fila["nombre_unidad_medida"].ToString();
                    listaResultados.Denominacion = fila["denominacion"].ToString();
                    listaResultados.ValorUmbral = Convert.ToSingle(fila["valor_umbral"]);
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
