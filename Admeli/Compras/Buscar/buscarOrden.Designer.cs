namespace Admeli.Compras.Buscar
{
    partial class buscarOrden
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel6 = new System.Windows.Forms.Panel();
            this.textBuscar = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.progressBarApp = new System.Windows.Forms.ProgressBar();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textBuscar);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 4);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1096, 66);
            this.panel6.TabIndex = 47;
            // 
            // textBuscar
            // 
            this.textBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textBuscar.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textBuscar.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textBuscar.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textBuscar.BorderThickness = 1;
            this.textBuscar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBuscar.isPassword = false;
            this.textBuscar.Location = new System.Drawing.Point(93, 17);
            this.textBuscar.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textBuscar.Name = "textBuscar";
            this.textBuscar.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.textBuscar.Size = new System.Drawing.Size(989, 32);
            this.textBuscar.TabIndex = 56;
            this.textBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBuscar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBuscar_KeyUp);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label19.Location = new System.Drawing.Point(15, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 19);
            this.label19.TabIndex = 54;
            this.label19.Text = "Buscar:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 70);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.panel1.Size = new System.Drawing.Size(1096, 453);
            this.panel1.TabIndex = 48;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(13, 12);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1070, 429);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            // 
            // progressBarApp
            // 
            this.progressBarApp.BackColor = System.Drawing.Color.White;
            this.progressBarApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarApp.Location = new System.Drawing.Point(0, 0);
            this.progressBarApp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBarApp.MarqueeAnimationSpeed = 10;
            this.progressBarApp.Maximum = 200;
            this.progressBarApp.Name = "progressBarApp";
            this.progressBarApp.RightToLeftLayout = true;
            this.progressBarApp.Size = new System.Drawing.Size(1096, 4);
            this.progressBarApp.TabIndex = 49;
            // 
            // buscarOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1096, 523);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.progressBarApp);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "buscarOrden";
            this.Text = "importar Orden de  Compra";
            this.Load += new System.EventHandler(this.BuscarProveedor_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private Bunifu.Framework.UI.BunifuMetroTextbox textBuscar;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView;
        protected System.Windows.Forms.ProgressBar progressBarApp;
    }
}