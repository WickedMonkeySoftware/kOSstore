//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kosstore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Like
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ScriptId { get; set; }
    
        public virtual UserProfileEF UserProfile { get; set; }
        public virtual Script Script { get; set; }
    }
}