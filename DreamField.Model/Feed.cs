//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DreamField.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    
    public partial class Feed
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feed()
        {
            this.RationFeeds = new HashSet<RationFeed>();
        }
    
        public int id { get; set; }
        public decimal price { get; set; }
        public float current_amount { get; set; }
        public int farm_id { get; set; }
        public string name { get; set; }
        public FeedTypes type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RationFeed> RationFeeds { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual FeedElement FeedElement { get; set; }


    }
}
