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
    public partial class Form2 : Form
    {
        SqlConnection conexion = new
        SqlConnection("server=(local)\\SQLEXPRESS;database=master; Integrated Security = SSPI");

        SqlCommand comandosql = new SqlCommand();

        public Form2()
        {
            InitializeComponent();


            textBox1.TextChanged += CheckFields;
            textBox2.TextChanged += CheckFields;
            textBox3.TextChanged += CheckFields;
            textBox4.TextChanged += CheckFields;
            textBox5.TextChanged += CheckFields;
            textBox6.TextChanged += CheckFields;
            textBox7.TextChanged += CheckFields;

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            CheckFields(null, null);
        }

        private void CheckFields(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                button2.Enabled = true;
            }
            else
            {
                   button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {         

            string nombre = textBox1.Text;
            string apellido1 = textBox2.Text;
            string apellido2 = textBox3.Text;
            int edad = int.Parse(textBox4.Text);
            string correo = textBox5.Text;
            string contraseña = textBox6.Text;
            string confirmaContraseña = textBox7.Text;
           

            if (contraseña != confirmaContraseña) 
            {
                MessageBox.Show("Contraseña confirmada erróneamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return; 
            }

            try
            {
                conexion.Open(); 

                comandosql.Connection = conexion;

                comandosql.CommandText = "INSERT INTO Usuarios (Nombre, Apellido1, Apellido2, Edad, Correo, Contraseña) VALUES (@Nombre, @Apellido1, @Apellido2, @Edad, @Correo, @Contraseña)";

                comandosql.Parameters.Clear();
                comandosql.Parameters.AddWithValue("@Nombre", nombre);
                comandosql.Parameters.AddWithValue("@Apellido1", apellido1);
                comandosql.Parameters.AddWithValue("@Apellido2", apellido2);
                comandosql.Parameters.AddWithValue("@Edad", edad);
                comandosql.Parameters.AddWithValue("@Correo", correo);
                comandosql.Parameters.AddWithValue("@Contraseña", contraseña);

                comandosql.ExecuteNonQuery(); 

                MessageBox.Show("Usuario agregado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar usuario: " + ex.Message);
            }
            finally
            {
                conexion.Close(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


}
  
