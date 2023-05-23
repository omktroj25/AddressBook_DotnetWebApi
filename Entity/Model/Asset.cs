using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Model
{
    public class Asset : BaseEntity
    {
        [ForeignKey("AddressBook")]
        public Guid? AddressBookId{get;set;}
        public string FileName{get;set;}="";
        public string FileType{get;set;}="";
        public byte[]? FileContent {get;set;}

        public virtual AddressBook? AddressBook{get;set;}
    }
}