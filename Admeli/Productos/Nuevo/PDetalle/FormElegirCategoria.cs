using Admeli.Componentes;
using Entidad;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admeli.Productos.Nuevo.PDetalle
{
    public partial class FormElegirCategoria : Form
    {
        private CategoriaModel categoriaModel = new CategoriaModel();
        internal CategoriaProducto catProducto { get; set; }

        public FormElegirCategoria()
        {
            InitializeComponent();
        }

        private async Task cargarRegistros()
        {
            catProducto = await categoriaModel.categoriaProducto(0);
        }

        #region ========================================== ROOT LOAD ==========================================
        private void FormElegirCategoria_Load(object sender, EventArgs e)
        {
            this.reLoad();
        }

        private async void reLoad()
        {
            await cargarRegistros();
            cargarCategorias();
            cargarCategoriasElegidas();
        } 
        #endregion

        #region ================================== CARGAR CATEGORIA ==================================
        private void cargarCategorias()
        {
            // loadState(true);
            try
            {
                // Cargando las categorias desde el webservice
                List<Categoria> categoriaList = catProducto.producto;
                if (categoriaList.Count <= 0) { return; }
                Categoria lastCategori = categoriaList.Last();
                categoriaList.Remove(lastCategori);

                // Cargando
                treeViewFrom.Nodes.Clear(); // limpiando
                treeViewFrom.Nodes.Add(lastCategori.idCategoria.ToString(), lastCategori.nombreCategoria); // Cargando categoria raiz

                List<TreeNode> listNode = new List<TreeNode>();

                foreach (Categoria categoria in categoriaList)
                {
                    TreeNode aux = new TreeNode(categoria.nombreCategoria);
                    aux.Name = categoria.idCategoria.ToString();
                    listNode.Add(aux);

                }
                treeviewVista(categoriaList, treeViewFrom.Nodes[0], listNode);

                // Estableciendo valores por defecto

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //loadState(false);
        }

        private void treeviewVista(List<Categoria> categoriaList, TreeNode padreNode, List<TreeNode> listNode)
        {
            if (categoriaList.Count == 0)
            {
                return;
            }
            else
            {
                Categoria aux = primerElemento(categoriaList);
                TreeNode auxTreeNode = buscar(aux, listNode);
                TreeNode nodePadre = buscarPadre(aux, listNode);
                if (hijos(categoriaList, aux))
                {
                    auxTreeNode.ImageIndex = 1;
                }
                if (nodePadre.Text != "")
                {
                    nodePadre.Nodes.Add(auxTreeNode);
                    treeviewVista(categoriaList, padreNode, listNode);
                }
                else
                {
                    padreNode.Nodes.Add(auxTreeNode);
                    treeviewVista(categoriaList, padreNode, listNode);
                }
            }
        }

        private Categoria primerElemento(List<Categoria> categoriaList)
        {

            Categoria lastCategori = categoriaList[0];//raiz
            categoriaList.Remove(lastCategori);
            return lastCategori;
        }

        private bool hijos(List<Categoria> categoriaList, Categoria padre)
        {
            bool tieneHijos = false;
            foreach (Categoria categoria in categoriaList)
            {
                if (categoria.padre == padre.nombreCategoria)
                {
                    tieneHijos = true;
                    break;
                }

            }
            return tieneHijos;
        }

        private TreeNode buscar(Categoria categoria, List<TreeNode> listNode)
        {
            TreeNode aux = new TreeNode();
            foreach (TreeNode node in listNode)
            {
                if (categoria.nombreCategoria == node.Text)
                {
                    aux = node;
                    break;
                }

            }
            return aux;
        }

        private TreeNode buscarPadre(Categoria categoria, List<TreeNode> listNode)
        {
            TreeNode aux = new TreeNode();

            foreach (TreeNode node in listNode)
            {
                if (categoria.padre == node.Text)
                {
                    aux = node;
                    break;
                }
            }
            return aux;
        } 
        #endregion

        private void cargarCategoriasElegidas()
        {
            // loadState(true);
            try
            {
                // Cargando las categorias desde el webservice
                List<Categoria> categoriaList = catProducto.todo;

                if (categoriaList.Count <= 0) { return; }
                //Colocar las Categorias elegidas en el ComboBox
                categoriaBindingSource.DataSource = categoriaList;

                Categoria lastCategori = categoriaList.Last();
                categoriaList.Remove(lastCategori);

                // Cargando
                treeViewTo.Nodes.Clear();//Limpiando
                treeViewTo.Nodes.Add(lastCategori.idCategoria.ToString(), lastCategori.nombreCategoria); // Cargando categoria raiz

                List<TreeNode> listNode = new List<TreeNode>();

                foreach (Categoria categoria in categoriaList)
                {
                    TreeNode aux = new TreeNode(categoria.nombreCategoria);
                    aux.Name = categoria.idCategoria.ToString();
                    listNode.Add(aux);

                }
                treeviewVista(categoriaList, treeViewTo.Nodes[0], listNode);

                // Estableciendo valores por defecto

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Listar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //loadState(false);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            DrawShape drawShape = new DrawShape();
            drawShape.rightLine(panel4, 221, 225, 228,2);
            drawShape.leftLine(panel5,221,225,228,2);
        }

        private void btnMovDerecha_Click(object sender, EventArgs e)
        {
            TreeNode tempNode = treeViewFrom.SelectedNode;
            if (tempNode != null)
            {
                //Eliminamos el nodo seleccionado en el TreeviewFrom
                treeViewFrom.Nodes.Remove(treeViewFrom.SelectedNode);
                //Agregamos el Nodo al TreeviewTo
                treeViewTo.Nodes.Add(tempNode);
            }
        }

        private void btnMovIzquierda_Click(object sender, EventArgs e)
        {
            TreeNode tempNode = treeViewTo.SelectedNode;
            if (tempNode != null)
            {
                //Eliminamos el nodo seleccionado en el TreeviewFrom
                treeViewTo.Nodes.Remove(treeViewTo.SelectedNode);
                //Agregamos el Nodo al TreeviewTo
                treeViewFrom.Nodes.Add(tempNode);
            }
        }
    }
}
