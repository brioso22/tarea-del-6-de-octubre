using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace adelnato_de_3
{
    public partial class Form1 : Form
    {
        Empleado Empleado = new Empleado();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "") {

                errorProvider1.SetError(txtNombre, "no ingreso el nombre");
                txtNombre.Focus();
                return;

            }
            else if (txtDUI.Text == "")
            {

                errorProvider1.SetError(txtDUI, "no ingreso el Dui");
                txtDUI.Focus();
                return;

            }

            
            else if (txtSalario.Text == "")
            {

                errorProvider1.SetError(txtSalario , "no ingreso el Salario ");
                txtSalario.Focus();
                return;

            }
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");

            conexion.Open();

            Empleado.Nombre = txtNombre.Text;
            Empleado.Dui = Convert.ToInt32(txtDUI.Text);
            Empleado.Salario = Convert.ToDouble(txtSalario.Text);
            Empleado.Afp = Empleado.AFP(Empleado.Salario);
            txtAFP.Text = Convert.ToString(Empleado.Afp);

            string cadena = "insert into [Empleados] (Nombre, Dui, Salario, Afp) values ('" + Empleado.Nombre + "','" + Empleado.Dui + "','" + Empleado.Salario + "','" + Empleado.Afp + "') ";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

            label7.Text = "SE GUARDO CON EXITO EL REGISTRO";
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");

            conexion.Open();

            Int32 cod;

            if (!Int32.TryParse(txtconsulta.Text, out cod))

            {
                errorProvider1.SetError(txtconsulta, "No ingresó el salario de forma correcta");
                txtSalario.Focus();
                return;
            }
            errorProvider1.SetError(txtconsulta, "");


            string cadena = "select id, nombre, dui, salario, afp from Empleados where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {

                dataGridView1.Rows[0].Cells[0].Value = registro.GetInt32(0);
                dataGridView1.Rows[0].Cells[1].Value = registro.GetString(1);
                dataGridView1.Rows[0].Cells[2].Value = registro.GetString(2);
                dataGridView1.Rows[0].Cells[3].Value = registro.GetDecimal(3);
                dataGridView1.Rows[0].Cells[4].Value = registro.GetDecimal(4);
            }
            else
                MessageBox.Show("No existe un Empleado con el código ingresado");
            conexion.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();

            string cod = txtconsulta.Text;

            string cadena = "delete from [Empleados] where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            int cant;
            cant = comando.ExecuteNonQuery();

            if (cant == 1)
            {

                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                MessageBox.Show("se borro el registro");
            }
            else {

                MessageBox.Show("se selecciono ningun registro");
            
            }
            conexion.Close();

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            string Dnombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string Ddui = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string Dsalario = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string DAFP = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            string cadena = "update [Empleados] set nombre='" + Dnombre + "', dui='" + Ddui + "', salario='" + Dsalario + "', afp='" + DAFP + "' where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            int cant;
            cant = comando.ExecuteNonQuery();

            if (cant == 1)
            {

                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
                MessageBox.Show("se modificaron los datos");
            }
            else
            {

                MessageBox.Show("se selecciono ningun registro");

            }
            conexion.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            string cadena = "select id, nombre, dui,salario,afp from Empleados";
            SqlCommand comando = new SqlCommand(cadena,conexion);
            SqlDataReader registro = comando.ExecuteReader();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            if (registro.HasRows)
            {
                while (registro.Read())
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = registro.GetInt32(0);
                    dataGridView1.Rows[n].Cells[1].Value = registro.GetString(1);
                    dataGridView1.Rows[n].Cells[2].Value = registro.GetString(2);
                    dataGridView1.Rows[n].Cells[3].Value = registro.GetDecimal(3);
                    dataGridView1.Rows[n].Cells[4].Value = registro.GetDecimal(4);

                }

            }
            else {

                MessageBox.Show("no hay registros");
            }



        }

    }
}



       

