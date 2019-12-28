using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityRemedy.Controllers
{
    public class ServiceProviderMaintenanceCommunityController : ApiController
    {
        [Authorize(Roles = "Admin,CommunityOwner,Resident,ServiceProvider")]
        [HttpGet]
        [Route("api/ServiceProviderMaintenanceCommunity/GetServiceProviderMaintenanceCommunity")]
        public HttpResponseMessage GetServiceProviderMaintenanceCommunity()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                List<ServiceProviderMaintenanceCommunity> serviceProviderMaintenanceCommunities;
                if (User.IsInRole("CommunityOwner"))
                {
                    serviceProviderMaintenanceCommunities = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Include("Community").Include("MaintenanceService").Include("ServiceProvider").Where(s => s.Community.UserId == userId).ToList();
                }
                else if (User.IsInRole("ServiceProvider"))
                {
                    serviceProviderMaintenanceCommunities = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Include("Community").Include("MaintenanceService").Include("ServiceProvider").Where(s => s.ServiceProvider.UserId == userId).ToList();
                }
                else if (User.IsInRole("Resident"))
                {
                    var resident = communityRemedyDBEntities.Residents.FirstOrDefault(r => r.UserId == userId);
                    serviceProviderMaintenanceCommunities = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Include("Community").Include("MaintenanceService").Include("ServiceProvider").Where(s => s.Community.CommunityID == resident.CommunityId).ToList();
                }
                else
                {
                    serviceProviderMaintenanceCommunities = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Include("Community").Include("MaintenanceService").Include("ServiceProvider").ToList();
                }
                if (serviceProviderMaintenanceCommunities != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, serviceProviderMaintenanceCommunities);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Service providers - maintenance service - community relationship found in database");
                }
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,ServiceProvider")]
        [HttpPost]
        [Route("api/ServiceProviderMaintenanceCommunity/CreateServiceProviderMaintenanceCommunity")]
        public HttpResponseMessage CreateServiceProviderMaintenanceCommunity([FromBody]ServiceProviderMaintenanceCommunity serviceProviderMaintenanceCommunity)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    serviceProviderMaintenanceCommunity.ServiceProvider = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(p => p.ProviderId == serviceProviderMaintenanceCommunity.ProviderId);
                    serviceProviderMaintenanceCommunity.Community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.CommunityID == serviceProviderMaintenanceCommunity.CommunityId);
                    serviceProviderMaintenanceCommunity.MaintenanceService = communityRemedyDBEntities.MaintenanceServices.FirstOrDefault(m => m.ServiceId == serviceProviderMaintenanceCommunity.ServiceId);

                    if (User.IsInRole("CommunityOwner"))
                    {
                        if (serviceProviderMaintenanceCommunity.Community.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create Service providers - maintenance service - community relationship");
                        }
                    }
                    else if (User.IsInRole("ServiceProvider"))
                    {
                        if (serviceProviderMaintenanceCommunity.ServiceProvider.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create Service providers - maintenance service - community relationship");
                        }
                    }
                    
                    communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Add(serviceProviderMaintenanceCommunity);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, serviceProviderMaintenanceCommunity);
                    message.Headers.Location = new Uri(Request.RequestUri + serviceProviderMaintenanceCommunity.RelationId.ToString());
                    return message;
                    
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,ServiceProvider")]
        [HttpPut]
        [Route("api/ServiceProviderMaintenanceCommunity/UpdateServiceProviderMaintenanceCommunity")]
        public HttpResponseMessage UpdateServiceProviderMaintenanceCommunity(int id, [FromBody]ServiceProviderMaintenanceCommunity serviceProviderMaintenanceCommunity)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    serviceProviderMaintenanceCommunity.ServiceProvider = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(p => p.ProviderId == serviceProviderMaintenanceCommunity.ProviderId);
                    serviceProviderMaintenanceCommunity.Community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.CommunityID == serviceProviderMaintenanceCommunity.CommunityId);
                    serviceProviderMaintenanceCommunity.MaintenanceService = communityRemedyDBEntities.MaintenanceServices.FirstOrDefault(m => m.ServiceId == serviceProviderMaintenanceCommunity.ServiceId);

                    if (User.IsInRole("CommunityOwner"))
                    {
                        if (serviceProviderMaintenanceCommunity.Community.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update Service providers - maintenance service - community relationship");
                        }
                    }
                    else if (User.IsInRole("ServiceProvider"))
                    {
                        if (serviceProviderMaintenanceCommunity.ServiceProvider.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update Service providers - maintenance service - community relationship");
                        }
                    }

                    var serviceProviderMaintenanceCommunityTobeUpdated = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.FirstOrDefault(r => r.RelationId == id);
                    if (serviceProviderMaintenanceCommunityTobeUpdated != null)
                    {
                        serviceProviderMaintenanceCommunityTobeUpdated.CommunityId = serviceProviderMaintenanceCommunity.CommunityId;
                        serviceProviderMaintenanceCommunityTobeUpdated.ProviderId = serviceProviderMaintenanceCommunity.ProviderId;
                        serviceProviderMaintenanceCommunityTobeUpdated.ServiceId = serviceProviderMaintenanceCommunity.ServiceId;
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, serviceProviderMaintenanceCommunityTobeUpdated);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service providers - maintenance service - community relationship with relation id " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner")]
        [HttpDelete]
        [Route("api/ServiceProviderMaintenanceCommunity/DeleteServiceProviderMaintenanceCommunity")]
        public HttpResponseMessage DeleteServiceProviderMaintenanceCommunity(int id)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    var serviceProviderMaintenanceCommunityTobeDeleted = communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Include("Community").FirstOrDefault(r => r.RelationId == id);
                    if (User.IsInRole("CommunityOwner"))
                    {
                        if (serviceProviderMaintenanceCommunityTobeDeleted.Community.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to delete Service providers - maintenance service - community relationship");
                        }
                    }
                    if (serviceProviderMaintenanceCommunityTobeDeleted != null)
                    {
                        communityRemedyDBEntities.ServiceProviderMaintenanceCommunities.Remove(serviceProviderMaintenanceCommunityTobeDeleted);
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service providers - maintenance service - community relationship with relation id " + id.ToString() + " not found");
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
