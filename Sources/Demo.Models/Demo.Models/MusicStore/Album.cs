namespace Demo.Models.MusicStore
{
    public class Album
    {
        public string AlbumArtUrl { get; set; }
        public Genre Genre { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public int CountSold { get; set; }
        public Artist Artist { get; set; }

        public override string ToString()
        {
            return string.Format("For {3} you can get \"{0}\", a {1} album by {2}", Title, Genre.Name, Artist.Name, Price.ToString("c"));
        }
    }
}
