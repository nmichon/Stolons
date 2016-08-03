﻿using Stolons.Models;
using Stolons.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stolons.ViewModels.Consumers
{
    public class ConsumerViewModel : IUserViewModel
    {
        public ConsumerViewModel()
        {

        }
        public ConsumerViewModel(Consumer consumer, Configurations.Role userRole)
        {
            Consumer = consumer;
            UserRole = userRole;
            OriginalEmail = consumer.Email;
        }

        public string OriginalEmail { get; set; }

        public Consumer Consumer { get; set; }

        [Display(Name = "Droit utilisateur ")]
        public Configurations.Role UserRole { get; set; }

        public User User
        {
            get
            {
                return Consumer;
            }
        }
    }
}
