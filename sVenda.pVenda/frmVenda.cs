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
    public partial class frmVenda : Form
    {
        double ValorTotal = 0;
        bool erro2 = true;
        List<cItemVenda> Listinha = new List<cItemVenda>();
        public frmVenda()
        {
            InitializeComponent();
        }

        private void frmVenda_Load(object sender, EventArgs e)
        {
            cProduto Produto = new cProduto();

            cmbCodProd.DisplayMember = "codigo";

            cmbCodProd.DataSource = Produto.Listar();
        }

        private void cmbCodProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cProduto ProdutoCMB = (cProduto)cmbCodProd.SelectedItem;
            txtVal.Text = ProdutoCMB.ValorUnitario.ToString();
        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            
            if (txtQtd.Text == "")
                errorProvider1.SetError(txtQtd, "Campo obrigatório!!!");
            else if(erro2)
            {
                
                cProduto ProdutoCMB2 = (cProduto)cmbCodProd.SelectedItem;
                cItemVenda ItemVenda = new cItemVenda();

                ItemVenda.Codigo = cmbCodProd.Text;
                ItemVenda.Nome = ProdutoCMB2.Nome;
                ItemVenda.ValorUnitario = Convert.ToDouble(txtVal.Text);
                ItemVenda.Quantidade = Convert.ToInt32(txtQtd.Text);


                Listinha.Add(ItemVenda);
                dgvProdutos.DataSource = null; 
                dgvProdutos.DataSource = Listinha;
                


                //Valor Total
                
                foreach (cItemVenda i in Listinha)
                {
                    ValorTotal = ValorTotal + (i.ValorUnitario * i.Quantidade);
                }
                lblValorTotal.Text = ValorTotal.ToString();
            }
        }
        
        private void btnConcluirVenda_Click(object sender, EventArgs e)
        {
            if (Listinha.Count == 0)
            {
                MessageBox.Show("Insira um produto!!");
            }
            else
            {
                cVenda venda = new cVenda();
                venda.CodigoVenda = DateTime.Now.ToString("ddMMyyHHmm");
                venda.DataVenda = Convert.ToDateTime("01/01/2021");
                venda.ItensDaVenda = Listinha;

                venda.IncluirVenda();

                MessageBox.Show("Venda realizada com sucesso!!");
                dgvProdutos.DataSource = null;
                Listinha.Clear();
                ValorTotal = 0;
                lblValorTotal.Text = ValorTotal.ToString();
            }
            
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente cancelar a venda?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        public void txtQtd_TextChanged(object sender, EventArgs e)
        {
            int erro = 0;
            if (int.TryParse(txtQtd.Text, out erro))
            {
                errorProvider1.Clear();
                erro2 = true;
            }
            else if (!int.TryParse(txtQtd.Text, out erro))
            {
                errorProvider1.SetError(txtQtd, "Permitido somente números!");
                erro2 = false;
            }
        }

    }
}
