using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace DesparcheBackend.BO.BlobStorage
{
    public class BlobStorage: IBlobStorage
    {

        public string SubirArchivoSoporte(string strRuta, string strNombreArchivoSistema, string strNombreArchivo, bool boolSoporte)
        {
            try
            {
                string connection = "DefaultEndpointsProtocol=https;AccountName=obrastorage;AccountKey=iYk2uKSqY+rcmRYKKuE/IW5WOLo4LuE/oYFUwDdpOMiS519P9tV6134v/eVEf3R2xgF/UvGWovlSO+OykSw/Wg==;EndpointSuffix=core.windows.net";
                string destContainer = "blobobra";

                CloudStorageAccount sa = CloudStorageAccount.Parse(connection);
                CloudBlobClient bc = sa.CreateCloudBlobClient();

                CloudBlobContainer container = bc.GetContainerReference(destContainer);
                container.CreateIfNotExistsAsync().Wait();
                string[] fileEntries = Directory.GetFiles(strRuta);
                string strNombreArchivog = null;
                foreach (string filePath in fileEntries)
                {
                    string key = strNombreArchivoSistema + Path.GetFileName(filePath);
                    string strArchivoBusqueda = Path.GetFileName(filePath);
                    //log.Info("Se ingresa a buscar el arhivo " + strNombreArchivo + " y lo comparo con el de la ruta " + strArchivoBusqueda + ";SISTEMA");
                    if (strArchivoBusqueda == strNombreArchivo)
                    {
                        strNombreArchivog = key;
                        if (!boolSoporte)
                            key = strNombreArchivo;
                        //key = strNombreArchivo;
                        //strNombreArchivog = key;
                        //log.Info("Se cargara el archivo: " + key + " en el blobstorage: " + destContainer + ";SISTEMA");
                        UploadBlob(container, key, filePath);
                    }
                }
                return container.Uri.ToString() + "/" + strNombreArchivog;
            }
            catch (Exception ex)
            {
                //log.Error(ex.ToString() + ";SISTEMA");
                throw new ArgumentNullException("Mensaje Se presentó un error al tratar de subir el archivo " + strNombreArchivo);
            }

        }

        static void UploadBlob(CloudBlobContainer container, string key, string fileName)
        {
            try
            {
                CloudBlockBlob b = container.GetBlockBlobReference(key);

                using (var fs = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    b.UploadFromStreamAsync(fs);
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.ToString() + ";SISTEMA");
                throw new ArgumentNullException("Mensaje Se presentó un error al momento de subir el archivo al contenedor");
            }
        }

        public string DescargaArchivoAzure(string strNombreArchivo, string strContainer)
        {
            try
            {

                //log.Info("Se descarga el archivo " + strNombreArchivo + ";SISTEMA");
                string connection = "DefaultEndpointsProtocol=https;AccountName=obrastorage;AccountKey=iYk2uKSqY+rcmRYKKuE/IW5WOLo4LuE/oYFUwDdpOMiS519P9tV6134v/eVEf3R2xgF/UvGWovlSO+OykSw/Wg==;EndpointSuffix=core.windows.net";
                string destContainer = "blobobra";
                CloudStorageAccount sa = CloudStorageAccount.Parse(connection);
                CloudBlobClient bc = sa.CreateCloudBlobClient();

                CloudBlobContainer container = bc.GetContainerReference(destContainer);
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(strNombreArchivo);

                var sasToken = cloudBlockBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                {
                    Permissions = SharedAccessBlobPermissions.Read,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddSeconds(20) // AddHours(1)//Assuming you want the link to expire after 1 hour
                });
                //log.Info("Se le asigna permiso de descarga al arhcivo " + strNombreArchivo + " por 20 segundos;SISTEMA");
                string strString = string.Format("{0}{1}", cloudBlockBlob.Uri.AbsoluteUri, sasToken);
                return strString;
            }
            catch (Exception ex)
            {
                //log.Error(ex.ToString().ToString() + ";SISTEMA");
                throw new ArgumentException("Mensaje Se presentó un error al momento de descargar el archivo");
            }
        }
    }
}
