using Piezas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Peon : Pieza
    {
        public Peon(int f, int c, int equipo) : base(f, c, equipo) { }
        public override List<Tuple<int, int>> Mover(int fila, int columna)
        {
            if (Fila == fila && Columna == columna)
            {
                throw new Exception("La pieza ya se encuentra en las coordenadas elegidas.");
            }
            return null;
        }

        public List<Tuple<int, int>> Mover(int fila, int columna, bool come)
        {
            if (Fila == fila || Equipo == 1 && fila - Fila != 1 || Equipo == 2 && Fila - fila != 1 || Columna != columna && !come)
            {
                throw new Exception("No es una movida válida para esta pieza.");
            }

            if (come && (Math.Abs(Columna - columna) != 1 || (Equipo == 1 && fila - Fila != 1 || Equipo == 2 && Fila - fila != 1)))
            {
                throw new Exception("No es una movida válida para esta pieza.");
            }

            Mover(fila, columna);

            return null;
        }
    }
}
