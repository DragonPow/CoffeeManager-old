//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainProject.Model
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class BILL : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL()
        {
            this.DETAILBILLs = new  ObservableCollection<DETAILBILL>();
        }
    
        private long ID { get; set; }
        public long TotalPrice { get; set; }
        public Nullable<System.DateTime> CheckoutDay { get; set; }
        private string ID_Voucher { get; set; }
        private Nullable<long> ID_Tables { get; set; }
        private Nullable<long> ID_Employee { get; set; }
    
        public virtual TABLE TABLE { get; set; }
        public virtual VOUCHER VOUCHER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<DETAILBILL> DETAILBILLs { get; set; }
        public virtual EMPLOYEE EMPLOYEE { get; set; }
    }
}
