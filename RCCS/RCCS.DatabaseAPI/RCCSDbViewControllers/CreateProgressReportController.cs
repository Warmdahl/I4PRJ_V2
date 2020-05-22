﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.Database.Data;
using RCCS.Database.Model;
using RCCS.Database.ViewModel;

namespace RCCS.RCCSDbViewControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    public class CreateProgressReportController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CreateProgressReportController(RCCSContext context)
        {
            _context = context;
        }

        [HttpGet("{cpr}")]
        public async Task<ProgressReportViewModel> GetProgressReport(long cpr)
        {
            var citizen = await _context.Citizens
                .AsNoTracking()
                .Include(c => c.RespiteCareRoom)
                .Include(c => c.ResidenceInformation)
                .FirstOrDefaultAsync(c => c.CPR == cpr);


            ProgressReportViewModel prvm = new ProgressReportViewModel
            {
                CPR = citizen.CPR,
                Name = citizen.FirstName + " " + citizen.LastName,
                RespiteCareHomeName = citizen.RespiteCareRoom.RespiteCareHomeName,
                PlannedDischargeDate = citizen.ResidenceInformation.PlannedDischargeDate
            };

            return prvm;
        }

        [HttpPost]
        public async Task<ActionResult<ProgressReport>> PostProgressReport(CreateProgressReportViewModel cprvm)
        {
            ProgressReport progressReport = new ProgressReport()
            {
                Date = DateTime.Now,
                Title = cprvm.Title,
                Report = cprvm.Report,
                ResponsibleCaretaker = cprvm.ResponsibleCaretaker,
                CitizenCPR = cprvm.CPR
                
            };

            await _context.ProgressReports.AddAsync(progressReport);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (ProgressReportExists(progressReport.Id))
                {
                    return Conflict();
                }
                else
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return CreatedAtAction("PostProgressReport", new { id = progressReport.Date }, progressReport);
        }

        private bool ProgressReportExists(long id)
        {
            return _context.ProgressReports.Any(e => e.Id == id);
        }

    }
}