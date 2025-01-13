import tkinter as tk
from tkinter import messagebox

# Clase que representa la Agenda de Turnos
class TurnosClinica:
    def __init__(self, root):
        self.root = root
        self.root.title("Agenda de Turnos - Clínica")
        self.root.geometry("700x600")
        self.root.resizable(False, False)

        # Lista que almacena los turnos de pacientes
        self.turnos = []

        # Marco para los campos de entrada y sus etiquetas
        self.input_frame = tk.Frame(self.root, bg="#f0f8ff")
        self.input_frame.pack(padx=10, pady=10, fill=tk.X)

        # Etiqueta y entrada para el nombre del paciente
        self.paciente_label = tk.Label(self.input_frame, text="Paciente:", bg="#f0f8ff")
        self.paciente_label.pack(anchor="w", padx=5)
        self.paciente_entry = tk.Entry(self.input_frame, width=40)
        self.paciente_entry.pack(padx=5, pady=5)

        # Etiqueta y entrada para la especialidad
        self.especialidad_label = tk.Label(self.input_frame, text="Especialidad:", bg="#f0f8ff")
        self.especialidad_label.pack(anchor="w", padx=5)
        self.especialidad_entry = tk.Entry(self.input_frame, width=30)
        self.especialidad_entry.pack(padx=5, pady=5)

        # Etiqueta y entrada para la fecha del turno
        self.turno_label = tk.Label(self.input_frame, text="Fecha (DD/MM/YYYY):", bg="#f0f8ff")
        self.turno_label.pack(anchor="w", padx=5)
        self.turno_entry = tk.Entry(self.input_frame, width=20)
        self.turno_entry.pack(padx=5, pady=5)

        # Etiqueta y entrada para la hora del turno
        self.hora_label = tk.Label(self.input_frame, text="Hora (HH:MM):", bg="#f0f8ff")
        self.hora_label.pack(anchor="w", padx=5)
        self.hora_entry = tk.Entry(self.input_frame, width=15)
        self.hora_entry.pack(padx=5, pady=5)

        # Botón para agregar turno
        self.add_turno_button = tk.Button(
            self.input_frame,
            text="Agregar Turno",
            command=self.agregar_turno,
            bg="#add8e6",
            fg="black"
        )
        self.add_turno_button.pack(pady=10)

        # Label para el visualizador de turnos
        self.turnos_label = tk.Label(self.root, text="Lista de Turnos", font=("Arial", 12, "bold"))
        self.turnos_label.pack(pady=5)

        # Listbox para mostrar los turnos
        self.turnos_listbox = tk.Listbox(self.root, width=50, height=10)
        self.turnos_listbox.pack(padx=10, pady=10)

        # Botón para eliminar turno
        self.delete_turno_button = tk.Button(
            self.root,
            text="Eliminar Turno",
            command=self.eliminar_turno,
            bg="#ffb6c1",
            fg="black"
        )
        self.delete_turno_button.pack(pady=5)

        # Botón para modificar turno
        self.edit_turno_button = tk.Button(
            self.root,
            text="Modificar Turno",
            command=self.modificar_turno,
            bg="#ffa07a",
            fg="black"
        )
        self.edit_turno_button.pack(pady=5)

    def agregar_turno(self):
        # Obtener datos del paciente, especialidad, turno y hora
        paciente = self.paciente_entry.get().strip()
        especialidad = self.especialidad_entry.get().strip()
        fecha = self.turno_entry.get().strip()
        hora = self.hora_entry.get().strip()

        # Validación básica
        if paciente and especialidad and fecha and hora:
            if not self.turno_exists(paciente, fecha, hora):
                turno = f"{paciente} - {especialidad} - {fecha} - {hora}"
                self.turnos.append(turno)  # Agregar el turno a la lista
                self.turnos_listbox.insert(tk.END, turno)  # Mostrarlo en la lista
                self.limpiar_campos()
            else:
                messagebox.showwarning("Advertencia", "Este turno ya está agendado.")
        else:
            messagebox.showwarning("Advertencia", "Por favor, complete todos los campos.")

    def turno_exists(self, paciente, fecha, hora):
        # Verifica si el turno ya existe en la lista (comparando paciente, fecha y hora)
        for turno in self.turnos:
            if paciente in turno and fecha in turno and hora in turno:
                return True
        return False

    def limpiar_campos(self):
        # Limpia las entradas
        self.paciente_entry.delete(0, tk.END)
        self.especialidad_entry.delete(0, tk.END)
        self.turno_entry.delete(0, tk.END)
        self.hora_entry.delete(0, tk.END)

    def eliminar_turno(self):
        # Eliminar el turno seleccionado
        selected_index = self.turnos_listbox.curselection()
        if selected_index:
            turno = self.turnos_listbox.get(selected_index)
            self.turnos_listbox.delete(selected_index)
            self.turnos.remove(turno)  # Eliminar de la lista de turnos
            messagebox.showinfo("Turno Eliminado", f"Turno '{turno}' ha sido eliminado.")
        else:
            messagebox.showwarning("Advertencia", "Por favor, seleccione un turno para eliminar.")

    def modificar_turno(self):
        # Modificar el turno seleccionado
        selected_index = self.turnos_listbox.curselection()
        if selected_index:
            turno = self.turnos_listbox.get(selected_index)
            self.turnos_listbox.delete(selected_index)
            self.turnos.remove(turno)

            # Rellenar los campos con los datos del turno seleccionado
            datos = turno.split(" - ")
            self.paciente_entry.insert(0, datos[0])
            self.especialidad_entry.insert(0, datos[1])
            self.turno_entry.insert(0, datos[2])
            self.hora_entry.insert(0, datos[3])
        else:
            messagebox.showwarning("Advertencia", "Por favor, seleccione un turno para modificar.")

# Llamar a la función principal
if __name__ == "__main__":
    root = tk.Tk()
    app = TurnosClinica(root)
    root.mainloop()
