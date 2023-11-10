namespace Reloj
{
    partial class Reloj
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.paisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirPaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarPaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxHora = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paisToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(428, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // paisToolStripMenuItem
            // 
            this.paisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirPaisToolStripMenuItem,
            this.borrarPaisToolStripMenuItem});
            this.paisToolStripMenuItem.Name = "paisToolStripMenuItem";
            this.paisToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.paisToolStripMenuItem.Text = "Pais";
            this.paisToolStripMenuItem.Click += new System.EventHandler(this.paisToolStripMenuItem_Click_1);
            // 
            // añadirPaisToolStripMenuItem
            // 
            this.añadirPaisToolStripMenuItem.Name = "añadirPaisToolStripMenuItem";
            this.añadirPaisToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.añadirPaisToolStripMenuItem.Text = "Añadir Pais";
            this.añadirPaisToolStripMenuItem.Click += new System.EventHandler(this.añadirPaísToolStripMenuItem_Click);
            // 
            // borrarPaisToolStripMenuItem
            // 
            this.borrarPaisToolStripMenuItem.Name = "borrarPaisToolStripMenuItem";
            this.borrarPaisToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.borrarPaisToolStripMenuItem.Text = "Borrar Pais";
            // 
            // textBoxHora
            // 
            this.textBoxHora.Location = new System.Drawing.Point(85, 61);
            this.textBoxHora.Name = "textBoxHora";
            this.textBoxHora.Size = new System.Drawing.Size(239, 20);
            this.textBoxHora.TabIndex = 9;
            this.textBoxHora.TextChanged += new System.EventHandler(this.textBoxHora_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Hora:";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Reloj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxHora);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Reloj";
            this.Text = "Reloj";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem paisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirPaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarPaisToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxHora;
        private System.Windows.Forms.Label label1;
    }
}

