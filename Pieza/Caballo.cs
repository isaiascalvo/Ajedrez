using Piezas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Caballo : Pieza
    {
        public Caballo(int f, int c, int equipo) : base(f, c, equipo) { }
        public override List<Tuple<int, int>> Mover(int fila, int columna)
        {
            if (Fila == fila && Columna == columna)
            {
                throw new Exception("La pieza ya se encuentra en las coordenadas elegidas.");
            }

            if (!(Math.Abs(Fila - fila) == 2 && Math.Abs(Columna - columna) == 1) && !(Math.Abs(Fila - fila) == 1 && Math.Abs(Columna - columna) == 2))
            {
                throw new Exception("No es una movida válida para esta pieza");
            }

            return null;
        }
    }
}
