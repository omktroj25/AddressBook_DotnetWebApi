using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class SetRefTerm
    {
        public Guid Id{get;set;}
        
        [ForeignKey("RefTerm")]
        public Guid RefTermId{get;set;}

        [ForeignKey("RefSet")]
        public Guid RefSetId{get;set;}

        public virtual RefTerm? RefTerm{get;set;}
        public virtual RefSet? RefSet{get;set;}
    }
}