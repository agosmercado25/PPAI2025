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

namespace PPAI2025.Interfaz
{

    public partial class PantResultadoRevisionManual : Form
    {
        private GestorResultadoRevisionManual gestor;
        private List<EventoSismico> listaEventos;
        private bool datosCargados = false;

        public PantResultadoRevisionManual(GestorResultadoRevisionManual gestor)
        {
            InitializeComponent();
            this.gestor = gestor;
        }
        public PantResultadoRevisionManual()
        {
        }

        public void Principal_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public void habilitarPantalla()
        {
            gestor = new GestorResultadoRevisionManual(this);
            PantResultadoRevisionManual ventana = new PantResultadoRevisionManual(gestor);
            listaEventos = gestor.opResultadoRevisionManual();
            ventana.mostrarDatosEventos(listaEventos);
            ventana.Show();

        }

        public void mostrarDatosEventos(List<EventoSismico> eventos)
        {
            dataGridEventos.DataSource = null;
            
            dataGridEventos.DataSource = eventos;
            
            dataGridEventos.Columns["FechaOcurrencia"].HeaderText = "Fecha de Ocurrencia";
            dataGridEventos.Columns["LatitudEpicentro"].HeaderText = "Latitud Epicentro";
            dataGridEventos.Columns["LatitudHipocentro"].HeaderText = "Latitud Hipocentro";
            dataGridEventos.Columns["LongitudEpicentro"].HeaderText = "Longitud Epicentro";
            dataGridEventos.Columns["LongitudHipocentro"].HeaderText = "Longitud Hipocentro";

            dataGridEventos.Columns["Id"].Visible = false;
            dataGridEventos.Columns["Magnitud"].Visible = false;
            dataGridEventos.Columns["Alcance"].Visible = false;
            dataGridEventos.Columns["Clasificacion"].Visible = false;
            dataGridEventos.Columns["Origen"].Visible = false;

            dataGridEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridEventos.MultiSelect = false;
            dataGridEventos.ClearSelection();

            datosCargados = true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) return;

            int index = e.RowIndex;

            DataGridViewRow filaSeleccionada = dataGridEventos.Rows[index];

            EventoSismico eventoSeleccionado = filaSeleccionada.DataBoundItem as EventoSismico;
            if (eventoSeleccionado != null)
            {
                gestor.tomarSeleccionEventoIngresado(eventoSeleccionado);
            }
            return;
        }
           

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            gestor.tomarSeleccionAccionConfirmar();
        }

        private void btnRevisionExperto_Click(object sender, EventArgs e)
        {
            gestor.buscarEstadoRevisionExperto();
        }

        private void tomarIngresoSeleccionAccion_Click(object sender, EventArgs e)
        {
            gestor.tomarSeleccionAccionIngresada();
        }
    }
}
