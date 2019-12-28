using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommunityRemedy.Controllers
{
    public class ServiceRequestController : ApiController
    {
        [Authorize(Roles = "Admin,CommunityOwner,Resident,ServiceProvider")]
        [HttpGet]
        [Route("api/ServiceRequest/GetServiceRequest")]
        public HttpResponseMessage GetServiceRequest()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                List<ServiceRequest> serviceRequests;
                if (User.IsInRole("CommunityOwner"))
                {
                    serviceRequests = communityRemedyDBEntities.ServiceRequests.Where(s => s.Community.UserId == userId).ToList();
                }
                else if (User.IsInRole("ServiceProvider"))
                {
                    serviceRequests = communityRemedyDBEntities.ServiceRequests.Where(s => s.ServiceProvider.UserId == userId).ToList();
                }
                else if (User.IsInRole("Resident"))
                {
                    serviceRequests = communityRemedyDBEntities.ServiceRequests.Where(s => s.Resident.UserId == userId).ToList();
                }
                else
                {
                    serviceRequests = communityRemedyDBEntities.ServiceRequests.ToList();
                }
                if (serviceRequests != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, serviceRequests);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Service providers - maintenance service - community relationship found in database");
                }
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,Resident")]
        [HttpPost]
        [Route("api/ServiceRequest/CreateServiceRequest")]
        public HttpResponseMessage CreateServiceRequest([FromBody]ServiceRequest serviceRequest)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    if (User.IsInRole("CommunityOwner"))
                    {
                        if (serviceRequest.Community.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create service request for community id " + serviceRequest.CommunityId);
                        }
                    }
                    else if (User.IsInRole("Resident"))
                    {
                        if (serviceRequest.Resident.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create service request for resident " + serviceRequest.Resident.ResidentId);
                        }
                    }

                    communityRemedyDBEntities.ServiceRequests.Add(serviceRequest);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, serviceRequest);
                    message.Headers.Location = new Uri(Request.RequestUri + serviceRequest.RequestId.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[Authorize(Roles = "Admin,CommunityOwner,ServiceProvider")]
        //[HttpPut]
        //[Route("api/ServiceRequest/UpdateServiceRequest")]
        //public HttpResponseMessage UpdateServiceRequest(int id, [FromBody]ServiceRequest serviceRequest)
        //{
        //    try
        //    {
        //        using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
        //        {
        //            var userId = User.Identity.GetUserId();
        //            if (User.IsInRole("CommunityOwner"))
        //            {
        //                if (serviceRequest.Community.UserId != userId)
        //                {
        //                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update Service providers - maintenance service - community relationship");
        //                }
        //            }
        //            else if (User.IsInRole("ServiceProvider"))
        //            {
        //                if (serviceRequest.ServiceProvider.UserId != userId)
        //                {
        //                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update Service providers - maintenance service - community relationship");
        //                }
        //            }

        //            var serviceRequestTobeUpdated = communityRemedyDBEntities.ServiceRequests.FirstOrDefault(r => r.RelationId == id);
        //            if (serviceRequestTobeUpdated != null)
        //            {
        //                serviceRequestTobeUpdated.CommunityId = serviceRequest.CommunityId;
        //                serviceRequestTobeUpdated.ProviderId = serviceRequest.ProviderId;
        //                serviceRequestTobeUpdated.ServiceId = serviceRequest.ServiceId;
        //                communityRemedyDBEntities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK, serviceRequestTobeUpdated);
        //            }
        //            else
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service providers - maintenance service - community relationship with relation id " + id.ToString() + " not found");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //[Authorize(Roles = "Admin,CommunityOwner")]
        //[HttpDelete]
        //[Route("api/ServiceRequest/DeleteServiceRequest")]
        //public HttpResponseMessage DeleteServiceRequest(int id)
        //{
        //    try
        //    {
        //        using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
        //        {
        //            var userId = User.Identity.GetUserId();
        //            var serviceRequestTobeDeleted = communityRemedyDBEntities.ServiceRequests.FirstOrDefault(r => r.RelationId == id);
        //            if (User.IsInRole("CommunityOwner"))
        //            {
        //                if (serviceRequestTobeDeleted.Community.UserId != userId)
        //                {
        //                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to delete Service providers - maintenance service - community relationship");
        //                }
        //            }
        //            if (serviceRequestTobeDeleted != null)
        //            {
        //                communityRemedyDBEntities.ServiceRequests.Remove(serviceRequestTobeDeleted);
        //                communityRemedyDBEntities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK);
        //            }
        //            else
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service providers - maintenance service - community relationship with relation id " + id.ToString() + " not found");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
    }
}
