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
using Stolons.Models.Messages;
using Stolons.ViewModels.News;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stolons.ViewModels.Chat;

namespace Stolons.Controllers
{
    public class ChatController : BaseController
    {

        public ChatController(ApplicationDbContext context, IHostingEnvironment environment,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceProvider serviceProvider) : base(serviceProvider, userManager, context, environment, signInManager)
        {

        }
        


        [HttpPost, ActionName("AddMessage")]
        public string AddMessage(string message)
        {
            var activeAdherentStolon = GetActiveAdherentStolon();
            _context.ChatMessages.Add(new ChatMessage(activeAdherentStolon, message));
            _context.SaveChanges();
            return JsonConvert.SerializeObject(true);
        }

    }
}
