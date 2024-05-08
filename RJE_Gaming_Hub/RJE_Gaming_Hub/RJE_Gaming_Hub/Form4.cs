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
    public partial class Form4 : Form
    {

        SqlConnection conexion = new
        SqlConnection("server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI");

        SqlCommand comandosql = new SqlCommand();

        public Form4()
        {
            InitializeComponent();
            CargarDatos();
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            button2.Enabled = false;
        }

        private void CargarDatos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI"))
                {
                    using (SqlCommand comandosql = new SqlCommand("SELECT Nombre, Consola, Descripción, Precio FROM Componentes", conexion))
                    {
                        listView1.Items.Clear();
                        conexion.Open();
                        using (SqlDataReader midatareader = comandosql.ExecuteReader())
                        {
                            while (midatareader.Read())
                            {
                                string dato1 = midatareader.GetString(0);
                                string dato2 = midatareader.GetString(1);
                                string dato3 = midatareader.GetString(2);
                                string dato4;

                                if (!midatareader.IsDBNull(3))
                                {
                                    if (float.TryParse(midatareader.GetValue(3).ToString(), out float precio))
                                    {
                                        dato4 = precio.ToString();
                                    }
                                    else
                                    {
                                        dato4 = "Valor no válido";
                                    }
                                }
                                else
                                {
                                    dato4 = "Valor no válido";
                                }

                                ListViewItem milista = listView1.Items.Add(dato1);
                                milista.SubItems.Add(dato2);
                                milista.SubItems.Add(dato3);
                                milista.SubItems.Add(dato4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AgregarElementoSeleccionado(ListViewItem item)
        {
            if (!checkedListBox1.Items.Contains(item.Text)) 
            {
                checkedListBox1.Items.Add(item.Text); 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                AgregarElementoSeleccionado(item);
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                AgregarElementoSeleccionado(e.Item);
            }
            else
            {
                checkedListBox1.Items.Remove(e.Item.Text); 
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection("server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI"))
                {
                    string consulta = "SELECT Nombre, Consola, Descripción, Precio FROM Componentes";
                    if (comboBox1.SelectedIndex != -1)
                    {
                        consulta += " WHERE Consola = @Consola";
                    }

                    using (SqlCommand comandosql = new SqlCommand(consulta, conexion))
                    {
                        if (comboBox1.SelectedIndex != -1)
                        {

                            comandosql.Parameters.AddWithValue("@Consola", comboBox1.SelectedItem.ToString());
                        }

                        listView1.Items.Clear();
                        conexion.Open();
                        using (SqlDataReader midatareader = comandosql.ExecuteReader())
                        {
                            while (midatareader.Read())
                            {
                                string dato1 = midatareader.GetString(0);
                                string dato2 = midatareader.GetString(1);
                                string dato3 = midatareader.GetString(2);
                                string dato4;

                                if (!midatareader.IsDBNull(3))
                                {
                                    if (float.TryParse(midatareader.GetValue(3).ToString(), out float precio))
                                    {
                                        dato4 = precio.ToString();
                                    }
                                    else
                                    {
                                        dato4 = "Valor no válido";
                                    }
                                }
                                else
                                {
                                    dato4 = "Valor no válido";
                                }

                                ListViewItem milista = listView1.Items.Add(dato1);
                                milista.SubItems.Add(dato2);
                                milista.SubItems.Add(dato3);
                                milista.SubItems.Add(dato4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form6 frmsobreautor = new Form6();
            frmsobreautor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = listView1.Items[i];
                    if (!item.Text.ToLower().Contains(searchText))
                    {
                        listView1.Items.Remove(item);
                    }
                }

                if (listView1.SelectedItems.Count == 0)
                {
                }
            }
            else
            {
                CargarDatos();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool alMenosUnoMarcado = e.NewValue == CheckState.Checked || checkedListBox1.CheckedItems.Count > 1;
            button2.Enabled = alMenosUnoMarcado;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel no está instalado en este equipo.");
                return;
            }

            Excel.Workbook workbook = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            worksheet.Cells[1, 1] = "Factura";
            worksheet.Cells[2, 1] = "Producto";
            worksheet.Cells[2, 2] = "Precio";

            // Variables para el precio total
            float precioTotal = 0;
            int rowIndex = 3;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                string nombreComponente = item.ToString();
                string precioComponente = ObtenerPrecio(nombreComponente);

                if (!string.IsNullOrEmpty(precioComponente))
                {
                    worksheet.Cells[rowIndex, 1] = nombreComponente;
                    worksheet.Cells[rowIndex, 2] = precioComponente;
                    precioTotal += float.Parse(precioComponente);
                    rowIndex++;
                }
                else
                {
                    MessageBox.Show($"No se encontró el precio para el componente {nombreComponente}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            worksheet.Cells[rowIndex, 1] = "Precio Total:";
            worksheet.Cells[rowIndex, 2] = precioTotal;

            excelApp.Visible = true;
        }

            private string ObtenerPrecio(string nombreComponente) { 
        
            string precio = "";

            try
            {
            
                conexion.Open();

          
                string query = "SELECT Precio FROM Componentes WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombreComponente);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
              
                        precio = reader["Precio"].ToString();
                    }
                    else
                    {
                        precio = "No se encontró el precio";
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el precio del componente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
       
                conexion.Close();
            }

            return precio;
        }
        public void RegistrarCompra(string nombreUsuario, string apellido1Usuario, string apellido2Usuario, string nombreComponente, string consola, DateTime fechaCompra, float precio, int cantidad, float total)
        {
            string connectionString = "server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI"; // Reemplaza esto con tu cadena de conexión real

            string query = "INSERT INTO HistorialCompra (NombreUsuario, Apellido1Usuario, Apellido2Usuario, NombreComponente, Consola, FechaCompra, Precio, Cantidad, Total) " +
                           "VALUES (@NombreUsuario, @Apellido1Usuario, @Apellido2Usuario, @NombreComponente, @Consola, @FechaCompra, @Precio, @Cantidad, @Total)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@Apellido1Usuario", apellido1Usuario);
                command.Parameters.AddWithValue("@Apellido2Usuario", apellido2Usuario);
                command.Parameters.AddWithValue("@NombreComponente", nombreComponente);
                command.Parameters.AddWithValue("@Consola", consola);
                command.Parameters.AddWithValue("@FechaCompra", fechaCompra);
                command.Parameters.AddWithValue("@Precio", precio);
                command.Parameters.AddWithValue("@Cantidad", cantidad);
                command.Parameters.AddWithValue("@Total", total);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Compra registrada correctamente en el historial.");
                    }
                    else
                    {
                        Console.WriteLine("Error al registrar la compra en el historial.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al registrar la compra en el historial: " + ex.Message);
                }
            }
        }


        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
