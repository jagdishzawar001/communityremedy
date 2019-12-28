using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace CommunityRemedy.Controllers
{
    public class CommunityController : ApiController
    {
        [Authorize(Roles = "Admin,CommunityOwner")]
        [HttpGet]
        [Route("api/Community/GetCommunity")]
        public HttpResponseMessage GetCommunity()
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var userId = User.Identity.GetUserId();
                List<Community> communities;
                if (User.IsInRole("CommunityOwner"))
                {
                    communities = communityRemedyDBEntities.Communities.Where(c => c.UserId == userId).ToList();
                }
                else
                {
                    communities = communityRemedyDBEntities.Communities.ToList();
                }
                if (communities != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, communities);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No communities found in database");
                }
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/Community/GetCommunity/Id")]
        public HttpResponseMessage GetCommunity(int Id)
        {
            using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
            {
                var community = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.CommunityID == Id);
                if (community != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, community);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "community with community id " + Id.ToString() + " not found");
                }
            }
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("api/Community/CreateCommunity")]
        public HttpResponseMessage CreateCommunity([FromBody]Community community)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    communityRemedyDBEntities.Communities.Add(community);
                    communityRemedyDBEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, community);
                    message.Headers.Location = new Uri(Request.RequestUri + community.CommunityID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,CommunityOwner")]
        [Route("api/Community/UpdateCommunity")]
        public HttpResponseMessage UpdateCommunity(int id, [FromBody]Community community)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var userId = User.Identity.GetUserId();
                    var communityTobeUpdated = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.CommunityID == id);
                    if (communityTobeUpdated != null)
                    {
                        if (User.IsInRole("CommunityOwner") && communityTobeUpdated.UserId != userId)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "User is not authorized to update community with community id " + id.ToString() + " not found");
                        }
                        communityTobeUpdated.Address = community.Address;
                        communityTobeUpdated.City = community.City;
                        communityTobeUpdated.Country = community.Country;
                        communityTobeUpdated.EmailId = community.EmailId;
                        communityTobeUpdated.GovtRegistrationId = community.GovtRegistrationId;
                        communityTobeUpdated.MobileContact = community.MobileContact;
                        communityTobeUpdated.Name = community.Name;
                        communityTobeUpdated.PhoneContact = community.PhoneContact;
                        communityTobeUpdated.State = community.State;
                        communityTobeUpdated.UserId = community.UserId;
                        communityTobeUpdated.Zip = community.Zip;

                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, communityTobeUpdated);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Community with community id " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/Community/RemoveCommunity")]
        public HttpResponseMessage RemoveCommunity(int id)
        {
            try
            {
                using (CommunityRemedyDBEntities communityRemedyDBEntities = new CommunityRemedyDBEntities())
                {
                    var communityTobeDeleted = communityRemedyDBEntities.Communities.FirstOrDefault(c => c.CommunityID == id);
                    if (communityTobeDeleted != null)
                    {
                        communityRemedyDBEntities.Communities.Remove(communityTobeDeleted);
                        communityRemedyDBEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Community with community id " + id.ToString() + " not found");
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
