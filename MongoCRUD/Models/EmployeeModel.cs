using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCRUD.Models
{
    public class EmployeeModel
    {
        public ObjectId id { get; set; }
        public string fname { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
    }
}
