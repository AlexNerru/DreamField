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
    
        public int Id { get; set; }
        public int Author_id { get; set; }
        public int Farm_id { get; set; }
        public System.DateTime Creation_datetime { get; set; }
        public int Animal { get; set; }
        public string Comment { get; set; }
    
        public virtual Farm Farm { get; set; }
        public virtual NormIndexGeneral NormIndexesGeneral { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RationFeed> RationFeeds { get; set; }
        public virtual User User { get; set; }
    }
}
