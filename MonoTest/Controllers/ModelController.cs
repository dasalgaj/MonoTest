using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleServices.Data;
using VehicleServices.Interfaces;
using VehicleServices.Models;

namespace MonoTest.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleModel _service;

        public ModelController(IVehicleModel service)
        {
            _service = service;
        }

        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            var result = _service.GetAllModel(this, searchString, sortOrder, pageNumber, pageSize);

            return View(result);
        }

        //CREATE
        //GET
        public IActionResult Create()
        {
            return View(_service.AddModelGet());
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicles obj)
        {

            if (_service.AddModel(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //EDIT
        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteModelGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteModelGet(id));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleModel obj)
        {

            if (_service.UpdatePostModel(ModelState, obj))
            {
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //DELETE
        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            if (_service.UpdateDeleteModelGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteModelGet(id));
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Task<bool> deleteTask = _service.DeletePostModel(id);

            bool b = await deleteTask;

            if (b)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}