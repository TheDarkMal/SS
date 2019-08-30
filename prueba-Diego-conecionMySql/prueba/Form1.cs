using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace prueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const string conecctionString = "datasource=localhost;port=3306;username=root;password=Diego Diaz Marin;database=ejemplo;";

        MySqlConnection conexion = new MySqlConnection(conecctionString);
        private void Form1_Load(object sender, EventArgs e)
        {
            mosttrar_BD();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.Controls)
            {
                if(c is TextBox)
                {
                    c.Text = "";
                }
                c.Enabled = true;
            }
            textBox1.Focus();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string query = "insert into ejemplo.nombres (nombre, documento, tipodocumento) values (?nombre, ?documento, ?tipodocumento);";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("?nombre", textBox1.Text);
            cmd.Parameters.AddWithValue("?documento", textBox2.Text);
            cmd.Parameters.AddWithValue("?tipodocumento", textBox3.Text);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            mosttrar_BD();
        }

        public void mosttrar_BD()
        {
            string query = "select * from nombres";
            
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
            DataSet dst = new DataSet();
            dap.Fill(dst);

            conexion.Open();
            dataGridView1.DataSource = dst.Tables[0];
            conexion.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string query = "update ejemplo.nombres set nombre = ?nombre, documento = ?documento, tipodocumento = ?tipodocumento where id = ?id";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("?nombre", textBox1.Text);
            cmd.Parameters.AddWithValue("?documento", textBox2.Text);
            cmd.Parameters.AddWithValue("?tipodocumento", textBox3.Text);
            cmd.Parameters.AddWithValue("?id", Convert.ToInt32(textBox4.Text));
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
            mosttrar_BD();
        }
    }
}
