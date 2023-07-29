using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class Category : Controller
    {

        private readonly ApplicationDbContext _db;

        public Category(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable <Web.Models.Category> objCategoryList = _db.Categories.ToList();  
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        ////POST
        //[HttpPut]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Web.Models.Category obj)
        //{
        //    _db.Categories.Add(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("index");
        //}
        [HttpPost]
        public IActionResult Create(Web.Models.Category obj)
        {
            if(obj.name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match exatly to the name");
            }
            if (ModelState.IsValid) // Add model validation check
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            // If ModelState is invalid, return to the view with validation errors
            return View(obj);
        }

		//GET
		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            //below two codes also have retrive the data from the databse
            //var categoryFromDbFrist = _db.Categories.FirstOrDefault(u=>u.id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }


			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Web.Models.Category obj)
		{
			if (obj.name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The Display Order cannot match exatly to the name");
			}
			if (ModelState.IsValid) // Add model validation check
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["Success"] = "Category Updated successfully";
                return RedirectToAction("Index");
			}

			// If ModelState is invalid, return to the view with validation errors
			return View(obj);
		}


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            //below two codes also have retrive the data from the databse
            //var categoryFromDbFrist = _db.Categories.FirstOrDefault(u=>u.id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }


            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["Success"] = "Category Deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
