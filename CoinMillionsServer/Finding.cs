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
    
    public partial class Finding
    {
        public Finding()
        {
            this.Ticket = new HashSet<Ticket>();
        }
    
        public int ID { get; set; }
        public int Numbers { get; set; }
        public int Stars { get; set; }
        public double Probability { get; set; }
        public double Gain { get; set; }
    
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
