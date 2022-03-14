using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoCRUD.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCRUD.Controllers
{
    public class DefaultController : Controller
    {
        MongoClient Client = null;
        IMongoDatabase db = null;
        public DefaultController(IConfiguration configuration)
        {
            Client = new MongoClient(configuration["MyKey"]);
            db = Client.GetDatabase("CompanyMongo");
        }
        public IActionResult Index()
        {
            var empdata = db.GetCollection<EmployeeModel>("tblEmployee").Find(new BsonDocument()).ToList();
            return View(empdata);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel obj)
        {
            var collection = db.GetCollection<EmployeeModel>("tblEmployee");
            collection.InsertOne(obj);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (ModelState.IsValid)
            {
                var collection = db.GetCollection<EmployeeModel>("tblEmployee")
                    .Find(Builders<EmployeeModel>.Filter.Eq("id", ObjectId.Parse(id)))
                    .SingleOrDefault();
                
                return View(collection);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel obj,string id)
        {
            var collection = db.GetCollection<EmployeeModel>("tblEmployee");
            var updateemp = collection.FindOneAndUpdate(
                Builders<EmployeeModel>.Filter.Eq("id", ObjectId.Parse(id)),
                Builders<EmployeeModel>.Update.Set("fname",obj.fname)
                .Set("email",obj.email).Set("mobile",obj.mobile));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var collection = db.GetCollection<EmployeeModel>("tblEmployee")
                    .Find(Builders<EmployeeModel>.Filter.Eq("id", ObjectId.Parse(id)))
                    .SingleOrDefault();
                return View(collection);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteRec(string id)
        {
            var collection = db.GetCollection<EmployeeModel>("tblEmployee");
            var delrec = collection.DeleteOne(Builders<EmployeeModel>.Filter.Eq("id", ObjectId.Parse(id)));
            return RedirectToAction("Index");
        }


        public IActionResult Details(string id)
        {
            var collection = db.GetCollection<EmployeeModel>("tblEmployee")
                   .Find(Builders<EmployeeModel>.Filter.Eq("id", ObjectId.Parse(id)))
                   .SingleOrDefault();
            return View(collection);
        }


    }
}
