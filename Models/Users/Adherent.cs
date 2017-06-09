﻿using Stolons.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Stolons.Configurations;

namespace Stolons.Models.Users
{
    public class Adherent : IAdherent
    {
        [Key]
        public Guid Id { get; set; }
        
        [Display(Name = "Est web admin")]
        public bool IsWebAdmin { get; set; }

        public List<AdherentStolon> AdherentStolons { get; set; }
        public List<ConsumerBill> ConsumerBills{ get; set; }
        public List<ProducerBill> ProducerBills { get; set; }
        public List<AdherentTransaction> Transactions { get; set; }

        [Display(Name = "Nom")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Prénom")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Avatar")]
        public string AvatarFileName { get; set; } //Nom de l'ilmage sur le server

        public string AvatarFilePath
        {
            get
            {
                if (String.IsNullOrWhiteSpace(AvatarFileName))
                    return "\\"+ Path.Combine(Configurations.AvatarStockagePath, Configurations.DefaultFileName);
                return "\\" + Path.Combine(Configurations.AvatarStockagePath, AvatarFileName);
            }
        }

        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Display(Name = "Code postal")]
        [Required]
        public string PostCode { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Display(Name = "Courriel")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        [Phone]
        public string PhoneNumber { get; set; }

        #region MailSubscription
        [Display(Name = "Informations importantes")]
        public bool ReceivedInformationsEmail { get; set; }

        [Display(Name = "Nouvelles")]
        public bool ReceivedNewsByEmail { get; set; } = false;

        [Display(Name = "Rappel à l'ouverture des commandes")]
        public bool ReceivedProductListByEmail { get; set; } = false;

        [Display(Name = "Bons plans")]
        public bool ReceivedGoodPlanByEmail { get; set; } = false;

        #endregion MailSubscription

        #region Producer

        [Display(Name = "Raison sociale")]
        public string CompanyName { get; set; }
        [Display(Name = "Superficie en m²")]
        public int Area { get; set; }

        private IList<string> _ExploitationPictures;
        [Display(Name = "Galerie d'exploitations")]
        [NotMapped]
        public IList<string> ExploitationPictures
        {
            get
            {

                return _ExploitationPictures;

            }
            set
            {
                _ExploitationPictures = value;
            }
        }
        public string ExploitationPicuresSerialized
        {
            get
            {
                if (_ExploitationPictures == null)
                {
                    return null;
                }
                else
                {
                    return String.Join(";", _ExploitationPictures);
                }
            }
            set
            {
                _ExploitationPictures = Tools.SerializeStringToList(value);
            }
        }

        public virtual ICollection<Product> Products { get; set; }

        [Display(Name = "Production")]
        public string Production { get; set; }
        [Display(Name = "Texte libre")]
        public string OpenText { get; set; }
        [Display(Name = "Année d'installation")]
        public int StartDate { get; set; }
        [Display(Name = "Lien vers le site web")]
        [Url]
        public string WebSiteLink { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        #endregion Produce
        
    }


}