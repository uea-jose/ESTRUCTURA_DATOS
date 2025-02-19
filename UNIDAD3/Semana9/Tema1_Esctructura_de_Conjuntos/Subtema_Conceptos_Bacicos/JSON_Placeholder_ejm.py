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
            lista_usuarios.insert(tk.END, "Nombre: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['name']}\n", 'etiqueta_despues')

            # Email
            lista_usuarios.insert(tk.END, "Email: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['email']}\n", 'etiqueta_despues')

            # Sitio web
            lista_usuarios.insert(tk.END, "Sitio web: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['website']}\n", 'etiqueta_despues')

            # Compañía
            lista_usuarios.insert(tk.END, "Compañía: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['company']['name']}\n", 'etiqueta_despues')
            lista_usuarios.insert(tk.END, f"  Slogan: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['company']['catchPhrase']}\n", 'etiqueta_despues')
            lista_usuarios.insert(tk.END, f"  Negocios: ", 'etiqueta_antes')
            lista_usuarios.insert(tk.END, f"{usuario['company']['bs']}\n", 'etiqueta_despues')

            lista_usuarios.insert(tk.END, "-" * 50 + "\n")

    except requests.exceptions.RequestException as e:
        messagebox.showerror("Error", f"Hubo un problema con la solicitud: {e}")


# Función para copiar registros al portapapeles
def copiar_registros():
    contenido = lista_usuarios.get(1.0, tk.END).strip()  # Obtener texto sin espacios extra
    if contenido:
        ventana.clipboard_clear()  # Limpiar el portapapeles
        ventana.clipboard_append(contenido)  # Copiar texto
        ventana.update()  # Actualizar portapapeles
        messagebox.showinfo("Copiado", "Registros copiados al portapapeles.")
    else:
        messagebox.showwarning("Vacío", "No hay registros para copiar.")


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
ventana.geometry("600x450")  # Ajustamos el tamaño de la ventana para más espacio

# Etiqueta de instrucción
label_instrucciones = tk.Label(ventana, text="¿Cuántos usuarios deseas ver?", font=("Arial", 12))
label_instrucciones.pack(pady=10)

# Campo de entrada para cantidad de usuarios
entry_cantidad = tk.Entry(ventana, font=("Arial", 12))
entry_cantidad.pack(pady=5)

# Botón para cargar los usuarios
boton_mostrar = tk.Button(ventana, text="Mostrar Usuarios", font=("Arial", 12), command=mostrar_usuarios)
boton_mostrar.pack(pady=5)

# Botón para copiar registros
boton_copiar = tk.Button(ventana, text="Copiar Registros", font=("Arial", 12), command=copiar_registros)
boton_copiar.pack(pady=5)

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

# Agregar etiquetas para colores personalizados
lista_usuarios.tag_configure('etiqueta_antes', foreground='blue', font=('Arial', 10, 'bold'))  # Antes de ":"
lista_usuarios.tag_configure('etiqueta_despues', foreground='green', font=('Arial', 10))  # Después de ":"

# Ejecutar la ventana principal
ventana.mainloop()
