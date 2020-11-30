using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.Pharmacies;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Backend.Service.SftpService
{
    public class SftpService : ISftpService
    {
        private readonly SftpConfig _config;

        public SftpService()
        {
            _config = new SftpConfig();
        }

        public IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = ".")
        {
            using var client = new SftpClient(_config.Host, _config.Port == 0 ? 22 : _config.Port, _config.Username, _config.Password);
            try
            {
                client.Connect();
                return client.ListDirectory(remoteDirectory);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            using var client = new SftpClient(_config.Host, _config.Port == 0 ? 22 : _config.Port, _config.Username, _config.Password);
            try
            {
                client.Connect();
                using var stream = File.OpenRead(localFilePath);
                client.UploadFile(stream, remoteFilePath, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void DownloadFile(string remoteFilePath, string localFilePath)
        {
            using var client = new SftpClient(_config.Host, _config.Port == 0 ? 22 : _config.Port, _config.Username, _config.Password);
            try
            {
                client.Connect();
                using var stream = File.Create(localFilePath);
                client.DownloadFile(remoteFilePath, stream, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void DeleteFile(string remoteFilePath)
        {
            using var client = new SftpClient(_config.Host, _config.Port == 0 ? 22 : _config.Port, _config.Username, _config.Password);
            try
            {
                client.Connect();
                client.Delete(remoteFilePath);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                client.Disconnect();
            }
        }
    }
}
