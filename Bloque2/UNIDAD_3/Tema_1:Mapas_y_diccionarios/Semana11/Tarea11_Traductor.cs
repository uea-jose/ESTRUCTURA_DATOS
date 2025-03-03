using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Diccionario con las palabras base en inglés y su traducción al español.
    // Este diccionario almacena las palabras en inglés como claves y sus traducciones en español como valores.
    static Dictionary<string, string> diccionarioInglesAEspanol = new Dictionary<string, string>
    {
        {"Time", "tiempo"},
        {"Person", "persona"},
        {"Year", "año"},
        {"Way", "camino/forma"},
        {"Day", "día"},
        {"Thing", "cosa"},
        {"Man", "hombre"},
        {"World", "mundo"},
        {"Life", "vida"},
        {"Hand", "mano"},
        {"Part", "parte"},
        {"Child", "niño/a"},
        {"Eye", "ojo"},
        {"Woman", "mujer"},
        {"Place", "lugar"},
        {"Work", "trabajo"},
        {"Week", "semana"},
        {"Case", "caso"},
        {"Point", "punto/tema"},
        {"Government", "gobierno"},
        {"Company", "empresa/compañía"}
    };

    // Diccionario adicional para realizar la traducción en sentido contrario: 
    // De español a inglés. Se crea usando el diccionario original con las claves y valores invertidos.
    static Dictionary<string, string> diccionarioEspanolAIngles = diccionarioInglesAEspanol
        .ToDictionary(x => x.Value.ToLower(), x => x.Key.ToLower()); // Convertimos a minúsculas para una comparación insensible a mayúsculas.

    // Función principal del programa que maneja el menú de opciones del traductor.
    static void Main()
    {
        while (true)
        {
            // Mostrar menú al usuario
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("====== Traductor =======");
            Console.WriteLine("=======================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Ingresar más palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    TraducirFrase(); // Si el usuario elige la opción 1, traduce una frase.
                    break;
                case "2":
                    IngresarPalabra(); // Si elige la opción 2, permite ingresar una nueva palabra al diccionario.
                    break;
                case "0":
                    Console.WriteLine("¡Hasta luego!"); // Si elige 0, sale del programa.
                    return;
                default:
                    Console.WriteLine("Opción no válida, por favor intente nuevamente.");
                    break;
            }
        }
    }

    // Función para traducir una frase ingresada por el usuario.
    static void TraducirFrase()
    {
        Console.Clear();

        // Mostrar las palabras del diccionario en inglés y español
        Console.WriteLine("Palabras disponibles en el diccionario:");
        foreach (var palabra in diccionarioInglesAEspanol)
        {
            Console.WriteLine($"{palabra.Key} - {palabra.Value}");
        }

        // Solicitar al usuario que ingrese una frase
        Console.WriteLine("\nIngrese una frase en español o inglés que contenga palabras del diccionario para traducir.");
        string frase = Console.ReadLine();

        // Separar la frase en palabras individuales usando el espacio como delimitador
        var palabras = frase.Split(' ').ToList();

        // Traducir las palabras que estén en el diccionario
        var fraseTraducida = palabras.Select(palabra =>
        {
            string palabraMinuscula = palabra.ToLower(); // Convertir la palabra a minúsculas para comparación insensible a mayúsculas

            // Si la palabra está en inglés, traducirla al español
            if (diccionarioInglesAEspanol.ContainsKey(palabra))
            {
                return diccionarioInglesAEspanol[palabra]; // Devuelve la traducción en español
            }
            // Si la palabra está en español, traducirla al inglés
            else if (diccionarioEspanolAIngles.ContainsKey(palabraMinuscula))
            {
                return diccionarioEspanolAIngles[palabraMinuscula]; // Devuelve la traducción en inglés
            }
            // Si la palabra no está en el diccionario, dejarla tal cual
            else
            {
                return palabra;
            }
        }).ToList(); // Convierte las palabras traducidas en una lista

        // Mostrar la frase traducida
        Console.WriteLine("\nSu frase traducida es: ");
        Console.WriteLine(string.Join(" ", fraseTraducida)); // Imprimir las palabras traducidas como una frase
        Console.WriteLine("\nPresione cualquier tecla para continuar.");
        Console.ReadKey();
    }

    // Función para ingresar más palabras al diccionario.
    static void IngresarPalabra()
    {
        Console.Clear();
        // Solicitar al usuario que ingrese una nueva palabra en el formato adecuado
        Console.WriteLine("Ingrese una nueva palabra en inglés seguida de su traducción en español.");
        Console.WriteLine("Formato: PalabraIngles - PalabraEspañol");
        string entrada = Console.ReadLine();

        // Separar la entrada por el guión y limpiar los espacios
        var partes = entrada.Split('-').Select(p => p.Trim()).ToList();

        // Si la entrada tiene el formato correcto
        if (partes.Count == 2)
        {
            // Convertir las palabras a minúsculas para evitar diferencias entre mayúsculas y minúsculas
            string palabraIngles = partes[0].ToLower();
            string palabraEspanol = partes[1].ToLower();

            // Verificar si la palabra ya existe en el diccionario y agregarla si no existe
            if (!diccionarioInglesAEspanol.ContainsKey(palabraIngles))
            {
                diccionarioInglesAEspanol.Add(palabraIngles, palabraEspanol); // Agregar al diccionario inglés -> español
                diccionarioEspanolAIngles.Add(palabraEspanol, palabraIngles); // Agregar al diccionario español -> inglés
                Console.WriteLine("Palabra agregada exitosamente.");
            }
            else
            {
                Console.WriteLine("La palabra ya existe en el diccionario.");
            }
        }
        else
        {
            Console.WriteLine("El formato es incorrecto. Asegúrese de usar el formato: PalabraIngles - PalabraEspañol");
        }

        Console.WriteLine("\nPresione cualquier tecla para continuar.");
        Console.ReadKey();
    }
}
