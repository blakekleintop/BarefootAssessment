using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace BarefootAssessment.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(string Terms)
        {
            ViewData["Terms"] = Terms;
            /*
             * Our user's names are not dynamic; they're abstracted to a simple json string in lieue of a future dynamic implementation.
             */
            string json = "[{\"name\":\"Lovelace\"},{\"name\":\"Hamilton\"},{\"name\":\"Hopper\"},{\"name\":\"Torvalds\"},{\"name\":\"Gosling\"},{\"name\":\"Eich\"},{\"name\":\"Crockford\"},{\"name\":\"Dahl\"}]";
            IEnumerable<Models.User> Users = JsonConvert.DeserializeObject<IEnumerable<Models.User>>(json);

            if (!String.IsNullOrEmpty(Terms))
            {
                Users = FilterUsersByName(Terms, Users);
            }

            return View(Users);
        }

        public IEnumerable<Models.User> FilterUsersByName(string Terms, IEnumerable<Models.User> Users)
        {
            Users = Users.Where(s => s.LoweredName.Contains(Terms.ToLower())).OrderBy(user => user.Name);

            return Users;
        }
    }
}