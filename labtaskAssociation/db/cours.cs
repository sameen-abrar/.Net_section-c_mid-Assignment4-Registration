//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace labtaskAssociation.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cours()
        {
            this.courseFaculties = new HashSet<courseFaculty>();
            this.courseStudents = new HashSet<courseStudent>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> deptid { get; set; }
        public Nullable<int> PreReq { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<courseFaculty> courseFaculties { get; set; }
        public virtual department department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<courseStudent> courseStudents { get; set; }
    }
}