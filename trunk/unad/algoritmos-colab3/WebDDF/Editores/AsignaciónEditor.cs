using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDDF.Modelo;

namespace WebDDF.Editores
{
    partial class AsignaciónEditor : Form
    {
        readonly Asignación asignación;
        readonly BindingList<Variable> variables = new BindingList<Variable>();

        public AsignaciónEditor(Asignación asignación)
        {
            InitializeComponent();
            this.asignación = asignación;
            foreach (Variable variable in asignación.Variables)
                variables.Add(new Variable { Nombre = variable.Nombre, Expresión = variable.Expresión });
            dataGridViewAsignaciones.DataSource = variables;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            asignación.Variables.Clear();
            foreach (Variable variable in variables)
                asignación.Variables.Add(new Variable { Nombre = variable.Nombre, Expresión = variable.Expresión });
            DialogResult = DialogResult.OK;
        }
    }
}
