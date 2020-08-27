using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using BlazorWBUploadFile.Shared;

namespace BlazorWBUploadFile.Client.Interfaces
{
   public interface IFileUploadService
    {
        Task<Response> FileUploadAsync(IFileListEntry file, string folderName);

    }
}
