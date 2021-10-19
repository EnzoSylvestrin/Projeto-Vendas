using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sVenda
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void SAIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void relatórioDeVendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelatorioDeVendas formRelatorio = new frmRelatorioDeVendas();
            formRelatorio.MdiParent = this;            
            formRelatorio.Show();
        }

        private void VENDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVenda formVenda = new frmVenda();
            formVenda.MdiParent = this;
            formVenda.Show();
        }

        private void cadastroDeProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroProduto formCadProduto = new frmCadastroProduto();
            formCadProduto.MdiParent = this;
            formCadProduto.Show();
        }

    }
}
