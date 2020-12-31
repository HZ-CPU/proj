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
    [MetadataType(typeof(MataClass))]


    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            this.MemberSelectClass = new HashSet<MemberSelectClass>();
            this.Manager = new HashSet<Manager>();
        }
    
        public int cId { get; set; }
        public string className { get; set; }
        public Nullable<int> cGradeId { get; set; }
        public Nullable<int> cTypeId { get; set; }
        public Nullable<int> coachId { get; set; }
        public string audio { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    
        public virtual ClassGrade ClassGrade { get; set; }
        public virtual ClassType ClassType { get; set; }
        public virtual Coach Coach { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberSelectClass> MemberSelectClass { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manager> Manager { get; set; }
    }
}
