using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using static Azure.Core.HttpHeader;
using static Odev_Dagitim_Portali.Service.ImanageImage;
using Odev_Dagitim_Portali.Helper;
using System.Security.Cryptography;

namespace Odev_Dagitim_Portali.Service
{
    public class ManageImage : IManageImage
    {

        public async Task<string> UploadFile(IFormFile _IFormFile)
        {
            string FileName = "";
            try
            {
                Guid guid = Guid.NewGuid();
                var guidstr = Guid.NewGuid().ToString();

                


                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = guidstr+"-"+_IFormFile.FileName ;
                var _GetFilePath = Helper.Common.GetFilePath(FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }
                
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Helper.Common.GetFilePath(FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


        

}
