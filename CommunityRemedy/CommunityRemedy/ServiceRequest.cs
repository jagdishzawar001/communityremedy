//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommunityRemedy
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceRequest
    {
        public long RequestId { get; set; }
        public Nullable<long> ServiceId { get; set; }
        public string CreatedUserId { get; set; }
        public Nullable<long> CommunityId { get; set; }
        public Nullable<long> ProviderId { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string LastUpdatedByUserId { get; set; }
        public Nullable<long> ResidentId { get; set; }
    
        public virtual Community Community { get; set; }
        public virtual MaintenanceService MaintenanceService { get; set; }
        public virtual Resident Resident { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
