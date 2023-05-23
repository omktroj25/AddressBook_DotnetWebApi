using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class Address : BaseEntity
    {

        [ForeignKey("AddressBook")]
        public Guid AddressBookId{get;set;}
        
        [ForeignKey("RefTerm")]
        public Guid AddressTypeId{get;set;}
        public string Line1{get;set;}="";
        public string Line2{get;set;}="";
        public string City{get;set;}="";
        public string Zipcode{get;set;}="";
        public Guid CountryTypeId{get;set;}
        public string State{get;set;}="";
        public virtual AddressBook? AddressBook{get;set;}
        public virtual RefTerm? RefTerm{get;set;}
    }   
}