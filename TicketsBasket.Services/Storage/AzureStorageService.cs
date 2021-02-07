using System;
using System.IO;
using System.Linq;
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

    public AzureStorageService(AzureStorageAccountOptions storageAccountOptions)
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

    public async Task<string> SaveBlobAsync(string containerName, IFormFile file, BlobType blobType)
    {

      if (file == null) return null;
      var fileName = file.FileName;
      var extension = Path.GetExtension(fileName);

      ValidateExtension(extension, blobType);

      var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{extension}";

      using (var stream = file.OpenReadStream())
      {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();
        var blob = container.GetBlobClient(newFileName);
        var blobInfo = await blob.UploadAsync(stream);
        
        return $"{_storageAccountOptions.AccountUrl}{containerName}/{newFileName}";
      }
    }

    private void ValidateExtension(string extension, BlobType blobType)
    {
      var allowedImageExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };
      var allowedDocumentExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt" };

      switch (blobType)
      {
        case BlobType.Image:
          if (!allowedImageExtensions.Contains(extension)) throw new BadImageFormatException();
          break;
        case BlobType.Document:
          if (!allowedDocumentExtensions.Contains(extension)) throw new NotSupportedException($"Document file is not supported {extension}");
          break;
        default:
          break;
      }
    }
  }
}