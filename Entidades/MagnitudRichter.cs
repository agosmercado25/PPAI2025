﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPAI2025.Entidades
{
    public class MagnitudRichter
    {
        private int id;
        private string descripcionMagnitud;
        private float numero;

        public int Id { get => id; set => id = value; }
        public string DescripcionMagnitud { get => descripcionMagnitud; set => descripcionMagnitud = value; }
        public float Numero { get => numero; set => numero = value; }

        public MagnitudRichter(int id, string descripcionMagnitud, int numero)
        {
            this.id = id;
            this.descripcionMagnitud = descripcionMagnitud;
            this.numero = numero;
        }

        public MagnitudRichter()
        {
        }

        public bool esAutodetectado()
        {
            return this.Numero < 4.0;
        }

        public float getNumero()
        {
            return this.Numero;
        }
    }
}
