using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.ViewModel;

namespace RCCS.DatabaseAPI.RCCSDbViewControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    public class RespiteCareHomeListController : ControllerBase
    {
        private readonly RCCSContext _context;

        public RespiteCareHomeListController(RCCSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<RespiteCareHomeListViewModel>> GetRespiteCareHomeList()
        {
            List<RespiteCareHomeListViewModel> rchlvmList = new List<RespiteCareHomeListViewModel>();

            var respiteCareHomes =
                await _context.RespiteCareHomes
                    .AsNoTracking()
                    .Include(rch => rch.RespiteCareRooms)
                        .ThenInclude(rcr => rcr.Citizen)
                            .ThenInclude(c => c.ResidenceInformation)
                    .ToListAsync();

            foreach (var respiteCareHome in respiteCareHomes)
            {
                var respiteCareRoomList = respiteCareHome.RespiteCareRooms;

                List<DateTime> almPlejeboligDischargeDateList = new List<DateTime>();
                List<DateTime> demensBoligDischargeDateList = new List<DateTime>();

                var almAvailable = 0;
                var demensAvailable = 0;

                var maxAlm = 0;
                var maxDem = 0;
                
                foreach(var respiteCareRoom in respiteCareRoomList)
                {
                    switch(respiteCareRoom.Type)
                    {
                        case "Alm. plejebolig":

                            maxAlm++;

                            if (respiteCareRoom.IsAvailable)
                            {
                                almAvailable++;
                            }

                            if (respiteCareRoom.Citizen != null)
                            {
                                almPlejeboligDischargeDateList
                                    .Add(respiteCareRoom.Citizen.ResidenceInformation.PlannedDischargeDate);
                            }

                            break;

                        case "Demensbolig":

                            maxDem++;

                            if (respiteCareRoom.IsAvailable)
                            {
                                demensAvailable++;
                            }

                            if (respiteCareRoom.Citizen != null)
                            {
                                demensBoligDischargeDateList
                                    .Add(respiteCareRoom.Citizen.ResidenceInformation.PlannedDischargeDate);
                            }

                            break;
                    }
                }

                RespiteCareHomeListViewModel almPlejebolig = new RespiteCareHomeListViewModel
                {
                    RespiteCareHome = respiteCareHome.Name,
                    Address = respiteCareHome.Address,
                    Type = "Alm. plejebolig",
                    RespiteCareRoomsTotal = maxAlm.ToString(),
                    AvailableRespiteCareRooms = almAvailable.ToString(),

                    NextAvailableRespiteCareRoom = 
                        almPlejeboligDischargeDateList.Any() ? 
                            almPlejeboligDischargeDateList.Min().ToString("s") : 
                            DateTime.Now.ToString("s")
                };

                rchlvmList.Add(almPlejebolig);

                RespiteCareHomeListViewModel demensBolig = new RespiteCareHomeListViewModel
                {
                    RespiteCareHome = respiteCareHome.Name,
                    Address = respiteCareHome.Address,
                    Type = "Demensbolig",
                    RespiteCareRoomsTotal = maxDem.ToString(),
                    AvailableRespiteCareRooms = demensAvailable.ToString(),

                    NextAvailableRespiteCareRoom = demensBoligDischargeDateList.Any() ?
                        demensBoligDischargeDateList.Min().ToString("s") :
                        DateTime.Now.ToString("s")
                };

                rchlvmList.Add(demensBolig);
            }
            return rchlvmList;
        }
    }
}
