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
    public partial class ServiceProviderMaintenanceCommunity
    {
        public long ProviderId { get; set; }
        public long ServiceId { get; set; }
        public long CommunityId { get; set; }
        public long RelationId { get; set; }
    
        public virtual Community Community { get; set; }
        public virtual MaintenanceService MaintenanceService { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}