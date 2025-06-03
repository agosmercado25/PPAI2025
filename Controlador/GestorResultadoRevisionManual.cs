using PPAI2025.AccesoDatos;
using PPAI2025.Entidades;
using PPAI2025.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPAI2025.Controlador
{
    public class GestorResultadoRevisionManual
    {
        private PantResultadoRevisionManual pantalla;
        private List<EventoSismico> listES;
        private List<EventoSismico> listESAD;
        private EventoSismico esSelec;
        private Estado esBloqRevi;
        private Estado esRechazado;
        private Estado esConfirmado;
        private Estado esRevisionExperto;
        private DateTime fechaHoraActual;
        private dynamic seriesTemporales;
        private Usuario sesionActual;
        private List<Estado> estados;
        private Estado estadoEnRevision;
        private List<CambioEstado> listCambiosEstados;

        public GestorResultadoRevisionManual(PantResultadoRevisionManual pantalla) 
        {
            this.pantalla = pantalla;
        }

        public List<EventoSismico> opResultadoRevisionManual()
        {
            cargarAtributos();
            (listESAD, listCambiosEstados) = buscaEventosAutodetectados();
            listESAD = ordenarPorFechaYHoraOcurrencia();
            return listESAD;
        }

        private void cargarAtributos()
        {
            this.estados = AD_Estado.BuscarEstados();
            this.listES = AD_EventoSismico.BuscarTodosEventosSismicos();
        }

        private (List<EventoSismico>, List<CambioEstado> ) buscaEventosAutodetectados()
        {
            List<EventoSismico> eventosAutodetectadosNoRevisados = new List<EventoSismico>();
            List<CambioEstado> ultimosCambiosEstados = new List<CambioEstado>();
            foreach (EventoSismico evento in listES)
            {

                if (evento.esEventoNoRevisado() != null)
                {
                    ultimosCambiosEstados.Add(evento.esEventoNoRevisado());
                    eventosAutodetectadosNoRevisados.Add(evento);
                }
            }

            List<EventoSismico> eventosAutodetectados = new List<EventoSismico>();
            foreach(EventoSismico evento in eventosAutodetectadosNoRevisados)
            {
                eventosAutodetectados.Add(evento.getDatos());
            }

            //MessageBox.Show("Cantidad de eventos Autodetectados no revisados " + eventosAutodetectadosNoRevisados.Count.ToString());
            
            return (eventosAutodetectados, ultimosCambiosEstados);
        }

        public List<EventoSismico> ordenarPorFechaYHoraOcurrencia()
        {
            var eventosOrdenados = listESAD.OrderBy(e => e.FechaOcurrencia).ToList();

            return eventosOrdenados;
        }

        public void tomarSeleccionEventoIngresado(EventoSismico eventoSeleccionado)
        {
            esSelec = eventoSeleccionado;
            esBloqRevi = buscarEstadoBloqueadoEnRevision();
            fechaHoraActual = getFechaHoraActual();
            this.sesionActual = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec,listCambiosEstados,fechaHoraActual, esBloqRevi, this.sesionActual);
            buscarDatosSismicosEventoSeleccionado(esSelec);
            habilitarOpcionVisualizarMapa();
            permitirModificacionDatos();
            pantalla.solicitarOpcionVisualizarMapa();
        }

        public void habilitarOpcionVisualizarMapa()
        {
            pantalla.solicitarOpcionVisualizarMapa();
        }

        public void tomarOpcionVisualizarMapaIngresada()
        {
            MessageBox.Show("No implementada");
        }

        public void permitirModificacionDatos()
        {
            pantalla.solicitarModificacionDatos();
        }
        public void tomarModificacionDatosIngresada()
        {
            solicitarSeleccionAccion();
        }
        public void solicitarSeleccionAccion()
        {
            pantalla.solicitarSeleccionAccion();
        }


        public void tomarSeleccionAccionIngresada()
        {

            //FALTA EL VALIDAR DATOS
            fechaHoraActual = getFechaHoraActual();
            esRechazado = buscarEstadoRechazado();
            
            //usuario = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esRechazado, this.sesionActual);
            //FIN CU
            finCU();

        }
        public void tomarSeleccionAccionConfirmar()
        {

            
            fechaHoraActual = getFechaHoraActual();
            esConfirmado = buscarEstadoConfirmado();

            //usuario = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esConfirmado, this.sesionActual);
            finCU();
            

        }
        public void tomarSeleccionAccionRevisionExperto()
        {
            fechaHoraActual = getFechaHoraActual();
            esRevisionExperto = buscarEstadoRevisionExperto();

            //usuario = buscarUsuarioLogueado();
            actualizarUltimoEstado(esSelec, listCambiosEstados, fechaHoraActual, esRevisionExperto, this.sesionActual);
            finCU();

        }


        public Estado buscarEstadoBloqueadoEnRevision()
        {
            foreach(Estado e in estados)
            {
                if(e.esAmbitoEventoSismico() && e.esBloqueadoEnRevision())
                {
                    return e;
                }
            }

            return null;
        }

        public DateTime getFechaHoraActual()
        {
            return DateTime.Now;
        }
        
        public Usuario buscarUsuarioLogueado()
        {
            Usuario usuarioActual = AppSession.SesionActual.conocerUsuario();
            return usuarioActual;
        }
        
        public void actualizarUltimoEstado(EventoSismico eventoSeleccionado,List<CambioEstado> listUltimos, DateTime fechaHoraActual, Estado estadoAsignar, Usuario usuarioActual)
        {
            // MessageBox.Show("Usuario " + usuarioActual.ToString());
            foreach (CambioEstado e in listUltimos)
            {
                if(e.IdEvento == eventoSeleccionado.Id)
                {
                    eventoSeleccionado.actualizarUltimoEstado(listUltimos, fechaHoraActual, estadoAsignar, usuarioActual);
                }
                
            }

        }

        private void buscarDatosSismicosEventoSeleccionado(EventoSismico eventoSeleccionado)
        {
            (string alcance, string clasificacion, string origen) = eventoSeleccionado.buscarDatosSismo();
            //this.seriesTemporales = eventoSeleccionado.buscarSeriesTemporales();
            this.seriesTemporales = eventoSeleccionado.buscarYClasificarSeriesTemporales();
            this.pantalla.CargarDatosEnTreeView(this.seriesTemporales, alcance, clasificacion, origen);
            llamarCUGenerarSismograma();
            habilitarOpcionVisualizarMapa();
        }

        public Estado buscarEstadoRechazado()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esRechazado())
                {
                    return e;
                }
            }

            return null;
        }
        public Estado buscarEstadoRevisionExperto()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esRevisionExperto())
                {
                    return e;
                }
            }

            return null;
        }
        public Estado buscarEstadoConfirmado()
        {
            foreach (Estado e in estados)
            {
                if (e.esAmbitoEventoSismico() && e.esConfirmado())
                {
                    return e;
                }
            }

            return null;
        }

        public void llamarCUGenerarSismograma()
        {

        }
        public void finCU()
        {
            MessageBox.Show("FIN CU");
            Application.Exit();
        }
    }
}
