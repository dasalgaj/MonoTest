using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VehicleServices.Models;

namespace VehicleServices.Interfaces
{
    public interface IVehicleModel
    {
        PagedResult<VehicleModel> GetAllModel(Controller controller, string searchString, string sortOrder, int pageNumber = 1, int pageSize = 3);
        Vehicles AddModelGet();
        bool AddModel(ModelStateDictionary ModelState, Vehicles vehicles);
        Task<VehicleModel?> UpdateDeleteModelGet(int? id);
        bool UpdatePostModel(ModelStateDictionary ModelState, VehicleModel vehicleMake);
        Task<bool> DeletePostModel(int? id);
    }
}
