using System;
using System.Collections.Generic;
using System.Linq;

class TorreHanoi
{
    private int numDiscos;
    private List<int>[] torres;

    public TorreHanoi(int numDiscos)
    {
        this.numDiscos = numDiscos;
        torres = new List<int>[3];
        for (int i = 0; i < 3; i++)
        {
            torres[i] = new List<int>();
        }

        // Agregar los discos a la primera torre (A)
        for (int i = numDiscos; i >= 1; i--)
        {
            torres[0].Add(i);
        }
    }

    public void ImprimirTorres()
    {
        int maxDisco = numDiscos;
        int anchoTorre = maxDisco * 2 + 2;
        string vacio = new string(' ', anchoTorre);

        // Crear la representación visual
        for (int i = maxDisco - 1; i >= 0; i--)
        {
            for (int t = 0; t < 3; t++)
            {
                if (torres[t].Count > i)
                {
                    int disco = torres[t][i];
                    string rellenoIzq = new string(' ', maxDisco - disco + 1);
                    string rellenoDer = new string(' ', maxDisco - disco);
                    Console.Write($"{rellenoIzq}#{disco}{new string('#', disco - 1)}{rellenoDer}");
                }
                else
                {
                    Console.Write(vacio);
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Imprimir etiquetas centradas para las torres
        string[] etiquetas = { "A", "B", "C" };
        foreach (var etiqueta in etiquetas)
        {
            Console.Write(etiqueta.PadLeft(anchoTorre / 2 + 1).PadRight(anchoTorre));
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private bool EsValido(int origen, int destino)
    {
        if (torres[origen].Count == 0) // Si la torre de origen está vacía
            return false;
        if (torres[destino].Count == 0) // Si la torre de destino está vacía
            return true;
        if (torres[origen][^1] > torres[destino][^1]) // No se puede mover un disco más grande sobre uno más pequeño
            return false;
        return true;
    }

    private bool MoverDisco(int origen, int destino)
    {
        if (EsValido(origen, destino))
        {
            int disco = torres[origen][^1]; // Obtener el último disco
            torres[origen].RemoveAt(torres[origen].Count - 1); // Eliminar el disco de la torre de origen
            torres[destino].Add(disco); // Agregar el disco a la torre de destino
            return true;
        }
        return false;
    }

    private bool JuegoCompleto()
    {
        return torres[2].Count == numDiscos && torres[2].SequenceEqual(new List<int>(Enumerable.Range(1, numDiscos).Reverse()));
    }

    public void Jugar()
    {
        while (!JuegoCompleto())
        {
            ImprimirTorres();

            Console.Write($"Escoge un número de disco (1-{numDiscos}): ");
            if (!int.TryParse(Console.ReadLine(), out int disco) || disco < 1 || disco > numDiscos)
            {
                Console.WriteLine("Número de disco inválido, intenta de nuevo.");
                continue;
            }

            int origen = -1;
            for (int i = 0; i < 3; i++)
            {
                if (torres[i].Contains(disco))
                {
                    origen = i;
                    break;
                }
            }

            if (origen == -1)
            {
                Console.WriteLine($"El disco #{disco} no está en ninguna torre. Intenta otro.");
                continue;
            }

            Console.Write($"Escoge la torre de destino para mover el disco #{disco} (A, B, C): ");
            string inputDestino = Console.ReadLine()?.ToUpper();
            if (inputDestino != "A" && inputDestino != "B" && inputDestino != "C")
            {
                Console.WriteLine("Opción de torre inválida, intenta de nuevo.");
                continue;
            }

            int destino = inputDestino switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                _ => -1
            };

            if (!MoverDisco(origen, destino))
            {
                Console.WriteLine($"Movimiento inválido. No se puede mover el disco #{disco} allí.");
            }
            else
            {
                Console.WriteLine($"Disco #{disco} movido de la torre {(char)('A' + origen)} a la torre {(char)('A' + destino)}.");
            }
        }

        ImprimirTorres();
        Console.WriteLine("¡GANASTE! El juego terminó.");
    }

    static void Main(string[] args)
    {
        Console.Write("Ingrese el número de discos: ");
        if (int.TryParse(Console.ReadLine(), out int numDiscos) && numDiscos > 0)
        {
            TorreHanoi juego = new TorreHanoi(numDiscos);
            juego.Jugar();
        }
        else
        {
            Console.WriteLine("Número de discos inválido.");
        }
    }
}
