using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlobPractice
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 建立CloudStorageAccount，連線字串使用開發用(連至emulator的blob)
            var storageConnectionString = "UseDevelopmentStorage=true";
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);

            // 建立容器與設定權限
            CloudBlobClient cloudBlobClient = account.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("test");
            cloudBlobContainer.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions =
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob };
            cloudBlobContainer.SetPermissionsAsync(permissions);

            // 將blob上傳至容器

            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var path = directoryInfo.Parent.Parent.FullName;
            var filePath = Path.Combine(path, "picture.jpg");
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference("picture.jpg");
            cloudBlockBlob.UploadFromFile(filePath);
        }
    }
}