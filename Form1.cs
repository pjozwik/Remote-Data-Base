using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WBD_GUI
{
    public partial class Form1 : Form
    {
        string connectionString = "DATA SOURCE = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)" +
                                               "(HOST = ora3.elka.pw.edu.pl)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ORA3INF.ELKA.PW.EDU.PL)));" +
                                               "User ID = pjozwik1; password = pjozwik1";
        string gender;
        string login;
        string password;
        int position;
        OracleConnection connection;
        DataTable dataTable;
        DataTable dataTable2;
        DataTable dataTable3;
        DataTable dataTable4;

        string[] itemsFromSelectedRow = new string[13];
        string[] itemsFromSelectedRow2 = new string[6];
        Form4 form4;

        public Form1(string login, string password, Form4 form4)
        {
            this.login = login;
            this.password = password;
            this.form4 = form4;
            if (login.Equals("admin") && password.Equals("admin"))
            {
                InitializeComponent();
                updateGridView(0);
                updateGridViewCustomers(0);
                updateGridViewProducts();
                upgradeDataViewOrders(0);
                comboTabels.Items.Add("Adresy");
                comboTabels.Items.Add("Wynagrodzenia");
                comboTabels.Items.Add("Pracownicy");
                comboTabels.Text = "Pracownicy";


            }
            else if (login.Equals("eltom@gmail.com") && password.Equals("ELTOM1234"))
            {
                InitializeComponent();
                tabControl1.Controls.Remove(tabPage1);
                tabControl1.Controls.Remove(tabPage3);
                updateGridViewProducts();
                upgradeDataViewOrders(1);
                grpbClientInfo.Enabled = false;
                grpbProducts.Enabled = false;
                btnAddCustomer.Enabled = false;
                btnDeleteCustomer.Enabled = false;
                btnUpdateCustomer.Enabled = false;
                btnResetCustomer.Enabled = false;
                updateGridViewCustomers(1);

            }
            else if (login.Equals("Lukasz98") && password.Equals("Lukasz98"))
            {
                InitializeComponent();
                updateGridView(1);
                tabControl1.Controls.Remove(tabCustomers);
                updateGridViewCustomers(0);
                updateGridViewProducts();
                upgradeDataViewOrders(0);
                comboTabels.Items.Add("Adresy");
                comboTabels.Items.Add("Wynagrodzenia");
                comboTabels.Items.Add("Pracownicy");
                comboTabels.Text = "Pracownicy";
                btnAdd.Enabled = false;
                gpbAddress.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
            }
            else
            {
                MessageBox.Show("Błedne hasło lub login", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            comboFormOfEmpl.Items.Add("UMOWA O PRACE");
            comboFormOfEmpl.Items.Add("UMOWA O DZIELO");
            comboFormOfEmpl.Items.Add("STAZ");
            comboFormOfEmpl.Items.Add("UMOWA ZLECENIE");
            comboFormOfEmpl.Items.Add("UMOWA AGENCYJNA");
            comboFormOfEmpl.Items.Add("SAMOZATRUDNIENIE");

            comboOfEmpl2.Items.Add("UMOWA O PRACE");
            comboOfEmpl2.Items.Add("UMOWA O DZIELO");
            comboOfEmpl2.Items.Add("STAZ");
            comboOfEmpl2.Items.Add("UMOWA ZLECENIE");
            comboOfEmpl2.Items.Add("UMOWA AGENCYJNA");
            comboOfEmpl2.Items.Add("SAMOZATRUDNIENIE");

            comboGender.Items.Add("Kobieta");
            comboGender.Items.Add("Mężczyzna");

            comboGender2.Items.Add("Kobieta");
            comboGender2.Items.Add("Mężczyzna");
            comboGender2.Text = "--Wybierz--";

            comboOfEmpl2.Text = "--Wybierz--";
            comboFormOfEmpl.Text = "--Wybierz--";
            comboGender.Text = "--Wybierz--";

            comboPosition.Items.Add("Magazynier");
            comboPosition.Items.Add("Kierowca");
            comboPosition.Text = "--Wybierz--";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateGridView(0);
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboGender.Text.Equals("Kobieta")) gender = "K";
            if (comboGender.Text.Equals("Mężczyzna")) gender = "M";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand insertCommand = new OracleCommand();
                    OracleCommand insertCommand2 = new OracleCommand();
                    OracleCommand selectCommand = new OracleCommand();
                    OracleCommand selectCommand2 = new OracleCommand();
                    OracleCommand insertCommand3 = new OracleCommand();

                    
                    string Pesel = null;

                    if (comboGender.Text.Equals("Kobieta")) gender = "K";
                    if (comboGender.Text.Equals("Mężczyzna")) gender = "M";

                    if (check())
                    {
                        if (comboPosition.Text == "Magazynier") position = 1;
                        if (comboPosition.Text == "Kierowca") position = 2;


                        //wprowadzenie nowego adresu do bazy danych
                        string command2 = "INSERT INTO Adresy VALUES(null," + "'" + txtCity.Text + "' , '" + txtBuildingNumber.Text + "' , '" + txtLocalNumber.Text + "' ," + "'" + txtPosteCode.Text + "' ," + "'" + txtStreet.Text + "')";
                        insertCommand.Connection = connection;
                        insertCommand.CommandText = command2;
                        insertCommand.CommandType = CommandType.Text;
                        insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();


                        //zwrócenie ID ostatniego dodanego adresu
                        string command3 = "SELECT  MAX(ID_ADRESU) FROM ADRESY";
                        selectCommand.Connection = connection;
                        selectCommand.CommandText = command3;
                        selectCommand.CommandType = CommandType.Text;
                        var count = selectCommand.ExecuteScalar();
                        selectCommand.Dispose();

                        //wprowadzenie nowego Pracownika do bazy danych
                        string command = "INSERT INTO Pracownicy VALUES(null," + "'" + txtName.Text + "'" + "," + "'" + txtSurname.Text + "'" + "," + "'" + gender + "'" + "," + "date '" + txtBirthDate.Text + "' " + ", '" + txtPesel.Text + "' ," + "date '" + txtDateEmplo.Text + "'" + "," + "'" + comboFormOfEmpl.Text + "'" + "," + txtPhoneNumber.Text + "," + txtAccountNumber.Text + ",17," + count + "," + position + ")";
                        insertCommand2.Connection = connection;
                        insertCommand2.CommandText = command;
                        insertCommand2.CommandType = CommandType.Text;
                        insertCommand2.ExecuteNonQuery();
                        insertCommand2.Dispose();


                        //zwrócenie ID ostatniego dodanego Pracownika
                        string command4 = "SELECT  MAX(ID_PRACOWNIKA) FROM PRACOWNICY";
                        selectCommand2.Connection = connection;
                        selectCommand2.CommandText = command4;
                        selectCommand2.CommandType = CommandType.Text;
                        var count2 = selectCommand2.ExecuteScalar();
                        selectCommand2.Dispose();

                        
                            //wprowadzenie nowego wynagrodzenia do bazy danych
                            string command5 = "INSERT INTO WYNAGRODZENIA VALUES(null, date '" + txtSalaryDate.Text + "'," + txtSalaryBrutto.Text + ", " + txtSalaryNetto.Text + "," + count2 + ")";
                            insertCommand3.Connection = connection;
                            insertCommand3.CommandText = command5;
                            insertCommand3.CommandType = CommandType.Text;
                            insertCommand3.ExecuteNonQuery();
                            insertCommand3.Dispose();
                        

                        this.updateGridView(0);
                        MessageBox.Show("Wiersz został dodany!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (OracleException exp)
                {
                    if (exp.Number == 984)
                    {
                        MessageBox.Show("Błąd podczas dodawania wiersza: błedny format danych ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void updateGridView(int choose)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    OracleDataAdapter adapter = null;
                    if (choose == 0)
                    {
                        adapter = new OracleDataAdapter("SELECT * FROM Pracownicy", connectionString);
                    }
                    else if (choose == 1)
                    {
                        adapter = new OracleDataAdapter("SELECT * FROM Pracownicy WHERE NAZWISKO= 'Chmielewski'", connectionString);
                    }
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }catch{
                    MessageBox.Show("Błąd podczas łączenia z serwerem ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
                
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand deleteCommand = new OracleCommand();
                    OracleCommand deleteCommand2 = new OracleCommand();
                    OracleCommand deleteCommand3 = new OracleCommand();


                    string command3 = "DELETE FROM WYNAGRODZENIA WHERE ID_PRACOWNIKA=" + itemsFromSelectedRow[0];
                    deleteCommand3.Connection = connection;
                    deleteCommand3.CommandText = command3;
                    deleteCommand3.CommandType = CommandType.Text;
                    deleteCommand3.ExecuteNonQuery();
                    deleteCommand3.Dispose();

                    string command2 = "DELETE FROM ADRESY WHERE ID_ADRESU=" + itemsFromSelectedRow[11];
                    deleteCommand2.Connection = connection;
                    deleteCommand2.CommandText = command2;
                    deleteCommand2.CommandType = CommandType.Text;
                    deleteCommand2.ExecuteNonQuery();
                    deleteCommand2.Dispose();

                    string command = "DELETE FROM PRACOWNICY WHERE ID_PRACOWNIKA=" + itemsFromSelectedRow[0];
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = command;
                    deleteCommand.CommandType = CommandType.Text;
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Dispose();

                    updateGridView(0);
                    clearTextBoxes();
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnAdd.Enabled = true;
                    MessageBox.Show("Wiersz został usuniety!");

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas usuwania wiersza: " + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }




        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OracleCommand selectCommand = new OracleCommand();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            clearTextBoxes();
            itemsFromSelectedRow[0] = dataGridView1.CurrentRow.Cells["ID_PRACOWNIKA"].Value.ToString();
            itemsFromSelectedRow[11] = dataGridView1.CurrentRow.Cells["ID_ADRESU"].Value.ToString();
            itemsFromSelectedRow[12] = dataGridView1.CurrentRow.Cells["ID_STANOWISKO"].Value.ToString();

            
            if (comboTabels.Text == "Pracownicy" && !string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells["ID_PRACOWNIKA"].Value.ToString()) && login.Equals("admin"))
            {
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;


                itemsFromSelectedRow[1] = dataGridView1.CurrentRow.Cells["IMIE"].Value.ToString();
                itemsFromSelectedRow[2] = dataGridView1.CurrentRow.Cells["NAZWISKO"].Value.ToString();

                if (dataGridView1.CurrentRow.Cells["PLEC"].Value.ToString().Equals("K")) itemsFromSelectedRow[3] = "Kobieta";
                if (dataGridView1.CurrentRow.Cells["PLEC"].Value.ToString().Equals("M")) itemsFromSelectedRow[3] = "Mężczyzna";

                
                itemsFromSelectedRow[4] = dataGridView1.CurrentRow.Cells["DATA_URODZENIA"].Value.ToString();
                itemsFromSelectedRow[5] = dataGridView1.CurrentRow.Cells["PESEL"].Value.ToString();
                itemsFromSelectedRow[6] = dataGridView1.CurrentRow.Cells["DATA_ZATRUDNIENIA"].Value.ToString();
                itemsFromSelectedRow[7] = dataGridView1.CurrentRow.Cells["FORMA_ZATRUDNIENIA"].Value.ToString();
                itemsFromSelectedRow[8] = dataGridView1.CurrentRow.Cells["NUMER_TELEFONU"].Value.ToString();
                itemsFromSelectedRow[9] = dataGridView1.CurrentRow.Cells["NUMER_KONTA"].Value.ToString();
                itemsFromSelectedRow[10] = dataGridView1.CurrentRow.Cells["ID_HURTOWNI"].Value.ToString();


                txtPosteCode.ForeColor = Color.Black;

                txtName.Text = itemsFromSelectedRow[1];
                txtSurname.Text = itemsFromSelectedRow[2];

                //po obsluzeniu eventu w pole daty wpisuje sie data w formacie dd.mm.yyyy natomiaast do zapytania potrzebujemy yyyy-mm-dd dlatego odwracam kolejność
                string[] splitted = itemsFromSelectedRow[4].Split('.');
                txtBirthDate.ForeColor = Color.Black;
                txtBirthDate.Text = splitted[2].Split(' ').GetValue(0) + "-" + splitted[1] + "-" + splitted[0];

                txtPesel.Text = itemsFromSelectedRow[5];

                //ta sama sytuacja z data
                string[] splitted1 = itemsFromSelectedRow[6].Split('.');
                txtDateEmplo.ForeColor = Color.Black;
                txtDateEmplo.Text = splitted1[2].Split(' ').GetValue(0) + "-" + splitted1[1] + "-" + splitted1[0];

                txtPhoneNumber.Text = itemsFromSelectedRow[8];
                txtAccountNumber.Text = itemsFromSelectedRow[9];

                comboGender.Text = itemsFromSelectedRow[3];
                comboFormOfEmpl.Text = itemsFromSelectedRow[7];

                if (dataGridView1.CurrentRow.Cells["ID_STANOWISKO"].Value.ToString().Equals("1")) comboPosition.Text = "Magazynier";
                if (dataGridView1.CurrentRow.Cells["ID_STANOWISKO"].Value.ToString().Equals("2")) comboPosition.Text = "Kierowca";

                txtSalaryDate.ForeColor = Color.Black;
                getEmployeeAdress();
                getSalary();

            }
        }

        private void clearTextBoxes()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtBirthDate.Clear();
            txtPesel.Clear();
            txtDateEmplo.Clear();
            comboFormOfEmpl.Text = "- -Wybierz- -";
            comboGender.Text = "- -Wybierz- -";
            comboPosition.Text = "- -Wybierz - -";
            txtCity.Clear();
            txtStreet.Clear();
            txtPosteCode.Clear();
            txtBuildingNumber.Clear();
            txtLocalNumber.Clear();
            txtSalaryBrutto.Clear();
            txtSalaryNetto.Clear();
            txtSalaryDate.Clear();
            txtPhoneNumber.Clear();
            txtAccountNumber.Clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.clearTextBoxes();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtBirthDate.ForeColor = Color.Silver;
            txtBirthDate.Text = "yyyy-mm-dd";
            txtPosteCode.ForeColor = Color.Silver;
            txtPosteCode.Text = "xx-xxx";
            txtSalaryDate.ForeColor = Color.Silver;
            txtSalaryDate.Text = "yyyy-mm-dd";
            txtDateEmplo.ForeColor = Color.Silver;
            txtDateEmplo.Text = "yyyy-mm-dd";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
            form4.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand updateCommand = new OracleCommand();
                    OracleCommand updateCommand2 = new OracleCommand();
                    OracleCommand updateCommand3 = new OracleCommand();

                    if (check())
                    {

                        if (comboPosition.Text == "Magazynier") position = 1;
                        if (comboPosition.Text == "Kierowca") position = 2;

                        if (comboGender.Text.Equals("Kobieta")) gender = "K";
                        if (comboGender.Text.Equals("Mężczyzna")) gender = "M";

                        string command = "UPDATE Pracownicy SET IMIE =" + "'" + txtName.Text + "'" + "," + "NAZWISKO = '" + txtSurname.Text + "'" + "," + "PLEC = '" + gender + "'" + "," + "DATA_URODZENIA = date '" + txtBirthDate.Text + "' " + ", PESEL = '" + txtPesel.Text + "'," + "DATA_ZATRUDNIENIA = date '" + txtDateEmplo.Text + "'" + "," + "FORMA_ZATRUDNIENIA = '" + comboFormOfEmpl.Text + "'" + ", NUMER_TELEFONU = " + txtPhoneNumber.Text + ",NUMER_KONTA = " + txtAccountNumber.Text + ",ID_HURTOWNI = 17,ID_ADRESU = " + itemsFromSelectedRow[11] + ", ID_STANOWISKO = " + position + " WHERE ID_PRACOWNIKA = " + itemsFromSelectedRow[0];
                        updateCommand.Connection = connection;
                        updateCommand.CommandText = command;
                        updateCommand.CommandType = CommandType.Text;
                        updateCommand.ExecuteNonQuery();
                        updateCommand.Dispose();

                        string command2 = "UPDATE ADRESY SET MIASTO= '" + txtCity.Text + "' , NUMER_BUDYNKU= '" + txtBuildingNumber.Text + "' , NUMER_LOKALU= '" + txtLocalNumber.Text + "' , KOD_POCZTOWY='" + txtPosteCode.Text + "' , ULICA= '" + txtStreet.Text + "' WHERE ID_ADRESU= " + itemsFromSelectedRow[11];
                        updateCommand2.Connection = connection;
                        updateCommand2.CommandText = command2;
                        updateCommand2.CommandType = CommandType.Text;
                        updateCommand2.ExecuteNonQuery();
                        updateCommand2.Dispose();

                        string command3 = "UPDATE WYNAGRODZENIA SET KWOTA_BRUTTO= " + txtSalaryBrutto.Text + " , KWOTA_NETTO=" + txtSalaryNetto.Text + ", DATA_WYPLATY= '" + txtSalaryDate.Text + "' WHERE ID_PRACOWNIKA= " + itemsFromSelectedRow[0];
                        updateCommand3.Connection = connection;
                        updateCommand3.CommandText = command3;
                        updateCommand3.CommandType = CommandType.Text;
                        updateCommand3.ExecuteNonQuery();
                        updateCommand3.Dispose();


                        updateGridView(0);
                        btnAdd.Enabled = true;
                        btnDelete.Enabled = false;
                        btnUpdate.Enabled = false;
                        clearTextBoxes();
                        MessageBox.Show("Wiersz został zaktualizowany!");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas aktualizowania wiersza: " + exp.Message);
                }

            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable.DefaultView;

            if (comboTabels.Text.Equals("Pracownicy"))
                dv.RowFilter = string.Format("IMIE LIKE '%{0}%' OR NAZWISKO LIKE '%{0}%' OR NUMER_TELEFONU LIKE '%{0}%'", txtSearch.Text);

            if (comboTabels.Text.Equals("Adresy"))
                dv.RowFilter = string.Format("MIASTO LIKE '%{0}%' OR ULICA LIKE '%{0}%' OR KOD_POCZTOWY LIKE '%{0}%'", txtSearch.Text);

            if (comboTabels.Text.Equals("Wynagrodzenia"))
                dv.RowFilter = string.Format("KWOTA_NETTO={0}", txtSearch.Text);

            dataGridView1.DataSource = dv.ToTable();


        }

        private void txtSearch2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DataView dv = dataTable.DefaultView;
                //dv.RowFilter = string.Format("IMIE LIKE '%{0}%' OR NAZWISKO LIKE '%{0}%' OR NUMER_TELEFONU LIKE '%{0}%'", txtSearch.Text);
                //dataGridView1.DataSource = dv.ToTable();


                OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM Pracownicy WHERE DATA_URODZENIA BETWEEN date '" + txtSearch2.Text + "' AND date'" + txtSearch3.Text + "'", connectionString);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clearTextBoxes();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            if (comboTabels.Text == "Pracownicy" && !string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells["ID_PRACOWNIKA"].Value.ToString()))
            {
                Form2 form2 = new Form2(itemsFromSelectedRow[0], itemsFromSelectedRow[11], itemsFromSelectedRow[12]);
                form2.Show();
            }
        }

        private void comboTabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string command = null;
            if (comboTabels.SelectedItem.Equals("Adresy"))
            {
                if (login.Equals("admin"))
                    command = "SELECT * FROM ADRESY";
                if (login.Equals("Lukasz98"))
                {
                    command = "SELECT a.Id_Adresu, a.Miasto, a.Numer_Budynku, a.Numer_lokalu, a.Kod_pocztowy, a.Ulica FROM ADRESY a " +
                           "JOIN PRACOWNICY p ON a.id_adresu = p.id_adresu " +
                           "WHERE p.nazwisko = 'Chmielewski'";
                }
                gpbAddress.Enabled = false;
                groupBox1.Enabled = false;
                groupBox4.Enabled = false;
                groupBox3.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnReset.Enabled = false;

            }
            if (comboTabels.SelectedItem.Equals("Wynagrodzenia"))
            {
                if (login.Equals("admin"))
                    command = "SELECT * FROM Wynagrodzenia";
                if (login.Equals("Lukasz98"))
                    command = "SELECT * FROM Wynagrodzenia WHERE ID_PRACOWNIKA=" + dataGridView1.CurrentCell.Value.ToString();
                gpbAddress.Enabled = false;
                groupBox1.Enabled = false;
                groupBox4.Enabled = false;
                groupBox3.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
                btnReset.Enabled = false;
            }
            if (comboTabels.SelectedItem.Equals("Pracownicy"))
            {
                if (login.Equals("admin"))
                {
                    command = "SELECT * FROM Pracownicy";
                    gpbAddress.Enabled = true;
                    groupBox1.Enabled = true;
                    groupBox4.Enabled = true;
                    groupBox3.Enabled = true;
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnReset.Enabled = true;
                }
                if (login.Equals("Lukasz98"))
                    command = "SELECT * FROM Pracownicy WHERE NAZWISKO= 'Chmielewski'";

            }


            OracleDataAdapter adapter1 = new OracleDataAdapter(command, connectionString);
            dataTable = new DataTable();
            adapter1.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            adapter1.Dispose();
        }

        //FILTRY
        private void btnFiltr_Click(object sender, EventArgs e)
        {
            string gender = null;

            try
            {
                if (comboGender2.Text.Equals("Kobieta")) gender = "K";
                else if (comboGender2.Text.Equals("Mężczyzna")) gender = "M";

                if (txtSearch2.Text != "yyyy-mm-dd" && txtSearch3.Text != "yyyy-mm-dd" && comboGender2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data uro i plec
                    dv.RowFilter = string.Format("DATA_URODZENIA >= #{0}# AND DATA_URODZENIA <= #{1}# AND PLEC LIKE '%{2}%'", txtSearch2.Text, txtSearch3.Text, gender);
                    dataGridView1.DataSource = dv.ToTable();

                }
                else if (txtSearch4.Text != "yyyy-mm-dd" && txtSearch5.Text != "yyyy-mm-dd" && comboGender2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data zatrudnienia i plec 
                    dv.RowFilter = string.Format("DATA_ZATRUDNIENIA >= #{0}# AND DATA_ZATRUDNIENIA <= #{1}# AND PLEC LIKE '%{2}%'", txtSearch2.Text, txtSearch3.Text, gender);
                    dataGridView1.DataSource = dv.ToTable();

                }
                else if (txtSearch2.Text != "yyyy-mm-dd" && txtSearch3.Text != "yyyy-mm-dd" && txtSearch4.Text != "yyyy-mm-dd" && txtSearch5.Text != "yyyy-mm-dd")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data zatrudnienia i data urodzenia
                    dv.RowFilter = string.Format("DATA_URODZENIA >= #{0}# AND DATA_URODZENIA <= #{1}# AND DATA_ZATRUDNIENIA >= #{2}# AND DATA_ZATRUDNIENIA <= #{3}#", txtSearch2.Text, txtSearch3.Text, txtSearch4.Text, txtSearch5.Text);
                    dataGridView1.DataSource = dv.ToTable();

                }
                else if (comboGender2.Text != "--Wybierz--" && comboOfEmpl2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr plec z forma zatrudnienia
                    dv.RowFilter = string.Format("PLEC LIKE '%{0}%' AND FORMA_ZATRUDNIENIA LIKE '%{1}%'", gender, comboOfEmpl2.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (txtSearch2.Text != "yyyy-mm-dd" && txtSearch3.Text != "yyyy-mm-dd" && comboOfEmpl2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data uro i forma zatrudnienia
                    dv.RowFilter = string.Format("DATA_URODZENIA >= #{0}# AND DATA_URODZENIA <= #{1}# AND FORMA_ZATRUDNIENIA LIKE '%{2}%'", txtSearch2.Text, txtSearch3.Text, comboOfEmpl2.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (txtSearch4.Text != "yyyy-mm-dd" && txtSearch5.Text != "yyyy-mm-dd" && comboOfEmpl2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data zatrudnienia i forma zatrudnienia
                    dv.RowFilter = string.Format("DATA_ZATRUDNIENIA >= #{0}# AND DATA_ZATRUDNIENIA <= #{1}# AND FORMA_ZATRUDNIENIA LIKE '%{2}%'", txtSearch4.Text, txtSearch5.Text, comboOfEmpl2.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (comboGender2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr plec 
                    dv.RowFilter = string.Format("PLEC LIKE '%{0}%'", gender);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (comboOfEmpl2.Text != "--Wybierz--")
                {
                    DataView dv = dataTable.DefaultView;  //filtr forma zatrudnienia
                    dv.RowFilter = string.Format("FORMA_ZATRUDNIENIA LIKE '%{0}%'", comboOfEmpl2.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (txtSearch2.Text != "yyyy-mm-dd" && txtSearch3.Text != "yyyy-mm-dd")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data uro
                    dv.RowFilter = string.Format("DATA_URODZENIA >= #{0}# AND DATA_URODZENIA <= #{1}#", txtSearch2.Text, txtSearch3.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else if (txtSearch4.Text != "yyyy-mm-dd" && txtSearch5.Text != "yyyy-mm-dd")
                {
                    DataView dv = dataTable.DefaultView;  //filtr data zatrudnienia i data urodzenia
                    dv.RowFilter = string.Format("DATA_ZATRUDNIENIA >= #{0}# AND DATA_ZATRUDNIENIA <= #{1}#", txtSearch4.Text, txtSearch5.Text);
                    dataGridView1.DataSource = dv.ToTable();
                }
                else
                {
                    MessageBox.Show("Można użyć tylko dwóch filtrów jednocześnie ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Błędny format filtru ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //RESET FILTRÓW
        private void btnResetFiltr_Click(object sender, EventArgs e)
        {
            txtSearch2.Clear();
            txtSearch3.Clear();
            updateGridView(0);
            comboOfEmpl2.Text = "--Wybierz--";
            comboGender2.Text = "--Wybierz--";

            txtSearch2.ForeColor = Color.Silver;
            txtSearch2.Text = "yyyy-mm-dd";
            txtSearch3.ForeColor = Color.Silver;
            txtSearch3.Text = "yyyy-mm-dd";
            txtSearch4.ForeColor = Color.Silver;
            txtSearch4.Text = "yyyy-mm-dd";
            txtSearch5.ForeColor = Color.Silver;
            txtSearch5.Text = "yyyy-mm-dd";
        }

        //wysyla zapytania do bazy i zwraca informacje zeby wypelnic textboxy
        public void getEmployeeAdress()
        {
            using (connection = new OracleConnection(connectionString))
            {
                connection.Open();
                //OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM Adresy WHERE ID_ADRESU= " + itemsFromSelectedRow[11], connectionString);
                //DataTable dataTable = new DataTable();
                //adapter.Fill(dataTable);
                //dataGridView3.DataSource = dataTable;
                OracleCommand cmd = new OracleCommand("SELECT * FROM Adresy WHERE ID_ADRESU= " + itemsFromSelectedRow[11], connection);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCity.Text = reader.GetString(1);
                        txtBuildingNumber.Text = reader.GetString(2);
                        txtLocalNumber.Text = reader.GetString(3);
                        txtPosteCode.Text = reader.GetString(4);
                        txtStreet.Text = reader.GetString(5);
                    }
                }

                reader.Close();

            }
            connection.Close();
        }

        public void getSalary()
        {
            using (connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM Wynagrodzenia WHERE ID_PRACOWNIKA= " + itemsFromSelectedRow[0], connection);
                OracleDataReader reader = cmd.ExecuteReader();

                string date;


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtSalaryBrutto.Text = reader.GetInt32(2).ToString();
                        txtSalaryNetto.Text = reader.GetInt32(3).ToString();
                        date = Convert.ToString(reader.GetDateTime(1).ToShortDateString());
                        txtSalaryDate.Text = date.Split('.').GetValue(2) + "-" + date.Split('.').GetValue(1) + "-" + date.Split('.').GetValue(0);
                    }
                }

                reader.Close();

            }
            connection.Close();
        }

        public bool check()
        {

            if (string.IsNullOrEmpty(txtCity.Text) || string.IsNullOrEmpty(txtStreet.Text) || string.IsNullOrEmpty(txtPosteCode.Text) || string.IsNullOrEmpty(txtLocalNumber.Text) || string.IsNullOrEmpty(txtBuildingNumber.Text) || string.IsNullOrEmpty(txtSalaryBrutto.Text) || string.IsNullOrEmpty(txtSalaryNetto.Text) || string.IsNullOrEmpty(txtSalaryDate.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtSurname.Text) || string.IsNullOrEmpty(txtBirthDate.Text) || string.IsNullOrEmpty(txtDateEmplo.Text) || comboFormOfEmpl.Equals("- - Wybierz - -") || comboGender.Equals("- - Wybierz - -") || comboPosition.Equals("- - Wybierz - -") || string.IsNullOrEmpty(txtAccountNumber.Text) || string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Błąd podczas dodawania wiersza: uzupełnji wszytkie pola ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            DateTime dt;
            string[] formats = { "yyyy-MMM-dd", "yyyy-MM-dd" };
            if (!DateTime.TryParseExact(txtBirthDate.Text, formats, CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out dt))
            {
                MessageBox.Show("Data powinna byc w formacie: yyyy-mm-dd ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!DateTime.TryParseExact(txtDateEmplo.Text, formats, CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out dt))
            {
                MessageBox.Show("Data powinna byc w formacie: yyyy-mm-dd ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!DateTime.TryParseExact(txtSalaryDate.Text, formats, CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out dt))
            {
                MessageBox.Show("Data powinna byc w formacie: yyyy-mm-dd ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!txtPosteCode.Text[2].ToString().Equals("-"))
            {
                MessageBox.Show("Kod pocztowy powinien być formatu: xx-xxx ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void txtPosteCode_Enter(object sender, EventArgs e)
        {
            if (txtPosteCode.Text == "xx-xxx")
            {
                txtPosteCode.Clear();

                txtPosteCode.ForeColor = Color.Black;
            }
        }

        private void txtPosteCode_Leave(object sender, EventArgs e)
        {
            if (txtPosteCode.Text == "")
            {
                txtPosteCode.Text = "xx-xxx";

                txtPosteCode.ForeColor = Color.Silver;
            }
        }

        private void txtSalaryDate_Enter(object sender, EventArgs e)
        {
            if (txtSalaryDate.Text == "yyyy-mm-dd")
            {
                txtSalaryDate.Clear();

                txtSalaryDate.ForeColor = Color.Black;
            }
        }

        private void txtSalaryDate_Leave(object sender, EventArgs e)
        {
            if (txtSalaryDate.Text == "")
            {
                txtSalaryDate.Text = "yyyy-mm-dd";

                txtSalaryDate.ForeColor = Color.Silver;
            }
        }

        private void txtBirthDate_Leave(object sender, EventArgs e)
        {
            if (txtBirthDate.Text == "")
            {
                txtBirthDate.Text = "yyyy-mm-dd";

                txtBirthDate.ForeColor = Color.Silver;
            }
        }

        private void txtBirthDate_Enter(object sender, EventArgs e)
        {
            if (txtBirthDate.Text == "yyyy-mm-dd")
            {
                txtBirthDate.Clear();

                txtBirthDate.ForeColor = Color.Black;
            }
        }

        private void txtDateEmplo_Enter(object sender, EventArgs e)
        {
            if (txtDateEmplo.Text == "yyyy-mm-dd")
            {
                txtDateEmplo.Clear();

                txtDateEmplo.ForeColor = Color.Black;
            }
        }

        private void txtDateEmplo_Leave(object sender, EventArgs e)
        {
            if (txtDateEmplo.Text == "")
            {
                txtDateEmplo.Text = "yyyy-mm-dd";

                txtDateEmplo.ForeColor = Color.Silver;
            }
        }


        //ZAKŁADKA KLIENCI

        public void updateGridViewCustomers(int choose)
        {
            OracleDataAdapter adapter = null;
            if (choose == 0)
            {
                adapter = new OracleDataAdapter("SELECT * FROM KLIENCI", connectionString);
            }
            else if (choose == 1)
            {
                adapter = new OracleDataAdapter("SELECT * FROM KLIENCI WHERE ADRES_MAILOWY= '" + login + "'", connectionString);
            }

            dataTable2 = new DataTable();
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    OracleCommand insertCommand = new OracleCommand();
                    OracleCommand insertCommand2 = new OracleCommand();
                    OracleCommand selectCommand = new OracleCommand();

                    if (check2())
                    {
                        //wprowadzenie nowego adresu do bazy danych
                        string command2 = "INSERT INTO Adresy VALUES(null," + "'" + txtCityCustomer.Text + "' , '" + txtBuildingCustomer.Text + "' ,'" + txtLocalCustomer.Text + "' ," + "'" + txtPostalCustomer.Text + "' ," + "'" + txtStreetCustomer.Text + "')";
                        insertCommand.Connection = connection;
                        insertCommand.CommandText = command2;
                        insertCommand.CommandType = CommandType.Text;
                        insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();

                        //zwrócenie ID ostatniego dodanego adresu
                        string command3 = "SELECT  MAX(ID_ADRESU) FROM ADRESY";
                        selectCommand.Connection = connection;
                        selectCommand.CommandText = command3;
                        selectCommand.CommandType = CommandType.Text;
                        var count = selectCommand.ExecuteScalar();
                        selectCommand.Dispose();

                        //wprowadzenie nowego Klienta do bazy danych
                        string command = "INSERT INTO KLIENCI VALUES(null," + "'" + txtNIP.Text + "'" + "," + "'" + txtFirmName.Text + "'" + "," + "'" + txtPhoneNumberCus.Text + "' ," + txtDiscount.Text + "," + " '" + txtEmailAdress.Text + "'" + ",17," + count + ")";
                        insertCommand2.Connection = connection;
                        insertCommand2.CommandText = command;
                        insertCommand2.CommandType = CommandType.Text;
                        insertCommand2.ExecuteNonQuery();
                        insertCommand2.Dispose();

                        this.updateGridViewCustomers(0);
                        clearTextBoxes2();
                        MessageBox.Show("Wiersz został dodany!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (OracleException exp)
                {
                    if (exp.Number == 913)
                        MessageBox.Show("Błąd podczas dodawania wiersza: Nieprawidłowy format pola Upust, wartość nalezy podać z '.'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Błąd podczas dodawania wiersza: Uzupełnji wszystkie pola", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand deleteCommand = new OracleCommand();
                    OracleCommand deleteCommand4 = new OracleCommand();
                    OracleCommand deleteCommand2 = new OracleCommand();
                    OracleCommand deleteCommand3 = new OracleCommand();
                    OracleCommand selectCommand = new OracleCommand();

                    string command2 = "DELETE FROM ADRESY WHERE ID_ADRESU=" + itemsFromSelectedRow2[1];
                    deleteCommand2.Connection = connection;
                    deleteCommand2.CommandText = command2;
                    deleteCommand2.CommandType = CommandType.Text;
                    deleteCommand2.ExecuteNonQuery();
                    deleteCommand2.Dispose();

                    OracleCommand cmd = new OracleCommand("SELECT * FROM ZAMOWIENIA WHERE ID_KLIENTA= " + itemsFromSelectedRow2[0], connection);
                    OracleDataReader reader = cmd.ExecuteReader();
                    int a = 0;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            a = reader.GetInt32(0);
                            
                        }
                    }

                    reader.Close();

                    string command5 = "DELETE FROM SZCZEGOLY_ZAMOWIENIA WHERE ID_ZAMOWIENIA=" + a;
                    deleteCommand4.Connection = connection;
                    deleteCommand4.CommandText = command5;
                    deleteCommand4.CommandType = CommandType.Text;
                    deleteCommand4.ExecuteNonQuery();
                    deleteCommand4.Dispose();

                    string command4 = "DELETE FROM ZAMOWIENIA WHERE ID_KLIENTA=" + itemsFromSelectedRow2[0];
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = command4;
                    deleteCommand.CommandType = CommandType.Text;
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Dispose();

                    string command3 = "DELETE FROM KLIENCI WHERE ID_KLIENTA=" + itemsFromSelectedRow2[0];
                    deleteCommand3.Connection = connection;
                    deleteCommand3.CommandText = command3;
                    deleteCommand3.CommandType = CommandType.Text;
                    deleteCommand3.ExecuteNonQuery();
                    deleteCommand3.Dispose();

                    

                    updateGridViewCustomers(0);
                    clearTextBoxes2();
                    btnDeleteCustomer.Enabled = false;
                    btnUpdateCustomer.Enabled = false;
                    btnAddCustomer.Enabled = true;
                    MessageBox.Show("Wiersz został usuniety!");

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas usuwania wiersza: " + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            clearTextBoxes2();
            btnDeleteCustomer.Enabled = false;
            btnUpdateCustomer.Enabled = false;
            itemsFromSelectedRow2[0] = dataGridView2.CurrentRow.Cells["ID_KLIENTA"].Value.ToString();
            itemsFromSelectedRow2[1] = dataGridView2.CurrentRow.Cells["ID_ADRESU"].Value.ToString();

            if (login.Equals("admin") && !string.IsNullOrEmpty(dataGridView2.CurrentRow.Cells["ID_KLIENTA"].Value.ToString()))
            {
                btnDeleteCustomer.Enabled = true;
                btnUpdateCustomer.Enabled = true;
                txtFirmName.Text = dataGridView2.CurrentRow.Cells["NAZWA_FIRMY"].Value.ToString();
                txtNIP.Text = dataGridView2.CurrentRow.Cells["NIP"].Value.ToString();
                txtPhoneNumberCus.Text = dataGridView2.CurrentRow.Cells["NUMER_TELEFONU"].Value.ToString();
                txtDiscount.Text = dataGridView2.CurrentRow.Cells["UPUST"].Value.ToString().Replace(',', '.'); ;
                txtEmailAdress.ForeColor = Color.Black;
                txtEmailAdress.Text = dataGridView2.CurrentRow.Cells["ADRES_MAILOWY"].Value.ToString();
                txtPostalCustomer.ForeColor = Color.Black;
                getCustomerAdress();
            }
        }

        public void getCustomerAdress()
        {
            using (connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand("SELECT * FROM Adresy WHERE ID_ADRESU= " + itemsFromSelectedRow2[1], connection);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCityCustomer.Text = reader.GetString(1);
                        txtBuildingCustomer.Text = reader.GetString(2);
                        txtLocalCustomer.Text = reader.GetString(3);
                        txtPostalCustomer.Text = reader.GetString(4);
                        txtStreetCustomer.Text = reader.GetString(5);
                    }
                }

                reader.Close();

            }
            connection.Close();
        }

        public void clearTextBoxes2()
        {
            txtFirmName.Clear();
            txtEmailAdress.Clear();
            txtNIP.Clear();
            txtPhoneNumberCus.Clear();
            txtDiscount.Clear();
            txtBuildingCustomer.Clear();
            txtLocalCustomer.Clear();
            txtStreetCustomer.Clear();
            txtCityCustomer.Clear();
            txtPostalCustomer.Clear();
        }

        private void btnResetCustomer_Click(object sender, EventArgs e)
        {
            btnAddCustomer.Enabled = true;
            btnDeleteCustomer.Enabled = false;
            btnUpdateCustomer.Enabled = false;
            clearTextBoxes2();
            txtPostalCustomer.ForeColor = Color.Silver;
            txtPostalCustomer.Text = "xx-xxx";
            txtEmailAdress.ForeColor = Color.Silver;
            txtEmailAdress.Text = "something@gmail.com";
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand updateCommand = new OracleCommand();
                    OracleCommand updateCommand2 = new OracleCommand();

                    if (check2())
                    {
                        string command = "UPDATE KLIENCI SET NAZWA_FIRMY =" + "'" + txtFirmName.Text + "'" + "," + "NIP = '" + txtNIP.Text + "'" + "," + "ADRES_MAILOWY = '" + txtEmailAdress.Text + "'" + "," + "UPUST =" + txtDiscount.Text + " WHERE ID_KLIENTA=" + itemsFromSelectedRow2[0];
                        updateCommand.Connection = connection;
                        updateCommand.CommandText = command;
                        updateCommand.CommandType = CommandType.Text;
                        updateCommand.ExecuteNonQuery();
                        updateCommand.Dispose();

                        string command2 = "UPDATE ADRESY SET MIASTO= '" + txtCityCustomer.Text + "' , NUMER_BUDYNKU= '" + txtBuildingCustomer.Text + "', NUMER_LOKALU= '" + txtLocalCustomer.Text + "' , KOD_POCZTOWY='" + txtPostalCustomer.Text + "' , ULICA= '" + txtStreetCustomer.Text + "' WHERE ID_ADRESU= " + itemsFromSelectedRow2[1];
                        updateCommand2.Connection = connection;
                        updateCommand2.CommandText = command2;
                        updateCommand2.CommandType = CommandType.Text;
                        updateCommand2.ExecuteNonQuery();
                        updateCommand2.Dispose();


                        updateGridViewCustomers(0);
                        btnAddCustomer.Enabled = true;
                        btnDeleteCustomer.Enabled = false;
                        btnUpdateCustomer.Enabled = false;
                        clearTextBoxes2();
                        MessageBox.Show("Wiersz został zaktualizowany!");
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas aktualizowania wiersza: " + exp.Message);
                }

            }
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable2.DefaultView;
            dv.RowFilter = string.Format("NAZWA_FIRMY LIKE '%{0}%' OR NIP LIKE '%{0}%' OR NUMER_TELEFONU LIKE '%{0}%' OR ADRES_MAILOWY LIKE '%{0}%' OR ID_KLIENTA LIKE {0}", txtSearchCustomer.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clearTextBoxes2();
            btnDeleteCustomer.Enabled = false;
            btnUpdateCustomer.Enabled = false;

            if (!string.IsNullOrEmpty(dataGridView2.CurrentRow.Cells["ID_KLIENTA"].Value.ToString()))
            {
                Form3 form3 = new Form3(itemsFromSelectedRow2[0]);
                form3.Show();
            }
        }

        public bool check2()
        {
            string postalCode = txtPostalCustomer.Text;
            if (string.IsNullOrEmpty(txtCityCustomer.Text) || string.IsNullOrEmpty(txtStreetCustomer.Text) || string.IsNullOrEmpty(txtPostalCustomer.Text) || string.IsNullOrEmpty(txtLocalCustomer.Text) || string.IsNullOrEmpty(txtBuildingCustomer.Text) || string.IsNullOrEmpty(txtFirmName.Text) || string.IsNullOrEmpty(txtNIP.Text) || string.IsNullOrEmpty(txtPhoneNumberCus.Text) || string.IsNullOrEmpty(txtEmailAdress.Text) || string.IsNullOrEmpty(txtDiscount.Text))
            {
                MessageBox.Show("Błąd podczas dodawania wiersza: uzupełnji wszytkie pola ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!postalCode[2].ToString().Equals("-"))
            {

                MessageBox.Show("Kod pocztowy powinien być formatu: xx-xxx ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        //ZAKLADKA PRODUKTY

        public void updateGridViewProducts()
        {
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM PRODUKTY", connectionString);
            dataTable3 = new DataTable();
            adapter.Fill(dataTable3);
            dataGridView4.DataSource = dataTable3;
        }

        private void btbAddProduct_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    OracleCommand insertCommand = new OracleCommand();
                    OracleCommand insertCommand2 = new OracleCommand();
                    OracleCommand selectCommand = new OracleCommand();

                    //wprowadzenie nowego adresu do bazy danych
                    string command2 = "INSERT INTO PRODUKTY VALUES(null," + "'" + txtProductName.Text + "' ," + txtProductPrice.Text + "," + txtProcuctDiscount.Text + " ," + txtProductVat.Text + ", 17)";
                    insertCommand.Connection = connection;
                    insertCommand.CommandText = command2;
                    insertCommand.CommandType = CommandType.Text;
                    insertCommand.ExecuteNonQuery();
                    insertCommand.Dispose();

                    updateGridViewProducts();
                    MessageBox.Show("Wiersz został dodany!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (OracleException exp)
                {
                    if(exp.Number == 913)
                    MessageBox.Show("Błąd podczas dodawania wiersza: Nieprawidłowy format pola Upust, wartość nalezy podać z '.'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Błąd podczas dodawania wiersza: Uzupełnji wszystkie pola", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnResetProduct_Click(object sender, EventArgs e)
        {
            btnAddProduct.Enabled = true;
            btnDeleteProduct.Enabled = false;
            btnUpdateProduct.Enabled = false;
            txtProcuctDiscount.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductVat.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dataTable3.DefaultView;
            dv.RowFilter = string.Format("NAZWA_TOWARU LIKE '%{0}%'", txtSearchProduct.Text);
            dataGridView4.DataSource = dv.ToTable();
        }

        private void dataGridView4_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnDeleteProduct.Enabled = false;
            btnUpdateProduct.Enabled = false;
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProcuctDiscount.Clear();
            txtProductVat.Clear();

            if (!string.IsNullOrEmpty(dataGridView4.CurrentRow.Cells["ID_PRODUKTU"].Value.ToString()))
            {
                btnDeleteProduct.Enabled = true;
                btnUpdateProduct.Enabled = true;
                txtProductName.Text = dataGridView4.CurrentRow.Cells["NAZWA_TOWARU"].Value.ToString();
                txtProductPrice.Text = dataGridView4.CurrentRow.Cells["CENA_NETTO"].Value.ToString();
                txtProcuctDiscount.Text = dataGridView4.CurrentRow.Cells["UPUST"].Value.ToString().Replace(',','.');
                txtProductVat.Text = dataGridView4.CurrentRow.Cells["STAWKA_VAT"].Value.ToString();
            }

        }



        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OracleCommand deleteCommand2 = new OracleCommand();

                    string command2 = "DELETE FROM PRODUKTY WHERE ID_PRODUKTU=" + dataGridView4.CurrentRow.Cells["ID_PRODUKTU"].Value.ToString();
                    deleteCommand2.Connection = connection;
                    deleteCommand2.CommandText = command2;
                    deleteCommand2.CommandType = CommandType.Text;
                    deleteCommand2.ExecuteNonQuery();
                    deleteCommand2.Dispose();

                    updateGridViewProducts();
                    btnAddProduct.Enabled = true;
                    btnDeleteProduct.Enabled = false;
                    btnUpdateProduct.Enabled = false;
                    txtProcuctDiscount.Clear();
                    txtProductName.Clear();
                    txtProductPrice.Clear();
                    txtProductVat.Clear();
                    btnDeleteProduct.Enabled = false;
                    btnUpdateProduct.Enabled = false;
                    btnAddProduct.Enabled = true;
                    MessageBox.Show("Wiersz został usuniety!");

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas usuwania wiersza: " + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand updateCommand = new OracleCommand();
                    OracleCommand updateCommand2 = new OracleCommand();
                    string command = "UPDATE PRODUKTY SET NAZWA_TOWARU =" + "'" + txtProductName.Text + "'" + ", CENA_NETTO= " + txtProductPrice.Text + ", UPUST= " + txtProcuctDiscount.Text + ", STAWKA_VAT= " + txtProductVat.Text + " WHERE ID_PRODUKTU=" + dataGridView4.CurrentRow.Cells["ID_PRODUKTU"].Value.ToString();
                    updateCommand.Connection = connection;
                    updateCommand.CommandText = command;
                    updateCommand.CommandType = CommandType.Text;
                    updateCommand.ExecuteNonQuery();
                    updateCommand.Dispose();

                    updateGridViewProducts();
                    btnAddProduct.Enabled = true;
                    btnDeleteProduct.Enabled = false;
                    btnUpdateProduct.Enabled = false;
                    txtProcuctDiscount.Clear();
                    txtProductName.Clear();
                    txtProductPrice.Clear();
                    txtProductVat.Clear();
                    btnDeleteProduct.Enabled = false;
                    btnUpdateProduct.Enabled = false;
                    btnAddProduct.Enabled = true;
                    MessageBox.Show("Wiersz został zaktualizowany!");

                }
                catch (OracleException exp)
                {
                    if (exp.Number == 1747)
                        MessageBox.Show("Błąd podczas dodawania wiersza: Nieprawidłowy format pola Upust, wartość nalezy podać z '.'", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else                       
                        MessageBox.Show("Błąd podczas aktualizowania wiersza: " + exp.Message);
                }

            }
        }


        //ZAKŁADKA ZAMÓWIENIA
        public void upgradeDataViewOrders(int choose)
        {
            OracleDataAdapter adapter = null;
            if (choose == 0)
            {
                adapter = new OracleDataAdapter("SELECT z.id_zamowienia,k.nazwa_firmy,s.ilosc,p.nazwa_towaru, z.data_zamowienia, z.numer_faktury, z.termin_platnosci, z.sposob_platnosci, z.statusoplacone, z.id_klienta FROM ZAMOWIENIA z " +
                                                "JOIN Klienci k ON k.id_klienta = z.id_klienta " +
                                                "JOIN SZCZEGOLY_ZAMOWIENIA s ON s.id_zamowienia = z.id_zamowienia " +
                                                "JOIN PRODUKTY p ON p.id_produktu = s.id_produktu ", connectionString);
            }
            else if (choose == 1)
            {
                adapter = new OracleDataAdapter("SELECT z.id_zamowienia,k.nazwa_firmy,s.ilosc,p.nazwa_towaru, z.data_zamowienia, z.numer_faktury, z.termin_platnosci, z.sposob_platnosci, z.statusoplacone, z.id_klienta FROM  ZAMOWIENIA z " +
                                                "JOIN KLIENCI k ON k.id_klienta = z.id_klienta " +
                                                "JOIN SZCZEGOLY_ZAMOWIENIA s ON s.id_zamowienia = z.id_zamowienia " +
                                                "JOIN PRODUKTY p ON p.id_produktu = s.id_produktu " +
                                                "WHERE k.adres_mailowy = '" + login + "'", connectionString);
            }

            dataTable4 = new DataTable();
            adapter.Fill(dataTable4);
            dataGridView5.DataSource = dataTable4;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

            DataView dv = dataTable4.DefaultView;
            dv.RowFilter = string.Format("STATUSOPLACONE LIKE '%{0}%' OR NUMER_FAKTURY LIKE '%{0}%' OR SPOSOB_PLATNOSCI LIKE '%{0}%' OR NAZWA_TOWARU LIKE '%{0}%' OR NAZWA_FIRMY LIKE '%{0}%'", txtSearchOrders.Text);
            dataGridView5.DataSource = dv.ToTable();
        }

        private void dataGridView5_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (login.Equals("admin") || login.Equals("Lukasz98"))
            {
                grpbProduct.Enabled = false;
                btnDeleteOrders.Enabled = false;
                txtIf.Clear();

                if (!string.IsNullOrEmpty(dataGridView5.CurrentRow.Cells["ID_ZAMOWIENIA"].Value.ToString()))
                {
                    grpbProduct.Enabled = true;
                    btnDeleteOrders.Enabled = true;
                    txtIf.Text = dataGridView5.CurrentRow.Cells["STATUSOPLACONE"].Value.ToString();
                }
            }
        }

        private void btnUpdateOrders_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OracleCommand updateCommand = new OracleCommand(); ;
                  
                        string command = "UPDATE ZAMOWIENIA SET STATUSOPLACONE = '" + txtIf.Text + "' WHERE ID_ZAMOWIENIA=" + dataGridView5.CurrentRow.Cells["ID_ZAMOWIENIA"].Value.ToString();
                        updateCommand.Connection = connection;
                        updateCommand.CommandText = command;
                        updateCommand.CommandType = CommandType.Text;
                        updateCommand.ExecuteNonQuery();
                        updateCommand.Dispose();


                        txtIf.Clear();
                        grpbProduct.Enabled = false;
                        upgradeDataViewOrders(0);
                        MessageBox.Show("Wiersz został zaktualizowany!");                  

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas aktualizowania wiersza: " + exp.Message);
                }

            }
        }

        private void btnDeleteOrders_Click(object sender, EventArgs e)
        {
            using (connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OracleCommand deleteCommand = new OracleCommand();
                    OracleCommand deleteCommand2 = new OracleCommand();

                    string command = "DELETE FROM SZCZEGOLY_ZAMOWIENIA WHERE ID_ZAMOWIENIA=" + dataGridView5.CurrentRow.Cells["ID_ZAMOWIENIA"].Value.ToString();
                    deleteCommand.Connection = connection;
                    deleteCommand.CommandText = command;
                    deleteCommand.CommandType = CommandType.Text;
                    deleteCommand.ExecuteNonQuery();
                    deleteCommand.Dispose();


                    string command2 = "DELETE FROM ZAMOWIENIA WHERE ID_ZAMOWIENIA=" + dataGridView5.CurrentRow.Cells["ID_ZAMOWIENIA"].Value.ToString();
                    deleteCommand2.Connection = connection;
                    deleteCommand2.CommandText = command2;
                    deleteCommand2.CommandType = CommandType.Text;
                    deleteCommand2.ExecuteNonQuery();
                    deleteCommand2.Dispose();

                    upgradeDataViewOrders(0);
                    btnDeleteOrders.Enabled = false;
                    btnUpdateOrders.Enabled = false;

                    MessageBox.Show("Wiersz został usuniety!");

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Błąd podczas usuwania wiersza: " + exp.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        //private void tabOrders_Click(object sender, EventArgs e)
        //{
        //    if (login.Equals("eltom@gmail.com"))
        //        upgradeDataViewOrders(1);
        //    else upgradeDataViewOrders(0);
        //}

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (login.Equals("eltom@gmail.com") && tabControl1.SelectedTab == tabOrders)
                upgradeDataViewOrders(1);
            else if (login.Equals("admin") && tabControl1.SelectedTab == tabOrders) upgradeDataViewOrders(0);
        }

        private void txtSearch2_Enter(object sender, EventArgs e)
        {
            if(txtSearch2.Text == "yyyy-mm-dd")
            {
                txtSearch2.Clear();

                txtSearch2.ForeColor = Color.Black;
            }
        }

        private void txtSearch2_Leave(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "")
            {
                txtSearch2.Text = "yyyy-mm-dd";

                txtSearch2.ForeColor = Color.Silver;
            }
        }

        private void txtSearch3_Enter(object sender, EventArgs e)
        {
            if (txtSearch3.Text == "yyyy-mm-dd")
            {
                txtSearch3.Clear();

                txtSearch3.ForeColor = Color.Black;
            }
        }

        private void txtSearch3_Leave(object sender, EventArgs e)
        {
            if (txtSearch3.Text == "")
            {
                txtSearch3.Text = "yyyy-mm-dd";

                txtSearch3.ForeColor = Color.Silver;
            }
        }

        private void txtSearch4_Enter(object sender, EventArgs e)
        {
            if (txtSearch4.Text == "yyyy-mm-dd")
            {
                txtSearch4.Clear();

                txtSearch4.ForeColor = Color.Black;
            }
        }

        private void txtSearch4_Leave(object sender, EventArgs e)
        {
            if (txtSearch4.Text == "")
            {
                txtSearch4.Text = "yyyy-mm-dd";

                txtSearch4.ForeColor = Color.Silver;
            }
        }

        private void txtSearch5_Enter(object sender, EventArgs e)
        {
            if (txtSearch5.Text == "yyyy-mm-dd")
            {
                txtSearch5.Clear();

                txtSearch5.ForeColor = Color.Black;
            }
        }

        private void txtSearch5_Leave(object sender, EventArgs e)
        {
            if (txtSearch5.Text == "")
            {
                txtSearch5.Text = "yyyy-mm-dd";

                txtSearch5.ForeColor = Color.Silver;
            }
        }

        private void txtPostalCustomer_Enter(object sender, EventArgs e)
        {
            if(txtPostalCustomer.Text == "xx-xxx")
            {
                txtPostalCustomer.Clear();

                txtPostalCustomer.ForeColor = Color.Black;
            }
        }

        private void txtPostalCustomer_Leave(object sender, EventArgs e)
        {
            if (txtPostalCustomer.Text == "")
            {
                txtPostalCustomer.Text = "xx-xxx";

                txtPostalCustomer.ForeColor = Color.Silver;
            }
        }

        private void txtEmailAdress_Enter(object sender, EventArgs e)
        {
            if(txtEmailAdress.Text == "something@gmail.com")
            {
                txtEmailAdress.Clear();

                txtEmailAdress.ForeColor = Color.Black;
            }
        }

        private void txtEmailAdress_Leave(object sender, EventArgs e)
        {
            if (txtEmailAdress.Text == "")
            {
                txtEmailAdress.Text = "something@gmail.com";

                txtEmailAdress.ForeColor = Color.Silver;
            }
        }

       
    }
}
