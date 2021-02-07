using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage
{
  public class AzureStorageService : IStorageService
  {
    private readonly AzureStorageAccountOptions _storageAccountOptions;
    private readonly BlobServiceClient _blobServiceClient;

    private AzureStorageService(AzureStorageAccountOptions storageAccountOptions)
    {
      _storageAccountOptions = storageAccountOptions;
      _blobServiceClient = new BlobServiceClient(_storageAccountOptions.ConnectionString);
    }

    public string GetProtectedUrl(string containerName, string blobName, DateTimeOffset expiryDate)
    {
      var container = _blobServiceClient.GetBlobContainerClient(containerName);
      var blobClient = container.GetBlobClient(Path.GetFileName(blobName));
      return blobClient.GenerateSasUri(BlobSasPermissions.Read, expiryDate).AbsoluteUri;
    }

    public async Task DeleteIfExistsAsync(string containerName, string blobName)
    {
      var container = _blobServiceClient.GetBlobContainerClient(containerName);
      var blobClient = container.GetBlobClient(Path.GetFileName(blobName));
      await blobClient.DeleteIfExistsAsync();
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