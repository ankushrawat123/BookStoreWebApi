using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class BookRL: IBookRL
    {
        //private readonly IConfiguration configuration;
        //private readonly IMongoCollection<Book> _book;
        //public BookRL(IConfig _config,IConfiguration configuration)
        //{
        //    this.configuration = configuration;
        //    var bookclient = new MongoClient(_config.ConnectionString);
        //    var database = bookclient.GetDatabase(_config.DatabaseName);
        //    _book = database.GetCollection<Book>("_book");
        //}

        
    }
}
