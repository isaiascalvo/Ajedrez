using Piezas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Alfil : Pieza
    {
        public Alfil(int f, int c, int equipo) : base(f, c, equipo) { }

        public override List<Tuple<int, int>> Mover(int fila, int columna)
        {
            if (Math.Abs(Fila - fila) != Math.Abs(Columna - columna))
            {
                throw new Exception("No es una movida válida para esta pieza");
            }

            if (Fila == fila && Columna == columna)
            {
                throw new Exception("La pieza ya se encuentra en las coordenadas elegidas.");
            }

            List<Tuple<int, int>> movimientos = new List<Tuple<int, int>>();

            if (fila > Fila)
            {
                if (columna > Columna)
                {
                    for (int i = 1; i < fila - Fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(Fila + i, Columna + i));
                    }
                }
                else
                {
                    for (int i = 1; i < fila - Fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(Fila + i, Columna - i));
                    }
                }

            }
            else
            {
                if (columna > Columna)
                {
                    for (int i = 1; i < Fila - fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(Fila - i, Columna + i));
                    }
                }
                else
                {
                    for (int i = 1; i < Fila - fila; i++)
                    {
                        movimientos.Add(new Tuple<int, int>(Fila - i, Columna - i));
                    }
                }
            }                                    

            return movimientos;
        }
    }
}
