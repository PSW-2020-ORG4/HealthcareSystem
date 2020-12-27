using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserService.CustomException;

namespace UserService.Notifications
{
    class TemplateFileRepository : ITemplateRepository
    {
        private string DirectoryPath;

        public TemplateFileRepository()
        {
            DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
        }

        public Template Get(string id)
        {
            string text = null;
            try
            {
                string filePath = Path.Combine(DirectoryPath, id);
                StreamReader reader = new StreamReader(filePath);
                text = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception)
            {
                throw new DataStorageException();
            }
            return new Template(text, "[", "]");
        }
    }
}
