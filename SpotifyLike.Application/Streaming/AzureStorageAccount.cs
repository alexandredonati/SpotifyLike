using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace SpotifyLike.Application.Streaming
{
    public class AzureStorageAccount
    {
        private string AccountName { get; set; }
        private string AccessKey { get; set; }
        public AzureStorageAccount(IConfiguration configuration)
        {
            AccountName = configuration["AzureStorageAccount:AccountName"];
            AccessKey = configuration["AzureStorageAccount:AccountName"]; ;
        }

        public async Task<string> UploadImage(string base64Image)
        {
            string blobUri = $"https://{AccountName}.blob.core.windows.net";
            string fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}.jpg";

            var imageByte = Convert.FromBase64String(base64Image);
            var stream = new MemoryStream(imageByte);

            var sharedKeyCredential = new StorageSharedKeyCredential(AccountName, AccessKey);
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), sharedKeyCredential);
            var blobContainer = blobServiceClient.GetBlobContainerClient("backdrop-images");
            var blobClient = blobContainer.GetBlobClient(fileName);

            await blobClient.UploadAsync(stream, true);

            return $"https://{AccountName}.blob.core.windows.net/backdrop-images/{fileName}";
        }
    }
}
