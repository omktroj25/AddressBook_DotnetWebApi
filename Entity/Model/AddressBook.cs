using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class AddressBook : BaseEntity
    {
        public AddressBook(){
            Emails= new HashSet<Email>();
            Phones= new HashSet<Phone>();
            Addresses= new HashSet<Address>();
        }

        [ForeignKey("User")]
        public Guid UserId{get;set;}
        public string FirstName{get;set;}="";
        public string LastName{get;set;}="";

        public virtual User? User{get;set;}
        public virtual Asset? Asset{get;set;}
        public virtual ICollection<Email> Emails{get;set;}
        public virtual ICollection<Phone> Phones{get;set;}
        public virtual ICollection<Address> Addresses{get;set;}
        

    }
}