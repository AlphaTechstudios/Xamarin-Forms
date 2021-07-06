using System;

namespace PdfFiles.Models.Events
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;

        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
