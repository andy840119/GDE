namespace GDE.App.Main.Levels.Metas
{
    public class Level
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public int ID { get; set; }
        public int Position { get; set; }
        public double Length { get; set; }
        public bool Verified { get; set; }
        public Folder Folder { get; set; }
        public Song Song { get; set; }
    }
}
