using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleServices.Data;
using VehicleServices.Interfaces;
using VehicleServices.Models;

namespace VehicleServices
{
    public class ModelServices : IVehicleModel
    {
        private readonly ApplicationDbContext _db;

        public ModelServices(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET ALL
        public PagedResult<VehicleModel> GetAllModel(Controller controller, string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            controller.ViewBag.CurrentSortOrder = sortOrder;
            controller.ViewBag.CurrentFilter = searchString;
            controller.ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var vehicles = from b in _db.VehicleModel
                           select b;

            var vehicleCount = vehicles.Count();

            //FILTERING
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(b => b.name.ToLower().Contains(searchString));
                vehicleCount = vehicles.Count();
            }

            //SORTING
            switch (sortOrder)
            {
                case "name":
                    vehicles = vehicles.OrderByDescending(b => b.name);
                    break;
                default:
                    vehicles = vehicles.OrderBy(b => b.name);
                    break;
            }

            //PAGING
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            vehicles = vehicles
                .Skip(ExcludeRecords)
                .Take(pageSize);

            var result = new PagedResult<VehicleModel>
            {
                Data = vehicles.AsNoTracking().ToList(),
                TotalItems = vehicleCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        // ADD GET
        public Vehicles AddModelGet()
        {
            Vehicles vehicles = new Vehicles();

            vehicles.vehicleMakeList = _db.VehicleMake.Select(s => new SelectListItem
            {
                Selected = false,
                Text = s.abrv,
                Value = s.id.ToString()
            });

            return vehicles;

        }

        // ADD POST
        public bool AddModel(ModelStateDictionary ModelState, Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Add(vehicles.vehicleModel);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // UPDATE/DELETE GET
        public async Task<VehicleModel?> UpdateDeleteModelGet(int? id)
        {
            var vehicleFromDb = await _db.VehicleModel.FindAsync(id);

            return vehicleFromDb;
        }

        // UPDATE POST
        public bool UpdatePostModel(ModelStateDictionary ModelState, VehicleModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleModel.Update(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // DELETE POST
        public async Task<bool> DeletePostModel(int? id)
        {
            var obj = await _db.VehicleModel.FindAsync(id);

            if (obj != null)
            {
                _db.VehicleModel.Remove(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
