﻿using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_hero.Entities
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
