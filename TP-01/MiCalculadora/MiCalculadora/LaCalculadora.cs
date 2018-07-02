
using Calculator;
using Number;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MiCalculadora1
{
    public partial class LaCalculadora : Form
    {
        public LaCalculadora()
        {
            InitializeComponent();
            Limpiar();
            txtNumero1.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            btnConvertirABinario.Enabled = true;
            btnConvertiADecimal.Enabled = true;

            Numero numero1 = new Numero(txtNumero1.Text);
            Numero numero2 = new Numero(txtNumero2.Text);
            
            string operador = cmbOperador.Text;
            lblResultado.Text = Operar(numero1,numero2,operador).ToString();
        }

 
        public static double Operar(Numero numero1, Numero numero2, string operador)
        {
            double retorno;

            Calculadora calculadora = new Calculadora();
            retorno = calculadora.Operar(numero1,numero2,operador);
            
            return retorno;
            
        }

        private void Limpiar()
        {
            txtNumero2.Text = "";
            cmbOperador.Text = "";
            txtNumero1.Text = "";           
            lblResultado.Text = "0";
            btnConvertirABinario.Enabled = false;
            btnConvertiADecimal.Enabled = false;
        }

        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            Numero numero = new Numero();
            lblResultado.Text = numero.DecimalBinario(lblResultado.Text.ToString());
            btnConvertirABinario.Enabled = false;
            btnConvertiADecimal.Enabled = true;
        }

        private void btnConvertiADecimal_Click(object sender, EventArgs e)
        {
            Numero numero = new Numero();
            lblResultado.Text = numero.BinarioDecimal(lblResultado.Text);
            btnConvertiADecimal.Enabled = false;
            btnConvertirABinario.Enabled = true;
        }

        private void txtNumero2_TextChanged(object sender, EventArgs e)
        {
            if(txtNumero2.Focused)
            {
                txtNumero1.Focus();
            }
        }
    }
}
