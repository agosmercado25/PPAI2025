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
using PPAI2025.dtos;

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
            
            btnSismograma.Enabled = true;
            return;
        }

        public void solicitarOpcionVisualizarMapa()
        {
            btnVisualizarMapa.Enabled = true;
        }

        public void tomarIngresoOpcVisualizarMapa()
        {
            gestor.tomarOpcionVisualizarMapaIngresada();
        }

        public void solicitarModificacionDatos()
        {
            //DialogResult dialogResult = MessageBox.Show("Desea modificar datos?", "Modificar datos", MessageBoxButtons.YesNo);
            tomarIngresoModificacionDatos();
        }

        public void tomarIngresoModificacionDatos()
        {
            gestor.tomarModificacionDatosIngresada();
        }

        public void solicitarSeleccionAccion()
        {
            MessageBox.Show("Seleccione una opcion a realizar para el evento");
            
            btnConfirmar.Enabled = true;
            btnRechazar.Enabled = true;
            btnRevisionExperto.Enabled = true;
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

        private void solicitarOpcionVisualizarMapa_Click(object sender, EventArgs e)
        {
            tomarIngresoOpcVisualizarMapa();
        }


        public void CargarDatosEnTreeView(List<GrupoEstacionDTO> gruposPorEstacion, string alcance, string clasificacion, string origen)
        {
            treeAgrupados.Enabled = true;
            // Limpiar el TreeView antes de agregar los datos
            treeAgrupados.Nodes.Clear();

            var datosSismoNode = new System.Windows.Forms.TreeNode($"Datos Sismo: \n "+$"Alcance: {alcance} \n "+$"Clasificacion: {clasificacion} \n "+$"Origen: {origen} " );
            treeAgrupados.Nodes.Add(datosSismoNode);
            foreach (var grupoEstacion in gruposPorEstacion)
            {
                // Crear un nodo para la estación sísmica
                var estacionNode = new System.Windows.Forms.TreeNode(grupoEstacion.NombreEstacion);


                foreach (var serie in grupoEstacion.SeriesDeEstaEstacion)
                {
                    // Crear un nodo para la serie
                    var serieNode = new System.Windows.Forms.TreeNode($"Serie: {serie.CondicionAlarma} - {serie.FechaHoraRegistro.ToShortDateString()} - {serie.FrecuenciaMuestreo} - {serie.FechaHoraInicioRegistroMuestras.ToShortDateString()}");


                    foreach (var muestra in serie.Muestras)
                    {
                        // Crear un nodo para la muestra
                        var muestraNode = new System.Windows.Forms.TreeNode($"Muestra: {muestra.FechaHoraMuestra.ToShortTimeString()}");

                        foreach (var detalle in muestra.DetalleMuestraSismica)
                        {
                            // Crear un nodo para el detalle
                            var detalleNode = new System.Windows.Forms.TreeNode($"Detalle: Valor = {detalle.Valor} - Tipo de Dato: {detalle.TipoDato}");
                            muestraNode.Nodes.Add(detalleNode);
                        }

                        // Agregar el nodo de la muestra a la serie
                        serieNode.Nodes.Add(muestraNode);
                    }

                    // Agregar el nodo de la serie a la estación
                    estacionNode.Nodes.Add(serieNode);
                }

                // Agregar el nodo de la estación al TreeView
                treeAgrupados.Nodes.Add(estacionNode);
            }
           

            // Expande todos los nodos para mostrar la jerarquía completa
            treeAgrupados.ExpandAll();
        }


    }




}
