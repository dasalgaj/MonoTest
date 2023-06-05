using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using VehicleServices.Data;
using VehicleServices.Interfaces;
using VehicleServices.Models;

namespace VehicleServices
{
    public class MakeServices : IVehicleMake
    {
        private readonly ApplicationDbContext _db;

        public MakeServices(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET ALL
        public PagedResult<VehicleMake> GetAllMake(Controller controller, string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3)
        {
            controller.ViewBag.CurrentSortOrder = sortOrder;
            controller.ViewBag.CurrentFilter = searchString;
            controller.ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name" : "";

            var vehicles = from b in _db.VehicleMake
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

            var result = new PagedResult<VehicleMake>
            {
                Data = vehicles.AsNoTracking().ToList(),
                TotalItems = vehicleCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        // ADD POST
        public bool AddMake(ModelStateDictionary ModelState, VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleMake.Add(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // UPDATE/DELETE GET
        public async Task<VehicleMake?> UpdateDeleteMakeGet(int? id)
        {
            var vehicleFromDb = await _db.VehicleMake.FindAsync(id);

            return vehicleFromDb;
        }

        // UPDATE POST
        public bool UpdatePostMake(ModelStateDictionary ModelState, VehicleMake obj)
        {
            if (ModelState.IsValid)
            {
                _db.VehicleMake.Update(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }

        // DELETE POST
        public async Task<bool> DeletePostMake(int? id)
        {
            var obj = await _db.VehicleMake.FindAsync(id);

            if (obj != null)
            {
                _db.VehicleMake.Remove(obj);
                _db.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
