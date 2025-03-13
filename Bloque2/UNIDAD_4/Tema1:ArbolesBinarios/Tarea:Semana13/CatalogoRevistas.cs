using System;

public class Node
{
    public string Title;  // Título de la revista.
    public Node Left;     // Nodo izquierdo en el árbol (revistas menores).
    public Node Right;    // Nodo derecho en el árbol (revistas mayores).

    public Node(string title)
    {
        Title = title;  // Asigna el título al nodo.
        Left = Right = null;  // Los nodos izquierdo y derecho inicialmente son nulos.
    }
}

public class BinarySearchTree
{
    public Node Root;  // Raíz del árbol de búsqueda binaria.

    // Inserción de un nuevo título en el árbol
    public void Insert(string title)
    {
        Root = InsertRec(Root, title);  // Llamada recursiva a la función InsertRec.
    }

    // Función recursiva para insertar un título en el árbol
    private Node InsertRec(Node root, string title)
    {
        if (root == null)
        {
            root = new Node(title);  // Si el nodo es nulo, creamos uno nuevo.
            return root;
        }

        // BST: Inserción recursiva
        // Compara el título con el nodo actual para decidir en qué lado insertar el nuevo nodo
        if (string.Compare(title, root.Title) < 0)
            root.Left = InsertRec(root.Left, title);  // Insertar en el subárbol izquierdo.
        else if (string.Compare(title, root.Title) > 0)
            root.Right = InsertRec(root.Right, title);  // Insertar en el subárbol derecho.

        return root;
    }

    // Función para buscar un título en el árbol
    public string Search(string title)
    {
        Node result = SearchRec(Root, title);  // Llamada recursiva a la función SearchRec.
        return result != null ? "Encontrado" : "No encontrado";  // Si lo encuentra, devuelve "Encontrado", de lo contrario "No encontrado".
    }

    // Función recursiva para buscar un título
    private Node SearchRec(Node root, string title)
    {
        if (root == null || root.Title == title)  // Caso base: si el nodo es nulo o el título es igual al del nodo actual.
            return root;

        // BST: Búsqueda recursiva
        if (string.Compare(title, root.Title) < 0)
            return SearchRec(root.Left, title);  // Buscar en el subárbol izquierdo.
        
        return SearchRec(root.Right, title);  // Buscar en el subárbol derecho.
    }

    // Función para mostrar todo el catálogo en orden
    public void ShowCatalog()
    {
        ShowCatalogRec(Root);  // Llamada recursiva a la función ShowCatalogRec.
    }

    // Función recursiva para mostrar todo el catálogo (recorrido en orden)
    private void ShowCatalogRec(Node root)
    {
        if (root != null)
        {
            ShowCatalogRec(root.Left);  // Primero muestra el subárbol izquierdo.
            Console.WriteLine(root.Title);  // Muestra el título de la revista.
            ShowCatalogRec(root.Right);  // Luego muestra el subárbol derecho.
        }
    }
}

class Program
{
    static void Main()
    {
        BinarySearchTree bst = new BinarySearchTree();

        // Insertando 10 títulos en el catálogo
        bst.Insert("Vistazo");
        bst.Insert("Expreso");
        bst.Insert("Cosas");
        bst.Insert("¡Hola! Ecuador");
        bst.Insert("Guayaquil al Día");
        bst.Insert("Caras Ecuador");
        bst.Insert("Mujer");
        bst.Insert("Tendencias");
        bst.Insert("Bocado");
        bst.Insert("Vanguardia");

        // Menú interactivo
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Catálogo de Revistas");
            Console.WriteLine("1. Buscar un título");
            Console.WriteLine("2. Salir");
            Console.Write("Seleccione una opción: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Ingrese el título a buscar: ");
                string title = Console.ReadLine();
                string result = bst.Search(title);
                Console.WriteLine(result);

                // Submenú para ver el catálogo
                Console.WriteLine("¿Desea ver todo el catálogo actual? (S/N)");
                string showCatalogOption = Console.ReadLine().ToUpper();
                if (showCatalogOption == "S")
                {
                    Console.WriteLine("\nCatálogo completo de revistas:");
                    bst.ShowCatalog();  // Mostrar todo el catálogo usando el recorrido en orden (in-order traversal).
                }

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            else if (option == "2")
            {
                break;  // Termina el programa si elige salir.
            }
            else
            {
                Console.WriteLine("Opción no válida.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
