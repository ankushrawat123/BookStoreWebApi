using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
