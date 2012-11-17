using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;

using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosPseint
{
    partial class SumaPrimosNoPrimos : BaseEjercicio
    {
        readonly BindingList<Numero> bindingList = new BindingList<Numero>();

        public SumaPrimosNoPrimos()
        {
            InitializeComponent();
            bindingList.ListChanged += bindingList_ListChanged;
            dataGridViewNumeros.DataSource = bindingList;
        }

        void bindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            decimal primos = 0, noprimos = 0;
            foreach (Numero numero in bindingList)
            {
                if (numero.EsPrimo)
                    primos += numero.Valor;
                else
                    noprimos += numero.Valor;
            }
            textBoxSumaPrimos.Text = primos.ToString();
            textBoxSumaNoPrimos.Text = noprimos.ToString();
        }
    }

    public class Numero : INotifyPropertyChanged
    {
        int valor;

        public int Valor
        {
            get
            {
                return valor;
            }
            set
            {
                valor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Valor"));
                esPrimo = ConfirmarPrimo(valor);
                OnPropertyChanged(new PropertyChangedEventArgs("EsPrimo"));
            }
        }

        bool esPrimo;

        public bool EsPrimo
        {
            get
            {
                return esPrimo;
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

        bool ConfirmarPrimo(int candidato)
        {
            if (candidato == 2)
                return true;
            if ((candidato % 2) == 0)
                return false;
            for (int i = 3; (i * i) <= candidato; i += 2)
            {
                if ((candidato % i) == 0)
                    return false;
            }
            return true;
        }
    }
}
