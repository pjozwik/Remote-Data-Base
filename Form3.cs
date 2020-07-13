using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WBD_GUI
{
    public partial class Form3 : Form
    {
        string connectionString = "DATA SOURCE = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)" +
                                               "(HOST = ora3.elka.pw.edu.pl)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ORA3INF.ELKA.PW.EDU.PL)));" +
                                               "User ID = pjozwik1; password = pjozwik1";
        string ID;
        DataTable dataTable3;

        public Form3(string clientID)
        {
            InitializeComponent();
            ID = clientID;
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM PRODUKTY", connectionString);
             dataTable3 = new DataTable();
            adapter.Fill(dataTable3);
            dataGridViewProducts.DataSource = dataTable3;

            comboPayment.Items.Add("GOTOWKA");
            comboPayment.Items.Add("PRZELEW");
            comboPayment.Items.Add("BLIK");

            comboPayment.Text = "- - Wybierz - -";

            
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    if (comboPayment.Text != "- - Wybierz - -" && !string.IsNullOrEmpty(txtAmount.Text))
                    {
                        

                        connection.Open();
                        OracleCommand insertCommand = new OracleCommand();
                        OracleCommand insertCommand2 = new OracleCommand();
                        OracleCommand selectCommand = new OracleCommand();


                        DateTime localDate = DateTime.Now;
                        string shortDate = localDate.ToShortDateString();
                        string date = shortDate.Split('.').GetValue(2) + "-" + shortDate.Split('.').GetValue(1) + "-" + shortDate.Split('.').GetValue(0);
                        string fax = shortDate.Split('.').GetValue(2) + "/" + shortDate.Split('.').GetValue(1) + "/" + shortDate.Split('.').GetValue(0) + "/";


                        Random random = new Random();
                        int number = random.Next(1000, 5000);

                        //wprowadzenie nowego zamowienia do bazy danych
                        string command2 = "INSERT INTO ZAMOWIENIA VALUES(null," + "'" + date + "' , '" + fax + number + "', '" + date + "' , '" + comboPayment.Text + "' , 'N' ," + ID + ")";
                        insertCommand.Connection = connection;
                        insertCommand.CommandText = command2;
                        insertCommand.CommandType = CommandType.Text;
                        insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();

                        //zwrócenie ID ostatniego dodanego zamówienia
                        string command3 = "SELECT  MAX(ID_ZAMOWIENIA) FROM ZAMOWIENIA";
                        selectCommand.Connection = connection;
                        selectCommand.CommandText = command3;
                        selectCommand.CommandType = CommandType.Text;
                        var count = selectCommand.ExecuteScalar();
                        selectCommand.Dispose();


                        string command4 = "INSERT INTO SZCZEGOLY_ZAMOWIENIA VALUES(" + count + "," + dataGridViewProducts.CurrentRow.Cells["ID_PRODUKTU"].Value.ToString() + "," + txtAmount.Text + ")";
                        insertCommand2.Connection = connection;
                        insertCommand2.CommandText = command4;
                        insertCommand2.CommandType = CommandType.Text;
                        insertCommand2.ExecuteNonQuery();
                        insertCommand2.Dispose();


                        MessageBox.Show("Zamówienie zostało złożone!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        comboPayment.Text = "- - Wybierz - -";
                        txtAmount.Clear();
                        comboPayment.Enabled = false;
                        txtAmount.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Błąd podczas składania zamówienia: Uzupełnij wszystkie pola", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas składania zamówienia: " + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridViewProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label1.Enabled = false;
            comboPayment.Enabled = false;
            label2.Enabled = false;
            txtAmount.Enabled = false;

            if (!string.IsNullOrEmpty(dataGridViewProducts.CurrentRow.Cells["ID_PRODUKTU"].Value.ToString()))
            {
                label1.Enabled = true;
                comboPayment.Enabled = true;
                label2.Enabled = true;
                txtAmount.Enabled = true;
            }

        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            
            
                DataView dv = dataTable3.DefaultView;
                dv.RowFilter = string.Format("NAZWA_TOWARU LIKE '%{0}%'", txtSearchProduct.Text);
                dataGridViewProducts.DataSource = dv.ToTable();
            
        }
    }
}
