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
    
    public partial class courseStudent
    {
        public int id { get; set; }
        public int courseid { get; set; }
        public int studentid { get; set; }
        public string status { get; set; }
        public string grade { get; set; }
        public Nullable<double> marks { get; set; }
    
        public virtual cours cours { get; set; }
        public virtual student student { get; set; }
    }
}