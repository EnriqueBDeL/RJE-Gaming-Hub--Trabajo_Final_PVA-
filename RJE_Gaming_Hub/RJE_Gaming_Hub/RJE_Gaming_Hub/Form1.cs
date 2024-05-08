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

namespace PF_26935244J_48846253A_24408975H
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI");
        SqlCommand comandosql = new SqlCommand();

        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form2 frmsobreautor = new Form2();
            frmsobreautor.ShowDialog();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correo = textBox1.Text;
            string contraseña = textBox2.Text;

            try
            {
                conexion.Open();
                comandosql.Connection = conexion;

                comandosql.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo AND Contraseña = @Contraseña";

                comandosql.Parameters.Clear(); 
                comandosql.Parameters.AddWithValue("@Correo", correo);
                comandosql.Parameters.AddWithValue("@Contraseña", contraseña);

                int result = (int)comandosql.ExecuteScalar(); 

                if (result > 0) 
                {
                    Form3 frmsobreautor = new Form3();
                    frmsobreautor.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error al encontrar tu usuario. Por favor, verifica tu correo y contraseña.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar credenciales: " + ex.Message);
            }
            finally
            {
                conexion.Close(); 
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

