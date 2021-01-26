using System;
using System.IO;

namespace NotificationService
{
    class TemplateFileRepository : ITemplateRepository
    {
        private readonly string DirectoryPath;

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
                throw new DataStorageException("Template loading failed.");
            }
            return new Template(text, "[", "]");
        }
    }
}
