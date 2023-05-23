using Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class User
    {
        public User(){
            AddressBooks = new HashSet<AddressBook>();
        }

        [Key]
        public Guid Id{get;set;}
        public string UserName{get;set;}="";
        public string Password{get;set;}="";
        public virtual ICollection<AddressBook> AddressBooks{get;set;}
    }
}