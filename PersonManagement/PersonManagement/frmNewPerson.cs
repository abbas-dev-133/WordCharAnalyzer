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
	public partial class FrmNewPerson : Form
	{
		private Person person = new Person();
		bool isNew= false;
		public FrmNewPerson(Person person= null)
		{
			InitializeComponent();
            if (person == null)
            {
                person = new Person();
                isNew = true;
            }

            this.person = person;


        }

        private void btnSave_Click(object sender, EventArgs e)
		{
		   
			person.Name = txtName.Text;
			person.FamilyName = txtFamilyName.Text;
			person.NationalCode = txtNationalCode.Text;
			if (rbMale.Checked)
				person.Gender = GenderType.Male;
			else
				if (rbFemale.Checked)
				person.Gender = GenderType.Female;
			else
				person.Gender = GenderType.Unknown;
			var result = person.Validate();
			if (!result.IsSuccess)
			{
				MessageBox.Show(result.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var frmPerson= Application.OpenForms[nameof(FrmPerson)] as FrmPerson;
			if (frmPerson != null)
			{
				if(isNew)
				frmPerson.persons.Add(person);

				frmPerson.FillDgv();
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

		private void frmNewPerson_Load(object sender, EventArgs e)
		{
			txtName.Text = person.Name;
			txtFamilyName.Text = person.FamilyName;
			txtNationalCode.Text = person.NationalCode;
			if (person.Gender == GenderType.Male)
				rbMale.Checked = true;
			else if (person.Gender == GenderType.Female)
				rbFemale.Checked = true;
		}
	}
}
