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

namespace WebDDF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void agregarAsignación_Click(object sender, EventArgs e)
        {
            lienzo.AddOperation(new Asignación());
        }

        private void agregarMientrasQue_Click(object sender, EventArgs e)
        {
            lienzo.AddOperation(new MientrasQue());
        }
    }
}
