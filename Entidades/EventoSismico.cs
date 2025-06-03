using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PPAI2025.dtos;


namespace PPAI2025.Entidades
{
    public class EventoSismico
    {
        private int id;
        private DateTime fechaHoraOcurrencia;
        private DateTime fechaHoraFin;
        private float latitudEpicentro;
        private float longitudEpicentro;
        private float latitudHipocentro;
        private float longitudHipocentro;
        private float valorMagnitud;
        private List<CambioEstado> cambioEstado;
        private List<SerieTemporal> serieTemporal;
        private ClasificacionSismo clasificacion;
        private MagnitudRichter magnitudRichter;
        private OrigenDeGeneracion origen;
        private AlcanceSismo alcance;

        public int Id { get => id; set => id = value; }
        public DateTime FechaOcurrencia { get => fechaHoraOcurrencia; set => fechaHoraOcurrencia = value; }
        public DateTime FechaHoraFin { get => fechaHoraFin; set => fechaHoraFin = value; }
        public float LatitudEpicentro { get => latitudEpicentro; set => latitudEpicentro = value; }
        public float LongitudEpicentro { get => longitudEpicentro; set => longitudEpicentro = value; }
        public float LatitudHipocentro { get => latitudHipocentro; set => latitudHipocentro = value; }
        public float LongitudHipocentro { get => longitudHipocentro; set => longitudHipocentro = value; }
        public float ValorMagnitud { get => valorMagnitud; set => valorMagnitud = value; }
        public List<CambioEstado> CambioEstado { get => cambioEstado; set => cambioEstado = value; }
        public List<SerieTemporal> SerieTemporal { get => serieTemporal; set => serieTemporal = value; }
        public ClasificacionSismo Clasificacion { get => clasificacion; set => clasificacion = value; }
        public MagnitudRichter Magnitud { get => magnitudRichter; set => magnitudRichter = value; }
        public OrigenDeGeneracion Origen { get => origen; set => origen = value; }
        public AlcanceSismo Alcance { get => alcance; set => alcance = value; }

        public EventoSismico(int id, DateTime fechaHoraOcurrencia, DateTime fechaHoraFin, float latitudEpicentro, float longitudEpicentro, float latitudHipocentro, float longitudHipocentro, float valorMagnitud,
            List<CambioEstado> cambioEstado, List<SerieTemporal> serieTemporal, ClasificacionSismo clasificacion, MagnitudRichter magnitudRichter, OrigenDeGeneracion origen, AlcanceSismo alcance)
        {
            this.id = id;
            this.fechaHoraOcurrencia = fechaHoraOcurrencia;
            this.fechaHoraFin = fechaHoraFin;
            this.latitudEpicentro = latitudEpicentro;
            this.longitudEpicentro = longitudEpicentro;
            this.latitudHipocentro = latitudHipocentro;
            this.longitudHipocentro = longitudHipocentro;
            this.valorMagnitud = valorMagnitud;
            this.cambioEstado = cambioEstado;
            this.serieTemporal = serieTemporal;
            this.clasificacion = clasificacion;
            this.magnitudRichter = magnitudRichter;
            this.origen = origen;
            this.alcance = alcance;
        }

        public EventoSismico()
        {
        }

        public CambioEstado esEventoNoRevisado()
        {
            CambioEstado cambioEstadoActual = this.CambioEstado
                                            .FirstOrDefault(ce => ce.esEstadoActual());

            if (cambioEstadoActual == null)
            {
                return null;
            }
            else
            {
                if (cambioEstadoActual.esNoRevisado() && this.Magnitud.esAutodetectado())
                {
                    return cambioEstadoActual;
                }
                return null;
            }
        }

        public EventoSismico getDatos()
        {
            this.Magnitud.getNumero();
            return this;
        }


        public void actualizarUltimoEstado(List<CambioEstado> listUltimos, DateTime fechaHoraActual, Estado estado, Usuario usuarioActual)
        {

            //MessageBox.Show("Cantidad cambios estado: " + this.CambioEstado.Count.ToString());

            CambioEstado cambioEstadoDelEvento = listUltimos
                    .FirstOrDefault(ce => ce.IdEvento == this.Id);

            if (cambioEstadoDelEvento != null)
            {
                cambioEstadoDelEvento.setFechaHoraFin(fechaHoraActual);
            }
            crearNuevoCambioEstado(estado,usuarioActual);
        }

        private void crearNuevoCambioEstado(Estado est, Usuario usuario)
        {
            CambioEstado nuevoCambio = new CambioEstado
            {
                Id = 100,
                FechaHoraFin = null,
                FechaHoraInicio = DateTime.Now,
                EstadoActual = est,
                IdEvento = this.Id,
                Usuario = usuario
            };

            MessageBox.Show("Nuevo cambio estado: " + nuevoCambio.EstadoActual.Nombre.ToString());
            this.CambioEstado.Add(nuevoCambio);
        }

        public (string, string, string) buscarDatosSismo()
        {

            var nombreAlcance = this.Alcance.getNombre();
            var nombreClasificacion = this.Clasificacion.getNombre();
            var nombreOrigen = this.Origen.getNombre();

            return (nombreAlcance, nombreClasificacion, nombreOrigen);
        }
        
        public object buscarSeriesTemporales()
        {
            var gruposPorEstacion = this.SerieTemporal
            .Where(s => s.buscarEstacionSismologica() != null)
            .GroupBy(s => s.buscarEstacionSismologica().Nombre)
            .Select(g => new
            {
                nombreEstacion = g.Key,
                seriesDeEstaEstacion = g.Select(s => s.getDatos()).ToList()
            })
            .ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonOutput = JsonSerializer.Serialize(gruposPorEstacion, options);

            
            return gruposPorEstacion;
        }
    }
    
}
