"""Asignación de 30 asientos en orden de llegada, una vez que todos los
asientos son vendidos.
o Descripción: Simula una línea de espera para una atracción en un
parque de diversiones.
o Objetivo: Asegurarse de que cada persona en la cola suba a la
atracción en el orden correcto."""
import tkinter as tk
from tkinter import messagebox
from collections import deque  # Importa deque para la cola FIFO


# Clase Centro de Recreación
class Centro_Recreacion:
    def __init__(self, root):
        self.root = root
        self.root.title("Centro de Recreación")
        self.root.geometry("700x620")  # Aumentamos la altura para más espacio
        self.root.resizable(True, True)  # Permitimos que la ventana sea redimensionable
        self.root.protocol("WM_DELETE_WINDOW", self.root.quit)
        self.root.bind("<Escape>", self.root.quit)
        self.root.configure(bg="#f9f9f9")

        # Frame con Canvas para permitir desplazamiento
        self.canvas = tk.Canvas(self.root)
        self.scrollbar = tk.Scrollbar(self.root, orient="vertical", command=self.canvas.yview)
        self.canvas.configure(yscrollcommand=self.scrollbar.set)

        self.scrollable_frame = tk.Frame(self.canvas)

        self.scrollable_frame.bind(
            "<Configure>",
            lambda e: self.canvas.configure(scrollregion=self.canvas.bbox("all"))
        )

        self.canvas.create_window((0, 0), window=self.scrollable_frame, anchor="nw")
        self.scrollbar.pack(side="right", fill="y")
        self.canvas.pack(side="left", fill="both", expand=True)

        # Defino un menú al inicio de la ventana con colores personalizados
        self.menu = tk.Menu(self.root, bg="#b0e0e6", fg="black")  # Color pastel
        self.root.config(menu=self.menu)

        # Crear una opción de menú "Archivo" con una opción "Salir"
        file_menu = tk.Menu(self.menu, tearoff=0, bg="#b0e0e6", fg="black", font=("Arial", 8))
        file_menu.add_command(label="Salir", command=self.root.quit)
        self.menu.add_cascade(label="Menu", menu=file_menu)

        # Marco para el campo de entrada (fondo amarillo claro)
        self.input_frame = tk.Frame(self.scrollable_frame, bg="#fffacd")
        self.input_frame.pack(padx=50, pady=40)  # Aumenta el padding para centrar mejor

        # Label para indicar ingreso al centro de recreación (fondo amarillo claro)
        self.entry_label = tk.Label(
            self.input_frame,
            text="Ingreso al Centro de Recreación",
            font=("Arial", 10, "bold"),
            bg="#fffacd",
            fg="black"
        )
        self.entry_label.pack()

        # Entry para la fila de espera (fondo blanco)
        self.entry_white = tk.Entry(self.scrollable_frame, width=100, justify='center', bg="white",
                                    font=("Arial", 8))  # Ajuste de fuente
        self.entry_white.pack(pady=10)

        # Botón para dejar pasar personas (color verde claro)
        self.pass_person_button = tk.Button(
            self.scrollable_frame,
            text="Dejar Pasar",
            command=self.pass_person,
            font=("Arial", 8),
            width=18,
            height=1,
            bg="#98fb98",  # Verde claro
            fg="black"
        )
        self.pass_person_button.pack(pady=5)

        # Botón para retirar personas (color rosa claro)
        self.remove_person_button = tk.Button(
            self.scrollable_frame,
            text="Retirar Persona",
            command=self.remove_person,
            font=("Arial", 8),
            width=18,
            height=1,
            bg="#ffb6c1",  # Rosa claro
            fg="black"
        )
        self.remove_person_button.pack(pady=5)

        # Botón para agregar personas a la fila (ahora ubicado más abajo)
        self.add_person_button = tk.Button(
            self.scrollable_frame,
            text="Ingreso Personas a Fila",
            command=self.add_people_to_queue,
            font=("Arial", 8, "bold"),
            width=18,
            height=1,
            bg="#add8e6",  # Azul claro
            fg="black"
        )
        self.add_person_button.pack(pady=5)

        # Botón para reiniciar el proceso
        self.reset_button = tk.Button(
            self.scrollable_frame,
            text="Reiniciar Proceso",
            command=self.reset_system,
            font=("Arial", 8, "bold"),
            width=18,
            height=1,
            bg="#f08080",  # Rojo claro
            fg="black"
        )
        self.reset_button.pack(pady=5)

        # Utilizamos deque para implementar la cola FIFO
        self.queue = deque()  # Fila FIFO (Cola)
        self.matrix = []  # Matriz para manejar los asientos
        self.rows = 5
        self.cols = 6
        self.create_empty_seating_matrix()

    def create_empty_seating_matrix(self):
        """Inicializa la matriz de asientos con valores vacíos."""
        self.matrix = [['-' for _ in range(self.cols)] for _ in range(self.rows)]  # Lista de listas
        self.update_seating_display()

    def update_seating_display(self):
        """Actualiza el cuadro para mostrar los asientos de forma gráfica."""
        # Eliminamos cualquier widget previo de los asientos
        for widget in self.input_frame.winfo_children():
            widget.destroy()

        # Crear una representación visual de los asientos como botones
        self.seat_buttons = []
        person_count = 0  # Contador para asignar los valores P0, P1, ...
        for row in range(self.rows):
            row_buttons = []
            for col in range(self.cols):
                seat = self.matrix[row][col]
                # Asignar la persona con el formato 'P0', 'P1', ...
                if seat != '-':
                    button_text = f"P{person_count}"  # Asigna un identificador P0, P1, ...
                    person_count += 1
                else:
                    button_text = "Vacío"
                button = tk.Button(self.input_frame, text=button_text, width=8, height=3,
                                   bg="lightblue" if seat == '-' else "lightgreen",
                                   font=("Arial", 10, "bold"))
                button.grid(row=row, column=col, padx=5, pady=5)
                row_buttons.append(button)
            self.seat_buttons.append(row_buttons)

    def add_people_to_queue(self):
        # Aquí usamos la cola FIFO para agregar 30 personas (usando deque)
        self.queue = deque(f"P{i}" for i in range(30, -1, -1))  # De P30 a P0, usando la sintaxis de "P{i}"
        # Ahora vamos a mostrar las personas de forma descendente (P30...P1)
        self.entry_white.delete(0, tk.END)
        queue_str = ",".join([f"P{i}" for i in range(30, -1, -1)])  # De P30 a P0, separados por comas
        self.entry_white.insert(0, queue_str)  # Convertimos la cola en una cadena

    def pass_person(self):
        if self.queue:
            person = self.queue.popleft()  # Extrae la primera persona de la fila (FIFO)
            # Mostrar las personas restantes en la cola de forma descendente
            queue_str = ",".join(self.queue)  # Solo actualiza la cola sin borrar todo
            self.entry_white.delete(0, tk.END)
            self.entry_white.insert(0, queue_str)  # Actualiza el Entry con la cola restante

            # Asignar el asiento de acuerdo al orden de llegada
            assigned = False
            for row in range(self.rows):
                for col in range(self.cols):
                    if self.matrix[row][col] == '-':  # Si hay un asiento disponible
                        self.matrix[row][col] = person  # Asigna el asiento a la persona
                        assigned = True
                        break  # Se rompe el ciclo al asignar el asiento

                if assigned:
                    break  # Salir del ciclo de filas cuando se ha asignado un asiento

            self.update_seating_display()

            if not assigned:
                messagebox.showwarning("Advertencia", "Todos los asientos están ocupados.")
        else:
            messagebox.showwarning("Advertencia", "No hay más personas en la fila.")

    def remove_person(self):
        if self.queue:
            self.queue.popleft()  # Retira la persona de la fila (FIFO)
            self.entry_white.delete(0, tk.END)
            self.entry_white.insert(0, ",".join(self.queue))  # Actualiza el Entry
        else:
            messagebox.showwarning("Advertencia", "No hay personas para retirar.")

    def reset_system(self):
        """Reinicia el sistema, vaciando la fila y los asientos."""
        self.queue.clear()  # Vaciar la cola
        self.create_empty_seating_matrix()  # Vaciar los asientos
        self.entry_white.delete(0, tk.END)  # Limpiar la visualización de la fila
        messagebox.showinfo("Reinicio", "El proceso ha sido reiniciado.")


if __name__ == "__main__":
    root = tk.Tk()
    app = Centro_Recreacion(root)
    root.mainloop()
