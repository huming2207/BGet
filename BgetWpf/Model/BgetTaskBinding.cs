using System.Windows.Media;

namespace BgetWpf.Model
{
    /// <summary>
    /// Data binding of TaskList, need to be evaluate later, may not work
    /// </summary>
    public class BgetTaskBinding
    {
        public string TaskID              { get; set; } // Content ID, leave blank if it's a normal task
        public string TaskTitle           { get; set; } // Title, can be video title or a file name
        public string TaskStatusText      { get; set; } // Status text, e.g. "Finished", "Seeding" or "Downloading"
        public Brush  TaskStatusColor     { get; set; } // Status text color, turns red if error occur, green when finishes
        public int    TaskProgressValue   { get; set; } // Progress bar value
        public string TaskProgressText    { get; set; } // Progress text, e.g. 95.27%
    }
}
