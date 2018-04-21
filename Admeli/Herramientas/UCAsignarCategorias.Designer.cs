namespace Admeli.Herramientas
{
    partial class UCAsignarCategorias
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAsignarCategorias));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dgvConCategoria = new System.Windows.Forms.DataGridView();
            this.categoriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgvSinCategorias = new System.Windows.Forms.DataGridView();
            this.productoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tlsHeader = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.panelHeaderItem1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkBxSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxselectConCategoria = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nombreCategoriaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxselecEsPrincipal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConCategoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinCategorias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            this.tlsHeader.SuspendLayout();
            this.panelHeaderItem1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panel3);
            this.panelContainer.Controls.Add(this.splitter1);
            this.panelContainer.Controls.Add(this.panel1);
            this.panelContainer.Controls.Add(this.tlsHeader);
            this.panelContainer.Controls.Add(this.panelHeaderItem1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(8, 8);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(1);
            this.panelContainer.Size = new System.Drawing.Size(882, 538);
            this.panelContainer.TabIndex = 7;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(416, 42);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(465, 454);
            this.panel3.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dgvConCategoria);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 41);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panel7.Size = new System.Drawing.Size(465, 413);
            this.panel7.TabIndex = 2;
            // 
            // dgvConCategoria
            // 
            this.dgvConCategoria.AllowUserToAddRows = false;
            this.dgvConCategoria.AllowUserToDeleteRows = false;
            this.dgvConCategoria.AutoGenerateColumns = false;
            this.dgvConCategoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConCategoria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvConCategoria.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvConCategoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbxselectConCategoria,
            this.nombreCategoriaDataGridViewTextBoxColumn,
            this.cbxselecEsPrincipal,
            this.idCategoria});
            this.dgvConCategoria.DataSource = this.categoriaBindingSource;
            this.dgvConCategoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConCategoria.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvConCategoria.Location = new System.Drawing.Point(8, 8);
            this.dgvConCategoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvConCategoria.Name = "dgvConCategoria";
            this.dgvConCategoria.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvConCategoria.RowHeadersVisible = false;
            this.dgvConCategoria.RowTemplate.Height = 25;
            this.dgvConCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConCategoria.Size = new System.Drawing.Size(449, 397);
            this.dgvConCategoria.TabIndex = 1;
            this.dgvConCategoria.Click += new System.EventHandler(this.dataGridView2_Click);
            // 
            // categoriaBindingSource
            // 
            this.categoriaBindingSource.DataSource = typeof(Entidad.Categoria);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(465, 41);
            this.panel5.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "LISTA DE PRODUCTOS";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(414, 42);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 454);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(1, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 454);
            this.panel1.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dgvSinCategorias);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 41);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panel6.Size = new System.Drawing.Size(413, 413);
            this.panel6.TabIndex = 1;
            // 
            // dgvSinCategorias
            // 
            this.dgvSinCategorias.AllowUserToAddRows = false;
            this.dgvSinCategorias.AllowUserToDeleteRows = false;
            this.dgvSinCategorias.AutoGenerateColumns = false;
            this.dgvSinCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinCategorias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSinCategorias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSinCategorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkBxSelect,
            this.idProducto,
            this.codigoProducto,
            this.nombreDataGridViewTextBoxColumn});
            this.dgvSinCategorias.DataSource = this.productoBindingSource;
            this.dgvSinCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSinCategorias.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvSinCategorias.Location = new System.Drawing.Point(8, 8);
            this.dgvSinCategorias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvSinCategorias.Name = "dgvSinCategorias";
            this.dgvSinCategorias.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSinCategorias.RowHeadersVisible = false;
            this.dgvSinCategorias.RowTemplate.Height = 25;
            this.dgvSinCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSinCategorias.Size = new System.Drawing.Size(397, 397);
            this.dgvSinCategorias.TabIndex = 0;
            this.dgvSinCategorias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dgvSinCategorias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSinCategorias_CellContentClick);
            // 
            // productoBindingSource
            // 
            this.productoBindingSource.DataSource = typeof(Entidad.Producto);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(413, 41);
            this.panel4.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = " *Solo aparecen los que no tienen Categoría";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Productos";
            // 
            // tlsHeader
            // 
            this.tlsHeader.AutoSize = false;
            this.tlsHeader.BackColor = System.Drawing.Color.White;
            this.tlsHeader.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlsHeader.GripMargin = new System.Windows.Forms.Padding(0);
            this.tlsHeader.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlsHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tlsHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo});
            this.tlsHeader.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tlsHeader.Location = new System.Drawing.Point(1, 496);
            this.tlsHeader.Name = "tlsHeader";
            this.tlsHeader.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tlsHeader.Size = new System.Drawing.Size(880, 41);
            this.tlsHeader.TabIndex = 6;
            this.tlsHeader.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 47);
            this.btnNuevo.Text = "  Guardar";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNuevo.ToolTipText = "Guardar cambios";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // panelHeaderItem1
            // 
            this.panelHeaderItem1.Controls.Add(this.label1);
            this.panelHeaderItem1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderItem1.Location = new System.Drawing.Point(1, 1);
            this.panelHeaderItem1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelHeaderItem1.Name = "panelHeaderItem1";
            this.panelHeaderItem1.Size = new System.Drawing.Size(880, 41);
            this.panelHeaderItem1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASIGNACIÓN DE CATEGORÍA(S) A(LOS) PRODUCTO(S)";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "precioCompra";
            this.dataGridViewTextBoxColumn1.HeaderText = "precioCompra";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "precioCompra";
            this.dataGridViewTextBoxColumn2.HeaderText = "precioCompra";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "precioCompra";
            this.dataGridViewTextBoxColumn3.HeaderText = "precioCompra";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "precioCompra";
            this.dataGridViewTextBoxColumn4.HeaderText = "precioCompra";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "precioCompra";
            this.dataGridViewTextBoxColumn5.HeaderText = "precioCompra";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // chkBxSelect
            // 
            this.chkBxSelect.FalseValue = "F";
            this.chkBxSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkBxSelect.HeaderText = "";
            this.chkBxSelect.Name = "chkBxSelect";
            this.chkBxSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkBxSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkBxSelect.TrueValue = "T";
            // 
            // idProducto
            // 
            this.idProducto.DataPropertyName = "idProducto";
            this.idProducto.HeaderText = "producto";
            this.idProducto.Name = "idProducto";
            this.idProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idProducto.Visible = false;
            // 
            // codigoProducto
            // 
            this.codigoProducto.DataPropertyName = "codigoProducto";
            this.codigoProducto.HeaderText = "Código";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "nombre";
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            this.nombreDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbxselectConCategoria
            // 
            this.cbxselectConCategoria.HeaderText = "";
            this.cbxselectConCategoria.Name = "cbxselectConCategoria";
            // 
            // nombreCategoriaDataGridViewTextBoxColumn
            // 
            this.nombreCategoriaDataGridViewTextBoxColumn.DataPropertyName = "nombreCategoria";
            this.nombreCategoriaDataGridViewTextBoxColumn.HeaderText = "Nombre Categoría";
            this.nombreCategoriaDataGridViewTextBoxColumn.Name = "nombreCategoriaDataGridViewTextBoxColumn";
            this.nombreCategoriaDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cbxselecEsPrincipal
            // 
            this.cbxselecEsPrincipal.HeaderText = "Es Principal";
            this.cbxselecEsPrincipal.Name = "cbxselecEsPrincipal";
            this.cbxselecEsPrincipal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // idCategoria
            // 
            this.idCategoria.DataPropertyName = "idCategoria";
            this.idCategoria.HeaderText = "Categoria";
            this.idCategoria.Name = "idCategoria";
            this.idCategoria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.idCategoria.Visible = false;
            // 
            // UCAsignarCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UCAsignarCategorias";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.Size = new System.Drawing.Size(898, 554);
            this.Load += new System.EventHandler(this.UCAsignarCategorias_Load);
            this.panelContainer.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConCategoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinCategorias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tlsHeader.ResumeLayout(false);
            this.tlsHeader.PerformLayout();
            this.panelHeaderItem1.ResumeLayout(false);
            this.panelHeaderItem1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelHeaderItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgvSinCategorias;
        private System.Windows.Forms.DataGridView dgvConCategoria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip tlsHeader;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.BindingSource categoriaBindingSource;
        private System.Windows.Forms.BindingSource productoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbxselectConCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCategoriaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbxselecEsPrincipal;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCategoria;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkBxSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
    }
}
