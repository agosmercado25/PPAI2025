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
    public class AD_Estado
    {
        public static List<Estado> BuscarEstados()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            List<Estado> listaResultados = null;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estado";

                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                listaResultados = new List<Estado>();

                foreach (DataRow fila in tabla.Rows)
                {
                    Estado nuevoEstado = new Estado();

                    nuevoEstado.Id = Convert.ToInt32(fila["id"]); 
                    nuevoEstado.Nombre = fila["nombre"].ToString(); 
                    nuevoEstado.Ambito = fila["ambito"].ToString();

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

        public static Estado agregarEstado(int idEstado)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Estado listaResultados = new Estado();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM estado WHERE id = @idEstado";

                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);

                if (tabla.Rows.Count > 0)
                {
                    DataRow fila = tabla.Rows[0];

                    listaResultados.Id = Convert.ToInt32(fila["id"]);
                    listaResultados.Nombre = fila["nombre"].ToString();
                    listaResultados.Ambito = fila["ambito"].ToString();
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
