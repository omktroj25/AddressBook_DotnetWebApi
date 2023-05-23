using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class Email : BaseEntity
    {

        [ForeignKey("AddressBook")]
        public Guid AddressBookId{get;set;}

        [ForeignKey("RefTerm")]
        public Guid EmailTypeId{get;set;}
        public string EmailId{get;set;}="";

        public virtual AddressBook? AddressBook{get;set;}
        public virtual RefTerm? RefTerm{get;set;}
    }   
}