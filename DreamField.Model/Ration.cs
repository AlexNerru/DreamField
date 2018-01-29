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
    
    public partial class Ration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ration()
        {
            this.RationFeeds = new HashSet<RationFeed>();
        }
    
        public long id { get; set; }
        public long author_id { get; set; }
        public long farm { get; set; }
        public System.DateTime creation_datetime { get; set; }
        public AnimalTypes animal { get; set; }
        public string comment { get; set; }
    
        public virtual Farm RationsFarm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RationFeed> RationFeeds { get; set; }
        public virtual User User { get; set; }
    }
}
