namespace BibliotecaWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox comboBoxCategoria;
        private System.Windows.Forms.TextBox textBoxISBN;
        private System.Windows.Forms.TextBox textBoxTitulo;
        private System.Windows.Forms.TextBox textBoxAutor;
        private System.Windows.Forms.TextBox textBoxAnio;
        private System.Windows.Forms.Label labelMensaje;
        private System.Windows.Forms.DataGridView dataGridViewLibros;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Label labelCategoria;
        private System.Windows.Forms.Label labelISBN;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelAutor;
        private System.Windows.Forms.Label labelAnio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxCategoria = new System.Windows.Forms.ComboBox();
            this.textBoxISBN = new System.Windows.Forms.TextBox();
            this.textBoxTitulo = new System.Windows.Forms.TextBox();
            this.textBoxAutor = new System.Windows.Forms.TextBox();
            this.textBoxAnio = new System.Windows.Forms.TextBox();
            this.labelMensaje = new System.Windows.Forms.Label();
            this.dataGridViewLibros = new System.Windows.Forms.DataGridView();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            
            // Labels
            this.labelCategoria = new System.Windows.Forms.Label();
            this.labelISBN = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelAutor = new System.Windows.Forms.Label();
            this.labelAnio = new System.Windows.Forms.Label();

            // labelCategoria
            this.labelCategoria.AutoSize = true;
            this.labelCategoria.Location = new System.Drawing.Point(50, 53);
            this.labelCategoria.Text = "Categoría:";
            this.labelCategoria.ForeColor = Color.DarkSlateGray;

            // labelISBN
            this.labelISBN.AutoSize = true;
            this.labelISBN.Location = new System.Drawing.Point(50, 83);
            this.labelISBN.Text = "ISBN:";
            this.labelISBN.ForeColor = Color.DarkSlateGray;

            // labelTitulo
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Location = new System.Drawing.Point(50, 113);
            this.labelTitulo.Text = "Título:";
            this.labelTitulo.ForeColor = Color.DarkSlateGray;

            // labelAutor
            this.labelAutor.AutoSize = true;
            this.labelAutor.Location = new System.Drawing.Point(50, 143);
            this.labelAutor.Text = "Autor:";
            this.labelAutor.ForeColor = Color.DarkSlateGray;

            // labelAnio
            this.labelAnio.AutoSize = true;
            this.labelAnio.Location = new System.Drawing.Point(50, 173);
            this.labelAnio.Text = "Año:";
            this.labelAnio.ForeColor = Color.DarkSlateGray;

            // comboBoxCategoria
            this.comboBoxCategoria.Location = new System.Drawing.Point(150, 50);
            this.comboBoxCategoria.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategoria.Items.AddRange(new object[] { "Ficción", "No Ficción", "Historia", "Ciencia", "Fantasía" });
            this.comboBoxCategoria.BackColor = Color.LightYellow;

            // textBoxISBN
            this.textBoxISBN.Location = new System.Drawing.Point(150, 80);
            this.textBoxISBN.Size = new System.Drawing.Size(100, 20);
            this.textBoxISBN.BackColor = Color.LavenderBlush;

            // textBoxTitulo
            this.textBoxTitulo.Location = new System.Drawing.Point(150, 110);
            this.textBoxTitulo.Size = new System.Drawing.Size(200, 20);
            this.textBoxTitulo.BackColor = Color.LavenderBlush;

            // textBoxAutor
            this.textBoxAutor.Location = new System.Drawing.Point(150, 140);
            this.textBoxAutor.Size = new System.Drawing.Size(200, 20);
            this.textBoxAutor.BackColor = Color.LavenderBlush;

            // textBoxAnio
            this.textBoxAnio.Location = new System.Drawing.Point(150, 170);
            this.textBoxAnio.Size = new System.Drawing.Size(100, 20);
            this.textBoxAnio.BackColor = Color.LavenderBlush;

            // dataGridViewLibros
            this.dataGridViewLibros.Location = new System.Drawing.Point(50, 230);
            this.dataGridViewLibros.Size = new System.Drawing.Size(500, 150);
            this.dataGridViewLibros.ColumnCount = 5;
            this.dataGridViewLibros.Columns[0].Name = "Categoría";
            this.dataGridViewLibros.Columns[1].Name = "ISBN";
            this.dataGridViewLibros.Columns[2].Name = "Título";
            this.dataGridViewLibros.Columns[3].Name = "Autor";
            this.dataGridViewLibros.Columns[4].Name = "Año";
            this.dataGridViewLibros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLibros.MultiSelect = false;
            this.dataGridViewLibros.BackgroundColor = Color.MintCream;

            // buttonAgregar
            this.buttonAgregar.Location = new System.Drawing.Point(150, 400);
            this.buttonAgregar.Size = new System.Drawing.Size(75, 23);
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.BackColor = Color.LightSkyBlue;

            // buttonEliminar
            this.buttonEliminar.Location = new System.Drawing.Point(230, 400);
            this.buttonEliminar.Size = new System.Drawing.Size(75, 23);
            this.buttonEliminar.Text = "Eliminar";
            this.buttonEliminar.BackColor = Color.LightSkyBlue;

            // Agregar controles
            this.Controls.Add(this.labelCategoria);
            this.Controls.Add(this.labelISBN);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.labelAutor);
            this.Controls.Add(this.labelAnio);
            this.Controls.Add(this.comboBoxCategoria);
            this.Controls.Add(this.textBoxISBN);
            this.Controls.Add(this.textBoxTitulo);
            this.Controls.Add(this.textBoxAutor);
            this.Controls.Add(this.textBoxAnio);
            this.Controls.Add(this.dataGridViewLibros);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.buttonEliminar);
            this.Text = "Biblioteca";
            this.BackColor = Color.Beige; // Fondo del formulario

            // Establecer tamaño inicial de la ventana
            this.ClientSize = new System.Drawing.Size(600, 500); // Ajusta el tamaño que desees
        }

        #endregion
    }
}
