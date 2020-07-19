namespace ArtistSimulator.Data.Models
{
    public enum ToolEnum { Brush, Scissors}
    
    static class Tool
    {
        public static ToolEnum CurrentTool = ToolEnum.Brush;
    }
}
