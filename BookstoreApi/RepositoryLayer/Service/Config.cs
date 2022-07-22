using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class Config : IConfig
    {
        public string ConnectionString {get;set;}
        public string DatabaseName {get;set;}
    }
}
