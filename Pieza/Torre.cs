using Piezas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Torre : Pieza
    {
        public Torre(int f, int c, int equipo) : base(f, c, equipo) { }
        public override List<Tuple<int,int>> Mover(int fila, int columna)
        {            
            if (Fila == fila && Columna == columna)
            {
                throw new Exception("La pieza ya se encuentra en las coordenadas elegidas.");
            }

            if (Fila != fila && Columna != columna)
            {
                throw new Exception("No es una movida válida para esta pieza");
            }

            List<Tuple<int, int>> movimientos = new List<Tuple<int, int>>();

            if (fila != Fila)
            {
                if (fila > Fila)
                {
                    for (int i = Fila + 1; i < fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(i,columna));
                    }

                }
                else
                {
                    for (int i = fila + 1; i < Fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(i, columna));
                    }
                }
            }

            if (columna != Columna)
            {
                if (columna > Columna)
                {
                    for (int i = Columna + 1; i < columna; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(fila, i));
                    }

                }
                else
                {
                    for (int i = columna + 1; i < Columna; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(fila, i));
                    }
                }
            }

            return movimientos;
        }
    }
}
