﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoinMillionsServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CoinMillionsModelContainer : DbContext
    {
        public CoinMillionsModelContainer()
            : base("name=CoinMillionsModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Finding> Findings { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
