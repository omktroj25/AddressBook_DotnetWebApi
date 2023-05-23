using Microsoft.EntityFrameworkCore;
using Entity.Model;

namespace Entity.Data
{
    public class ApiDbContext : DbContext
    {
        /* These lines of code are defining the database tables that will be used in the application.
        Each line represents a table with the specified name and type of data it will store. For
        example, the `Users` table will store instances of the `User` class, the `AddressBooks`
        table will store instances of the `AddressBook` class, and so on. The `DbSet` class is used
        to represent a table in Entity Framework, and the `virtual` keyword allows for lazy loading
        of related entities. */
        public virtual DbSet<User> Users{get;set;} = null!;
        public virtual DbSet<AddressBook> AddressBooks{get;set;} = null!;
        public virtual DbSet<Asset> Assets{get;set;} = null!;
        public virtual DbSet<Email> Emails{get;set;} = null!;
        public virtual DbSet<Phone> Phones{get;set;} = null!;
        public virtual DbSet<Address> Addresses{get;set;} = null!;
        public virtual DbSet<RefTerm> RefTerms{get;set;} = null!;
        public virtual DbSet<SetRefTerm> SetRefTerms{get;set;} = null!;
        public virtual DbSet<RefSet> RefSets{get;set;} = null!;
        

        /* This is the constructor for the `ApiDbContext` class. It takes an instance of
        `DbContextOptions<ApiDbContext>` as a parameter, which is used to configure the context. The
        `: base(options)` part of the constructor calls the base class constructor with the
        `options` parameter, which initializes the context with the specified options. */
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
  
       /// <summary>
       /// The OnModelCreating function sets up initial data for users, reference sets, reference terms,
       /// and set reference terms in a database using Entity Framework Core.
       /// </summary>
       /// <param name="ModelBuilder">ModelBuilder is a class in Entity Framework Core that is used to
       /// configure the model for a context. It provides a fluent API for configuring entities,
       /// relationships, and other aspects of the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeding data in user table
            modelBuilder.Entity<User>().HasData(
                new User{
                    Id = Guid.NewGuid(),
                    UserName = "User@outlook.com",
                    Password = "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8=", // Password@123
                },
                new User{
                    Id = Guid.NewGuid(),
                    UserName = "Propel@propelinc.com",
                    Password = "dM0p8PMqmscp69xac484T6OErIqk5WM4qDtV+MzVGdY=", // Propel@123
                }
            );

            //Seeding data in refset table
            Guid EmailId = Guid.NewGuid();
            Guid PhoneId = Guid.NewGuid();
            Guid AddressId = Guid.NewGuid();
            Guid CountryId = Guid.NewGuid();
            modelBuilder.Entity<RefSet>().HasData(
                new RefSet{ Id = EmailId, Key = "EMAIL_ADDRESS_TYPE", Description = "Email details"},
                new RefSet{ Id = PhoneId, Key = "PHONE_NUMBER_TYPE", Description = "Phone details"},
                new RefSet{ Id = AddressId, Key = "ADDRESS_TYPE", Description = "Address details"},
                new RefSet{ Id = CountryId, Key = "COUNTRY", Description = "Country details"}
            );

            //Seeding data in refterm table
            Guid PersonalId = Guid.NewGuid();
            Guid WorkId = Guid.NewGuid();
            Guid AlternateId = Guid.NewGuid();
            Guid UsId = Guid.NewGuid();
            Guid IndiaId=Guid.NewGuid();
            modelBuilder.Entity<RefTerm>().HasData(
                new RefTerm{ Id = PersonalId, Key = "PERSONAL", Description = "Personal details", SortOrder = 1 },
                new RefTerm{ Id = WorkId, Key = "WORK", Description = "Work details", SortOrder = 2 },
                new RefTerm{ Id = AlternateId, Key = "ALTERNATE", Description = "Alternate details", SortOrder = 3 },
                new RefTerm{ Id = UsId, Key = "INDIA", Description = "India", SortOrder = 4 },
                new RefTerm{ Id = IndiaId, Key = "USA", Description = "United States", SortOrder = 4 }
            );

            //Seeding data in setrefterm table
            modelBuilder.Entity<SetRefTerm>().HasData(
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = EmailId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = EmailId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = AlternateId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = AddressId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = AddressId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = CountryId, RefTermId = UsId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = CountryId, RefTermId = IndiaId }
            );

        }

    }    
}