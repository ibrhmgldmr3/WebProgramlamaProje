using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaProje.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAnalyticsController : ControllerBase
    {
        private readonly SalonDbContext _context;

        public EmployeeAnalyticsController(SalonDbContext context)
        {
            _context = context;
        }
        private string GetUserRole()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail != null)
            {
                var user = _context.Kullanicilar.SingleOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    return user.Role;
                }
            }
            return null;
        }


        // GET: api/EmployeeAnalytics/Productivity
        [HttpGet("Productivity")]
        public async Task<ActionResult<IEnumerable<Calisan>>> GetProductivities()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }

            return await _context.Calisanlar
                                 .Include(ep => ep.CalisanId)
                                 .ToListAsync();
        }

        // GET: api/EmployeeAnalytics/Earnings
        [HttpGet("Earnings")]
        public async Task<ActionResult<IEnumerable<Calisan>>> GetEarnings()
        {
            var userRole = GetUserRole();
            if (userRole != "Admin")
            {
                return Unauthorized();
            }

            return await _context.Calisanlar
                                 .Include(ee => ee.CalisanId)
                                 .ToListAsync();
        }
    }
}