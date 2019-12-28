using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityRemedy.Controllers
{
    public class MaintenanceServiceController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/MaintenanceService/GetMaintenanceService")]
        public HttpResponseMessage GetMaintenanceService()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var services = communityRemedyDBEntities.MaintenanceServices.ToList();
                
                if (services != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, services);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No maintenance service found in database");
                }
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/MaintenanceService/GetMaintenanceService/Id")]
        public HttpResponseMessage GetMaintenanceService(int id)
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var service = communityRemedyDBEntities.MaintenanceServices.FirstOrDefault(m=>m.ServiceId ==id);

                if (service != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, service);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No maintenance service for service id " +id.ToString() + " found");
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/MaintenanceService/CreateMaintenanceService")]
        public HttpResponseMessage CreateMaintenanceService([FromBody]MaintenanceService service)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    communityRemedyDBEntities.MaintenanceServices.Add(service);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, service);
                    message.Headers.Location = new Uri(Request.RequestUri + service.ServiceId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/MaintenanceService/UpdateMaintenanceService")]
        public HttpResponseMessage UpdateMaintenanceService(int id, [FromBody]MaintenanceService maintenanceService)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var serviceTobeUpdated = communityRemedyDBEntities.MaintenanceServices.FirstOrDefault(m => m.ServiceId == id);
                    if (serviceTobeUpdated != null)
                    {
                        serviceTobeUpdated.ServiceName = maintenanceService.ServiceName;
                        serviceTobeUpdated.Details = maintenanceService.Details;
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, serviceTobeUpdated);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Maintenance service with service id " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/MaintenanceService/DeleteMaintenanceService")]
        public HttpResponseMessage DeleteMaintenanceService(int id)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var serviceTobeDeleted = communityRemedyDBEntities.MaintenanceServices.FirstOrDefault(m => m.ServiceId == id);
                    if (serviceTobeDeleted != null)
                    {
                        communityRemedyDBEntities.MaintenanceServices.Remove(serviceTobeDeleted);
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Maintenace service with service id " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
