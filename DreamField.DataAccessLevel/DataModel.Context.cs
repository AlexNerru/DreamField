﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DreamField.DataAccessLevel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using DreamField.Model;
    
    public partial class DreamFieldEntities : DbContext
    {
        public DreamFieldEntities()
            : base("name=DreamFieldEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Farm> Farms { get; set; }
        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Herd> Herds { get; set; }
        public virtual DbSet<RationFeed> RationFeeds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ration> Rations { get; set; }
        public virtual DbSet<FeedElement> FeedElements { get; set; }
        public virtual DbSet<Norm> Norms { get; set; }
        public virtual DbSet<RationStructure> RationStructures { get; set; }
    }
}
