# NexerAssign

DEVELOPMENT SPECIFICATIONS:

A new customer has developed their own IoT solution which is able to collect weather information from a weather station. The weather station sends information over a Microsoft Azure IoT hub in near-real-time. Today there are two Azure services that handle the information.
Data Receiver: Saves the information that arrives in CSV files to the Azure Blob Storage. The files are written continuously, one file per day, and contain the weather station units and sensor types (such as temperature, humidity, and rainfall).
Data Compressor: Compresses the CSV files regularly and moves them into a common compressed ZIP file for each unit and sensor type.

The temporary files are saved in the format: /{{deviceId}}/{{sensorType}}/{{date}}.csv

The compressed file is stored according to the format: /{{deviceId}}/{{sensorType}}/historical.zip

CSV file format:
columns separated with ';'
always 2 columns
first column represents date and time in format yyyy-MM-ddThh:mm:ss
second column represents measured value, float with ',' as decimal separator, without leading zeros (for example value ',04' means 0,04)

The API will initially have two methods.
Collect all of the measurements for one day, one sensor type, and one unit. Examples of how a call could look:

/api/v1/devices/testdevice/data/2018-09-18/temperature
getdata?deviceId=testdevice&date=2018-09-18&sensorType=temperature

Collect all data points for one unit and one day. Examples of how a call could look:
/api/v1/devices/testdevice/data/2018-09-18
getdatafordevice?deviceId=testdevice&date=2018-09-18 Returns temperature, humidity, and rainfall for the day.

REQUIREMENTS:

In order for the solution to be avaliated, you need to install Azure Blob Storage Explorer that can be download here:
https://azure.microsoft.com/en-us/products/storage/storage-explorer/#overview

To connect to the storage and check on the files you must use this connection string:
BlobEndpoint=https://sigmaiotexercisetest.blob.core.windows.net/;QueueEndpoint=https://sigmaiotexercisetest.queue.core.windows.net/;FileEndpoint=https://sigmaiotexercisetest.file.core.windows.net/;TableEndpoint=https://sigmaiotexercisetest.table.core.windows.net/;SharedAccessSignature=sv=2017-11-09&ss=bfqt&srt=sco&sp=rl&se=2028-09-27T16:27:24Z&st=2018-09-27T08:27:24Z&spr=https&sig=eYVbQneRuiGn103jUuZvNa6RleEeoCFx1IftVin6wuA%3D

To help with the connection you can use these instructions:
https://docs.microsoft.com/en-us/azure/vs-azure-tools-storage-manage-with-storage-explorer?tabs=windows#shared-access-signature-sas-connection-string


FINAL COMMENTS:

1- A pagination feature is missing, as the files have too many rows to be shown it would be nice to have a pagination controller, by the time I realized this it would take me too much time to implement the feature. I added a resultCount variable to return the top results so the solution would not break.
2- My AzureRepository could have been split in more functions to facilitate in the unit tests. I tried to implement a xUnit to test and faced too many problems mocking what was needed to the point that I gave up. There is a partial code of the unit test that I tried to develop.]
3- The tests can be done using the Swagger UI for the two methods. The sensorType is an Enum 0-Temperature, 1-Humidity and 2-Rainfall

