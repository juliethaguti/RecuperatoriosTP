using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Entidades
{
    public class Correo :IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> paquetes;

        #region Propiedades
        public List<Paquete> Paquetes 
        {
            get
            {
                return this.paquetes;
            }
            set
            {
                this.paquetes = value;
            }
        }
        #endregion

        #region Constructor
        public Correo()
        {
            mockPaquetes = new List<Thread>();
            paquetes = new List<Paquete>();
        }
        #endregion

        #region Métodos
        public void FinEntregas()
        {
            foreach (Thread hilo in this.mockPaquetes)
            {
                if (hilo.IsAlive == true)
                {
                    hilo.Abort();
                }
            }
        }

        public string MostrarDatos(IMostrar<List<Paquete>> elementos)
        {
            List<Paquete> listaPaquetes = (List<Paquete>)((Correo)elementos).paquetes;
            StringBuilder sb = new StringBuilder();

            foreach(Paquete paquete in listaPaquetes)
            {
                sb.AppendFormat("{0} para {1} ({2})", paquete.TrackingID,
                paquete.DireccionEntrega,paquete.Estado);
            }
            return sb.ToString();
        }
        #endregion

        #region Sobrecarga
        public static Correo operator +(Correo c, Paquete p)
        {
            foreach (Paquete aux in c.Paquetes)
            {
                if (p == aux)
                {
                    throw new TrackingIdRepetidoException("El paquete ya está en la lista");
                }
            }
            c.Paquetes.Add(p);
            Thread hilo = new Thread(p.MockCicloDeVida);
            c.mockPaquetes.Add(hilo);
            hilo.Start();
            return c;
        }
        #endregion
    }
}
