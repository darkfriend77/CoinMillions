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
    
    public partial class ChangeTx : TransactionDetail
    {
        public bool Validation { get; set; }
    
        public virtual TicketTx TicketTx { get; set; }
    }
}
