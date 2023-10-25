using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Bank_management_project
{
    public partial class openingForm : Form
    {
        public openingForm()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = "Data Source=LAPTOP-Q7KTNQDN\\SQLEXPRESS;Initial Catalog=\"Bank Management Database\";Integrated Security=True";

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                var insertQuery = "insert into Account values(@AccountId, @Accountname, @AccountAmount)";

                SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@AccountId", accounID_textBox.Text);
                sqlCommand.Parameters.AddWithValue("@AccountName", AccountName_textBox.Text);
                sqlCommand.Parameters.AddWithValue("@AccountAmount", double.Parse(AccountAmount_textBox.Text));

                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Data Inserted Succesfully!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somthing went wrong");
            }


        }

        private void amountButton_Click(object sender, EventArgs e)
        {
            //search
            try
            {
                var connectionString = "Data Source=LAPTOP-Q7KTNQDN\\SQLEXPRESS;Initial Catalog=\"Bank Management Database\";Integrated Security=True";

                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string readCommand = "";

                /*if (AccountId.Text.ToString() == "")
                {
                    readCommand = "select * from student";
                }
                else
                {
                    readCommand = "select * from student where matrixId=@matrixId";
                }*/
                readCommand = "select * from Account where AccountId=@AccountId";
                SqlCommand sqlCommand = new SqlCommand(readCommand, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@AccountId", accounID_textBox.Text);


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();

                sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                //MessageBox.Show(dataTable);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }

        }

        private void withdrawButton_Click(object sender, EventArgs e)
        {
            Withdraw form = new Withdraw();
            form.Show();
            this.Hide();
        }

        private void depositeButton_Click(object sender, EventArgs e)
        {
            Deposite form = new Deposite();
            form.Show();
            this.Hide();
        }
    }
}
