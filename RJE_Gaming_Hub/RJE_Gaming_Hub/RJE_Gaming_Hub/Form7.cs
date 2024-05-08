using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.IO;
using Microsoft.Office.Core;

namespace PF_26935244J_48846253A_24408975H
{
    public partial class Form7 : Form
    {

        public string NombrePiezaForm6 { get; set; }
        public string ConsolaForm6 { get; set; }
        public Image ImageForm6 { get; set; }

        public Form7()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtnombre.Text = null;
            txtciudad.Text = null;
            txtcodigop.Text = null;
            txtcorreo.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codigopostal;
            if ((txtnombre.Text == "") || (txtciudad.Text == "") || (txtcodigop.Text == "") || (txtcorreo.Text == ""))
            {
                MessageBox.Show("Ha de rellenar el formulario.");
            }
            else if ((!int.TryParse(txtcodigop.Text, out codigopostal) || codigopostal <= 9999 || codigopostal > 99999))
            {
                MessageBox.Show("El codigo postal es erroneo intentelo de nuevo");
            }
            else
            {
                MessageBox.Show("Los datos se han rellenado correctamente.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel no está instalado en este equipo.");
                return;
            }

            try
            {
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                worksheet.Cells[1, 1] = "Datos del Formulario:";

                worksheet.Cells[3, 1] = "Nombre de la pieza:";
                worksheet.Cells[3, 2] = NombrePiezaForm6;

                worksheet.Cells[4, 1] = "Consola:";
                worksheet.Cells[4, 2] = ConsolaForm6;

                worksheet.Cells[6, 1] = "Nombre:";
                worksheet.Cells[6, 2] = txtnombre.Text;

                worksheet.Cells[7, 1] = "Ciudad:";
                worksheet.Cells[7, 2] = txtciudad.Text;

                worksheet.Cells[8, 1] = "Código Postal:";
                worksheet.Cells[8, 2] = txtcodigop.Text;

                if (ImageForm6 != null)
                {
                    string imagePath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                    ImageForm6.Save(imagePath);
                    worksheet.Shapes.AddPicture(imagePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0, 100, 100);
                }

                worksheet.Columns.AutoFit();

                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos en Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
             
                excelApp.Quit();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
