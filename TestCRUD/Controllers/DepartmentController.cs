using Microsoft.AspNetCore.Mvc;
using TestCRUD.Data;

namespace TestCRUD.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDBContext db;

        public DepartmentController(ApplicationDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.Departments.OrderBy(m=>m.Name).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Edit(int ?id)
        {
            var res = db.Departments.Find(id);
            if (id != null)
            {
                return View(res);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Update(department);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(department);
        }

        public IActionResult Delete(int? id)
        {
            var res = db.Departments.Find(id);
            if (id != null)
            {
                db.Departments.Remove(res);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(res);
        }

    }
}
