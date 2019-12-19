using Entidades;
using Piezas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ajedrez
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pieza> tablero = new List<Pieza>();            

            IniciarTablero();

            do
            {
                Console.WriteLine("Ajedrez!\n");
                DibujarTablero();
                string coordenada;            
                Console.WriteLine("Ingrese una coordenada o 0 para salir:");
                coordenada = Console.ReadLine();
                if (coordenada == "0")
                {
                    Console.Clear();
                    Console.WriteLine("Gracias por jugar Ajedrez!!! Saludos.");
                    return;
                }
                Tuple<int, int> xy = CoordenadaValida(coordenada);
                Pieza p1 = xy != null ? tablero.FirstOrDefault(x => x.Fila == xy.Item1 && x.Columna == xy.Item2) : null;
                while (xy == null || p1 == null)
                {
                    if(xy == null)
                        Console.WriteLine("La coordenada ingresada anteriormente es inválida.\nIngrese una nueva coordenada:");
                    else
                        Console.WriteLine("La coordenada ingresada anteriormente no contiene una pieza.\nIngrese una nueva coordenada:");
                    coordenada = Console.ReadLine();
                    xy = CoordenadaValida(coordenada);
                    p1 = xy != null ? tablero.FirstOrDefault(x => x.Fila == xy.Item1 && x.Columna == xy.Item2) : null;
                }
                                                 
                Console.WriteLine("Ingrese una coordenada de destino:");

                coordenada = Console.ReadLine();
                Tuple<int, int> wz = CoordenadaValida(coordenada);
                Pieza p2 = wz != null ? tablero.FirstOrDefault(x => x.Fila == wz.Item1 && x.Columna == wz.Item2) : null;

                if (wz != null && (p2 == null || p2.Equipo != p1.Equipo))
                {
                    try
                    {
                        List<Tuple<int, int>> camino = new List<Tuple<int, int>>();

                        if (p1.GetType().Name == "Peon")
                        {
                            camino = ((Peon)p1).Mover(wz.Item1, wz.Item2, p2 != null);
                        }
                        else
                            camino = p1.Mover(wz.Item1, wz.Item2);

                        if (camino != null)
                        {
                            foreach (var mov in camino)
                            {
                                if (tablero.Any(x => x.Fila == mov.Item1 && x.Columna == mov.Item2))
                                {
                                    throw new Exception("Otra pieza en camino.");
                                }
                            }
                        }

                        p1.Fila = wz.Item1;
                        p1.Columna = wz.Item2;
                        if (p2 != null)
                            tablero.Remove(p2);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        wz = null;
                        p2 = null;
                    }                   
                }  
                else
                {
                    if (wz == null)
                        Console.WriteLine("La coordenada ingresada anteriormente es inválida.");
                    else
                        Console.WriteLine("No se pueden comer piezas del mismo color.");
                }

                Console.WriteLine("Presione cualquier tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            } while (tablero.Count() > 0);


            void DibujarTablero()
            {
                bool band = true;
                string caracter = "   ";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write("   ");
                for (char t = 'a'; t <= 'h'; t++)
                {
                    Console.Write(" " + t + " ");
                }
                Console.WriteLine("   ");
                for (int f = 7; f >= 0; f--)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" " + (f + 1) + " ");
                    for (int c = 0; c <= 7; c++)
                    {
                        caracter = "   ";
                        if (band)            
                            Console.BackgroundColor = ConsoleColor.White;
                        else
                            Console.BackgroundColor = ConsoleColor.Black;

                        Pieza pieza = tablero.Where(x => x.Fila == f && x.Columna == c).FirstOrDefault();

                        if (pieza != null)
                        {
                            caracter = pieza.Inicial;                           
                            Console.ForegroundColor = pieza.Color;                            
                        }                           

                        if (c != 7) band = !band;                          
                                                                     
                        Console.Write(caracter);
                    }

                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(" " + (f + 1) + " ");
                }

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Write("   ");
                for (char t = 'a'; t <= 'h'; t++)
                {
                    Console.Write(" " + t + " ");
                }
                Console.WriteLine("   ");

                Console.ResetColor();               
            }

            void IniciarTablero()
            {
                List<int> listaInicial;

                do
                {
                    listaInicial = new List<int>();
                    for (int i = 0; i < 64; i++)
                    {
                        listaInicial.Add(new Random().Next(1000000000));
                    }
                }
                while (listaInicial.Distinct().Count() != 64);

                List<int> listaOrdenada = listaInicial.OrderBy(p => p).ToList();

                int n = listaOrdenada.IndexOf(listaInicial.First());

                listaInicial.RemoveAt(0);

                int f = n / 8;
                int c = n % 8;

                tablero.Add(new Alfil(f, c, 1));

                int pos = 0;
                int x;
                int y;

                do
                {
                    n = listaOrdenada.IndexOf(listaInicial.ElementAt(pos));
                    x = n / 8;
                    y = n % 8;
                    pos++;
                }
                while (EsBlanco(f, c) == EsBlanco(x, y));

                tablero.Add(new Alfil(x, y, 1));

                listaInicial.RemoveAt(pos - 1);

                n = listaOrdenada.IndexOf(listaInicial.First());

                listaInicial.RemoveAt(0);

                f = n / 8;
                c = n % 8;

                tablero.Add(new Alfil(f, c, 2));

                pos = 0;

                do
                {
                    n = listaOrdenada.IndexOf(listaInicial.ElementAt(pos));
                    x = n / 8;
                    y = n % 8;
                    pos++;
                }
                while (EsBlanco(f, c) == EsBlanco(x, y));

                tablero.Add(new Alfil(x, y, 2));

                listaInicial.RemoveAt(pos - 1);


                for (int i = 1; i <= 2; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        n = listaOrdenada.IndexOf(listaInicial.First());

                        listaInicial.RemoveAt(0);

                        f = n / 8;
                        c = n % 8;

                        tablero.Add(new Peon(f, c, i));
                    }
                
                    for (int j = 0; j < 2; j++)
                    {
                        n = listaOrdenada.IndexOf(listaInicial.First());

                        listaInicial.RemoveAt(0);

                        f = n / 8;
                        c = n % 8;

                        tablero.Add(new Torre(f, c, i));
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        n = listaOrdenada.IndexOf(listaInicial.First());

                        listaInicial.RemoveAt(0);

                        f = n / 8;
                        c = n % 8;

                        tablero.Add(new Caballo(f, c, i));
                    }
                   
                    n = listaOrdenada.IndexOf(listaInicial.First());

                    listaInicial.RemoveAt(0);

                    f = n / 8;
                    c = n % 8;

                    tablero.Add(new Rey(f, c, i));

                    n = listaOrdenada.IndexOf(listaInicial.First());

                    listaInicial.RemoveAt(0);

                    f = n / 8;
                    c = n % 8;

                    tablero.Add(new Reina(f, c, i));
                }

            }

            Tuple<int, int> CoordenadaValida(string cd)
            {
                if (cd.Length != 2)
                {
                    return null;
                }

                var nro = cd.Substring(0,1);
                var letra = cd.Substring(1, 1);

                if (cd.Length > 2)
                {
                    return null;
                }

                if (!char.TryParse(letra.ToLower(), out char l) || l > char.Parse("h") || l < char.Parse("a") ||
                    !int.TryParse(nro, out int numero) || numero > 8 || numero < 1)
                    return null;
                else
                    return new Tuple<int, int> (numero - 1, Orden(l));                                
            }
        }

        private static int Orden(char l)
        {
            switch (l)
            {
                case 'a':
                    return 0;
                case 'b':
                    return 1;
                case 'c':
                    return 2;
                case 'd':
                    return 3;
                case 'e':
                    return 4;
                case 'f':
                    return 5;
                case 'g':
                    return 6;
                default:
                    return 7;
            }
        }

        public static bool EsBlanco(int f, int c)
        {
            return f % 2 == 0 ? c % 2 == 0 : c % 2 != 0;
        }        
    }
}