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
    public partial class MaintenanceService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaintenanceService()
        {
            this.ServiceProviderMaintenanceCommunities = new HashSet<ServiceProviderMaintenanceCommunity>();
            this.ServiceRequests = new HashSet<ServiceRequest>();
        }
    
        public long ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Details { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProviderMaintenanceCommunity> ServiceProviderMaintenanceCommunities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}