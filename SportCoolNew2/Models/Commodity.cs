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
    using System.ComponentModel.DataAnnotations;
    using static SportCoolNew2.Models.MetaData;
    [MetadataType(typeof(MataCommodity))]

    public partial class Commodity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commodity()
        {
            this.CommodityManager = new HashSet<CommodityManager>();
            this.Evaluation = new HashSet<Evaluation>();
            this.Category = new HashSet<Category>();
            this.Member = new HashSet<Member>();
        }
    
        public int commodityId { get; set; }
        public string commodityName { get; set; }
        public Nullable<int> commodityPrice { get; set; }
        public Nullable<double> commodityDiscount { get; set; }
        public string commodityDescription { get; set; }
        public Nullable<int> commodityStock { get; set; }
        public string commodityImage { get; set; }
        public Nullable<int> shopId { get; set; }
        public Nullable<bool> soldTF { get; set; }
    
        public virtual Shop Shop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommodityManager> CommodityManager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Member { get; set; }
    }
}