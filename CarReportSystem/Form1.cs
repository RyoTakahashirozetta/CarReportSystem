using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                CarReport carReport = new CarReport
                {
                    //BindingListへ登録
                    CreatedSate = dtpDate.Value.Date,
                    Author = cbAuthor.Text,
                    Name = cbName.Text,
                    Report = tbReport.Text,
                    Picture = pbImage.Image,
                    Maker = makername()
                };
                //dgvへの表示
                _carReports.Insert(0, carReport);

                btDelete2.Enabled = false;
                btFix.Enabled = false;

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
            selectedCarReport.Picture = pbImage.Image;
            selectedCarReport.Maker = makername();

            

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
            if (_carReports.Count == 0)
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

            dtpDate.Value = DateTime.Now;
            cbAuthor.Text = null;
            cbName.Text = null;
            pbImage.Image = null;
            tbReport.Text = null; 

        }

        private void dgvCarReport_Click(object sender, EventArgs e)
        {
            if (dgvCarReport.CurrentRow != null)
            {
                var test = dgvCarReport.CurrentRow.Cells[3].Value; //選択している行の指定したセルの値を取得
                /* CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
                 dtpDate.Value = selectedCarReport.CreatedSate.Date;
                 cbAuthor.Text = selectedCarReport.Author;
                 cbName.Text = selectedCarReport.Name;
                 tbReport.Text = selectedCarReport.Report;
                 pbImage.Image = selectedCarReport.Picture;*/
                radioButtonSelect();
                initButton();
            }
        }



        private void radioButtonSelect()
         {
            CarReport selectedCarReport = _carReports[dgvCarReport.CurrentRow.Index];
            if (CarMaker.トヨタ == selectedCarReport.Maker)
             {
                 radioButton1.Checked = true;
             }
            else if (CarMaker.日産 == selectedCarReport.Maker)
            {
                radioButton2.Checked = true;
            }
            else if (CarMaker.ホンダ == selectedCarReport.Maker)
            {
                radioButton3.Checked = true;
            }
            else if (CarMaker.スバル == selectedCarReport.Maker)
            {
                radioButton4.Checked = true;
            }
            else if (CarMaker.外車 == selectedCarReport.Maker)
            {
                radioButton5.Checked = true;
            }
            else if (CarMaker.その他 == selectedCarReport.Maker)
            {
                radioButton6.Checked = true;
            }
        }

        private void btOpen_Click_1(object sender, EventArgs e)
        {
            this.carReportTableAdapter.Fill(this.infosys202025DataSet.CarReport);
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
    }
}
