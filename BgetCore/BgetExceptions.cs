using System;
using System.Collections.Generic;
using System.Text;

namespace BgetCore
{
    public class VideoInfoNotFoundException : Exception
    {
        public VideoInfoNotFoundException() { }

        public VideoInfoNotFoundException(string message) : base(message) { }

        public VideoInfoNotFoundException(string message, Exception inner) : base(message, inner) { }

        public override string Message { get { return "Video not found!"; } }
    }

    public class VideoUrlNotFoundException : Exception
    {
        public VideoUrlNotFoundException() { }

        public VideoUrlNotFoundException(string message) : base(message) { }

        public VideoUrlNotFoundException(string message, Exception inner) : base(message, inner) { }

        public override string Message { get { return "Cannot fetch video URL!"; } }
    }
}
