using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobPortal.Data.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.WebApp.Controllers
{
    [ApiController]
    [Route("jobCount")]
    public class JobCountController : ControllerBase
    {
        private readonly DataDbContext _context;
        public JobCountController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> JobCount()
        {
            DateTime lastWeek = DateTime.Now.AddDays(-7);
            int jobCount = await _context.Jobs
                .Where(j => j.CreateDate.HasValue && j.CreateDate.Value >= lastWeek)
                .CountAsync();

            return Ok(jobCount);
        }
    }
}