using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ModelPatient
    {
        static public List<string> GetData()
        {
            List<string> data = new List<string>();

            data.Add("Marko Molić");
            data.Add("Ana Patić");
            data.Add("Anastasija Opot");
            data.Add("Danijel Kat");
            data.Add("Rajko Obrić");
            data.Add("Jovana Javić");
            data.Add("Dragana Tatić");
            data.Add("Dušan Zarić");
            data.Add("Aleksandat Tot");
            data.Add("Milica Sarić");
            data.Add("Ratko Lović");
            data.Add("Ana Marija Rajić");

            return data;
        }
    }
}
