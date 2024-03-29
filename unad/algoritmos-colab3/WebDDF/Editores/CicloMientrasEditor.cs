﻿using System;
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
    partial class CicloMientrasEditor : Form
    {
        readonly CicloMientras cicloMientras;

        public CicloMientrasEditor(CicloMientras cicloMientras)
        {
            InitializeComponent();
            this.cicloMientras = cicloMientras;
            textBoxExpresión.Text = cicloMientras.Expresión;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            cicloMientras.Expresión = textBoxExpresión.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
