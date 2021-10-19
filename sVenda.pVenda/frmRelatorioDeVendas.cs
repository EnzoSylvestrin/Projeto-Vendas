using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sVenda.pRegraDeNegocio;

namespace sVenda
{
    public partial class frmRelatorioDeVendas : Form
    {  
        public frmRelatorioDeVendas()
        {
            InitializeComponent();
        }

        private void frmRelatorioDeVendas_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Tudo");
            comboBox1.Items.Add("Código do Produto");
            txtCodProduto.Enabled = false;
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            cItemVenda venda = new cItemVenda();

            string op = comboBox1.Text;

            if (op == "")
            {
                MessageBox.Show("Selecione uma das opções!");
                errorProvider1.SetError(comboBox1, "Obrigatório!!!");
            }
            else if (op == "Tudo")
            {
                try
                {
                    dgvRelatorio.DataSource = venda.Listar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Houve problema ao Listar os dados. Detalhe " + ex.Message, "Atenção...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (op == "Código do Produto")
            {
                try
                {
                    if (txtCodProduto.Text == "")
                        errorProvider1.SetError(txtCodProduto, "Por favor informar o código do produto!");
                    else
                    {
                        dgvRelatorio.DataSource = venda.Listar(txtCodProduto.Text);
                        errorProvider1.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Houve problema ao Listar os dados. Detalhe " + ex.Message, "Atenção...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string op = comboBox1.Text;

            if (op == "Tudo")
            {
                txtCodProduto.Enabled = false;
                txtCodProduto.Clear();
            }
            else if (op == "Código do Produto")
                txtCodProduto.Enabled = true;
                dgvRelatorio.DataSource = null;
        }
    }
}