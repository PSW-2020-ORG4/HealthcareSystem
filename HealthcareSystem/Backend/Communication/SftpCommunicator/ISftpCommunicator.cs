using Renci.SshNet.Sftp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Communication.SftpCommunicator
{
    public interface ISftpCommunicator
    {
        IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = ".");
        Task<bool> UploadFile(string localFilePath, string remoteFilePath);
        void DownloadFile(string remoteFilePath, string localFilePath);
        void DeleteFile(string remoteFilePath);
    }
}
