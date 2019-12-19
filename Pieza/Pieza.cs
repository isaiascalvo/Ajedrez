using System;
using System.Collections.Generic;

namespace Piezas
{
    public abstract class Pieza
    {
        public int Equipo { get; set; }
        public System.ConsoleColor Color {
            get
            {
                return Equipo == 1 ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
            }
        }
        public int Fila { get; set; }

        public int Columna { get; set; }

        public abstract List<Tuple<int, int>> Mover(int fila, int columna);

        public virtual string Inicial
        {
            get
            {
                return " " + this.GetType().Name.Substring(0, 1) + " ";
            }
        }

        public Pieza(int f, int c, int equipo)
        {
            Fila = f;
            Columna = c;
            Equipo = equipo;
        }
    }
}
