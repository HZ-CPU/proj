//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportCoolNew2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetails
    {
        public int orderId { get; set; }
        public Nullable<int> tel { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public string receiver { get; set; }
        public string receivedAddress { get; set; }
        public Nullable<int> paymentId { get; set; }
        public Nullable<int> orderStatusId { get; set; }
        public Nullable<int> mId { get; set; }
        public Nullable<int> price { get; set; }
        public string OrderGuid { get; set; }
        public Nullable<int> PId { get; set; }
        public Nullable<int> Qty { get; set; }
        public string Name { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
