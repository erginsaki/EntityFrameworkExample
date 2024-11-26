using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkExample
{
    public partial class FormLocation : Form
    {
        public FormLocation()
        {
            InitializeComponent();
        }
        

        public class ApplicationDbContext : DbContext
        {
            public DbSet<Guide> Guides { get; set; }

            public ApplicationDbContext() : base("name=EntityFrameworkExEntities") { }
        }

        private void LoadGuideData()

        {
            // Veritabanı bağlamını oluşturun (ApplicationDbContext veya benzeri)
            using (var database = new ApplicationDbContext())
            {
                // Guide tablosundaki verileri alın
                var guideList = database.Guides.ToList();

                // ComboBox'a veri ekleyin
                comboBoxGuide.DataSource = guideList;

                // ComboBox'ta görünen metni belirleyin (GuideName ve GuideSurname kombinasyonu)
                comboBoxGuide.DisplayMember = "FullName"; // ComboBox'ta görünen alan
                comboBoxGuide.ValueMember = "GuideId"; // ComboBox'un arka planda tutacağı değer

                // Eğer isterseniz GuideId'yi default olarak seçebilirsiniz
                comboBoxGuide.SelectedIndex = 0; // İlk elemanı seç
            }
        }

        EntityFrameworkExEntities db = new EntityFrameworkExEntities();

        private void btnList_Click(object sender, EventArgs e)
        {
            var values = db.Location.ToList();
            dataGridView.DataSource = values;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Location location = new Location(); 
            location.LocationName = txtName.Text;
            location.LocationCity = txtCity.Text;
            location.LocationCountry = txtCountry.Text;
            location.LocationCapacity = (byte)numericUpDown1.Value; //(byte) : type casting - tür dönüşümü veya byte.Parse(numericupdown.value.ToString())
            location.LocationPrice = decimal.Parse(txtPrice.Text);  // (byte) ifadesi, explicit (açık) bir tür dönüşümüdür.
            location.LocationDay = txtTime.Text;
            location.GuideId = (int)comboBoxGuide.SelectedValue;
            db.Location.Add(location);
            db.SaveChanges();
            MessageBox.Show("Yeni Lokasyon Eklendi");
        }

        private void FormLocation_Load(object sender, EventArgs e)
        {
            LoadGuideData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtId.Text);

            var updatedValue = db.Location.Find(id);
            updatedValue.LocationName= txtName.Text;
            updatedValue.LocationCity = txtCity.Text;
            updatedValue.LocationCountry = txtCountry.Text;
            updatedValue.LocationCapacity = (byte)numericUpDown1.Value;
            updatedValue.LocationPrice = decimal.Parse(txtPrice.Text);
            updatedValue.LocationDay = txtTime.Text;
            updatedValue.GuideId = (int)comboBoxGuide.SelectedValue;
            db.SaveChanges();

            MessageBox.Show("Rehber Başarıyla Güncellendi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var deletedValue = db.Location.Find(id);
            db.Location.Remove(deletedValue); 
            db.SaveChanges();
            MessageBox.Show("Lokasyon Silindi");
        }
    }
}
