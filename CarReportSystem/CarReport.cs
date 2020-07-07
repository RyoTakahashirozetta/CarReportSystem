using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarReportSystem
{
    class CarReport
    {
        public DateTime CreatedSate { get; set; } //作成日
        public string Author { get; set; }        //記録者
        public CarMaker Maker { get; set; }       //メーカー（列挙型）
        public string Name { get; set; }          //車名
        public string Report { get; set; }        //レポート
        public Image Picture { get; set; }        //画像

    }

    //メーカー
    public enum CarMaker
    {
        DEFAULT,
        トヨタ,
        日産,
        ホンダ,
        スバル,
        外車,
        その他
    }
}
