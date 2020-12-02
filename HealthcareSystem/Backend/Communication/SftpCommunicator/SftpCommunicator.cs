using Backend.Model.Pharmacies;
using Microsoft.Extensions.Options;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backend.Communication.SftpCommunicator
{
    public class SftpCommunicator : ISftpCommunicator
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public SftpCommunicator(IOptions<SftpConfig> sftpOptions)
        {
            _host = sftpOptions.Value.Host;
            _port = sftpOptions.Value.Port;
            _username = sftpOptions.Value.Username;
            _password = sftpOptions.Value.Password;
        }

        public IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = ".")
        {
            using var client = new SftpClient(_host, _port == 0 ? 22 : _port, _username, _password);
            try
            {
                client.Connect();
                return client.ListDirectory(remoteDirectory);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return Enumerable.Empty<SftpFile>();
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void UploadFile(string localFilePath, string remoteFilePath)
        {
            using var client = new SftpClient(_host, _port == 0 ? 22 : _port, _username, _password);
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
            using var client = new SftpClient(_host, _port == 0 ? 22 : _port, _username, _password);
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
            using var client = new SftpClient(_host, _port == 0 ? 22 : _port, _username, _password);
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
