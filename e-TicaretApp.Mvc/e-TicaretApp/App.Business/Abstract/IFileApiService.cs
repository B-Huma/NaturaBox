using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IFileApiService
    {
        Task<string?> UploadFileAsync(IFormFile file);
        Task<Stream?> DownloadFileAsync(string fileName);
    }
}
