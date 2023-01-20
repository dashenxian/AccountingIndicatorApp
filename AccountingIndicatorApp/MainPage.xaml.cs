using AccountingIndicator;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using NPOI.SS.Formula.Functions;

namespace AccountingIndicatorApp;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        await ShareFile(Microsoft.Maui.ApplicationModel.DataTransfer.Share.Default);
    }
    public async Task ShareFile(IShare share)
    {
        var homeworkBuilder = new HomeworkBuilder
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now
        };
        var bytes = homeworkBuilder.CreateWord();
        string fn = $"{DateTime.Now:yyyyMMddhhmmss}.docx";
        string file = Path.Combine(FileSystem.CacheDirectory, fn);

        await File.WriteAllBytesAsync(file, bytes);

        await share.RequestAsync(new ShareFileRequest
        {
            Title = "分享计算",
            File = new ShareFile(file)
        });
    }
}

