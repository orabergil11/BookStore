using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookStore.Server
{
    public class DataBase
    {
        XmlSerializer res;

        public void Save<T>(T data, string fileName)
        {
            res = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                res.Serialize(fs, data);
            }
        }
    }
}
