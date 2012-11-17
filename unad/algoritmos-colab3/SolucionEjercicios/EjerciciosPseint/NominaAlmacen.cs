using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosPseint
{
    public partial class NominaAlmacen : BaseEjercicio
    {
        readonly BindingList<Empleado> bindingList = new BindingList<Empleado>();

        public NominaAlmacen()
        {
            InitializeComponent();
            bindingList.ListChanged += bindingList_ListChanged;
            dataGridViewEmpleados.DataSource = bindingList;
        }

        void bindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                decimal sumaComisiones = 0, sumaTotalVentas = 0, sumaSueldoTotal = 0;
                foreach (Empleado empleado in bindingList)
                {
                    sumaComisiones += empleado.Comision;
                    sumaTotalVentas += empleado.TotalVentas;
                    sumaSueldoTotal += empleado.SueldoTotal;
                }
                textBoxTotalComisiones.Text = sumaComisiones.ToString("C");
                textBoxTotalVentas.Text = sumaTotalVentas.ToString("C");
                textBoxTotalNomina.Text = sumaSueldoTotal.ToString("C");
            }
        }
    }

    class Empleado : INotifyPropertyChanged
    {
        static int numero = 1;

        public Empleado()
        {
            nombre = "Empleado #" + numero++;
        }

        readonly string nombre;

        public string Nombre
        {
            get { return nombre; }
        }

        decimal totalVentas;

        public decimal TotalVentas
        {
            get { return totalVentas; }
            set
            {
                totalVentas = value;
                if (totalVentas >= 1000000 && totalVentas < 5000000)
                {
                    comision = totalVentas * 0.05M;
                }
                else if (totalVentas >= 5000000 && totalVentas < 10000000)
                {
                    comision = totalVentas * 0.1M;
                }
                else
                {
                    comision = 0;
                }
                OnPropertyChanged(new PropertyChangedEventArgs("Comision"));
                OnPropertyChanged(new PropertyChangedEventArgs("TotalVentas"));
                OnPropertyChanged(new PropertyChangedEventArgs("SueldoTotal"));
            }
        }

        decimal salarioBase;

        public decimal SalarioBase
        {
            get { return salarioBase; }
            set
            {
                salarioBase = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SalarioBase"));
                OnPropertyChanged(new PropertyChangedEventArgs("SueldoTotal"));
            }
        }

        decimal comision = 0;

        public decimal Comision
        {
            get { return comision; }
            set
            {
                comision = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Comision"));
            }
        }

        public decimal SueldoTotal
        {
            get
            {
                return SalarioBase + comision;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
