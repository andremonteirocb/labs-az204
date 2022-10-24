using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public interface IStorageService
{
    BlobServiceClient CreateConnection();
    Task<BlobContainerClient> CreateContainer(BlobServiceClient blobServiceClient);
    Task Upload(BlobContainerClient containerClient);
    Task Load(BlobContainerClient containerClient);
    Task Download(BlobContainerClient containerClient);
    Task Delete(BlobContainerClient containerClient);
}
public class StorageService : IStorageService
{
    private readonly string _containerName;
    private readonly string _localPath;
    private readonly string _fileName;
    public StorageService()
    {
        _containerName = "wtblob" + Guid.NewGuid().ToString();
        _localPath = "caminho_arquivos_fisicos";
        _fileName = "wtfile" + Guid.NewGuid().ToString() + ".txt";
    }
    
    public BlobServiceClient CreateConnection()
    {
        // Copy the connection string from the portal in the variable below.
        string storageConnectionString = "access_key___key1___connectionstring";

        // Create a client that can authenticate with a connection string
        BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
        return blobServiceClient;
    }
    public async Task<BlobContainerClient> CreateContainer(BlobServiceClient blobServiceClient)
    {
        // Create the container and return a container client object
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(_containerName);
        Console.WriteLine("A container named '" + _containerName + "' has been created. " +
            "\nTake a minute and verify in the portal." +
            "\nNext a file will be created and uploaded to the container.");
        Console.WriteLine("Press 'Enter' to continue.");
        Console.ReadLine();

        return containerClient;
    }
    public async Task Upload(BlobContainerClient containerClient)
    {
        // Create a local file in the ./data/ directory for uploading and downloading
        string localFilePath = Path.Combine(_localPath, _fileName);

        // Write text to the file
        await File.WriteAllTextAsync(localFilePath, "Hello, World!");

        // Get a reference to the blob
        BlobClient blobClient = containerClient.GetBlobClient(_fileName);

        Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

        // Open the file and upload its data
        using FileStream uploadFileStream = File.OpenRead(localFilePath);
        await blobClient.UploadAsync(uploadFileStream, true);

        Console.WriteLine("\nThe file was uploaded. We'll verify by listing" +
                " the blobs next.");
        Console.WriteLine("Press 'Enter' to continue.");
        Console.ReadLine();
    }
    public async Task Load(BlobContainerClient containerClient)
    {
        // List blobs in the container
        Console.WriteLine("Listing blobs...");
        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            Console.WriteLine("\t" + blobItem.Name);

        Console.WriteLine("\nYou can also verify by looking inside the " +
                "container in the portal." +
                "\nNext the blob will be downloaded with an altered file name.");
        Console.WriteLine("Press 'Enter' to continue.");
        Console.ReadLine();
    }
    public async Task Download(BlobContainerClient containerClient)
    {
        string localFilePath = Path.Combine(_localPath, _fileName);

        // Download the blob to a local file
        // Append the string "DOWNLOADED" before the .txt extension 
        string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

        Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

        // Get a reference to the blob
        BlobClient blobClient = containerClient.GetBlobClient(_fileName);

        // Download the blob's contents and save it to a file
        BlobDownloadInfo download = await blobClient.DownloadAsync();

        using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
            await download.Content.CopyToAsync(downloadFileStream);

        Console.WriteLine("\nLocate the local file in the data directory created earlier to verify it was downloaded.");
        Console.WriteLine("The next step is to delete the container and local files.");
        Console.WriteLine("Press 'Enter' to continue.");
        Console.ReadLine();
    }
    public async Task Delete(BlobContainerClient containerClient)
    {
        // Delete the container and clean up local files created
        Console.WriteLine("\n\nDeleting blob container...");
        await containerClient.DeleteAsync();

        string localFilePath = Path.Combine(_localPath, _fileName);

        // Download the blob to a local file
        // Append the string "DOWNLOADED" before the .txt extension 
        string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

        Console.WriteLine("Deleting the local source and downloaded files...");

        File.Delete(localFilePath);
        File.Delete(downloadFilePath);

        Console.WriteLine("Finished cleaning up.");
    }
}