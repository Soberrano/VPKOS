﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UserProfile
    {
        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual int Id { get; set; }
        
        public virtual string UserName { get; set; }

        public UserProfile() : this(null) { }

        public UserProfile(User user)
        {
            if (user != null)
            {
                Email = user.Email;
                EmailConfirmed = user.EmailConfirmed;
                Id = user.Id;
                UserName = user.UserName;
            }
        }
    }
}