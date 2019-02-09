using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDEdit.Utilities.Objects.GeometryDash
{
    /// <summary>Contains the metadata of a song.</summary>
    public class SongMetadata
    {
        /// <summary>The ID of the song.</summary>
        public int ID { get; set; }
        /// <summary>The title of the song.</summary>
        public string Title { get; set; }
        /// <summary>The author of the song.</summary>
        public string Author { get; set; }
        /// <summary>The URL to the song on Newgrounds.</summary>
        public string URL => $"https://www.newgrounds.com/audio/listen/{ID}";

        /// <summary>Initializes a new instance of the <seealso cref="SongMetadata"/> class.</summary>
        public SongMetadata() { }
    }
}
