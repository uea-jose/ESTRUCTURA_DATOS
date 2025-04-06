//Practico experimental 4 Encuentro de vuelos baratos a partir de una base de datos
using System;
using System.Collections.Generic;
using Gtk;
using Cairo;

namespace VuelosBaratos
{
    // Clase que representa el grafo de vuelos
    class GrafoVuelos
    {
        private int V;  // Número de vértices (ciudades)
        public List<Tuple<int, int>>[] adj;  // Lista de adyacencia (vuelos)

        // Constructor que inicializa el grafo con un número de vértices
        public GrafoVuelos(int vertices)
        {
            V = vertices;
            adj = new List<Tuple<int, int>>[V];
            for (int i = 0; i < V; i++)
                adj[i] = new List<Tuple<int, int>>();  // Inicializa la lista de adyacencia
        }

        // Método para añadir un vuelo (arista) entre dos ciudades con un costo
        public void AddVuelo(int origen, int destino, int costo)
        {
            adj[origen].Add(new Tuple<int, int>(destino, costo));  // Añade un vuelo con su costo
        }

        // Método de Dijkstra para encontrar el costo mínimo desde una ciudad de inicio
        public Dictionary<int, int> Dijkstra(int ciudadInicio)
        {
            var distancias = new Dictionary<int, int>();  // Diccionario para almacenar las distancias mínimas
            var visitado = new HashSet<int>();  // Conjunto para almacenar las ciudades visitadas
            var colaPrioridad = new PriorityQueue<(int costo, int ciudad), int>();  // Cola de prioridad para el algoritmo

            // Inicializa todas las distancias a infinito
            for (int i = 0; i < V; i++)
                distancias[i] = int.MaxValue;

            distancias[ciudadInicio] = 0;  // La distancia a la ciudad de inicio es 0
            colaPrioridad.Enqueue((0, ciudadInicio), 0);  // Añade la ciudad de inicio a la cola

            // Bucle principal del algoritmo de Dijkstra
            while (colaPrioridad.Count > 0)
            {
                var (costoActual, ciudadActual) = colaPrioridad.Dequeue();  // Dequeue de la ciudad con menor costo

                // Si ya fue visitada, se omite
                if (visitado.Contains(ciudadActual))
                    continue;

                visitado.Add(ciudadActual);  // Marca la ciudad como visitada

                // Recorre los vecinos de la ciudad actual
                foreach (var vecino in adj[ciudadActual])
                {
                    int ciudadDestino = vecino.Item1;  // Ciudad de destino
                    int costoVuelo = vecino.Item2;  // Costo del vuelo
                    int nuevoCosto = costoActual + costoVuelo;  // Calcula el nuevo costo

                    // Si el nuevo costo es menor, actualiza la distancia y añade a la cola
                    if (nuevoCosto < distancias[ciudadDestino])
                    {
                        distancias[ciudadDestino] = nuevoCosto;
                        colaPrioridad.Enqueue((nuevoCosto, ciudadDestino), nuevoCosto);
                    }
                }
            }

            return distancias;  // Retorna las distancias mínimas desde la ciudad de inicio
        }
    }

    // Clase principal que contiene la interfaz gráfica y la lógica del programa
    class Program
    {
        // Método principal que inicializa la ventana y muestra los resultados
        static void Main(string[] args)
        {
            Application.Init();  // Inicializa la aplicación GTK

            // Crea una ventana con título "Vuelos Baratos"
            Window ventana = new Window("Vuelos Baratos");
            ventana.SetDefaultSize(800, 600);  // Establece el tamaño de la ventana
            ventana.SetPosition(WindowPosition.Center);  // Centra la ventana

            VBox vbox = new VBox();  // Crea un contenedor vertical
            ventana.Add(vbox);  // Añade el contenedor a la ventana

            // Crea una etiqueta para mostrar los resultados
            Label resultadosLabel = new Label("Costos mínimos desde Quito:");
            vbox.PackStart(resultadosLabel, false, false, 10);  // Añade la etiqueta al contenedor

            // Crea un área de dibujo para la representación gráfica
            DrawingArea drawingArea = new DrawingArea();
            drawingArea.Drawn += OnDrawn;  // Asocia el evento de dibujo
            vbox.PackStart(drawingArea, true, true, 10);  // Añade el área de dibujo al contenedor

            // Crea el grafo de vuelos y añade vuelos (aristas) entre ciudades
            GrafoVuelos vuelos = new GrafoVuelos(5);
            vuelos.AddVuelo(0, 1, 100);
            vuelos.AddVuelo(0, 2, 300);
            vuelos.AddVuelo(1, 2, 100);
            vuelos.AddVuelo(1, 3, 400);
            vuelos.AddVuelo(2, 3, 100);
            vuelos.AddVuelo(3, 4, 200);

            // Ejecuta el algoritmo de Dijkstra desde la ciudad 0 (Quito)
            var costosMinimos = vuelos.Dijkstra(0);

            // Muestra los resultados de los costos mínimos
            foreach (var par in costosMinimos)
            {
                resultadosLabel.Text += $"\nCiudad {par.Key}: Costo mínimo = {par.Value}";
            }

            // Muestra la representación textual del grafo en la consola
            Console.WriteLine("Representación del grafo:");
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Ciudad {i} -> ");
                foreach (var vuelo in vuelos.adj[i])
                {
                    Console.Write($"Ciudad {vuelo.Item1} (Costo {vuelo.Item2}) ");
                }
                Console.WriteLine();
            }

            // Establece el color de fondo de la ventana
            ventana.OverrideBackgroundColor(Gtk.StateFlags.Normal, new Gdk.RGBA { Red = 1, Green = 0.89, Blue = 0.88, Alpha = 1 });
            ventana.ShowAll();  // Muestra todos los elementos de la ventana
            ventana.DeleteEvent += delegate { Application.Quit(); };  // Cierra la aplicación al cerrar la ventana
            Application.Run();  // Inicia la aplicación GTK
        }

        // Método que dibuja la representación gráfica del grafo
        static void OnDrawn(object sender, DrawnArgs args)
        {
            var cr = args.Cr;  // Obtenemos el contexto de dibujo
            var allocation = ((Widget)sender).Allocation;  // Obtiene el tamaño del área de dibujo

            cr.SetSourceRGB(0.9, 0.9, 0.9);  // Establece el color de fondo (gris claro)
            cr.Rectangle(0, 0, allocation.Width, allocation.Height);  // Dibuja un rectángulo que cubre todo el área
            cr.Fill();  // Rellena el fondo

            // Diccionario con los nombres de las ciudades
            var nombresCiudades = new Dictionary<int, string>
            {
                { 0, "Quito" },
                { 1, "Guayaquil" },
                { 2, "Cuenca" },
                { 3, "Loja" },
                { 4, "Manta" }
            };

            // Inicializa el grafo de vuelos con los mismos vuelos
            GrafoVuelos vuelos = new GrafoVuelos(5);
            vuelos.AddVuelo(0, 1, 100);
            vuelos.AddVuelo(0, 2, 300);
            vuelos.AddVuelo(1, 2, 100);
            vuelos.AddVuelo(1, 3, 400);
            vuelos.AddVuelo(2, 3, 100);
            vuelos.AddVuelo(3, 4, 200);

            // Diccionario con las posiciones de las ciudades para su visualización
            var posiciones = new Dictionary<int, (double, double)>
            {
                { 0, (100, 100) },
                { 1, (300, 100) },
                { 2, (500, 100) },
                { 3, (300, 300) },
                { 4, (500, 300) }
            };

            // Dibuja las aristas con el costo de los vuelos
            for (int i = 0; i < vuelos.adj.Length; i++)
            {
                foreach (var vuelo in vuelos.adj[i])
                {
                    var origenPos = posiciones[i];
                    var destinoPos = posiciones[vuelo.Item1];
                    double midX = (origenPos.Item1 + destinoPos.Item1) / 2;
                    double midY = (origenPos.Item2 + destinoPos.Item2) / 2;

                    // Dibuja la línea que representa el vuelo
                    cr.SetSourceRGB(0.5, 0.7, 0.2);
                    cr.LineWidth = 2;
                    cr.MoveTo(origenPos.Item1, origenPos.Item2);
                    cr.LineTo(destinoPos.Item1, destinoPos.Item2);
                    cr.Stroke();

                    // Dibuja el costo sobre la línea
                    cr.SetSourceRGB(0, 0, 0);  // Color negro
                    cr.SelectFontFace("Sans", FontSlant.Normal, FontWeight.Normal);
                    cr.SetFontSize(12);
                    cr.MoveTo(midX + 5, midY - 5);
                    cr.ShowText($"${vuelo.Item2}");  // Muestra el costo
                }
            }

            // Dibuja los nodos (ciudades)//
            foreach (var pos in posiciones)
            {
                cr.SetSourceRGB(0.1, 0.3, 0.8);  // Establece el color de los nodos (azul)
                cr.Arc(pos.Value.Item1, pos.Value.Item2, 15, 0, 2 * Math.PI);  // Dibuja un círculo (nodo)
                cr.Fill();  // Rellena el nodo

                // Muestra el nombre de la ciudad
                cr.SetSourceRGB(0, 0, 0);  // Color negro
                cr.SelectFontFace("Sans", FontSlant.Normal, FontWeight.Bold);
                cr.SetFontSize(12);
                cr.MoveTo(pos.Value.Item1 + 20, pos.Value.Item2);
                cr.ShowText(nombresCiudades[pos.Key]);  // Muestra el nombre de la ciudad
            }
        }
    }
}
