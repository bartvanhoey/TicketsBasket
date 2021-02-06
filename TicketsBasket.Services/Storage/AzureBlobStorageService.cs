using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage
{
  public class AzureBlobStorageService : IStorageService
  {
    private readonly AzureStorageAccountOptions _storageAccountOptions;
    private readonly BlobServiceClient _blobServiceClient;

    private AzureBlobStorageService(AzureStorageAccountOptions storageAccountOptions)
    {
      _storageAccountOptions = storageAccountOptions;
      _blobServiceClient = new BlobServiceClient(_storageAccountOptions.ConnectionString);
    }

    public string GetProtectedUrl(string container, string blob, DateTime expiryDate)
    {
      throw new NotImplementedException();
    }

    public Task<string> RemoveBlobAsync(string containerName, string blobName)
    {
      throw new NotImplementedException();
    }

    public async Task<string> SaveBlobAsync(string containerName, IFormFile file)
    {
      var fileName = file.FileName;
      var extension = Path.GetExtension(fileName);

      var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{extension}";

      using (var stream = file.OpenReadStream())
      {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();
        var blob = container.GetBlobClient(newFileName);
        await blob.UploadAsync(stream);
        return $"{_storageAccountOptions.AccountUrl}/{containerName}/{newFileName}";
      }



    }
  }
}