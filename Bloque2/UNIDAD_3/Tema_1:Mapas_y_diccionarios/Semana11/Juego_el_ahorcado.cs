using System;
using System.Collections.Generic;
//dasdsd
public class Program
{
    private class PalabrasAleatorias
    {
        private Dictionary<string, List<string>> diccionarioPalabras;

        public PalabrasAleatorias()
        {
            diccionarioPalabras = new Dictionary<string, List<string>>
            {
                { "animales", new List<string> { "gato", "perro", "loro", "tigre", "oso" } },
                { "frutas", new List<string> { "uva", "mango", "kiwi", "pera", "limon" } },
                { "paises", new List<string> { "china", "rusia", "india", "brasil", "chile" } }
            };
        }

        public string ObtenerPalabraAleatoria(string categoria)
        {
            if (!diccionarioPalabras.ContainsKey(categoria))
                throw new ArgumentException("Categoría no encontrada.");

            Random rand = new Random();
            List<string> palabras = diccionarioPalabras[categoria];
            return palabras[rand.Next(palabras.Count)].ToLower();
        }
    }

    private class PalabrasMatch
    {
        private LinkedList<char> letrasMostradas;

        public PalabrasMatch(int longitud)
        {
            letrasMostradas = new LinkedList<char>();
            for (int i = 0; i < longitud; i++)
                letrasMostradas.AddLast('_');
        }

        public void ActualizarEstado(char letra, List<int> posiciones)
        {
            LinkedListNode<char> nodoActual = letrasMostradas.First;

            foreach (int pos in posiciones)
            {
                var temp = nodoActual;
                for (int i = 0; i < pos; i++)
                    temp = temp.Next ?? nodoActual;

                temp.Value = letra;
            }
        }

        public string ObtenerEstado()
        {
            return string.Join(" ", letrasMostradas);
        }
    }

    private class Adivina
    {
        private Dictionary<char, List<int>> mapaLetras;
        private Stack<char> intentosFallidos;
        private Queue<string> mensajesJuego;
        private int intentosRestantes;
        public string PalabraSecreta { get; }

        public Adivina(string palabra)
        {
            PalabraSecreta = palabra;
            intentosRestantes = 6;
            intentosFallidos = new Stack<char>();
            mensajesJuego = new Queue<string>();
            mapaLetras = new Dictionary<char, List<int>>();

            for (int i = 0; i < palabra.Length; i++)
            {
                char letra = palabra[i];
                if (!mapaLetras.ContainsKey(letra))
                    mapaLetras[letra] = new List<int>();

                mapaLetras[letra].Add(i);
            }
        }

        public bool IntentarLetra(char letra)
        {
            letra = char.ToLower(letra);

            if (mapaLetras.ContainsKey(letra))
            {
                mapaLetras.Remove(letra);
                return true;
            }

            intentosFallidos.Push(letra);
            intentosRestantes--;
            mensajesJuego.Enqueue($"Letra '{letra}' incorrecta!");
            return false;
        }

        public bool PalabraCompleta()
        {
            return mapaLetras.Count == 0;
        }

        public int IntentosRestantes => intentosRestantes;
        public IEnumerable<char> IntentosErrores => intentosFallidos;
        public Queue<string> Mensajes => mensajesJuego;  // Ahora retorna Queue<string> directamente
    }

    public static void Main()
    {
        Console.WriteLine("¡JUEGO DEL AHORCADO!");
        Console.WriteLine("-------------------\n");

        var generador = new PalabrasAleatorias();
        string palabra = generador.ObtenerPalabraAleatoria("animales");
        var juego = new Adivina(palabra);
        var display = new PalabrasMatch(palabra.Length);

        while (juego.IntentosRestantes > 0 && !juego.PalabraCompleta())  // ← Corregido el llamado al método
        {
            Console.Clear();
            Console.WriteLine($"Palabra: {display.ObtenerEstado()}");
            Console.WriteLine($"Intentos restantes: {juego.IntentosRestantes}");
            Console.WriteLine($"Errores: {string.Join(", ", juego.IntentosErrores)}");

            while (juego.Mensajes.Count > 0)  
                Console.WriteLine(juego.Mensajes.Dequeue());  // ← Ahora funciona porque Mensajes es una Queue<string>

            Console.Write("\nIngrese una letra: ");
            string entrada = Console.ReadLine() ?? string.Empty;  // ← Evita valores null

            if (string.IsNullOrEmpty(entrada) || entrada.Length > 1 || !char.IsLetter(entrada[0]))
            {
                Console.WriteLine("Entrada inválida. Intente nuevamente.");
                Console.ReadKey();
                continue;
            }

            char intento = char.ToLower(entrada[0]);

            if (juego.IntentarLetra(intento))
            {
                var posiciones = new List<int>();
                for (int i = 0; i < juego.PalabraSecreta.Length; i++)
                {
                    if (juego.PalabraSecreta[i] == intento)
                        posiciones.Add(i);
                }
                display.ActualizarEstado(intento, posiciones);
            }
        }

        Console.Clear();
        if (juego.PalabraCompleta())
            Console.WriteLine($"¡GANASTE! La palabra era: {juego.PalabraSecreta}");
        else
            Console.WriteLine($"¡PERDISTE! La palabra era: {juego.PalabraSecreta}");
    }
}

