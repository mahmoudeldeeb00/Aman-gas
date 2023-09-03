using BL.DTOS;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IManagerService
    {
        public Task <Response<string>> TransfereSalesManAsync(int StationId, int SalesId);
        public Task<Response<string>> AcceptSalesRequestsAsync(int RequestId, int Deny , string Editor);
        public Task<Response<List<SalesRequestDTO>>> PendingSalesRequestsAsync();
        public Task<Response<FuelSettingDTO>> GetFuelSettingAsync(int FuelId );
        public Task<Response<string>> SetFuelSettingAsync(FuelSettingDTO DTO);
    }
}
