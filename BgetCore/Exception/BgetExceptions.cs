using System;
using System.Collections.Generic;
using System.Text;

namespace BgetCore.Exception
{
    public class VideoInfoNotFoundException : System.Exception
    {
        public VideoInfoNotFoundException() { }

        public VideoInfoNotFoundException(string message) : base(message) { }

        public VideoInfoNotFoundException(string message, System.Exception inner) : base(message, inner) { }

        public override string Message => "Video not found!";
    }

    public class VideoUrlNotFoundException : System.Exception
    {
        public VideoUrlNotFoundException() { }

        public VideoUrlNotFoundException(string message) : base(message) { }

        public VideoUrlNotFoundException(string message, System.Exception inner) : base(message, inner) { }

        public override string Message => "Cannot fetch video URL!";
    }
}
