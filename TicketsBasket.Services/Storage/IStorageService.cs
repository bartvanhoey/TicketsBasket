using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {
         Task<string> SaveBlobAsync(string containerName, IFormFile file);
         Task<string> RemoveBlobAsync(string containerName, string blobName);
         string GetProtectedUrl(string container, string blob, DateTime expiryDate );

    }
}