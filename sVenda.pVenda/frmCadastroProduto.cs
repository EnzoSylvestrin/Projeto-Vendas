using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sVenda.pRegraDeNegocio;
using sVenda.pBancoDeDados;

namespace sVenda
{
    public partial class frmCadastroProduto : Form
    {
        public frmCadastroProduto()
        {
            InitializeComponent();
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            try
            {
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                cProduto Produto = new cProduto();
                dgvProdutos.DataSource = Produto.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar dados, mensagem de erro: " + ex.Message);
            }

        }

        public bool Consistir()
        {
            double valor;
            if (txtCodigo.Enabled)
            {
                if (txtCodigo.Text.Trim() == "" || txtNome.Text.Trim() == "" || txtValor.Text.Trim() == "")
                {
                    MessageBox.Show("Preencha todos os campos");
                    return false;
                }
                else if (!double.TryParse(txtValor.Text, out valor))
                {
                    errorProvider1.SetError(txtValor, "Digite apenas números no campo Valor");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                if (txtNome.Text.Trim() == "" || txtValor.Text.Trim() == "")
                {
                    MessageBox.Show("Preencha todos os campos");
                    return false;
                }
                else if (!double.TryParse(txtValor.Text, out valor))
                {
                    errorProvider1.SetError(txtValor, "Digite apenas números no campo Valor");
                    return false;
                }
                else
                    return true;
            }
        }


        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            cItemVenda venda = new cItemVenda();
            if (venda.VendaRegistrada(txtCodigo.Text))
                MessageBox.Show("Não é possível apagar o produto!");
            else if (MessageBox.Show("Deseja realmente excluir permanentemente o produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                cProduto Produto = new cProduto(txtCodigo.Text, txtNome.Text, Convert.ToDouble(txtValor.Text));
                Produto.Excluir();
                dgvProdutos.DataSource = Produto.Listar();
                txtNome.Clear();
                txtValor.Clear();
                txtCodigo.Clear();
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnIncluir.Enabled = true;
                txtCodigo.Enabled = true;
                txtCodigo.Focus();

            }
        }

        private void dgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cProduto Produto = new cProduto()
            {
                Codigo = dgvProdutos["codigo", e.RowIndex].Value.ToString(),
                Nome = dgvProdutos["nome", e.RowIndex].Value.ToString(),
                ValorUnitario = double.Parse(dgvProdutos["ValorUnitario", e.RowIndex].Value.ToString())

            };

            txtCodigo.Text = Produto.Codigo;
            txtNome.Text = Produto.Nome;
            txtValor.Text = Produto.ValorUnitario.ToString();

            btnExcluir.Enabled = true;
            btnAlterar.Enabled = true;
            btnIncluir.Enabled = false;
            txtCodigo.Enabled = false;
            errorProvider1.Clear();

        }

        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
              try
            {
                if (Consistir())
                {
                    cProduto Produto = new cProduto(txtCodigo.Text, txtNome.Text, Convert.ToDouble(txtValor.Text));
                    Produto.Incluir();
                    dgvProdutos.DataSource = Produto.Listar();
                    txtCodigo.Clear();
                    txtNome.Clear();
                    txtValor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incluir, mensagem de erro: " + ex.Message);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

            if (Consistir())
            {
                cProduto Produto = new cProduto(txtCodigo.Text, txtNome.Text, Convert.ToDouble(txtValor.Text));
                Produto.Alterar();
                dgvProdutos.DataSource = Produto.Listar();
                txtNome.Clear();
                txtValor.Clear();
                txtCodigo.Clear();
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnIncluir.Enabled = true;
                txtCodigo.Enabled = true;
                txtCodigo.Focus();

            }


        }

        private void txtValor_Leave_1(object sender, EventArgs e)
        {
            txtValor.Text = txtValor.Text.Replace(",", ".");
        }
        
    }
}