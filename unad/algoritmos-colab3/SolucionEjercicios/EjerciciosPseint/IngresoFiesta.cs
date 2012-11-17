using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolucionEjercicios.EjerciciosPseint
{
    public partial class IngresoFiesta : BaseEjercicio
    {
        public IngresoFiesta()
        {
            InitializeComponent();
        }

        readonly string[] ClavesValidas = new[] { "TOCTOC1", "TOCTOC2", "TOCTOC3", "TOCTOC4", "TOCTOC5" };

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            int cantidadClaves = 0;
            TextBox[] clavesIngresadas = new[] { textBoxClave1, textBoxClave2, textBoxClave3, textBoxClave4, textBoxClave5, textBoxClave6 };
            foreach (TextBox claveIngresada in clavesIngresadas)
            {
                if (ClavesValidas.Contains(claveIngresada.Text))
                    ++cantidadClaves;
            }
            if (cantidadClaves >= 2)
            {
                textBoxResultado.Text = string.Format("Has acertado {0}. PERMITE EL INGRESO A LA FIESTA\r\n", cantidadClaves);
                int rifa = new Random().Next(900) + 100;
                textBoxResultado.AppendText("Participas en la rifa con el número " + rifa);
            }
            else
            {
                textBoxResultado.Text = "NO PUEDE INGRSAR A LA FIESTA";
            }
        }
    }
}
