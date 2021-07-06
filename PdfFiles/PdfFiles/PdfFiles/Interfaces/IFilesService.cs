using PdfFiles.Models.Events;
using System;

namespace PdfFiles.Interfaces
{
    public interface IFilesService
    {
        string StorageDirectory { get; }
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        void OpenFile(string path);
    }
}
