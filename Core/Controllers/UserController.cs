using Core.Database;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class UserController
    {
        public ApplicationUser FindById(object userId)
        {
            ApplicationUser user = null;

            using (var db = new DatabaseContext())
            {
                user = db.Users.Find(userId);
            }

            return user;
        }
    }
}
