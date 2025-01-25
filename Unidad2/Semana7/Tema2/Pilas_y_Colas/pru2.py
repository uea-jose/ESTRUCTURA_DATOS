using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Crear una pila de enteros
        Stack<int> pilaEnteros = new Stack<int>();

        // Verificar si la pila está vacía antes de cualquier operación
        if (pilaEnteros.Count == 0)
        {
            Console.WriteLine("Pilas 0: La pila no ha sido creada o aún no tiene elementos.\n");
        }

        // Agregar elementos a la pila
        Console.WriteLine("Agregando elementos a la pila...");
        pilaEnteros.Push(10);
        Console.WriteLine("Se ha agregado el elemento 10");
        pilaEnteros.Push(20);
        Console.WriteLine("Se ha agregado el elemento 20");
        pilaEnteros.Push(30);
        Console.WriteLine("Se ha agregado el elemento 30");
        pilaEnteros.Push(40);
        Console.WriteLine("Se ha agregado el elemento 40\n");

        Console.WriteLine("Se han insertado elementos en la pila. La pila contiene elementos.\n");

        // Recorrer elementos de la pila
        Console.WriteLine("Recorriendo elementos en la pila (sin eliminarlos):");
        foreach (var item in pilaEnteros)
        {
            Console.WriteLine($"Elemento: {item}");
        }
        Console.WriteLine();

        // Mostrar el elemento en la cima sin eliminarlo (Peek)
        int elementoSuperior = pilaEnteros.Peek();
        Console.WriteLine($"Elemento en la cima (Peek): {elementoSuperior}\n");

        // Eliminar un elemento de la pila (Pop)
        int elementoSacado = pilaEnteros.Pop();
        Console.WriteLine($"Se ha sacado el elemento {elementoSacado}");
        Console.WriteLine("Se eliminó un elemento de la pila usando Pop.\n");

        // Recorrer nuevamente los elementos después de eliminar
        Console.WriteLine("Elementos restantes en la pila después de eliminar uno:");
        foreach (var item in pilaEnteros)
        {
            Console.WriteLine($"Elemento: {item}");
        }

        // Verificar si la pila está vacía
        Console.WriteLine();
        if (pilaEnteros.Count == 0)
        {
            Console.WriteLine("La pila está vacía.");
        }
        else
        {
            Console.WriteLine("La pila aún tiene elementos.");
        }
    }
}
