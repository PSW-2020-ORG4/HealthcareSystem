﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ModelDoctor
    {
        static public List<string> GetData()
        {
            List<string> data = new List<string>();

            data.Add("Opšta praksa - dr.Marko Marković");
            data.Add("Opšta praksa - dr.Marija Opić");
            data.Add("Opšta praksa - dr.Dragan Nikolić");
            data.Add("Oftamolog - dr.Petar Makor");
            data.Add("Ginekolog - dr.Ana Rac");
            data.Add("Plumolog - dr.Tatjana Mopić");
            data.Add("Hirurg - dr.Ognjen Katić");
            data.Add("Hirurg - dr.Jelena Obradović");
            data.Add("Radiolog - dr.Slavica Jelić");
            data.Add("Ortoped - dr.Jovan Ratić");
            data.Add("Kardiolog - dr.Vladislav Popović");
            

            return data;
        }
    }
}
