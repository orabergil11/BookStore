using BookStore.DataBase.Interfaces;
using BookStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Windows.Storage;

namespace BookStore.DataBase
{
    internal class FileSystemDataContext : IRepository<Product>
    {
        private StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;
        private StorageFile _storageFile;
        private IList<Product> _products;

        private static FileSystemDataContext _instance;
        public static FileSystemDataContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FileSystemDataContext();
                return _instance;
            }
        }

        private FileSystemDataContext()
        {
            Init();
        }

        private async void Init()
        {
            _storageFile = await _storageFolder.CreateFileAsync("products.json",
                CreationCollisionOption.OpenIfExists);

            _products = new List<Product>();

            //todo:
            //read file content
            //deserialize to _products
        }

        public bool Delete(Guid id)
        {
            return _products.Remove(Get(id));
        }

        public Product Get(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> Get(Predicate<Product> filter = null)
        {
            if (filter == null)
                return _products;

            return _products.Where(p => filter.Invoke(p));
        }

        public Guid Insert(Product item)
        {
            _products.Add(item);
            return item.Id;
        }

        public Product Update(Product item)
        {
            Delete(item.Id);
            Insert(item);
            return item;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save<T>(T data, string fileName)
        {
            JsonSerializer jsonSerializer = new JsonSerializer { Formatting = Newtonsoft.Json.Formatting.Indented };
            using (var streamFile = File.Open(fileName, FileMode.Create))
            using (var writer = new StreamWriter(streamFile))
            using (var jsonWriter = new JsonTextWriter(writer))
                jsonSerializer.Serialize(jsonWriter, data, typeof(T));
        }
    }
}
