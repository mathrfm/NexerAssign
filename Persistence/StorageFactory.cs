using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace Persistence
{
    public class StorageFactory : IStorageFactory
    {
        private readonly string _containerName;
        private readonly string _sasToken;

        public StorageFactory(IConfiguration config)
        {
            _sasToken = config.GetConnectionString("AzureBlobStorage");
            _containerName = config.GetSection("ContainerName").Value;
        }

        public async Task<BlobContainerClient> GetBlobContainer()
        {
            var blobServiceClient = new BlobServiceClient(_sasToken);
            return blobServiceClient.GetBlobContainerClient(_containerName);        
        }

    }
}
