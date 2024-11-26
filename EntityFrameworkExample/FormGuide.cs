using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkExample
{
    public partial class FormGuide : Form
    {
        public FormGuide()
        {
            InitializeComponent();
        }

        EntityFrameworkExEntities db = new EntityFrameworkExEntities();

        private void btnList_Click(object sender, EventArgs e)
        {

            var values = db.Guide.ToList();
            dataGridView1.DataSource = values;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            Guide guide = new Guide();
            guide.GuideName = txtName.Text;
            guide.GuideSurname = txtSurname.Text;
            db.Guide.Add(guide);
            db.SaveChanges();
            MessageBox.Show("Rehber Eklendi");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtId.Text);

            var removeValue = db.Guide.Find(id);
            db.Guide.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Rehber Silindi");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);

            var updatedValue = db.Guide.Find(id);
            updatedValue.GuideName = txtName.Text;
            updatedValue.GuideSurname = txtSurname.Text;
            db.SaveChanges();

            MessageBox.Show("Rehber Başarıyla Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtId.Text);

            var values = db.Guide.Where(X => X.GuideId == id).ToList();
            dataGridView1.DataSource = values;
           
        }

        private void FormGuide_Load(object sender, EventArgs e)
        {

        }
    }
}   
