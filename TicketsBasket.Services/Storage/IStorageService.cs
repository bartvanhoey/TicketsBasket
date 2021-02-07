using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {
         Task<string> SaveBlobAsync(string containerName, IFormFile file, BlobType blobType);
         Task DeleteIfExistsAsync(string containerName, string blobName);
         string GetProtectedUrl(string containerName, string blobName, DateTimeOffset expiryDate );

    }
}