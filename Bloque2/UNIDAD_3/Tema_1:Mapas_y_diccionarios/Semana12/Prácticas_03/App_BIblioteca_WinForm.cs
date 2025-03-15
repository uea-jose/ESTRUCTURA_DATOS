using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BibliotecaWinForms
{
    public partial class Form1 : Form
    {
        private HashSet<string> categorias;
        private ArbolBinario libros;

        public Form1()
        {
            InitializeComponent();
            InicializarCategorias();
            libros = new ArbolBinario(); // Árbol binario para almacenar libros
            buttonAgregar.Click += new EventHandler(buttonAgregar_Click);
            buttonEliminar.Click += new EventHandler(buttonEliminar_Click);
        }

        private void InicializarCategorias()
        {
            // Se usa un HashSet para evitar duplicados en las categorías
            categorias = new HashSet<string> { "Ficción", "Novela", "Drama", "Juvenil" };
            comboBoxCategoria.Items.AddRange(categorias.ToArray());
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            if (comboBoxCategoria.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBoxISBN.Text) ||
                string.IsNullOrWhiteSpace(textBoxTitulo.Text) ||
                string.IsNullOrWhiteSpace(textBoxAutor.Text) ||
                string.IsNullOrWhiteSpace(textBoxAnio.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(textBoxAnio.Text, out int anio))
            {
                MessageBox.Show("Ingrese un año válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el ISBN ya existe
            if (libros.Existe(textBoxISBN.Text))
            {
                MessageBox.Show("El libro con este ISBN ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear un nuevo libro con los datos ingresados
            Libro nuevoLibro = new Libro(
                comboBoxCategoria.SelectedItem.ToString(),
                textBoxISBN.Text,
                textBoxTitulo.Text,
                textBoxAutor.Text,
                anio);

            libros.Insertar(nuevoLibro); // Insertar en el árbol binario
            ActualizarDataGridView();
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewLibros.SelectedRows.Count > 0)
            {
                var isbnsSeleccionados = dataGridViewLibros.SelectedRows
                                        .Cast<DataGridViewRow>()
                                        .Select(row => row.Cells[1].Value.ToString())
                                        .ToList();

                foreach (string isbn in isbnsSeleccionados)
                {
                    libros.Eliminar(isbn); // Eliminar del árbol binario
                }

                ActualizarDataGridView();
            }
            else
            {
                MessageBox.Show("Seleccione un libro para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarDataGridView()
        {
            dataGridViewLibros.Rows.Clear();

            var librosOrdenados = libros.ObtenerListaOrdenada()
                                        .OrderBy(libro => libro.Categoria)
                                        .ThenBy(libro => libro.Anio)
                                        .ToList();

            foreach (var libro in librosOrdenados)
            {
                dataGridViewLibros.Rows.Add(libro.Categoria, libro.ISBN, libro.Titulo, libro.Autor, libro.Anio);
            }
        }
    }

    public class Libro
    {
        public string Categoria { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }

        public Libro(string categoria, string isbn, string titulo, string autor, int anio)
        {
            Categoria = categoria;
            ISBN = isbn;
            Titulo = titulo;
            Autor = autor;
            Anio = anio;
        }
    }

    public class Nodo
    {
        public Libro Datos;
        public Nodo Izquierdo, Derecho;

        public Nodo(Libro datos)
        {
            Datos = datos;
            Izquierdo = Derecho = null;
        }
    }

    public class ArbolBinario
    {
        private Nodo raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        public void Insertar(Libro nuevoLibro)
        {
            raiz = InsertarRecursivo(raiz, nuevoLibro);
        }

        private Nodo InsertarRecursivo(Nodo actual, Libro nuevoLibro)
        {
            if (actual == null)
            {
                return new Nodo(nuevoLibro);
            }

            if (string.Compare(nuevoLibro.ISBN, actual.Datos.ISBN) < 0)
            {
                actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, nuevoLibro);
            }
            else
            {
                actual.Derecho = InsertarRecursivo(actual.Derecho, nuevoLibro);
            }

            return actual;
        }

        public void Eliminar(string isbn)
        {
            raiz = EliminarRecursivo(raiz, isbn);
        }

        private Nodo EliminarRecursivo(Nodo actual, string isbn)
        {
            if (actual == null)
            {
                return actual;
            }

            if (string.Compare(isbn, actual.Datos.ISBN) < 0)
            {
                actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, isbn);
            }
            else if (string.Compare(isbn, actual.Datos.ISBN) > 0)
            {
                actual.Derecho = EliminarRecursivo(actual.Derecho, isbn);
            }
            else
            {
                if (actual.Izquierdo == null)
                {
                    return actual.Derecho;
                }
                else if (actual.Derecho == null)
                {
                    return actual.Izquierdo;
                }
                
                Nodo sucesor = ObtenerMinimo(actual.Derecho);
                actual.Datos = sucesor.Datos;
                actual.Derecho = EliminarRecursivo(actual.Derecho, sucesor.Datos.ISBN);
            }
            return actual;
        }

        private Nodo ObtenerMinimo(Nodo actual)
        {
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual;
        }

        public List<Libro> ObtenerListaOrdenada()
        {
            List<Libro> lista = new List<Libro>();
            InOrden(raiz, lista);
            return lista;
        }

        private void InOrden(Nodo actual, List<Libro> lista)
        {
            if (actual != null)
            {
                InOrden(actual.Izquierdo, lista);
                lista.Add(actual.Datos);
                InOrden(actual.Derecho, lista);
            }
        }

        public bool Existe(string isbn)
        {
            return Buscar(raiz, isbn) != null;
        }

        private Nodo Buscar(Nodo actual, string isbn)
        {
            if (actual == null) return null;
            int comparacion = string.Compare(isbn, actual.Datos.ISBN);
            if (comparacion == 0) return actual;
            return comparacion < 0 ? Buscar(actual.Izquierdo, isbn) : Buscar(actual.Derecho, isbn);
        }
    }
}