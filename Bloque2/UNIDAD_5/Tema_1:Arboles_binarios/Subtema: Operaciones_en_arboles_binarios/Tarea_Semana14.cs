using System;

namespace ArbolBinarioDemo
{
    class Program
    {
        // Nodo del árbol binario con dato tipo string
        class Nodo
        {
            public string Valor;
            public Nodo Izquierda;
            public Nodo Derecha;

            public Nodo(string valor)
            {
                Valor = valor;
                Izquierda = null;
                Derecha = null;
            }
        }

        // Árbol binario de búsqueda para strings
        class ArbolBinario
        {
            public Nodo Raiz;

            public void Insertar(string valor)
            {
                Raiz = InsertarRecursivo(Raiz, valor);
            }

            private Nodo InsertarRecursivo(Nodo nodo, string valor)
            {
                if (nodo == null) return new Nodo(valor);
                if (string.Compare(valor, nodo.Valor) < 0)
                    nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, valor);
                else if (string.Compare(valor, nodo.Valor) > 0)
                    nodo.Derecha = InsertarRecursivo(nodo.Derecha, valor);
                return nodo;
            }

            public void RecorridoInOrden()
            {
                Console.WriteLine("\nRecorrido In-Orden:");
                InOrden(Raiz);
                Console.WriteLine();
            }

            private void InOrden(Nodo nodo)
            {
                if (nodo != null)
                {
                    InOrden(nodo.Izquierda);
                    Console.Write(nodo.Valor + " ");
                    InOrden(nodo.Derecha);
                }
            }

            public void RecorridoPreOrden()
            {
                Console.WriteLine("\nRecorrido Pre-Orden:");
                PreOrden(Raiz);
                Console.WriteLine();
            }

            private void PreOrden(Nodo nodo)
            {
                if (nodo != null)
                {
                    Console.Write(nodo.Valor + " ");
                    PreOrden(nodo.Izquierda);
                    PreOrden(nodo.Derecha);
                }
            }

            public void RecorridoPostOrden()
            {
                Console.WriteLine("\nRecorrido Post-Orden:");
                PostOrden(Raiz);
                Console.WriteLine();
            }

            private void PostOrden(Nodo nodo)
            {
                if (nodo != null)
                {
                    PostOrden(nodo.Izquierda);
                    PostOrden(nodo.Derecha);
                    Console.Write(nodo.Valor + " ");
                }
            }

            public Nodo Buscar(string valor)
            {
                return BuscarRec(Raiz, valor);
            }

            private Nodo BuscarRec(Nodo nodo, string valor)
            {
                if (nodo == null || nodo.Valor == valor)
                    return nodo;
                if (string.Compare(valor, nodo.Valor) < 0)
                    return BuscarRec(nodo.Izquierda, valor);
                else
                    return BuscarRec(nodo.Derecha, valor);
            }

            public void MostrarRutaBusqueda(string valor)
            {
                Nodo actual = Raiz;
                Console.Write("Ruta de búsqueda: ");
                while (actual != null)
                {
                    Console.Write(actual.Valor + " -> ");
                    if (valor == actual.Valor)
                    {
                        Console.WriteLine("Encontrado");
                        return;
                    }
                    else if (string.Compare(valor, actual.Valor) < 0)
                        actual = actual.Izquierda;
                    else
                        actual = actual.Derecha;
                }
                Console.WriteLine("No encontrado");
            }
        }

        // Simulación del juego de dados usando un árbol binario
        static void SimulacionJuegoDeDados()
        {
            ArbolBinario arbol = new ArbolBinario();

            // Insertar posibles resultados de lanzamientos de dados
            arbol.Insertar("2");
            arbol.Insertar("3");
            arbol.Insertar("4");
            arbol.Insertar("5");
            arbol.Insertar("6");
            arbol.Insertar("7");
            arbol.Insertar("8");
            arbol.Insertar("9");
            arbol.Insertar("10");
            arbol.Insertar("11");
            arbol.Insertar("12");

            // Recorrido de los posibles resultados
            arbol.RecorridoInOrden();

            // Simulación de la búsqueda de un resultado de dados
            Console.Write("Ingrese el resultado de dados que desea buscar: ");
            string resultado = Console.ReadLine();
            Nodo encontrado = arbol.Buscar(resultado);
            Console.WriteLine(encontrado != null ? "¡Resultado encontrado!" : "¡Resultado no encontrado!");
        }

        static void Main(string[] args)
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("====== MENÚ ÁRBOLES BINARIOS ======");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Recorrido In-Orden");
                Console.WriteLine("3. Recorrido Pre-Orden");
                Console.WriteLine("4. Recorrido Post-Orden");
                Console.WriteLine("5. Buscar valor");
                Console.WriteLine("6. Mostrar ruta de búsqueda");
                Console.WriteLine("7. Simulación de Juego de Dados");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese valor a insertar: ");
                        string valor = Console.ReadLine();
                        // Aquí se debe crear un objeto de ArbolBinario para insertar
                        break;
                    case "2":
                        // Mostrar recorrido In-Orden
                        break;
                    case "3":
                        // Mostrar recorrido Pre-Orden
                        break;
                    case "4":
                        // Mostrar recorrido Post-Orden
                        break;
                    case "5":
                        // Buscar valor en el árbol binario
                        break;
                    case "6":
                        // Mostrar ruta de búsqueda
                        break;
                    case "7":
                        SimulacionJuegoDeDados();
                        Console.ReadKey();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione una tecla para continuar.");
                        Console.ReadKey();
                        break;


                }
            } while (opcion != "0"); //
        }
    }
}
