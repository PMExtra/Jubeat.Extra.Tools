using System.Text.RegularExpressions;

namespace Jubeat.Extra.Models.Maps.Memo
{
    public static class MemoDefinitions
    {
        public const string EmptyCharset = @"□口";
        public const string RestCharset = @"－—-";
        public const string LeftCharset = @"＜<";
        public const string UpCharset = @"∧^";
        public const string RightCharset = @"＞>";
        public const string DownCharset = @"∨v";
        public const string HorizontalCharset = @"—_－-―";
        public const string VerticalCharset = @"｜|";
        public const string CrossCharset = @"＋+";
        public const string SpaceCharset = "\t 　";
        public const string SeparatorCharset = @"|｜";
        public const string CircleNumberCharset = @"①②③④⑤⑥⑦⑧⑨⑩⑪⑫⑬⑭⑮⑯⑰⑱⑲⑳㉑㉒㉓㉔㉕㉖㉗㉘㉙㉚㉛㉜㉝㉞㉟㊱㊲㊳㊴㊵㊶㊷㊸㊹㊺㊻㊼㊽㊾㊿";
        public const string HwNumberCharset = @"123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string FwNumberCharset = @"１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";

        public const char DefaultEmpty = '□';
        public const char DefaultRest = '－';
        public const char DefaultLeft = '＜';
        public const char DefaultUp = '∧';
        public const char DefaultRight = '＞';
        public const char DefaultDown = '∨';
        public const char DefaultHorizontal = '—';
        public const char DefaultVertical = '｜';
        public const char DefaultCross = '＋';
        public const char DefaultSpace = '\t';
        public const char DefaultSeperator = '|';
        public const char DefaultTo = '-';

        /// <summary>
        ///     Measure ordinal
        /// </summary>
        /// <remarks>
        ///     Example: "1", "50"
        /// </remarks>
        public static readonly Regex OrdinalRegex = new Regex(@"^(?<ordinal>\d+)$", RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Memo data line
        /// </summary>
        /// <remarks>
        ///     Example: "□□□□ |－－－－|", "③□□⑤ |③④⑤⑥|"
        /// </remarks>
        public static readonly Regex MemoLineRegex = new Regex(@"^(?<buttons>\S{4})\s*([｜\|](?<beat>\S+)[｜\|])?$", RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Memo right part
        /// </summary>
        /// <remarks>
        ///     Example: "－－－－", "(120)①－－－"
        /// </remarks>
        public static readonly Regex BeatRegex = new Regex(@"\G([\(（](?<bpm>\d+)[\)）])?(?<hit>\S)", RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Cosmos bpm tag
        /// </summary>
        /// <remarks>
        ///     Example: "BPM: 195", "BPM: 95-120"
        /// </remarks>
        public static readonly Regex CosmosBpmRegex = new Regex(@"^BPM[\:：]\s*(?<bpmmin>\d+)([\-－\~～—]\s*(?<bpmmax>\d+))?$", RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Classic bpm tag
        /// </summary>
        /// <remarks>
        ///     Example: "BPM=90", "①－BPM=90,②BPM=95,－BPM=100"
        /// </remarks>
        public static readonly Regex ClassicBpmRegex = new Regex(@"\G((?<note>\S+?)?BPM=(?<bpm>\d+)[,，]?\s*)", RegexOptions.ExplicitCapture);

        /// <summary>
        ///     Comment line
        /// </summary>
        /// <remarks>
        ///     Example: "(BPM=190)"
        /// </remarks>
        public static readonly Regex CommentRegex = new Regex(@"^[\(（].*[\)）]$");
    }
}
