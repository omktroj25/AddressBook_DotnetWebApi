using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class RefTerm
    {
        public RefTerm(){
            SetRefTerms = new HashSet<SetRefTerm>();
            Emails= new HashSet<Email>();
            Phones= new HashSet<Phone>();
            Addresses= new HashSet<Address>();
        }

        [Key]
        public Guid Id{get;set;}
        public string Key{get;set;}="";
        public string Description{get;set;}="";
        public int SortOrder{get;set;}

        public virtual ICollection<SetRefTerm> SetRefTerms{get;set;}
        public virtual ICollection<Email> Emails{get;set;}
        public virtual ICollection<Phone> Phones{get;set;}
        public virtual ICollection<Address> Addresses{get;set;}
    }
}