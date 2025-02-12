import tkinter as tk
from tkinter import messagebox
import requests


# Función para obtener usuarios desde la API
def obtener_usuarios(cantidad):
    url = "https://jsonplaceholder.typicode.com/users"
    try:
        # Realizamos la solicitud GET a la API
        response = requests.get(url)
        response.raise_for_status()  # Asegura que la solicitud fue exitosa

        # Convertimos la respuesta JSON a un diccionario
        usuarios = response.json()

        # Mostrar solo la cantidad de usuarios seleccionados
        usuarios_seleccionados = usuarios[:cantidad]

        # Limpiar la lista de usuarios antes de mostrar nuevos
        lista_usuarios.delete(1.0, tk.END)

        # Agregar los usuarios seleccionados a la lista
        for usuario in usuarios_seleccionados:
            # Nombre
            lista_usuarios.insert(tk.END, "Nombre: ")
            lista_usuarios.insert(tk.END, f"{usuario['name']}\n", 'normal')  # Texto normal

            # Email
            lista_usuarios.insert(tk.END, "Email: ")
            lista_usuarios.insert(tk.END, f"{usuario['email']}\n", 'normal')

            # Sitio web
            lista_usuarios.insert(tk.END, "Sitio web: ")
            lista_usuarios.insert(tk.END, f"{usuario['website']}\n", 'normal')

            # Compañía
            lista_usuarios.insert(tk.END, "Compañía: ")
            lista_usuarios.insert(tk.END, f"{usuario['company']['name']}\n", 'normal')
            lista_usuarios.insert(tk.END, f"  Slogan: {usuario['company']['catchPhrase']}\n", 'normal')
            lista_usuarios.insert(tk.END, f"  Negocios: {usuario['company']['bs']}\n", 'normal')

            lista_usuarios.insert(tk.END, "-" * 50 + "\n")

    except requests.exceptions.RequestException as e:
        messagebox.showerror("Error", f"Hubo un problema con la solicitud: {e}")


# Función que se ejecuta cuando se presiona el botón o la tecla Enter
def mostrar_usuarios(event=None):
    try:
        cantidad = int(entry_cantidad.get())  # Obtener cantidad desde la entrada
        if cantidad <= 0:
            messagebox.showwarning("Entrada inválida", "Por favor ingrese un número positivo.")
        else:
            obtener_usuarios(cantidad)  # Llamar a la función para obtener usuarios
    except ValueError:
        messagebox.showwarning("Entrada inválida", "Por favor ingrese un número válido.")


# Crear la ventana principal
ventana = tk.Tk()
ventana.title("Mostrar Usuarios de la API")
ventana.geometry("600x400")  # Ajustamos el tamaño de la ventana para mayor espacio

# Etiqueta de instrucción
label_instrucciones = tk.Label(ventana, text="¿Cuántos usuarios deseas ver?", font=("Arial", 12))
label_instrucciones.pack(pady=10)

# Campo de entrada para cantidad de usuarios
entry_cantidad = tk.Entry(ventana, font=("Arial", 12))
entry_cantidad.pack(pady=5)

# Botón para cargar los usuarios
boton_mostrar = tk.Button(ventana, text="Mostrar Usuarios", font=("Arial", 12), command=mostrar_usuarios)
boton_mostrar.pack(pady=10)

# Asociar la tecla Enter para llamar a la función mostrar_usuarios
entry_cantidad.bind("<Return>", mostrar_usuarios)

# Crear el contenedor de texto con barra de desplazamiento
frame = tk.Frame(ventana)
frame.pack(pady=10)

# Barra de desplazamiento (Scrollbar)
scrollbar = tk.Scrollbar(frame)
scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

# Caja de texto para mostrar los usuarios
lista_usuarios = tk.Text(frame, height=15, width=70, font=("Arial", 10), wrap=tk.WORD, yscrollcommand=scrollbar.set)
lista_usuarios.pack()

# Configuración de la barra de desplazamiento
scrollbar.config(command=lista_usuarios.yview)

# Agregar etiquetas de negrita para los encabezados
lista_usuarios.tag_configure('normal', font=('Arial', 10))  # Normal font
lista_usuarios.tag_configure('bold', font=('Arial', 10, 'bold'))  # Negrita

# Ejecutar la ventana principal
ventana.mainloop()


