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
    
    public partial class RationStructure
    {
        public int id { get; set; }
        public double roughage { get; set; }
        public double juicy_food { get; set; }
        public double concentrates { get; set; }
    
        public virtual Ration Ration { get; set; }
    }
}