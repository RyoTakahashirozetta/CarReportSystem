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
        DateTime CreatedSate; //作成日
        string Author;        //記録者
        CarMaker Maker;       //メーカー（列挙型）
        string Name;          //車名
        string Report;        //レポート
        Image Picture;        //画像

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
