using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazilimYonetimAraci.Models
{
    public class ModelUserRolesAndRoleNames
    {
        public ModelUserRoles UserRoles { get; set; }

        public ModelRoleNames RoleNames { get; set; }

        public ModelUserRolesAndRoleNames()
        {
            UserRoles = new ModelUserRoles();
            RoleNames = new ModelRoleNames();
        }

    }
}