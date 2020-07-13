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
    public partial class Form2 : Form
    {
        string ID;
        string ID_2;
        string ID_3;

        public Form2(string ID,string ID_2,string ID_3)
        {
            InitializeComponent();
            this.ID = ID;
            this.ID_2 = ID_2;
            this.ID_3 = ID_3;
        }
        string connectionString = "DATA SOURCE = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)" +
                                              "(HOST = ora3.elka.pw.edu.pl)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ORA3INF.ELKA.PW.EDU.PL)));" +
                                              "User ID = pjozwik1; password = pjozwik1";

        private void Form2_Load(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                //connection.Open();
                OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM ADRESY WHERE ID_ADRESU= " + ID_2, connectionString);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                OracleDataAdapter adapter2 = new OracleDataAdapter("SELECT * FROM WYNAGRODZENIA WHERE ID_PRACOWNIKA= " + ID, connectionString);
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);
                dataGridView2.DataSource = dataTable2;

                OracleDataAdapter adapter3 = new OracleDataAdapter("SELECT * FROM STANOWISKA WHERE ID_STANOWISKO= " + ID_3, connectionString);
                DataTable dataTable3 = new DataTable();
                adapter3.Fill(dataTable3);
                dataGridView3.DataSource = dataTable3;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
