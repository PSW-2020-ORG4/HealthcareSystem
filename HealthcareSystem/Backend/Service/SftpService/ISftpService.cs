using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Backend.Service.SftpService
{
    public interface ISftpService
    {
        IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = ".");
        void UploadFile(string localFilePath, string remoteFilePath);
        void DownloadFile(string remoteFilePath, string localFilePath);
        void DeleteFile(string remoteFilePath);
    }
}
