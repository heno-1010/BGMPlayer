using System;
using System.Collections.Generic;
using System.Text;

namespace BGMPlayer
{
    public class VideoItem
    { 
        public string VideoTitle { get; set; }
        public string VideoUrl { get; set; }

        public VideoItem(string name, string url)
        {
            VideoTitle = name;
            VideoUrl = url;
        }
    }
}
