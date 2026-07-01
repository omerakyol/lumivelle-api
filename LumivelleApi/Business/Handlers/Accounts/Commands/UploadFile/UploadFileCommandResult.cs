namespace Business.Handlers.Accounts.Commands.UploadFile;

public class UploadFileCommandResult
{
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }
}