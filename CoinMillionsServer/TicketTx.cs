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
    
    public partial class TicketTx
    {
        public int ID { get; set; }
        public string TxId { get; set; }
        public double Amount { get; set; }
        public string Sender { get; set; }
    
        public virtual ChangeTx ChangeTx { get; set; }
    }
}
