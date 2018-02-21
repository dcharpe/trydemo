﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TryDemo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class mastermodelEntities : DbContext
    {
        public mastermodelEntities()
            : base("name=mastermodelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AGOAL> AGOALs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<MASTER> MASTERs { get; set; }
        public virtual DbSet<POST> POSTs { get; set; }
        public virtual DbSet<QfourGOAL> QfourGOALs { get; set; }
        public virtual DbSet<QoneGOAL> QoneGOALs { get; set; }
        public virtual DbSet<QthreeGOAL> QthreeGOALs { get; set; }
        public virtual DbSet<QtwoGOAL> QtwoGOALs { get; set; }
        public virtual DbSet<SUBCATEGORY> SUBCATEGORies { get; set; }
        public virtual DbSet<TEAM> TEAMs { get; set; }
        public virtual DbSet<WSTREAM> WSTREAMs { get; set; }
    
        public virtual int InsertInto(string teamName, string wstreamName, string categName, string subcategName, Nullable<int> agoalValue)
        {
            var teamNameParameter = teamName != null ?
                new ObjectParameter("teamName", teamName) :
                new ObjectParameter("teamName", typeof(string));
    
            var wstreamNameParameter = wstreamName != null ?
                new ObjectParameter("wstreamName", wstreamName) :
                new ObjectParameter("wstreamName", typeof(string));
    
            var categNameParameter = categName != null ?
                new ObjectParameter("categName", categName) :
                new ObjectParameter("categName", typeof(string));
    
            var subcategNameParameter = subcategName != null ?
                new ObjectParameter("subcategName", subcategName) :
                new ObjectParameter("subcategName", typeof(string));
    
            var agoalValueParameter = agoalValue.HasValue ?
                new ObjectParameter("agoalValue", agoalValue) :
                new ObjectParameter("agoalValue", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertInto", teamNameParameter, wstreamNameParameter, categNameParameter, subcategNameParameter, agoalValueParameter);
        }
    }
}
