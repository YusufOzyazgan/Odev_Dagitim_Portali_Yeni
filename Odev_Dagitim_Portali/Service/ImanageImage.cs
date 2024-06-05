namespace Odev_Dagitim_Portali.Service
{
    public class ImanageImage
    {
        public interface IManageImage
        {
            Task<string> UploadFile(IFormFile _IFormFile);
            Task<(byte[], string, string)> DownloadFile(string FileName);
        }
    }
}
