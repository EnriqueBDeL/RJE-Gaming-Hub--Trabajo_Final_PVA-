using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace PF_26935244J_48846253A_24408975H
{
    public partial class Form5 : Form
    {
        public string Correo;

        private readonly string connectionString = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Pack, Componente, Precios FROM Pack";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string pack = reader["Pack"].ToString();
                        string componente = reader["Componente"].ToString();
                        string precios = reader["Precios"].ToString();

                        ListViewItem item = new ListViewItem(pack);
                        item.SubItems.Add(componente);
                        item.SubItems.Add(precios);
                        listView1.Items.Add(item);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        private void GenerarExcel()
        {
            Excel.Application excelApp = new Excel.Application();

            if (excelApp == null)
            {
                MessageBox.Show("Excel no está instalado en este equipo.");
                return;
            }

            try
            {
                Excel.Workbook workbook = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

                worksheet.Cells[1, 1] = "Factura";
                worksheet.Cells[2, 1] = "Producto";
                worksheet.Cells[2, 2] = "Precio";

                float precioTotal = 0;
                int rowIndex = 3;

                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    string nombrePack = item.Text;
                    string precioPack = ObtenerPrecioPack(nombrePack);

                    if (!string.IsNullOrEmpty(precioPack))
                    {
                        worksheet.Cells[rowIndex, 1] = nombrePack;

                        float precio;
                        if (float.TryParse(precioPack, out precio))
                        {
                            worksheet.Cells[rowIndex, 2] = precio;
                            precioTotal += precio;
                        }
                        else
                        {
                            MessageBox.Show($"El precio para el pack {nombrePack} no tiene un formato numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        rowIndex++;
                    }
                    else
                    {
                        MessageBox.Show($"No se encontró el precio para el pack {nombrePack}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
                worksheet.Cells[rowIndex, 1] = "Precio Total:";
                worksheet.Cells[rowIndex, 2] = precioTotal.ToString("F"); 
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el archivo Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private string ObtenerPrecioPack(string nombrePack)
        {
            string precio = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Precios FROM Pack WHERE Pack = @NombrePack";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@NombrePack", nombrePack);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    precio = result != null ? result.ToString().Trim() : "No se encontró el precio";

                  
                    precio = precio.Replace(" ", "");

                  
                    float precioFloat;
                    if (float.TryParse(precio, out precioFloat))
                    {
                        
                        precio = precioFloat.ToString();
                    }
                    else
                    {
                       
                        precio = "Precio no válido";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el precio del pack: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return precio;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

