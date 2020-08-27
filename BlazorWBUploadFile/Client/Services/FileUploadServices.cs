using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorInputFile;
using BlazorWBUploadFile.Client.Interfaces;
using BlazorWBUploadFile.Shared;
using Newtonsoft.Json;

namespace BlazorWBUploadFile.Client.Services
{
    public class FileUploadServices : IFileUploadService
    {
        public HttpClient _httpClient { get; }
        public FileUploadServices(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        //44323
        public async Task<Response> FileUploadAsync(IFileListEntry file, string folderName)
        {
            try
            {
                var ms = new MemoryStream();
                await file.Data.CopyToAsync(ms);
                var content = new MultipartFormDataContent {
                        {
                        new ByteArrayContent(ms.GetBuffer()), folderName, file.Name
                    }
                    };
                var response = await _httpClient.PostAsync("UploadFile/FileUploadAsync", content);
                var readContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response>(readContent);

                return new Response(result.Success, result.Message);

            }
            catch (Exception ex)
            {
                return new Response(-1, ex.Message);
            }
        }
    }
}
