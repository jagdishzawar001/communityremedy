using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CommunityRemedy.Controllers
{
    public class ResidentController : ApiController
    {
        [Authorize(Roles = "Admin,CommunityOwner,Resident")]
        [HttpGet]
        [Route("api/Resident/GetResidents")]
        public HttpResponseMessage GetResidents()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                List<Resident> residents;

                if (User.IsInRole("Resident"))
                {
                    residents = communityRemedyDBEntities.Residents.Include("Community").Where(r => r.UserId == userId).ToList();
                }
                if (User.IsInRole("CommunityOwner"))
                {
                    var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.UserId == userId);
                    residents = communityRemedyDBEntities.Residents.Include("Community").Where(r => r.CommunityId == community.CommunityID).ToList();
                }
                else
                {
                    residents = communityRemedyDBEntities.Residents.Include("Community").ToList();
                }

                if (residents != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, residents);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No residents found in database");
                }
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,Resident")]
        [HttpGet]
        [Route("api/Resident/GetResidents/Id")]
        public HttpResponseMessage GetResidents(int id)
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                Resident resident=null;
                if (User.IsInRole("Resident"))
                {
                    resident = communityRemedyDBEntities.Residents.Include("Community").FirstOrDefault(r => r.ResidentId == id && r.UserId==userId);
                }
                else if (User.IsInRole("CommunityOwner"))
                {
                    var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.UserId == userId);
                    resident = communityRemedyDBEntities.Residents.Include("Community").FirstOrDefault(r => r.ResidentId == id && r.CommunityId ==community.CommunityID);
                }
                else
                {
                    resident = communityRemedyDBEntities.Residents.Include("Community").FirstOrDefault(r => r.ResidentId == id);
                }
                    
                if (resident != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, resident);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resident with resident id " + id.ToString() + " not found");
                }
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,Resident")]
        [HttpPost]
        [Route("api/Resident/CreateResident")]
        public HttpResponseMessage CreateResident([FromBody]Resident resident)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    if (User.IsInRole("CommunityOwner"))
                    {
                        var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.UserId == userId);
                        if(community.CommunityID != resident.CommunityId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"The user is not authorise to create resident");
                        }
                    }
                    else if (User.IsInRole("Resident"))
                    {
                        if(userId!=resident.UserId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to create resident");
                        }
                    }
                    communityRemedyDBEntities.Residents.Add(resident);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, resident);
                    message.Headers.Location = new Uri(Request.RequestUri + resident.ResidentId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Admin,CommunityOwner,Resident")]
        [HttpPut]
        [Route("api/Resident/UpdateResident")]
        public HttpResponseMessage UpdateResident(int id, [FromBody]Resident resident)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    if (User.IsInRole("CommunityOwner"))
                    {
                        var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.UserId == userId);
                        if (community.CommunityID != resident.CommunityId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update resident");
                        }
                    }
                    else if (User.IsInRole("Resident"))
                    {
                        if (userId != resident.UserId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update resident");
                        }
                    }

                    var residentTobeUpdated = communityRemedyDBEntities.Residents.FirstOrDefault(r => r.ResidentId == id);
                    if (residentTobeUpdated != null)
                    {
                        residentTobeUpdated.FirstName = resident.FirstName;
                        residentTobeUpdated.LastName = resident.LastName;
                        residentTobeUpdated.DOB = resident.DOB;
                        residentTobeUpdated.CommunityId = resident.CommunityId;
                        residentTobeUpdated.ApartmentNumber = resident.ApartmentNumber;
                        residentTobeUpdated.MobileContact = resident.MobileContact;
                        residentTobeUpdated.UserId = resident.UserId;
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, residentTobeUpdated);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resident with resident id " + id.ToString() + " not found");
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
        [Route("api/Resident/DeleteResident")]
        public HttpResponseMessage DeleteResident(int id)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    var residentTobeDeleted = communityRemedyDBEntities.Residents.FirstOrDefault(r => r.ResidentId == id);
                    if (User.IsInRole("CommunityOwner"))
                    {
                        var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.UserId == userId);
                        if (community.CommunityID != residentTobeDeleted.CommunityId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "The user is not authorise to update resident");
                        }
                    }
                    if (residentTobeDeleted != null)
                    {
                        communityRemedyDBEntities.Residents.Remove(residentTobeDeleted);
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resident with resident id " + id.ToString() + " not found");
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
