using Microsoft.AspNetCore.Mvc;
using VehicleServices.Data;
using VehicleServices.Interfaces;
using VehicleServices.Models;

namespace MonoTest.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleMake _service;

        public VehicleController(IVehicleMake service)
        {
            _service = service;
        }

        public IActionResult Index(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            var result = _service.GetAllMake(this, searchString, sortOrder, pageNumber, pageSize);

            return View(result);
        }

        //CREATE
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleMake obj)
        {

            if (_service.AddMake(ModelState, obj))
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

            if (_service.UpdateDeleteMakeGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteMakeGet(id));
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleMake obj)
        {

            if (_service.UpdatePostMake(ModelState, obj))
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

            if (_service.UpdateDeleteMakeGet(id) == null)
            {
                return NotFound();
            }

            return View(await _service.UpdateDeleteMakeGet(id));
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            Task<bool> deleteTask = _service.DeletePostMake(id);

            bool b = await deleteTask;

            if (b)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}