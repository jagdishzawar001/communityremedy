using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityRemedy.Controllers
{
    public class RoleController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetRoles()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var roles = communityRemedyDBEntities.ApplicationRoles.ToList();
                if (roles != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, roles);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No role found in ApplicationRoles");
                }
            }
        }
    }
}
