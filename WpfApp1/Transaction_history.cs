//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction_history
    {
        public long AccID { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<long> Sender { get; set; }
        public Nullable<long> Reciever { get; set; }
        public Nullable<System.DateTime> WithdrawTime { get; set; }
        public Nullable<System.DateTime> DepositTime { get; set; }
        public Nullable<System.DateTime> TransferTime { get; set; }
    }
}
