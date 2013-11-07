//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Transaction : TransactionDetail
    {
        public Transaction()
        {
            this.Tickets = new HashSet<Ticket>();
        }
    
        public State State { get; set; }
        public Type Type { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
    
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual DrawBlock DrawBlock { get; set; }
        public virtual Transaction ParentTx { get; set; }
        public virtual Transaction ChildTx { get; set; }
    }
}