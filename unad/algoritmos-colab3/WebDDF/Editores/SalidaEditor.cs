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
    partial class SalidaEditor : Form
    {
        readonly Salida salida;

        public SalidaEditor(Salida salida)
        {
            InitializeComponent();
            this.salida = salida;
            textBoxExpresión.Text = salida.Expresión;
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            salida.Expresión = textBoxExpresión.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
