using System;
using System.Windows.Forms;

public class Libro
{
    // Propiedades del libro
    public string ISBN { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnioPublicacion { get; set; }
    public string Categoria { get; set; }

    // Constructor que inicializa las propiedades
    public Libro(string isbn, string titulo, string autor, int anio, string categoria)
    {
        ISBN = isbn;
        Titulo = titulo;
        Autor = autor;
        AnioPublicacion = anio;
        Categoria = categoria;
    }
}

public class Nodo
{
    // Nodo del árbol binario, que contiene un libro y sus nodos izquierdo y derecho
    public Libro Libro;
    public Nodo Izquierda, Derecha;

    // Constructor del nodo
    public Nodo(Libro libro)
    {
        Libro = libro;
        Izquierda = Derecha = null;
    }
}

public class ArbolLibros
{
    private Nodo raiz;

    // Método público para insertar un libro en el árbol
    public void Insertar(Libro libro)
    {
        raiz = InsertarRec(raiz, libro);
    }

    // Método privado recursivo para insertar el libro en el árbol binario
    private Nodo InsertarRec(Nodo nodo, Libro libro)
    {
        // Si el nodo es nulo, se crea un nuevo nodo
        if (nodo == null) return new Nodo(libro);

        // Compara el título del libro y decide si insertarlo en la izquierda o en la derecha
        if (string.Compare(libro.Titulo, nodo.Libro.Titulo) < 0)
            nodo.Izquierda = InsertarRec(nodo.Izquierda, libro);
        else
            nodo.Derecha = InsertarRec(nodo.Derecha, libro);

        return nodo;
    }

    // Método público para buscar un libro por su título
    public Libro Buscar(string titulo)
    {
        return BuscarRec(raiz, titulo);
    }

    // Método privado recursivo para buscar un libro en el árbol binario
    private Libro BuscarRec(Nodo nodo, string titulo)
    {
        if (nodo == null) return null;

        // Si se encuentra el libro, se devuelve
        if (titulo == nodo.Libro.Titulo) return nodo.Libro;

        // Si no se encuentra, se busca en la rama correspondiente
        return string.Compare(titulo, nodo.Libro.Titulo) < 0 ?
            BuscarRec(nodo.Izquierda, titulo) : BuscarRec(nodo.Derecha, titulo);
    }

    // Método público para eliminar un libro por su título
    public void Eliminar(string titulo)
    {
        raiz = EliminarRec(raiz, titulo);
    }

    // Método privado recursivo para eliminar un libro en el árbol binario
    private Nodo EliminarRec(Nodo nodo, string titulo)
    {
        if (nodo == null) return null;

        // Se compara el título y se navega por el árbol
        if (string.Compare(titulo, nodo.Libro.Titulo) < 0)
            nodo.Izquierda = EliminarRec(nodo.Izquierda, titulo);
        else if (string.Compare(titulo, nodo.Libro.Titulo) > 0)
            nodo.Derecha = EliminarRec(nodo.Derecha, titulo);
        else
        {
            // Caso de un nodo sin hijos o con un solo hijo
            if (nodo.Izquierda == null) return nodo.Derecha;
            else if (nodo.Derecha == null) return nodo.Izquierda;

            // Caso de un nodo con dos hijos
            Nodo minNodo = ObtenerMin(nodo.Derecha);
            nodo.Libro = minNodo.Libro;
            nodo.Derecha = EliminarRec(nodo.Derecha, minNodo.Libro.Titulo);
        }

        return nodo;
    }

    // Método para obtener el nodo con el valor mínimo (más a la izquierda)
    private Nodo ObtenerMin(Nodo nodo)
    {
        while (nodo.Izquierda != null)
            nodo = nodo.Izquierda;

        return nodo;
    }
}

public class BibliotecaApp : Form
{
    private ArbolLibros arbol = new ArbolLibros();
    private ComboBox categoriaCombo;
    private TextBox isbnTextBox, tituloTextBox, autorTextBox, anioTextBox;
    private DataGridView dataGridView;
    private Label mensajeError;

    // Constructor de la aplicación, se configuran los controles de la interfaz
    public BibliotecaApp()
    {
        Text = "Biblioteca";
        Size = new System.Drawing.Size(600, 400);

        // Configuración del formulario
        FlowLayoutPanel panel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10) };
        Controls.Add(panel);

        // Etiqueta para mensajes de error
        mensajeError = new Label { ForeColor = System.Drawing.Color.Red };
        panel.Controls.Add(mensajeError);

        // Añadir la etiqueta y ComboBox para seleccionar la categoría
        Label categoriaLabel = new Label { Text = "Categoría" };
        panel.Controls.Add(categoriaLabel);

        categoriaCombo = new ComboBox();
        categoriaCombo.Items.Add("Novela");
        categoriaCombo.Items.Add("Ficción");
        categoriaCombo.Items.Add("Drama");
        categoriaCombo.Items.Add("Juvenil");
        panel.Controls.Add(categoriaCombo);

        // Crear campos de texto para ISBN, Título, Autor y Año
        panel.Controls.Add(CrearCampo("ISBN:", out isbnTextBox));
        panel.Controls.Add(CrearCampo("Título:", out tituloTextBox));
        panel.Controls.Add(CrearCampo("Autor:", out autorTextBox));
        panel.Controls.Add(CrearCampo("Año:", out anioTextBox));

        // Botón para agregar un libro
        Button agregarButton = new Button { Text = "Agregar Libro" };
        agregarButton.Click += AgregarLibro;
        panel.Controls.Add(agregarButton);

        // Botón para eliminar un libro
        Button eliminarButton = new Button { Text = "Eliminar Libro" };
        eliminarButton.Click += EliminarLibro;
        panel.Controls.Add(eliminarButton);

        // DataGridView para mostrar la lista de libros
        dataGridView = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        dataGridView.Columns.Add("ISBN", "ISBN");
        dataGridView.Columns.Add("Título", "Título");
        dataGridView.Columns.Add("Autor", "Autor");
        dataGridView.Columns.Add("Año", "Año");
        dataGridView.Columns.Add("Categoría", "Categoría");
        panel.Controls.Add(dataGridView);
    }

    // Método para crear los campos de texto con una etiqueta
    private FlowLayoutPanel CrearCampo(string labelText, out TextBox textBox)
    {
        FlowLayoutPanel flowPanel = new FlowLayoutPanel();
        Label label = new Label { Text = labelText };
        textBox = new Tex
