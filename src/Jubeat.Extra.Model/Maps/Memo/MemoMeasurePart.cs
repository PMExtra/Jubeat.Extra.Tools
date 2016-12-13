namespace Jubeat.Extra.Models.Maps.Memo
{
    public class MemoMeasurePart
    {
        private readonly int[,] _buttons = new int[4, 4];

        public static implicit operator int[,](MemoMeasurePart t)
        {
            return t._buttons;
        }
    }
}
