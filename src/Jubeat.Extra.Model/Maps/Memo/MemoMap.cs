using System.Collections.Generic;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMap
    {
        internal const string EmptyCharacters = @"□口_－—\-―\|｜";
        internal const string RestCharacters = @"—－";
        internal const string HalfRestCharacters = @"\-";
        internal const string LeftCharacters = @"＜\<";
        internal const string UpCharacters = @"∧\^";
        internal const string RightCharacters = @"＞\>";
        internal const string DownCharacters = @"∨v";
        internal const string HorizontalCharacters = @"―_－\-";
        internal const string VerticalCharacters = @"｜\|";
        internal const string NumberCharacters = @"①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳㉑㉒㉓㉔㉕㉖㉗㉘㉙㉚㉛㉜㉝㉞㉟㊱㊲㊳㊴㊵㊶㊷㊸㊹㊺㊻㊼㊽㊾㊿";
        internal const string HalfNumberCharacters = @"123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public int Bpm { get; set; }

        public IList<MemoMeasure> Measures { get; set; }
    }
}
