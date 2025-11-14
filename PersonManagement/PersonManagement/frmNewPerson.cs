using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonManagement
{
    public partial class frmNewPerson : Form
    {
        bool ValidateInput(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtFamilyName.Text))
            {
                errorMessage = "لطفا همه فیلدها را پر کنید.";
                return false;
            }

            if (!rbMale.Checked && !rbFemale.Checked)
            {
                errorMessage = "لطفا جنسیت را انتخاب کنید.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNationalCode.Text) || txtNationalCode.Text.Length != 10)
            {
                errorMessage = "لطفا کد ملی معتبر ۱۰ رقمی وارد کنید.";
                return false;
            }

            return true;
        }

        public frmNewPerson()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = ValidateInput(out string errorMessage);
            if (! isValid)
            {
                MessageBox.Show(errorMessage, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var people = new Person();
            people.Name = txtName.Text;
            people.FamilyName = txtFamilyName.Text;
            people.NationalCode = txtNationalCode.Text;
            if (rbMale.Checked)
                people.Gender = GenderType.Male;
            else
                if (rbFemale.Checked)
                people.Gender = GenderType.Female;
            else
                people.Gender = GenderType.Unknown;

            var frmPerson= Application.OpenForms[nameof(FrmPerson)] as FrmPerson;
            if (frmPerson != null)
            {
                frmPerson.people.Add(people);
                frmPerson.FillDGV();
            }
            this.Close();



        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
