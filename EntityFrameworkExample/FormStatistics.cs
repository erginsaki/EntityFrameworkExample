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
    public partial class FormStatistics : Form
    {
        public FormStatistics()
        {
            InitializeComponent();
        }

        EntityFrameworkExEntities db = new EntityFrameworkExEntities();

        private void FormStatistics_Load(object sender, EventArgs e)
        {

            #region Toplam Lokasyon Sayısı

            lblLocationValue.Text = db.Location.Count().ToString();

            lblCapacityValue.Text = db.Location.Sum(X => X.LocationCapacity).ToString();

            lblGuideValue.Text = db.Guide.Count().ToString();

            lblAvgCapacity.Text = db.Location.Average(x => x.LocationCapacity).ToString();

            lblAvgPrice.Text = db.Location.Average(x => (decimal?)x.LocationPrice)?.ToString("F2"); //virgülden Sonra 2 basamak gelsin

            int lastCountryId = db.Location.Max(x => x.LocationId);
            lblLastCountry.Text = db.Location.Where(x => x.LocationId == lastCountryId).Select(y => y.LocationCountry).FirstOrDefault().ToString();

            lblItalyCapacity.Text = db.Location.Where(x => x.LocationCity == "Roma").Select(y => y.LocationCapacity).FirstOrDefault().ToString();

            lblTurkiyeAvgCapacity.Text = db.Location.Where(x => x.LocationCountry == "Türkiye").Average(y => y.LocationCapacity).ToString();

            var italyGuideId = db.Location.Where(x => x.LocationName == "İtalya Turu" ).Select(y => y.GuideId).FirstOrDefault(); 
            lblItalyGuide.Text = db.Guide.Where(x=> x.GuideId == italyGuideId).Select(y=> y.GuideName + " " +y.GuideSurname).FirstOrDefault().ToString();

            var maxCapacity = db.Location.Max(X => X.LocationCapacity);
            lblMaxCapacity.Text = db.Location.Where(x => x.LocationCapacity == maxCapacity).Select(y => y.LocationCity).FirstOrDefault().ToString();    

            var maxPrice = db.Location.Max(x => x.LocationPrice);
            lblMaxPrice.Text = db.Location.Where(x => x.LocationPrice == maxPrice).Select(y => y.LocationName).FirstOrDefault().ToString();

            var guideIdByNameVeli = db.Guide.Where(x => x.GuideName == "Veli" && x.GuideSurname == "Büyük").Select(y => y.GuideId).FirstOrDefault();
            lblGuideTours.Text = db.Location.Where(x => x.GuideId == guideIdByNameVeli).Count().ToString();

            #endregion

        }
    }
}
