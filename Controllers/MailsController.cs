using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stolons.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Stolons.Helpers;
using Stolons.Models.Users;
using System.Collections.Generic;
using Stolons.Services;
using Stolons.ViewModels.Mails;

namespace Stolons.Controllers
{
    public class MailsController : BaseController
    {


        public MailsController(ApplicationDbContext context, IHostingEnvironment environment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider serviceProvider) : base(serviceProvider, userManager, context, environment, signInManager)
        {   
        }



        // GET: News
        public IActionResult Index(MailMessage mailMessage)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            if (mailMessage == null)
                mailMessage = new MailMessage();
            return View(mailMessage);
        }

        // GET: News/Details/5
        public IActionResult Preview(MailMessage mailMessage)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            return View(mailMessage);
        }
        
        public IActionResult SendToSympathizers(MailMessage mailMessage)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            return View("Report", SendMail(_context.Adherents, mailMessage));
        }
        
        public IActionResult SendToConsumers(MailMessage mailMessage)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            return View("Report",SendMail(_context.Adherents, mailMessage));
        }
        
        public IActionResult SendToProducers(MailMessage mailMessage)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            return View("Report",SendMail(_context.Adherents, mailMessage));
        }

        // GET: News/Details/5
        public IActionResult SendToAllUser(MailMessage mailMessage,Stolon stolon)
        {
            if (!Authorized(Role.Volunteer))
                return Unauthorized();

            List<IAdherent> users = new List<IAdherent>();
            users.AddRange(_context.Sympathizers.Where(x=>x.StolonId == stolon.Id && x.ReceivedInformationsEmail));
            users.AddRange(_context.Adherents.Include(x=>x.AdherentStolons).Where(x=>x.AdherentStolons.Any(consStol=>consStol.StolonId == stolon.Id)));
            //On peut envoyer deux fois � un producteur qui est un consomateur
            users.AddRange(_context.Adherents.Include(x => x.AdherentStolons).Where(x => x.AdherentStolons.Any(consStol => consStol.StolonId == stolon.Id)));
            return View("Report",  SendMail(users.Distinct(),mailMessage));
        }

        private MailsSendedReport SendMail(IEnumerable<IAdherent> users, MailMessage mailMessage)
        {
            MailsSendedReport report = new MailsSendedReport();
            foreach (Adherent user in users)
            {
                if (!String.IsNullOrWhiteSpace(user.Email))
                { 
                    try
                    {
                        AuthMessageSender.SendEmail(user.Email,
                                                        user.Name,
                                                        mailMessage.Title,
                                                        mailMessage.Message);
                        report.MailsSended++;
                    }
                    catch(Exception except)
                    {
                        report.MailsNotSended++;
                    }
                }
            }
            return report;
        }
    }
}