using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AllMax;
using AllMax.Antero;

namespace AnteroDBConfig
{
    public static class StringExtension
    {
        //don''t need this in real world!!
        public static Boolean ContainsOnlyDigits(this String str)
        {
            if (str == null) throw new NullReferenceException();
            return str.All(c => char.IsDigit(c));
        }
    }
    public partial class frmMain : Form
    {
        private bool blnWasButtonClicked = false;
        public frmMain()
        {
            InitializeComponent();
        }
        public enum PasswordTypeEnum
        {
            None,
            Password,
            Validated
        }

        private static uint AllMaxLicenseCRC32(char[] buffer)
        {
            if (buffer.Length == 0)
            {
                return 0u;
            }
            uint[] array = new uint[256];
            for (uint num = 0u; num <= 255u; num += 1u)
            {
                uint num2 = num;
                for (int i = 1; i <= 8; i++)
                {
                    if (num2 % 2u != 0u)
                    {
                        num2 = (num2 >> 1 ^ 3988292384u);
                    }
                    else
                    {
                        num2 >>= 1;
                    }
                }
                array[(int)num] = num2;
            }
            uint num3 = uint.MaxValue;
            uint num4 = 0u;
            while ((ulong)num4 < (ulong)((long)buffer.Length))
            {
                num3 = (array[(int)((byte)(num3 & 255u) ^ (byte)buffer[(int)num4])] ^ num3 >> 8);
                num4 += 1u;
            }
            return ~num3;
        }


        private bool PasswordTest(string _txtPassword)
        {
            PasswordTypeEnum _pwType = PasswordTypeEnum.None;
            string _password = "";
            if (!string.IsNullOrEmpty(_txtPassword))
            {
                string text = AllMaxLicenseCRC32(("SUPPORT" + DateTime.Today.ToString("yyyyMMdd")).ToCharArray()).ToString("X8").ToLower();
                if (_txtPassword.Length == 8 && _txtPassword.StartsWith("allmax", StringComparison.OrdinalIgnoreCase) && _txtPassword.Substring(6).ContainsOnlyDigits() && int.Parse(_txtPassword.Substring(6)) == DateTime.Today.Day)
                {
                    _pwType = PasswordTypeEnum.Password;
                    _password = text;
                }
                else if (this.txtPassword.Text.Equals(text, StringComparison.OrdinalIgnoreCase))
                {
                    _pwType = PasswordTypeEnum.Validated;
                }
            }
            if (_pwType != PasswordTypeEnum.None)
                return true;
            return false;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            txtPOTD.Text = AllMaxLicenseCRC32(("SUPPORT" + DateTime.Today.ToString("yyyyMMdd")).ToCharArray()).ToString("X8").ToLower();
            //load settings
            btnPassReveal.Height = txtPassword.Height;
            string szServer = RegistryValues.AnteroDbLocation;
            txtServer.Text = szServer + "," + Convert.ToString(RegistryValues.AnteroDbPort);
            txtDatabase.Text = RegistryValues.AnteroDbName;
            txtUsername.Text = RegistryValues.AnteroDbUsername;
            txtPassword.Text = RegistryValues.AnteroDbPassword;
            if (RegistryValues.AnteroDbSpecifyAuth)
                cmbAuthMethod.SelectedIndex = 1;
            else
                cmbAuthMethod.SelectedIndex = 0;

        }

        private void btnPassReveal_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnPassReveal_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to cancel changes and exit?", "Cancel Changes?", MessageBoxButtons.YesNo) == DialogResult.No)
               return;
            blnWasButtonClicked = true;
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!blnWasButtonClicked)
                if (MessageBox.Show("Are you sure you wish to cancel changes and exit?", "Cancel Changes?", MessageBoxButtons.YesNo) == DialogResult.No)
                    e.Cancel = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to save changes and exit?", "Save Changes?", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (cmbAuthMethod.SelectedIndex == 1)
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username cannot be empty in SQL Server Integrated Authentication", "Error", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty in SQL Server Integrated Authentication", "Error", MessageBoxButtons.OK);
                    return;
                }
                RegistryValues.AnteroDbSpecifyAuth = true;
                RegistryValues.AnteroDbUsername = txtUsername.Text;
                RegistryValues.AnteroDbPassword = txtPassword.Text;
            }
            else
            {
                RegistryValues.AnteroDbSpecifyAuth = false;
                RegistryValues.AnteroDbUsername = "";
                RegistryValues.AnteroDbPassword = "";
            }
            if (txtServer.Text.Contains(","))
            {
                string szHostname = txtServer.Text.Split(',')[0];
                UInt16 iPort = 0;
                try { iPort = Convert.ToUInt16(txtServer.Text.Split(',')[1]); }
                catch { iPort = 0;  }
                if (iPort == 0)
                    iPort = 1433;
                RegistryValues.AnteroDbLocation = szHostname;
                RegistryValues.AnteroDbPort = iPort;
            }
            else
            {
                RegistryValues.AnteroDbLocation = txtServer.Text;
                RegistryValues.AnteroDbPort = 1433;
            }
            RegistryValues.AnteroDbName = txtDatabase.Text;
            blnWasButtonClicked = true;
            this.Close();
        }

        private void cmbAuthMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthMethod.SelectedIndex == 1)
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
        }
        private string CalcMD5Hash(byte[] byteInput)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] encrypted = md5Hash.ComputeHash(byteInput);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < encrypted.Length; i++)
                    sBuilder.Append(encrypted[i].ToString("x2"));
                return sBuilder.ToString();
            }
        }
        private bool VerifyMD5Hash(string hash1,string hash2)
        {
            if (string.IsNullOrWhiteSpace(hash1) || string.IsNullOrWhiteSpace(hash2))
                return false;
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hash1, hash2))
                return true;
            else
                return false;
        }
    }
}
