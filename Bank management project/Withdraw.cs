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

namespace Bank_management_project
{
    public partial class Withdraw : Form
    {
        private string connectionString = "Data Source=LAPTOP-Q7KTNQDN\\SQLEXPRESS;Initial Catalog=\"Bank Management Database\";Integrated Security=True";
        public Withdraw()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            openingForm form = new openingForm();
            form.Show();
            this.Hide();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            int accountId = int.Parse(accounID_textBox.Text);
            decimal withdrawalAmount = decimal.Parse(WithdrawalAmount_textBox.Text);
            withdraw(accountId, withdrawalAmount);
        }

        private void withdraw(int accountId, decimal amount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT AccountAmount FROM Account WHERE AccountID = @AccountId", connection);
                selectCommand.Parameters.AddWithValue("@AccountId", accountId);
                decimal currentBalance = (decimal)selectCommand.ExecuteScalar();

                if (currentBalance >= amount)
                {
                    decimal newBalance = currentBalance - amount;
                    SqlCommand updateCommand = new SqlCommand("UPDATE Account SET AccountAmount = @NewBalance WHERE AccountID = @AccountId", connection);
                    updateCommand.Parameters.AddWithValue("@NewBalance", newBalance);
                    updateCommand.Parameters.AddWithValue("@AccountId", accountId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Withdrawal successful. New balance: " + newBalance, "Success");
                    }
                    else
                    {
                        MessageBox.Show("Withdrawal failed.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient balance.", "Error");
                }
            }
        }

    }
}
