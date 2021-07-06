using Android.Content;
using Android.Widget;
using AndroidX.Core.Content;
using PdfFiles.Interfaces;
using PdfFiles.Models.Events;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(PdfFiles.Droid.Services.AndroidFilesService))]
namespace PdfFiles.Droid.Services
{
    public class AndroidFilesService : IFilesService
    {
        public string StorageDirectory => Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void DownloadFile(string url, string folder)
        {
            string pathToNewFolder = Path.Combine(StorageDirectory, folder);
            Directory.CreateDirectory(pathToNewFolder);
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception exp)
            {

               if(OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
                }
            }
        }

        public void OpenFile(string path)
        {
            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(new Java.IO.File(path)).ToString());
            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            var tt = File.Exists(path);
            Context context = Android.App.Application.Context;
            Android.Net.Uri uri = FileProvider.GetUriForFile(context, context.PackageName + ".provider", new Java.IO.File(path));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, mimeType);
            intent.AddFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            Intent chooserInten = Intent.CreateChooser(intent, "Open with:");
            chooserInten.AddFlags(ActivityFlags.NewTask);

            try
            {
                context.StartActivity(intent);
            }
            catch (Exception exp)
            {
                Toast.MakeText(context, "No application available to View PDF", ToastLength.Short).Show();
            }
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
                }                
            }
            else
            {
                if (OnFileDownloaded != null)
                {
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
                }
            }
        }
    }
}