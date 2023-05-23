using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class Phone : BaseEntity
    {

        [ForeignKey("AddressBook")]
        public Guid AddressBookId{get;set;}

        [ForeignKey("RefTerm")]
        public Guid PhoneTypeId{get;set;}
        public string PhoneNumber{get;set;}="";
        public virtual AddressBook? AddressBook{get;set;}
        public virtual RefTerm? RefTerm{get;set;}
    }   
}