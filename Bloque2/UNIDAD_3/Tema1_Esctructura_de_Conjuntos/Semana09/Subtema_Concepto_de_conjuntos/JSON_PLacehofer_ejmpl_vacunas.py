#
import tkinter as tk  # Importa la biblioteca tkinter para crear interfaces gráficas.
from tkinter import messagebox  # Importa el módulo messagebox para mostrar mensajes de alerta o información.
from tkinter import ttk  # Importa el módulo ttk (Themed Tkinter) para acceder a widgets como Treeview.
import random  # Importa el módulo random para generar datos aleatorios.

# Listas de nombres y apellidos para generar nombres aleatorios
nombres_base = ["Carlos", "María", "Juan", "Ana", "Luis", "Laura", "Pedro", "Sofía", "Miguel", "Camila"]
apellidos_base = ["García", "Martínez", "Rodríguez", "López", "Pérez", "Sánchez", "Gómez", "Díaz", "Fernández",
                  "Torres"]

vacunas = ["Pfizer", "AstraZeneca", "Sin Vacuna"]  # Lista con los tipos de vacuna disponibles.


# Función para generar personas con información de vacunas de forma equilibrada
def generar_personas(cantidad):
    for row in tree.get_children():
        tree.delete(row)
##
    personas = []
    for _ in range(cantidad):
        nombre = f"{random.choice(nombres_base)} {random.choice(apellidos_base)}"
        tipo_vacuna = random.choices(vacunas, weights=[0.4, 0.3, 0.3])[0]
        dosis = 2 if tipo_vacuna in ["Pfizer", "AstraZeneca"] else 0
        personas.append((nombre, tipo_vacuna, dosis))

    for nombre, tipo_vacuna, dosis in personas:
        # Define el color según el tipo de vacuna
        if tipo_vacuna == "Pfizer":
            tag = "pfizer"
        elif tipo_vacuna == "AstraZeneca":
            tag = "astrazeneca"
        else:
            tag = "sin_vacuna"

        tree.insert("", tk.END, values=(nombre, tipo_vacuna, dosis), tags=(tag))


# Función para copiar los registros al portapapeles
def copiar_registros():
    contenido = ""
    for row in tree.get_children():
        nombre, tipo_vacuna, dosis = tree.item(row, "values")
        contenido += f"Nombre: {nombre}\nTipoVacuna: {tipo_vacuna}\nNo.Dosis: {dosis}\n{'-' * 50}\n"
    if contenido:
        ventana.clipboard_clear()
        ventana.clipboard_append(contenido)
        ventana.update()
        messagebox.showinfo("Copiado", "Registros copiados al portapapeles.")
    else:
        messagebox.showwarning("Vacío", "No hay registros para copiar.")


# Función para mostrar las personas generadas al presionar el botón o presionar Enter
def mostrar_personas(event=None):
    try:
        cantidad = int(entry_cantidad.get())
        if cantidad <= 0:
            messagebox.showwarning("Entrada inválida", "Ingrese un número positivo.")
        else:
            generar_personas(cantidad)
    except ValueError:
        messagebox.showwarning("Entrada inválida", "Ingrese un número válido.")


# Crear la ventana principal
ventana = tk.Tk()
ventana.title("Generador de Datos de Vacunas")
ventana.geometry("600x800")  # Tamañito: Ajuste de altura de la ventana

# Etiqueta de instrucciones
label_instrucciones = tk.Label(ventana, text="¿Cuántas personas deseas generar?", font=("Arial", 12))
label_instrucciones.pack(pady=10)

# Entrada de cantidad
entry_cantidad = tk.Entry(ventana, font=("Arial", 12), width=20)
entry_cantidad.pack(pady=5)

# Botón para generar personas
boton_mostrar = tk.Button(ventana, text="Generar Personas", font=("Arial", 12), command=mostrar_personas)
boton_mostrar.pack(pady=5)

# Botón para copiar registros
boton_copiar = tk.Button(ventana, text="Copiar Registros", font=("Arial", 12), command=copiar_registros)
boton_copiar.pack(pady=5)

# Crear un Frame para el Treeview (tabla)
frame = tk.Frame(ventana, width=500, height=700)  # Tamañito: Ajuste de altura del frame
frame.pack(pady=20)

# Crear el Treeview (tabla)
tree = ttk.Treeview(frame, columns=("Nombre", "TipoVacuna", "No.Dosis"), show="headings",
                    height=20)  # Tamañito: Aumentar el número de filas visibles
tree.pack(side=tk.LEFT)

# Configurar los encabezados de las columnas
tree.heading("Nombre", text="Nombre")
tree.heading("TipoVacuna", text="Tipo Vacuna")
tree.heading("No.Dosis", text="No. Dosis")

# Configurar el ancho de las columnas
tree.column("Nombre", width=200)
tree.column("TipoVacuna", width=150)
tree.column("No.Dosis", width=100)

# Agregar una barra de desplazamiento vertical
scrollbar = tk.Scrollbar(frame, orient="vertical", command=tree.yview)
scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
tree.config(yscrollcommand=scrollbar.set)

# Configuración de colores para las etiquetas
tree.tag_configure("pfizer", background="#A7D8F2")  # Azul claro para Pfizer
tree.tag_configure("astrazeneca", background="#F3E7C1")  # Amarillo claro para AstraZeneca
tree.tag_configure("sin_vacuna", background="#D3D3D3")  # Gris claro para personas no vacunadas

# Vincular la tecla Enter a la función mostrar_personas
ventana.bind("<Return>", mostrar_personas)

ventana.mainloop()
