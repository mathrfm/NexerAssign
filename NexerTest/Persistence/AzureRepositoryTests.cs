using Azure;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Models.Enums;
using Microsoft.Extensions.Configuration;
using Moq;
using Persistence;

namespace NexerTest.Persistence
{
    public class AzureRepositoryTests
    {
        private readonly AzureRepository _azureRepository;
        private readonly Mock<BlobClient> _blobClient;


        public AzureRepositoryTests() 
        {
            var storageFactoryMock = new Mock<IStorageFactory>();
            _blobClient = new Mock<BlobClient>();

            var blobContainerClient = new Mock<BlobContainerClient>();
            blobContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_blobClient.Object);


            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                 .Build();

            storageFactoryMock.Setup(_ => _.GetBlobContainer())
                .ReturnsAsync(blobContainerClient.Object);

            _azureRepository = new AzureRepository(storageFactoryMock.Object,config);
        }

        [Theory]
        [InlineData("dockan", "2019-01-10", SensorType.Humidity)]
        public void WhenGetCsvByDeviceIdDateAndType_Success(string deviceId, DateTime date, SensorType? sensorType) 
        {
            
            //Arrange
            _blobClient.Setup(x => x.ExistsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(true, Mock.Of<Response>()));

            var downloadInfoMock = new Mock<BlobDownloadInfo>();
            downloadInfoMock.Setup(x => x.Content)
                .Returns(File.OpenText("testFile.txt").BaseStream);
            _blobClient.Setup(x => x.DownloadAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(downloadInfoMock.Object, Mock.Of<Response>()));


            //Act
            var result = _azureRepository.GetCsvByDeviceIdDateAndType(deviceId, date, sensorType);



            //Assert
            Assert.NotNull(result);
        }
    }
}
