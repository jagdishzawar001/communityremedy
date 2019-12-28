using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityRemedy.Controllers
{
    public class ServiceProviderController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/ServiceProvider/GetServiceProviders")]
        public HttpResponseMessage GetServiceProviders()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var serviceProviders = communityRemedyDBEntities.ServiceProviders.ToList();
                if (serviceProviders != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, serviceProviders);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Service providers found in database");
                }
            }

        }

        [Authorize(Roles = "Admin,ServiceProvider")]
        [HttpGet]
        [Route("api/ServiceProvider/GetServiceProviders/Id")]
        public HttpResponseMessage GetServiceProviders(int id)
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                ServiceProvider serviceProvider = null;
                if (User.IsInRole("ServiceProvider"))
                {
                    serviceProvider = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(s => s.ProviderId == id && s.UserId==userId);
                }
                else
                {
                    serviceProvider = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(s => s.ProviderId == id);
                }

                if (serviceProvider != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, serviceProvider);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service provider with provider id " + id.ToString() + " either not found or you are not authorise to access it");
                }
            }
        }

        [Authorize(Roles = "Admin,ServiceProvider")]
        [HttpPost]
        [Route("api/ServiceProvider/CreateServiceProvider")]
        public HttpResponseMessage CreateServiceProvider([FromBody]ServiceProvider serviceProvider)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    
                    if (User.IsInRole("ServiceProvider"))
                    {
                        if (userId != serviceProvider.UserId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create resident");
                        }
                    }
                    communityRemedyDBEntities.ServiceProviders.Add(serviceProvider);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, serviceProvider);
                    message.Headers.Location = new Uri(Request.RequestUri + serviceProvider.ProviderId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin,ServiceProvider")]
        [HttpPut]
        [Route("api/ServiceProvider/UpdateServiceProvider")]
        public HttpResponseMessage UpdateServiceProvider(int id, [FromBody]ServiceProvider serviceProvider)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    if (User.IsInRole("ServiceProvider"))
                    {
                        if (userId != serviceProvider.UserId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update resident");
                        }
                    }

                    var serviceProviderTobeUpdated = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(s => s.ProviderId == id);
                    if (serviceProviderTobeUpdated != null)
                    {
                        serviceProviderTobeUpdated.Address = serviceProvider.Address;
                        serviceProviderTobeUpdated.City = serviceProvider.City;
                        serviceProviderTobeUpdated.ContactNumber = serviceProvider.ContactNumber;
                        serviceProviderTobeUpdated.Country = serviceProvider.Country;
                        serviceProviderTobeUpdated.GSTN = serviceProvider.GSTN;
                        serviceProviderTobeUpdated.ProviderName = serviceProvider.ProviderName;
                        serviceProviderTobeUpdated.UserId = serviceProvider.UserId;
                        serviceProviderTobeUpdated.Website = serviceProvider.Website;
                        serviceProviderTobeUpdated.Zip = serviceProvider.Zip;
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, serviceProviderTobeUpdated);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service provider with provider id " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin,ServiceProvider")]
        [HttpDelete]
        [Route("api/ServiceProvider/DeleteServiceProvider")]
        public HttpResponseMessage DeleteServiceProvider(int id)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    var serviceProviderTobeDeleted = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(s => s.ProviderId == id);
                    if (User.IsInRole("ServiceProvider"))
                    {
                        var serviceProvider = communityRemedyDBEntities.ServiceProviders.FirstOrDefault(s => s.UserId == userId);
                        if (serviceProvider.ProviderId != serviceProviderTobeDeleted.ProviderId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to delete service provider");
                        }
                    }
                    if (serviceProviderTobeDeleted != null)
                    {
                        communityRemedyDBEntities.ServiceProviders.Remove(serviceProviderTobeDeleted);
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service provider with provider id " + id.ToString() + " not found");
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
