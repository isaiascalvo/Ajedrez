using Piezas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Rey : Pieza
    {
        public Rey(int f, int c, int equipo) : base(f, c, equipo) { }
        public override List<Tuple<int, int>> Mover(int fila, int columna)
        {
            if (Math.Abs(Fila - fila) > 1 || Math.Abs(Columna - columna) > 1)
            {
                throw new Exception("No es una movida válida para esta pieza");
            }

            if (Fila == fila && Columna == columna)
            {
                throw new Exception("La pieza ya se encuentra en las coordenadas elegidas.");
            }

            return null;
        }

        public override string Inicial {
            get
            {
                return " K ";
            }
        }
    }
}