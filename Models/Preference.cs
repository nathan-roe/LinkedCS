using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LinkedCS.Models
{
    public class Preference
    {
        public int PreferenceId {get;set;}

        public string ViewPoint {get;set;}
        public int UserForeignKey { get; set; }
        public User UserWithPreference {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}