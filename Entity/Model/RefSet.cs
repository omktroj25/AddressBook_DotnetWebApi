using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class RefSet
    {
        public RefSet(){
             SetRefTerms = new HashSet<SetRefTerm>();
        }

        public Guid Id{get;set;}
        public string Key{get;set;}="";
        public string Description{get;set;}="";
        
        public virtual ICollection<SetRefTerm> SetRefTerms{get;set;}
    }
}