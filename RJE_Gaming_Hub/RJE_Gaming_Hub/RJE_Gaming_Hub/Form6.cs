using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace PF_26935244J_48846253A_24408975H
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtnombrepieza.Text = "";
            cbconsola.Text = "";
            pictureBox1.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbconsola.Text == "")
            {
                MessageBox.Show("Formulario incompleto.");
            }
            else if ((cbconsola.Text != "") && (txtnombrepieza.Text == "") && (pictureBox1.Image == null))
            {
                MessageBox.Show("Formulario incompleto.");
            }
            else if ((cbconsola.Text != "") && (txtnombrepieza.Text != "") && (pictureBox1.Image == null))
            {
                Form7 form7 = new Form7();
                DialogResult result = form7.ShowDialog();
            }
            else if ((cbconsola.Text != "") && (txtnombrepieza.Text == "") && (pictureBox1.Image != null))
            {
                Form7 form7 = new Form7();
                DialogResult result = form7.ShowDialog();
            }
            else if ((cbconsola.Text != "") && (txtnombrepieza.Text != "") && (pictureBox1.Image != null))
            {
                Form7 form7 = new Form7();
                DialogResult result = form7.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Habilitar el botón
                button5.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Cargar la imagen. Picfoto es el PictureBox.
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cbconsola_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtnombrepieza_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.NombrePiezaForm6 = txtnombrepieza.Text;
            form7.ConsolaForm6 = cbconsola.Text;
            form7.ImageForm6 = pictureBox1.Image;
            form7.ShowDialog();

        }



    }
}
