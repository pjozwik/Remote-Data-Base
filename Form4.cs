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
    public partial class Form4 : Form
    {
        private string login;
        private string password;

        public Form4()
        {
            InitializeComponent();
        }

        private void btnLogging_Click(object sender, EventArgs e)
        {
            if (password.Equals("admin") && login.Equals("admin") || password.Equals("ELTOM1234") && login.Equals("eltom@gmail.com") || password.Equals("Lukasz98") && login.Equals("Lukasz98"))
            {
                Hide();
                Form1 form1 = new Form1(login, password, this);
                form1.Show();
            }
            else
            {                              
                MessageBox.Show("Błąd podczas logowania: błędny login", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtPassword.Text))
            btnLogging.Enabled = true;
            else  btnLogging.Enabled = false;

            login = txtLogin.Text;
            password = txtPassword.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close(); 
            
        }

        public string getLogin()
        {
            return login;
        }

        public string getPassword()
        {
            return password;
        }
    }
}
