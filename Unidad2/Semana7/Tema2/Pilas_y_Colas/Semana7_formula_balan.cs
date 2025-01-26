using System;
using System.Collections.Generic;
//
//
//
///ssadsds
class Program
{
    static bool EsBalanceada(string formula)
    {
        Stack<char> pila = new Stack<char>(); // Pila para almacenar los paréntesis
        List<int> posicionesAbiertas = new List<int>(); // Para guardar las posiciones de los paréntesis abiertos

        // Mostrar la fórmula original antes de procesarla
        Console.WriteLine("Fórmula original: " + formula);

        // Mostrar la fórmula con un formato donde resaltamos los paréntesis balanceados
        char[] formulaConResaltado = formula.ToCharArray();
        
        foreach (char c in formula)
        {
            // Si encontramos un paréntesis de apertura, lo agregamos a la pila
            if (c == '{' || c == '[' || c == '(')
            {
                pila.Push(c);
                posicionesAbiertas.Add(formula.IndexOf(c));
                Console.WriteLine($"Agregado '{c}' a la pila.");
            }
            // Si encontramos un paréntesis de cierre
            else if (c == '}' || c == ']' || c == ')')
            {
                // Si la pila está vacía o no coincide el paréntesis de apertura con el de cierre, no está balanceado
                if (pila.Count == 0)
                {
                    Console.WriteLine($"Error: No hay paréntesis de apertura para '{c}'. Fórmula no balanceada.");
                    return false;
                }

                char top = pila.Pop();
                Console.WriteLine($"Se ha sacado '{top}' de la pila al encontrar '{c}'.");

                if ((c == '}' && top != '{') || (c == ']' && top != '[') || (c == ')' && top != '('))
                {
                    Console.WriteLine($"Error: Paréntesis de cierre '{c}' no coincide con '{top}'. Fórmula no balanceada.");
                    return false;
                }
            }
        }

        // Si la pila está vacía, significa que todos los paréntesis se emparejaron correctamente
        if (pila.Count == 0)
        {
            Console.WriteLine("\nLa fórmula está balanceada.");
            Console.WriteLine("Fórmula balanceada: " + formula);  // Muestra la fórmula balanceada
            return true;
        }
        else
        {
            Console.WriteLine("\nError: Hay paréntesis de apertura sin cierre. Fórmula no balanceada.");
            return false;
        }
    }

    static void Main()
    {
        // Ejemplo balanceado
        string formulaBalanceada = "{[(7+3)*(4-2)]/(6+2)}";

        // Mostrar la fórmula balanceada
        Console.WriteLine("\nFórmula balanceada:");
        Console.WriteLine("Antes de la verificación: " + formulaBalanceada);

        // Verificación de balanceo
        bool resultadoBalanceada = EsBalanceada(formulaBalanceada);
        
        // Mostrar el resultado final
        if (resultadoBalanceada)
        {
            Console.WriteLine("\nLa fórmula está balanceada.");
            Console.WriteLine("Fórmula balanceada después de la verificación: " + formulaBalanceada);  // Muestra la fórmula balanceada después de la verificación
        }
        else
        {
            Console.WriteLine("\nLa fórmula no está balanceada.");
        }

        Console.WriteLine("\n-------------------------------\n");

        // Ejemplo desequilibrado
        string formulaDesequilibrada = "{[(7+3)*(4-2)]/(6+2}";

        // Mostrar la fórmula desequilibrada
        Console.WriteLine("\nFórmula desequilibrada:");
        Console.WriteLine("Antes de la verificación: " + formulaDesequilibrada);

        // Verificación de balanceo
        bool resultadoDesequilibrada = EsBalanceada(formulaDesequilibrada);
        
        // Mostrar el resultado final
        if (resultadoDesequilibrada)
        {
            Console.WriteLine("\nLa fórmula está balanceada.");
            Console.WriteLine("Fórmula balanceada después de la verificación: " + formulaDesequilibrada);  // Muestra la fórmula balanceada después de la verificación
        }
        else
        {
            Console.WriteLine("\nLa fórmula no está balanceada.");
        }
    }
}

