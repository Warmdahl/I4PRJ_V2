using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;
using RCCS.DatabaseCitizenResidency.ViewModel;

namespace RCCS.DatabaseAPI.RCCSCitizenResidencyDbViewControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Coordinator")]
    public class CreateCitizenController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CreateCitizenController(RCCSContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(CreateCitizenViewModel ccvm)
        {
            Citizen citizen = new Citizen
            {
                FirstName = ccvm.FirstName,
                LastName = ccvm.LastName,
                CPR = ccvm.CPR
            };

            Relative relative = new Relative
            {
                FirstName = ccvm.RelativeFirstName,
                LastName = ccvm.RelativeLastName,
                PhoneNumber = ccvm.PhoneNumber,
                Relation = ccvm.Relation,
                IsPrimary = ccvm.IsPrimary,
                Citizen = citizen
            };

            ResidenceInformation residenceInformation = new ResidenceInformation
            {
                StartDate = ccvm.StartDate,
                ReevaluationDate = ccvm.ReevaluationDate,
                PlannedDischargeDate = ccvm.PlannedDischargeDate,
                ProspectiveSituationStatusForCitizen = ccvm.ProspectiveSituationStatusForCitizen,
                Citizen = citizen
            };

            CitizenOverview citizenOverview = new CitizenOverview
            {
                CareNeed = ccvm.CareNeed,
                PurposeOfStay = ccvm.PurposeOfStay,
                NumberOfReevaluations = 0,
                Citizen = citizen
            };

            var respiteCareHomeTemp = await _context.RespiteCareHomes.FirstOrDefaultAsync(rch => rch.Name == ccvm.RespiteCareHomeName);

            respiteCareHomeTemp.AvailableRespiteCareRooms = (respiteCareHomeTemp.AvailableRespiteCareRooms - 1);
            _context.Entry(respiteCareHomeTemp).State = EntityState.Modified;

            var rcrType = "";

            switch (ccvm.Type)
            {
                case 0:
                    rcrType = "Alm. plejebolig";
                    break;
                case 1:
                    rcrType = "Demensbolig";
                    break;
            }

            var availableRespiteCareRoom =
                await _context.RespiteCareRooms
                    .FirstOrDefaultAsync(rcr => (rcr.Type == rcrType)
                                                && (rcr.IsAvailable)
                                                && (rcr.RespiteCareHomeName == ccvm.RespiteCareHomeName));

            availableRespiteCareRoom.CitizenCPR = ccvm.CPR;
            availableRespiteCareRoom.IsAvailable = false;
            _context.Entry(availableRespiteCareRoom).State = EntityState.Modified;

            await _context.Relatives.AddAsync(relative);
            await _context.ResidenceInformations.AddAsync(residenceInformation);
            await _context.CitizenOverviews.AddAsync(citizenOverview);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (CitizenExists(citizen.CPR))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return CreatedAtAction("PostCitizen", new { id = ccvm.CPR }, ccvm);
        }
        private bool CitizenExists(long id)
        {
            return _context.Citizens.Any(e => e.CPR == id);
        }


        [HttpPut]
        public async Task<ActionResult<Citizen>> PutCitizen(CreateCitizenViewModel ccvm)
        {

            var existingCitizen = await _context.Citizens
                .Include(c => c.Relatives)
                .Include(c => c.ResidenceInformation)
                .Include(c => c.CitizenOverview)
                .Include(c => c.RespiteCareRoom)
                    .ThenInclude(c => c.RespiteCareHome)
                .Where(c => c.CPR == ccvm.CPR)
                .FirstOrDefaultAsync<Citizen>();

            if (existingCitizen != null)
            {
                //Citizen
                existingCitizen.FirstName = ccvm.FirstName;
                existingCitizen.LastName = ccvm.LastName;
                existingCitizen.CPR = existingCitizen.CPR; //Do not change CPR

                //only implementation for one relative
                foreach (var relative in existingCitizen.Relatives)
                {
                    //Relative
                    relative.FirstName = ccvm.RelativeFirstName;
                    relative.LastName = ccvm.RelativeLastName;
                    relative.PhoneNumber = ccvm.PhoneNumber;
                    relative.Relation = ccvm.Relation;
                    relative.IsPrimary = ccvm.IsPrimary;
                }

                //Residence information
                existingCitizen.ResidenceInformation.StartDate = ccvm.StartDate;
                existingCitizen.ResidenceInformation.ReevaluationDate = ccvm.ReevaluationDate;
                existingCitizen.ResidenceInformation.PlannedDischargeDate = ccvm.PlannedDischargeDate;
                existingCitizen.ResidenceInformation.ProspectiveSituationStatusForCitizen = ccvm.ProspectiveSituationStatusForCitizen;

                //CitizenOverview
                existingCitizen.CitizenOverview.CareNeed = ccvm.CareNeed;
                existingCitizen.CitizenOverview.PurposeOfStay = ccvm.PurposeOfStay;

                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{cpr}")]
        public async Task<CreateCitizenViewModel> GetCreateCitizin(long cpr)
        {
            var citizen = await _context.Citizens
                .AsNoTracking()
                .Include(c => c.Relatives)
                .Include(c => c.ResidenceInformation)
                .Include(c => c.CitizenOverview)
                .Include(c => c.RespiteCareRoom)
                    .ThenInclude(c => c.RespiteCareHome)
                .FirstOrDefaultAsync(c => c.CPR == cpr);

            int temp = 0;
            if (citizen.RespiteCareRoom.Type == "Demensbolig")
            {
                temp = 1;
            };

            CreateCitizenViewModel ccvm = new CreateCitizenViewModel
            {
                CPR = citizen.CPR,
                FirstName = citizen.FirstName,
                LastName = citizen.LastName,
                RelativeFirstName = citizen.Relatives[0].FirstName,
                RelativeLastName = citizen.Relatives[0].LastName,
                PhoneNumber = citizen.Relatives[0].PhoneNumber,
                Relation = citizen.Relatives[0].Relation,
                IsPrimary = citizen.Relatives[0].IsPrimary,
                StartDate = citizen.ResidenceInformation.StartDate,
                ReevaluationDate = citizen.ResidenceInformation.ReevaluationDate,
                PlannedDischargeDate = citizen.ResidenceInformation.PlannedDischargeDate,
                ProspectiveSituationStatusForCitizen = citizen.ResidenceInformation.ProspectiveSituationStatusForCitizen,
                CareNeed = citizen.CitizenOverview.CareNeed,
                PurposeOfStay = citizen.CitizenOverview.PurposeOfStay,
                RespiteCareHomeName = citizen.RespiteCareRoom.RespiteCareHomeName,
                Type = temp
            };

            return ccvm;
        }

    }


}
