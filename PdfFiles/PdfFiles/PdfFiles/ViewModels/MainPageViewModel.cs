using PdfFiles.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PdfFiles.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IFilesService filesService;
        public ICommand DownloadPdfFileCommand { get; private set; }
        public ICommand OpenPdfFileCommand { get; private set; }
        
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "PDF File";
            DownloadPdfFileCommand = new DelegateCommand(DownloadPdfFile);
            OpenPdfFileCommand = new DelegateCommand(OpenPdfFile);
            filesService = DependencyService.Get<IFilesService>();
        }

        private void OpenPdfFile()
        {
            string pathToNewFolder = Path.Combine(filesService.StorageDirectory, "PdfFiles");
            string pathToNewFile = Path.Combine(pathToNewFolder, "Microsoft_Press_eBook_CreatingMobileAppswithXamarinForms_PDF.pdf");
            filesService.OpenFile(pathToNewFile);
        }

        private void DownloadPdfFile()
        {
            filesService.OnFileDownloaded += FilesService_OnFileDownloaded;
            filesService.DownloadFile("https://download.microsoft.com/download/7/8/8/788971A6-C4BB-43CA-91DC-557B8BE72928/Microsoft_Press_eBook_CreatingMobileAppswithXamarinForms_PDF.pdf", "PdfFiles");
           
        }

        private void FilesService_OnFileDownloaded(object sender, Models.Events.DownloadEventArgs e)
        {
           
        }
    }
}
