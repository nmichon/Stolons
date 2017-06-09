using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stolons.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using Stolons.Models.Users;

namespace Stolons.Controllers
{
    public class BillsController : BaseController
    {
        public BillsController(ApplicationDbContext context, IHostingEnvironment environment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider serviceProvider) : base(serviceProvider, userManager, context, environment, signInManager)
        {

        }

        // GET: Bills
        [Authorize()]
        public async Task<IActionResult> Index()
        {
            Adherent stolonsUser = await this.GetCurrentAdherentAsync();
            if(stolonsUser is Adherent)
            {
                return View(_context.ProducerBills.Where(x=>x.Adherent.Email == stolonsUser.Email).OrderBy(x=>x.EditionDate).ToList<IBill>());
            }
            else if (stolonsUser is Adherent)
            {
                return View(_context.ConsumerBills.Where(x => x.Adherent.Email == stolonsUser.Email).OrderBy(x => x.EditionDate).ToList<IBill>());
            }
            return View();//ERROR
        }
        
        // GET: ShowBill
        public IActionResult ShowBill(string id)
        {
            IBill bill = _context.ConsumerBills.FirstOrDefault(x => x.BillNumber == id);
            if(bill != null)
                return View(bill);
            bill = _context.ProducerBills.Include(x=>x.Adherent).FirstOrDefault(x => x.BillNumber == id);
            if (bill != null)
                return View(bill);
            //Bill not found
            return View(null);
        }
    }
}