using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VehicleServices.Models;

namespace VehicleServices.Interfaces
{
    public interface IVehicleMake
    {
        PagedResult<VehicleMake> GetAllMake(Controller controller, string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3);
        bool AddMake(ModelStateDictionary ModelState, VehicleMake vehicleMake);
        Task<VehicleMake?> UpdateDeleteMakeGet(int? id);
        bool UpdatePostMake(ModelStateDictionary ModelState, VehicleMake vehicleMake);
        Task<bool> DeletePostMake(int? id);
    }
}