using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class DetalleMuestraSismica
    {
        private int id;
        private float valor;
        private TipoDeDato tipoDato;
        

        public int Id { get => id; set => id = value; }
        public float Valor { get => valor; set => valor = value; }
        public TipoDeDato TipoDato { get => tipoDato; set => tipoDato = value; }

        public DetalleMuestraSismica(int id, float valor, TipoDeDato tipoDato)
        {
            this.id = id;
            this.valor = valor;
            this.tipoDato = tipoDato;
        }

        public DetalleMuestraSismica()
        {
        }

        public object getDatos()
        {
            return new
            {
                id = this.Id,
                valor = this.Valor,
                tipoDato = this.TipoDato?.getDenominacion(),
                unidadMedida = this.TipoDato?.getUnidadMedida()
            };
        }
    }
}
