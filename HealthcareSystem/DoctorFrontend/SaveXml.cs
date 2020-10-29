using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WpfApp1
{
    class SaveXml
    {
        public static void saveData(object obj, string fileName)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(fileName);
            xs.Serialize(writer, obj);
            writer.Close();
        }
    }
}
