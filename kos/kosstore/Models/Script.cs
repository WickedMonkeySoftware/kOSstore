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
    
    public partial class Script
    {
        public Script()
        {
            this.ScriptComments = new HashSet<ScriptComment>();
            this.Likes = new HashSet<Like>();
        }
    
        public int ID { get; set; }
        public string ScriptName { get; set; }
        public string Script1 { get; set; }
        public int UserId { get; set; }
    
        public virtual UserProfileEF UserProfile { get; set; }
        public virtual ICollection<ScriptComment> ScriptComments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
