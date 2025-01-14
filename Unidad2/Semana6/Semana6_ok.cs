// Crear una lista enlazada con 50 números enteros, del 1 al 999 generados ////aleatoriamente. Una
// vez creada la lista,vez creada la lista, se deben eliminar los nodos que estén fuerase deben eliminar los nodos que estén fuera de un rango de valoresde un rango de valores leídosleídos
// desde el teclado.desde el teclado

using System;

public class Nodo {
    public int Data;
    public Nodo Next;
    
    public Nodo(int dato) {
        this.Data = dato;
        this.Next = null;
    }
}

public class ListaSimple {
    private Nodo head;  // Cambié a privado para proteger el acceso directo.

    public ListaSimple() {
        head = null;
    }

    // Insertar un Nodo al inicio
    public void InsertarInicio(int dato) {
        var nuevoNodo = new Nodo(dato);
        nuevoNodo.Next = head;
        head = nuevoNodo;
    }

    // Insertar un Nodo al final
    public void InsertarFinal(int dato) {
        Nodo nuevoNodo = new Nodo(dato);
        if (head == null) {
            head = nuevoNodo;
        } else {
            Nodo actual = head;
            while (actual.Next != null) {
                actual = actual.Next;
            }
            actual.Next = nuevoNodo;
        }
    }

    // Eliminar nodos fuera del rango
    public void EliminarFueraDeRango(int min, int max) {
        while (head != null && (head.Data < min || head.Data > max)) {
            head = head.Next;
        }

        if (head == null) return;

        Nodo actual = head;
        while (actual.Next != null) {
            if (actual.Next.Data < min || actual.Next.Data > max) {
                actual.Next = actual.Next.Next;
            } else {
                actual = actual.Next;
            }
        }
    }

    // Mostrar solo los valores dentro del rango en formato de lista normal
    public void MostrarListaFiltrada(int min, int max) {
        Nodo actual = head;
        bool isFirst = true;
        Console.Write("[");
        while (actual != null) {
            if (actual.Data >= min && actual.Data <= max) {
                if (!isFirst) {
                    Console.Write(", ");
                }
                Console.Write(actual.Data);
                isFirst = false;
            }
            actual = actual.Next;
        }
        Console.WriteLine("]");
    }

    // Buscar el nodo con el dato
    public Nodo Buscar(int dato) {
        Nodo actual = head;
        while (actual != null && actual.Data != dato) {
            actual = actual.Next;
        }
        return actual;
    }

    // Propiedad para acceder al primer nodo (opcional)
    public Nodo Head {
        get { return head; }
    }
}

class Program {
    static void Main(string[] args) {
        ListaSimple lista = new ListaSimple();
        Random rand = new Random();

        // Insertar 50 números aleatorios en la lista
        for (int i = 0; i < 50; i++) {
            lista.InsertarFinal(rand.Next(1, 1000));
        }

        // Mostrar la lista original
        Console.WriteLine("Lista original:");
        lista.MostrarListaFiltrada(int.MinValue, int.MaxValue);  // Mostrar todos los valores

        // Encontrar el mínimo y máximo en la lista
        Nodo actual = lista.Head;
        int minValor = int.MaxValue;
        int maxValor = int.MinValue;
        while (actual != null) {
            if (actual.Data < minValor) minValor = actual.Data;
            if (actual.Data > maxValor) maxValor = actual.Data;
            actual = actual.Next;
        }

        // Sugerir valores para el rango
        Console.WriteLine($"Ingrese el valor mínimo del rango (valor mínimo sugerido: {minValor}): ");
        int valorMinimo = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine($"Ingrese el valor máximo del rango (valor máximo sugerido: {maxValor}): ");
        int valorMaximo = Convert.ToInt32(Console.ReadLine());

        // Eliminar nodos fuera del rango
        lista.EliminarFueraDeRango(valorMinimo, valorMaximo);

        // Mostrar la lista después de eliminar los nodos fuera del rango
        Console.WriteLine("Lista después de eliminar nodos fuera del rango:");
        lista.MostrarListaFiltrada(valorMinimo, valorMaximo);  // Mostrar solo valores dentro del rango
    }
}