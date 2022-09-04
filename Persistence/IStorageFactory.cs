using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Models.Enums;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Persistence
{
    public interface IStorageFactory
    {
        Task<BlobContainerClient> GetBlobContainer();
    }
}
