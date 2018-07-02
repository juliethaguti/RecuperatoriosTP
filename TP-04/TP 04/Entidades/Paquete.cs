using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public enum EEstado
    {
        Ingresado,EnViaje,Entregado
    }

    public class Paquete : IMostrar<Paquete>
    {
        private string direccionEntrega;
        private EEstado estado;
        private string trackingID;
        public delegate void DelegadoEstado(object sender, EventArgs e);
        public event DelegadoEstado InformaEstado;
        public delegate void DelegadoException(string mensaje);
        public event DelegadoException InformaNoGuardo;

        #region Propiedades
        public string DireccionEntrega
        {
            get
            {
                return this.direccionEntrega;
            }
            set
            {
                this.direccionEntrega = value;
            }
        }

        public EEstado Estado
        {
            get
            {
                return this.estado;
            }
            set
            {
                this.estado = value;
            }
        }

        public string TrackingID 
        {
            get
            {
                return this.trackingID;
            }
            set
            {
                this.trackingID = value;
            }
        }
        #endregion

        #region Constructor
        public Paquete(string direccionEntrega, string trackingID)
        {
            this.DireccionEntrega = direccionEntrega;
            this.TrackingID = trackingID;
        }
        #endregion

        #region Métodos
        public void MockCicloDeVida()
        {
            while (this.Estado != EEstado.Entregado)
            {
                System.Threading.Thread.Sleep(10000);
                this.Estado++;
                InformaEstado.Invoke(this, EventArgs.Empty);
            }
            try
            {
                PaqueteDAO.Insertar(this);
            }
            catch(TrackingIdRepetidoException tire)
            {
                InformaNoGuardo.Invoke(tire.Message);
            }
        }

        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            return String.Format("{0} para {1}", this.TrackingID, this.DireccionEntrega);
        }
        #endregion

        #region Sobrecargas
        public static bool operator ==(Paquete p1, Paquete p2)
        {
            bool retorno = false;
            if (p1.TrackingID == p2.TrackingID)
            {
                retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Paquete p1, Paquete p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            return this.MostrarDatos(this);
        }
        #endregion
    }
}
