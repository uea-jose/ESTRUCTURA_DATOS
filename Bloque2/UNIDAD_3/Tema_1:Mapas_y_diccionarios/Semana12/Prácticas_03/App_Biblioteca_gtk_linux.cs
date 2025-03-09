using Gtk;
using System;

public class Libro
{
    public string ISBN { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int AnioPublicacion { get; set; }
    public string Categoria { get; set; }

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
    public Libro Libro;
    public Nodo Izquierda, Derecha;

    public Nodo(Libro libro)
    {
        Libro = libro;
        Izquierda = Derecha = null;
    }
}

public class ArbolLibros
{
    private Nodo raiz;

    public void Insertar(Libro libro)
    {
        raiz = InsertarRec(raiz, libro);
    }

    private Nodo InsertarRec(Nodo nodo, Libro libro)
    {
        if (nodo == null) return new Nodo(libro);

        if (string.Compare(libro.Titulo, nodo.Libro.Titulo) < 0)
            nodo.Izquierda = InsertarRec(nodo.Izquierda, libro);
        else
            nodo.Derecha = InsertarRec(nodo.Derecha, libro);

        return nodo;
    }

    public Libro Buscar(string titulo)
    {
        return BuscarRec(raiz, titulo);
    }

    private Libro BuscarRec(Nodo nodo, string titulo)
    {
        if (nodo == null) return null;

        if (titulo == nodo.Libro.Titulo) return nodo.Libro;

        return string.Compare(titulo, nodo.Libro.Titulo) < 0 ? 
            BuscarRec(nodo.Izquierda, titulo) : BuscarRec(nodo.Derecha, titulo);
    }

    public void Eliminar(string titulo)
    {
        raiz = EliminarRec(raiz, titulo);
    }

    private Nodo EliminarRec(Nodo nodo, string titulo)
    {
        if (nodo == null) return null;

        if (string.Compare(titulo, nodo.Libro.Titulo) < 0)
            nodo.Izquierda = EliminarRec(nodo.Izquierda, titulo);
        else if (string.Compare(titulo, nodo.Libro.Titulo) > 0)
            nodo.Derecha = EliminarRec(nodo.Derecha, titulo);
        else
        {
            if (nodo.Izquierda == null) return nodo.Derecha;
            else if (nodo.Derecha == null) return nodo.Izquierda;

            Nodo minNodo = ObtenerMin(nodo.Derecha);
            nodo.Libro = minNodo.Libro;
            nodo.Derecha = EliminarRec(nodo.Derecha, minNodo.Libro.Titulo);
        }

        return nodo;
    }

    private Nodo ObtenerMin(Nodo nodo)
    {
        while (nodo.Izquierda != null)
            nodo = nodo.Izquierda;

        return nodo;
    }
}

public class BibliotecaApp : Gtk.Window
{
    private ArbolLibros arbol = new ArbolLibros();
    private ComboBoxText categoriaCombo;
    private Entry isbnEntry, tituloEntry, autorEntry, anioEntry;
    private TreeView treeView;
    private ListStore listStore;
    private Label mensajeError;

    public BibliotecaApp() : base("Biblioteca")
    {
        SetDefaultSize(600, 400);
        ModifyBg(StateType.Normal, new Gdk.Color(230, 240, 250));

        VBox vbox = new VBox { BorderWidth = 10 };
        mensajeError = new Label("") { UseMarkup = true };
        vbox.PackStart(mensajeError, false, false, 5);

        // Añadir el Label "Categoría" en negrita
        Label categoriaLabel = new Label() { Markup = "<b>Categoría</b>" };
        vbox.PackStart(categoriaLabel, false, false, 5);

        categoriaCombo = new ComboBoxText();
        categoriaCombo.AppendText("Novela");
        categoriaCombo.AppendText("Ficción");
        categoriaCombo.AppendText("Drama");
        categoriaCombo.AppendText("Juvenil");
        vbox.PackStart(categoriaCombo, false, false, 5);

        vbox.PackStart(CrearCampo("ISBN:", out isbnEntry), false, false, 5);
        vbox.PackStart(CrearCampo("Título:", out tituloEntry), false, false, 5);
        vbox.PackStart(CrearCampo("Autor:", out autorEntry), false, false, 5);
        vbox.PackStart(CrearCampo("Año:", out anioEntry), false, false, 5);

        Button agregarButton = new Button("Agregar Libro") { Relief = ReliefStyle.None };
        agregarButton.ModifyBg(StateType.Normal, new Gdk.Color(100, 180, 255));
        agregarButton.ModifyBg(StateType.Prelight, new Gdk.Color(0, 100, 200));
        agregarButton.Clicked += AgregarLibro;
        vbox.PackStart(agregarButton, false, false, 5);

        Button eliminarButton = new Button("Eliminar Libro") { Relief = ReliefStyle.None };
        eliminarButton.ModifyBg(StateType.Normal, new Gdk.Color(255, 100, 100));
        eliminarButton.ModifyBg(StateType.Prelight, new Gdk.Color(200, 0, 0));
        eliminarButton.Clicked += EliminarLibro;
        vbox.PackStart(eliminarButton, false, false, 5);

        treeView = new TreeView();
        listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
        treeView.Model = listStore;

        treeView.AppendColumn("ISBN", new CellRendererText(), "text", 0);
        treeView.AppendColumn("Título", new CellRendererText(), "text", 1);
        treeView.AppendColumn("Autor", new CellRendererText(), "text", 2);
        treeView.AppendColumn("Año", new CellRendererText(), "text", 3);
        treeView.AppendColumn("Categoría", new CellRendererText(), "text", 4);

        vbox.PackStart(treeView, true, true, 5);
        Add(vbox);
        ShowAll();
    }

    private HBox CrearCampo(string labelText, out Entry entry)
    {
        HBox hbox = new HBox();
        hbox.PackStart(new Label(labelText), false, false, 5);
        entry = new Entry();
        hbox.PackStart(entry, true, true, 5);
        return hbox;
    }

    private void AgregarLibro(object sender, EventArgs e)
    {
        string isbn = isbnEntry.Text;
        string titulo = tituloEntry.Text;
        string autor = autorEntry.Text;
        string categoria = categoriaCombo.ActiveText;
        int anio = int.TryParse(anioEntry.Text, out int result) ? result : 0;

        if (string.IsNullOrWhiteSpace(categoria))
        {
            mensajeError.Markup = "<span foreground='red'>Por favor, seleccione una categoría.</span>";
            return;
        }

        Libro libro = new Libro(isbn, titulo, autor, anio, categoria);
        arbol.Insertar(libro);
        listStore.AppendValues(isbn, titulo, autor, anio.ToString(), categoria);
    }

    private void EliminarLibro(object sender, EventArgs e)
    {
        TreeIter iter;
        if (treeView.Selection.GetSelected(out iter))
        {
            string titulo = (string)listStore.GetValue(iter, 1); // Obtener el título del libro seleccionado.
            arbol.Eliminar(titulo); // Eliminar el libro del árbol binario.
            listStore.Remove(ref iter); // Eliminar el libro de la interfaz gráfica.
        }
        else
        {
            mensajeError.Markup = "<span foreground='red'>Por favor, seleccione un libro para eliminar.</span>";
        }
    }

    public static void Main()
    {
        Application.Init();
        new BibliotecaApp();
        Application.Run();
    }
}
