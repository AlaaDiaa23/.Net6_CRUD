using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCRUD.Data;

namespace TestCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public EmployeeController(ApplicationDBContext dBContext )
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            var res = dBContext.Employees.Include(m => m.Department).ToList();
            return View(res);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.depart = dBContext.Departments.OrderBy(m=>m.Name).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee e)
        {
            Upload(e);
            if (ModelState.IsValid)
            {
                dBContext.Employees.Add(e);
                dBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depart = dBContext.Departments.OrderBy(m => m.Name).ToList();

            return View();
        }

        private void Upload(Employee e)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                //upload an image
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                e.Image = ImageName;


            }
            else if (e.Image == null)
            {
                //default
                e.Image = "Default.jpg";
            }
            else
            {
                //no change in image in edit
                e.Image = e.Image;
            }
        }

        public IActionResult Edit(int id)
        {
            ViewBag.depart = dBContext.Departments.OrderBy(m => m.Name).ToList();
            var res = dBContext.Employees.Find(id);
           
                return View("Create",res);
            
        }
        [HttpPost]
        public IActionResult Edit(Employee e)
        {
            Upload(e);
            
            if (ModelState.IsValid)
            {
                dBContext.Employees.Update(e);
                dBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.depart = dBContext.Departments.OrderBy(m => m.Name).ToList();

            return View();
        }
        public IActionResult Delete(int id)
        {
            var res = dBContext.Employees.Find(id);
            dBContext.Employees.Remove(res);
            dBContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
