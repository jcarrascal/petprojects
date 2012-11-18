using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebDDF.Editores
{
    partial class AsignacionEditor : Form
    {
        public AsignacionEditor()
        {
            InitializeComponent();
        }
    }

    public class AsignacionÍtem
    {
        public string Variable { get; set; }

        public string Expresion { get; set; }
    }
}
