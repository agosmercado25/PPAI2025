using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PPAI2025.Controlador;
using PPAI2025.Entidades;
using PPAI2025.Interfaz;

namespace PPAI2025
{
    public partial class Login1 : Form
    {
        public Login1()
        {
            InitializeComponent();
        }

        private void Login1_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            PantResultadoRevisionManual menuPrincipal = new PantResultadoRevisionManual();
            this.Hide();
            menuPrincipal.regResultRevisionManual();
            

            //if (se != null)
            //{
            //    Principal menuPrincipal = new Principal();
            //    menuPrincipal.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Los valores ingresados son incorrectos");
            //}
        }
    }
}
