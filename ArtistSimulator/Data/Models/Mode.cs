using System;

namespace ArtistSimulator.Data.Models
{
    public enum ModeEnum { BrushAndScissors, Brush }

    static class Mode
    {
        public static ModeEnum CurrentMode = ModeEnum.BrushAndScissors;

        public static void SwitchToNext()
        {
            int modeNum = (int)CurrentMode;
            modeNum++;

            CurrentMode = Enum.IsDefined(typeof(ModeEnum), modeNum) ? (ModeEnum)modeNum : 0;
        }
    }
}
