using Renci.SshNet.Sftp;
using System.Collections.Generic;

namespace Backend.Communication.SftpCommunicator
{
    public interface ISftpCommunicator
    {
        IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = ".");
        void UploadFile(string localFilePath, string remoteFilePath);
        void DownloadFile(string remoteFilePath, string localFilePath);
        void DeleteFile(string remoteFilePath);
    }
}
