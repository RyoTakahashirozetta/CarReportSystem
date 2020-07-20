using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            //dgvCarReport.DataSource = _carReports;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (cbAuthor.Text == "" || cbName.Text == "")
            {
                MessageBox.Show("記録者、車名を入力してください", "エラーメッセージ");
            }
            else
            {
                /*
                CarReport carReport = new CarReport
                {
                    //BindingListへ登録
                    CreatedSate = dtpDate.Value.Date,
                    Author = cbAuthor.Text,
                    Name = cbName.Text,
                    Report = tbReport.Text,
                    Picture = pbImage.Image,
                    Maker = makername()
                };*/

                

                


                //dgvへの表示
                //_carReports.Insert(0, carReport);

                btDelete2.Enabled = false;
                //btFix.Enabled = false;

                //コンボボックスの入力候補に追加
                setComboBoxMakerAuthor(cbAuthor.Text);
                setComboBoxMakerName(cbName.Text);

                //表示欄を初期状態に戻す
                reset();
            }
            
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            if (ofdOpenImage.ShowDialog() == DialogResult.OK)
            {
                //選択した画像をピクチャーボックスに表示
                pbImage.Image = Image.FromFile(ofdOpenImage.FileName);
                //ピクチャーボックスのサイズに画像を調整
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            pbImage.Image = null;
        }

        private CarMaker makername()
        {
            

            if (toyota.Checked == true)
            {
                return CarMaker.トヨタ;
            }

            else if (nissan.Checked == true)
            {
                return CarMaker.日産;
            }

            else if (honnda.Checked == true)
            {
                return CarMaker.ホンダ;
            }

            else if (subaru.Checked == true)
            {
                return CarMaker.スバル;
            }

            else if (gaisya.Checked == true)
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
            /*
            CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
            selectedCarReport.CreatedSate = dtpDate.Value;
            selectedCarReport.Author = cbAuthor.Text;
            selectedCarReport.Name = cbName.Text;
            selectedCarReport.Report = tbReport.Text;
            selectedCarReport.Picture = pbImage.Image;
            selectedCarReport.Maker = makername();
            */

            dgvCarReport.CurrentRow.Cells[1].Value = dtpDate.Value;
            dgvCarReport.CurrentRow.Cells[2].Value = cbAuthor.Text;
            dgvCarReport.CurrentRow.Cells[4].Value = cbName.Text;
            dgvCarReport.CurrentRow.Cells[5].Value = tbReport.Text;

            this.Validate();
            this.carReportBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202025DataSet);


            dgvCarReport.Refresh(); //データグリッドビューの再描画1
        }

        private void btDelete2_Click(object sender, EventArgs e)
        {
            
            _carReports.RemoveAt(dgvCarReport.CurrentRow.Index);
            
            dgvCarReport.ClearSelection();

            btDelete2.Enabled = false;

            btFix.Enabled = false;
        }

        void initButton()
        {
            if (dgvCarReport == null)
            {
                btFix.Enabled = false;
                btDelete2.Enabled = false;
            }
            else
            {
                btFix.Enabled = true;
                btDelete2.Enabled = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'infosys202025DataSet.CarReport' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            
            initButton();
            dgvCarReport.Columns[0].Visible = false; //idを非表示にする
        }

        private void setComboBoxMakerAuthor(string maker)
        {
            if (!cbAuthor.Items.Contains(maker))
            {
                cbAuthor.Items.Add(maker);
            }
            
        }

        private void setComboBoxMakerName(string maker)
        {
            if (!cbName.Items.Contains(maker))
            {
                cbName.Items.Add(maker);
            }
        }

        private void reset()
        {
            dgvCarReport.ClearSelection();

            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = null;
            cbName.Text = null;
            pbImage.Image = null;
            tbReport.Text = null; 

        }

        private void dgvCarReport_Click(object sender, EventArgs e)
        {
            
            if (dgvCarReport.CurrentRow != null)
            {
                //var Maker = dgvCarReport.CurrentRow.Cells[3].Value; //選択している行の指定したセルの値を取得
                /* CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
                 dtpDate.Value = selectedCarReport.CreatedSate.Date;
                 cbAuthor.Text = selectedCarReport.Author;
                 cbName.Text = selectedCarReport.Name;
                 tbReport.Text = selectedCarReport.Report;
                 pbImage.Image = selectedCarReport.Picture;*/
                // radioButtonSelect();
                dtpDate.Text = dgvCarReport.CurrentRow.Cells[1].Value.ToString();
                cbAuthor.Text = dgvCarReport.CurrentRow.Cells[2].Value.ToString();
                cbName.Text = dgvCarReport.CurrentRow.Cells[4].Value.ToString();
                tbReport.Text = dgvCarReport.CurrentRow.Cells[5].Value.ToString();
                SelectMaker();      
                initButton();
            }
        }



        private void radioButtonSelect()
         {
            CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
            if (CarMaker.トヨタ == selectedCarReport.Maker)
             {
                 toyota.Checked = true;
             }
            else if (CarMaker.日産 == selectedCarReport.Maker)
            {
                nissan.Checked = true;
            }
            else if (CarMaker.ホンダ == selectedCarReport.Maker)
            {
                honnda.Checked = true;
            }
            else if (CarMaker.スバル == selectedCarReport.Maker)
            {
                subaru.Checked = true;
            }
            else if (CarMaker.外車 == selectedCarReport.Maker)
            {
                gaisya.Checked = true;
            }
            else if (CarMaker.その他 == selectedCarReport.Maker)
            {
                sonota.Checked = true;
            }
        }

        private void btOpen_Click_1(object sender, EventArgs e)
        {
            this.carReportTableAdapter.Fill(this.infosys202025DataSet.CarReport);
            SelectMaker();
            //オープンファイルダイアログを表示
            /*
            if (ofdOpenData.ShowDialog() == DialogResult.OK)
            {

                using (FileStream fs = new FileStream(ofdOpenData.FileName, FileMode.Open))
                {
                    try
                    {
                        BinaryFormatter formatter = new BinaryFormatter();


                        _carReports = (BindingList<CarReport>)formatter.Deserialize(fs);
                        dgvCarReport.DataSource = _carReports;
                        dgvCarReport_Click(sender, e);
                    }
                    catch (SerializationException se)
                    {
                        Console.WriteLine("Failed to deserialize. Reason: " + se.Message);
                        throw;

                    }

                }


            }*/
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btSav_Click(object sender, EventArgs e)
        {
            //セーブファイルダイアログを表示
            if (sfdSaveData.ShowDialog() == DialogResult.OK)
            {

                BinaryFormatter formatter = new BinaryFormatter();

                //ファイルストリームを生成
                using (FileStream fs = new FileStream(sfdSaveData.FileName, FileMode.Create))
                {
                    try
                    {
                        //シリアル化して保存
                        formatter.Serialize(fs, _carReports);
                    }
                    catch (SerializationException se)
                    {
                        Console.WriteLine("Failed to serialize. Reason: " + se.Message);
                        throw;
                    }



                }

            }
        }

        private void carReportBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.carReportBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202025DataSet);

        }

        private void SelectMaker()
        {

            var Maker = dgvCarReport.CurrentRow.Cells[3].Value;
            switch (Maker.ToString())
            {
                case "トヨタ":
                    toyota.Checked = true;
                    break;

                case "日産":
                    nissan.Checked = true;
                    break;

                case "ホンダ":
                    honnda.Checked = true;
                    break;

                case "スバル":
                    subaru.Checked = true;
                    break;

                case "外車":
                    gaisya.Checked = true;
                    break;

                default:
                    sonota.Checked = true;
                    break;

                
            }
        }

        // バイト配列をImageオブジェクトに変換
        public static Image ByteArrayToImage(byte[] byteData)
        {
            ImageConverter imgconv = new ImageConverter();
            Image img = (Image)imgconv.ConvertFrom(byteData);
            return img;
        }

        // Imageオブジェクトをバイト配列に変換
        public static byte[] ImageToByteArray(Image img)
        {
            ImageConverter imgconv = new ImageConverter();
            byte[] byteData = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return byteData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgvCarReport.CurrentRow.Cells[2].Value = cbAuthor.Text;
            this.Validate();
            this.carReportBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.infosys202025DataSet);
        }
    }
}
