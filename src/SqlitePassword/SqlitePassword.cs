using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace SqlitePassword
{
    public partial class SqlitePassword : Form
    {
        private SQLiteConnection conn;

        public SqlitePassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void ChangePassword(string filePath, string oldPwd, string newPwd)
        {
            conn = new SQLiteConnection();
            conn.ConnectionString = "Data Source=" + filePath;
            if (!string.IsNullOrEmpty(oldPwd))
            {
                conn.ConnectionString += ";Password=" + oldPwd;
            }
            try
            {
                conn.Open();
                conn.ChangePassword(newPwd);
                MessageBox.Show("Password change successfully", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxFile.Text) || !File.Exists(tbxFile.Text))
            {
                MessageBox.Show("File not exist.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ChangePassword(tbxFile.Text, tbxOldPwd.Text, tbxNewPwd.Text);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDlg = new OpenFileDialog())
            {
                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    tbxFile.Text = fileDlg.FileName;
                }
            }
        }
    }
}
