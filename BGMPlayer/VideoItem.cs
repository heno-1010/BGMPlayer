using System;
using System.Collections.Generic;
using System.Text;

namespace BGMPlayer
{
    public class VideoItem
    { 
        public required string VideoTitle { get; set; }
        public required string VideoUrl { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is VideoItem other)
            {
                return this.VideoTitle == other.VideoTitle && this.VideoUrl == other.VideoUrl;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (VideoTitle,VideoUrl).GetHashCode();
        }

    }
}
