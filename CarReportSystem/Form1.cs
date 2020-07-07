using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarReportSystem
{
    public partial class Form1 : Form
    {
        BindingList<CarReport> _carReports = new BindingList<CarReport>();

        public Form1()
        {
            InitializeComponent();
            dgvCarReport.DataSource = _carReports;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            CarReport carReport = new CarReport
            {
                //BindingListへ登録
                CreatedSate = dtpDate.Value,
                Author = cbAuthor.Text,
                Name = cbName.Text,
                Report = tbReport.Text,
                Picture = pictureBox1.Image,
                Maker = makername()
            };
            _carReports.Insert(0,carReport);

        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //選択した画像をピクチャーボックスに表示
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                //ピクチャーボックスのサイズに画像を調整
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private CarMaker makername()
        {
            

            if (radioButton1.Checked == true)
            {
                return CarMaker.トヨタ;
            }

            else if (radioButton2.Checked == true)
            {
                return CarMaker.日産;
            }

            else if (radioButton3.Checked == true)
            {
                return CarMaker.ホンダ;
            }

            else if (radioButton4.Checked == true)
            {
                return CarMaker.スバル;
            }

            else if (radioButton5.Checked == true)
            {
                return CarMaker.外車;
            }

            else 
            {
                return CarMaker.その他;
            }

        }

        private void btFix_Click(object sender, EventArgs e)
        {
            CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
            selectedCarReport.CreatedSate = dtpDate.Value;
            selectedCarReport.Author = cbAuthor.Text;
            selectedCarReport.Name = cbName.Text;
            selectedCarReport.Report = tbReport.Text;
            selectedCarReport.Picture = pictureBox1.Image;
            selectedCarReport.Maker = makername();

           dgvCarReport.Refresh(); //データグリッドビューの再描画1
        }
    }
}
