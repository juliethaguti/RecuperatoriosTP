using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace FrmPpal
{
    public partial class Form1 : Form
    {
        Correo correo;
        public Form1()
        {
            InitializeComponent();
            correo = new Correo();
            mtxtTrackingID.Focus();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Paquete paq = new Paquete(txtDireccion.Text, mtxtTrackingID.Text);
            paq.InformaEstado += paq_InformaEstado;

            try
            {
                correo += paq;
            }
            catch (TrackingIdRepetidoException)
            {
                string mensaje = "El Tracking ID "+paq.TrackingID+" ya figura en la lista de envios.";
                MessageBox.Show(mensaje, "Paquete repetido", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            ActualizarEstado();
        }

        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke( d, new object[] {sender, e} );
            }
            else 
            {
                ActualizarEstado();
            }
        }

        private void ActualizarEstado()
        {
            lstEstadoEntregado.Items.Clear();
            lstEstadoEnViaje.Items.Clear();
            lstEstadoIngresado.Items.Clear();

            foreach (Paquete p in correo.Paquetes)
            {
                switch (p.Estado)
                {
                    case EEstado.Ingresado:
                        lstEstadoIngresado.Items.Add(p);
                        break;
                    case EEstado.EnViaje:
                        lstEstadoEnViaje.Items.Add(p);
                        break;
                    case EEstado.Entregado:
                        lstEstadoEntregado.Items.Add(p);
                        break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.correo.FinEntregas();
        }

        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            rtbMostrar.Clear();
            if (!(object.ReferenceEquals(elemento,null)))
            {
                this.rtbMostrar.Text = elemento.MostrarDatos(elemento) + System.Environment.NewLine;
                try
                {
                    GuardaString.Guardar(this.rtbMostrar.Text, "salida.txt");
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mostrarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }
    }
}
