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
    
    public partial class TicketTx : TransactionDetail
    {
        public string Sender { get; set; }
    
        public virtual Finding Findings { get; set; }
        public virtual ChangeTx ChangeTx { get; set; }
    }
}
