using Entity.Data;
using AddressBookApi.Controller;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Entity.Model;
using Service;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AddressBookApi;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBookApiTest
{
    public class BaseTesting
    {
        /* The below code is declaring several fields in a C# class. These fields include a controller
        for an address book, services and repositories interfaces for the address book, a
        configuration object, a database context object, options builder for the database context, a
        logger object, and a mapper object. These fields are likely used for managing and
        interacting with an address book application. */
        public readonly AddressBookController addressBookController;
        public readonly IAddressBookService addressBookServices;
        public readonly IAddressBookRepository addressBookRepositories;
        public readonly IConfiguration _config;
        public readonly ApiDbContext _context;
        private readonly DbContextOptionsBuilder<ApiDbContext> optionsBuilder;
        public readonly ILogger<AddressBookRepository> _logger;
        public readonly IMapper _mapper;

       /* The below code is declaring four static variables of type Guid in C#. The first variable is
       named "userId" and is assigned a new Guid value using the Guid.NewGuid() method. The other
       three variables are named "addressBookId1", "addressBookId2", and "addressBookId3" and are
       also assigned new Guid values using the same method. These variables can be used throughout
       the program to uniquely identify objects or entities. */
        public static Guid userId = Guid.NewGuid();
        public static Guid addressBookId1 = Guid.NewGuid();
        public static Guid addressBookId2 = Guid.NewGuid();
        public static Guid addressBookId3 = Guid.NewGuid();

        public BaseTesting()
        {

           /* This code is setting up the necessary dependencies and configurations for testing the
           AddressBookController in an in-memory database. */
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new ApiDbContext(optionsBuilder.Options);
            _context.SaveChanges();

            ServiceProvider provider = new ServiceCollection().AddLogging().BuildServiceProvider();
            ILoggerFactory? factory = provider.GetService<ILoggerFactory>();
            _logger = factory!.CreateLogger<AddressBookRepository>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            _mapper = mapperConfiguration.CreateMapper();
            
           /* `_context.Database.EnsureDeleted()` is deleting the in-memory database if it exists, and
           `_context.Database.EnsureCreated()` is creating a new in-memory database. This is done to
           ensure that each test runs on a clean and fresh database, without any data from previous
           tests. */
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();


            /* The below code is creating a new user with a given name and password, adding it to a
            collection of users, and then saving the changes to the database. The user's ID is not
            shown in the code snippet and is likely defined elsewhere. */
            string name = "User@propelinc.com";
            User[] users = new User[]
            {
                new User{ Id = userId , UserName = name , Password = "/3vZexp3id3Sd1Ei/WgX8xc2ctqfgCzuxX8oQyW/WJ8="}
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();


            /* The below code is generating unique identifiers (GUIDs) for different reference sets
            such as email addresses, phone numbers, addresses, and countries. It then creates an
            array of RefSet objects with these identifiers and their corresponding keys and
            descriptions. Finally, it adds these reference sets to the database using Entity
            Framework and saves the changes. */
            Guid EmailId = Guid.NewGuid();
            Guid PhoneId = Guid.NewGuid();
            Guid AddressId = Guid.NewGuid();
            Guid CountryId = Guid.NewGuid();
            RefSet[] refSet = new RefSet[]
            {
                new RefSet{ Id = EmailId, Key = "EMAIL_ADDRESS_TYPE", Description = "Email details"},
                new RefSet{ Id = PhoneId, Key = "PHONE_NUMBER_TYPE", Description = "Phone details"},
                new RefSet{ Id = AddressId, Key = "ADDRESS_TYPE", Description = "Address details"},
                new RefSet{ Id = CountryId, Key = "COUNTRY", Description = "Country details"}
            };
            _context.RefSets.AddRange(refSet);
            _context.SaveChanges();


            /* The below code is generating and adding new instances of the RefTerm class to the
            database context. Each instance has a unique identifier (GUID) and contains information
            about a specific reference term, such as its key, description, and sort order. The
            SaveChanges method is then called to persist the changes to the database. */
            Guid PersonalId = Guid.NewGuid();
            Guid WorkId = Guid.NewGuid();
            Guid AlternateId = Guid.NewGuid();
            Guid UsId = Guid.NewGuid();
            Guid IndiaId = Guid.NewGuid();
            RefTerm[] refTerm = new RefTerm[]
            {
                new RefTerm{ Id = PersonalId, Key = "PERSONAL", Description = "Personal details", SortOrder = 1 },
                new RefTerm{ Id = WorkId, Key = "WORK", Description = "Work details", SortOrder = 2 },
                new RefTerm{ Id = AlternateId, Key = "ALTERNATE", Description = "Alternate details", SortOrder = 3 },
                new RefTerm{ Id = UsId, Key = "INDIA", Description = "India", SortOrder = 4 },
                new RefTerm{ Id = IndiaId, Key = "USA", Description = "United States", SortOrder = 4 }
            };
            _context.RefTerms.AddRange(refTerm);
            _context.SaveChanges();


            /* The below code is adding a list of SetRefTerm objects to a database context and saving
            the changes. Each SetRefTerm object contains an Id, a RefSetId, and a RefTermId. These
            objects are used to establish relationships between different sets of terms in the
            database. Specifically, the code is creating relationships between email, phone,
            address, and country sets, and the personal, work, and alternate terms within those
            sets. */
            SetRefTerm[] SetRefTerm = new SetRefTerm[]
            {
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = EmailId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = EmailId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = PhoneId, RefTermId = AlternateId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = AddressId, RefTermId = PersonalId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = AddressId, RefTermId = WorkId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = CountryId, RefTermId = UsId },
                new SetRefTerm { Id = Guid.NewGuid(), RefSetId = CountryId, RefTermId = IndiaId }
            };
            _context.SetRefTerms.AddRange(SetRefTerm);
            _context.SaveChanges();


            /* The below code is creating an array of AddressBook objects and adding them to the
            database using Entity Framework. The AddressBook objects contain information such as the
            user ID, first name, last name, and creation/update dates. The SaveChanges() method is
            called to persist the changes to the database. */
            AddressBook[] addressBook = new AddressBook[]
            {
                new AddressBook { Id = addressBookId1 , UserId = userId , FirstName = "Balasubramanian" , LastName = "V" , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new AddressBook { Id = addressBookId2 , UserId = userId , FirstName = "Kumar" , LastName = "S" , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new AddressBook { Id = addressBookId3 , UserId = userId , FirstName = "Ganesh" , LastName = "L" , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow }
            };
            _context.AddressBooks.AddRange(addressBook);
            _context.SaveChanges();


            /* The below code is adding three email records to a database using Entity Framework in C#.
            Each email record has a unique identifier (Id), an address book identifier
            (AddressBookId), an email address (EmailId), an email type identifier (EmailTypeId), a
            flag indicating whether the email is active (IsActive), and information about who
            created and updated the record (CreatedBy, UpdatedBy, CreatedDate, UpdatedDate). The
            emails are added to the database using the AddRange method and then saved using the
            SaveChanges method. */
            Email[] email = new Email[]
            {
                new Email { Id = Guid.NewGuid() , AddressBookId = addressBookId1, EmailId = "vbalaas@gmail.com" , EmailTypeId = PersonalId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Email { Id = Guid.NewGuid() , AddressBookId = addressBookId2, EmailId = "kumars@gmail.com" , EmailTypeId = WorkId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Email { Id = Guid.NewGuid() , AddressBookId = addressBookId3, EmailId = "Sivas@gmail.com" , EmailTypeId = WorkId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow }
            };
            _context.Emails.AddRange(email);
            _context.SaveChanges();


            /* The below code is creating three instances of the Phone class and adding them to the
            Phones table in the database using Entity Framework. Each phone has a unique Id, a
            PhoneNumber, a PhoneTypeId (which is a foreign key to the PhoneTypes table), an IsActive
            flag, and CreatedBy, UpdatedBy, CreatedDate, and UpdatedDate fields. The SaveChanges
            method is then called to persist the changes to the database. */
            Phone[] phones = new Phone[]
            {
                new Phone { Id = Guid.NewGuid() , AddressBookId = addressBookId1 , PhoneNumber = "9872981231" , PhoneTypeId = PersonalId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Phone { Id = Guid.NewGuid() , AddressBookId = addressBookId2 , PhoneNumber = "9872673890" , PhoneTypeId = WorkId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Phone { Id = Guid.NewGuid() , AddressBookId = addressBookId3 , PhoneNumber = "9230985467" , PhoneTypeId = AlternateId , IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow }
            };
            _context.Phones.AddRange(phones);
            _context.SaveChanges();


            /* The below code is adding three new Address objects to a database using Entity Framework
            Core. The addresses are being added to the Addresses DbSet in the context and then saved
            to the database using SaveChanges(). Each Address object has various properties such as
            Id, AddressBookId, Line1, Line2, City, Zipcode, State, CountryTypeId, AddressTypeId,
            CreatedBy, UpdatedBy, CreatedDate, and UpdatedDate. These properties are being set with
            specific values for each Address object. */
            Address[] addresses = new Address[]
            {
                new Address { Id = Guid.NewGuid() , AddressBookId = addressBookId1 , Line1 = "12131" , Line2 = "street" , City = "chennai" , Zipcode = "69991" , State = "Tamilnadu" , CountryTypeId = IndiaId , AddressTypeId = PersonalId , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Address { Id = Guid.NewGuid() , AddressBookId = addressBookId2 , Line1 = "678934" , Line2 = "road" , City = "florida" , Zipcode = "897456" , State = "London" , CountryTypeId = UsId , AddressTypeId = WorkId , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
                new Address { Id = Guid.NewGuid() , AddressBookId = addressBookId3 , Line1 = "987651" , Line2 = "Bypass" , City = "nanjing" , Zipcode = "876234" , State = "China" , CountryTypeId = UsId , AddressTypeId = PersonalId , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow }
            };
            _context.Addresses.AddRange(addresses);
            _context.SaveChanges();


           /* The below code is creating an array of Asset objects and adding them to the database
           using Entity Framework. The Asset objects contain properties such as Id, AddressBookId,
           FileName, FileType, FileContent, IsActive, CreatedBy, UpdatedBy, CreatedDate, and
           UpdatedDate. The AddRange method is used to add the array of Asset objects to the Assets
           DbSet in the database context, and the SaveChanges method is used to save the changes to
           the database. */
            Asset[] asset = new Asset[]
            {
                new Asset { Id = Guid.NewGuid() , AddressBookId = addressBookId1 , FileName = "Propel.jpg" , FileType = "image/jpeg",FileContent = new byte[] { 0x12, 0x34, 0x56, 0x78 } ,IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow  },
                new Asset { Id = Guid.NewGuid() , AddressBookId = addressBookId2 , FileName = "AddressBook.png" , FileType = "image/png",FileContent = new byte[] { 0x12, 0x34, 0x56, 0x78 } ,IsActive = true , CreatedBy = userId , UpdatedBy = userId , CreatedDate = DateTime.UtcNow , UpdatedDate = DateTime.UtcNow },
            };
            _context.Assets.AddRange(asset);
            _context.SaveChanges();


            /* The below code is creating instances of three classes: AddressBookController,
            AddressBookServices, and AddressBookRepositories. Each instance is being passed four
            parameters: _config, _context, _logger, and _mapper. These parameters are likely
            dependencies that the classes need in order to function properly. */
            addressBookController = new AddressBookController(_config, _context, _logger, _mapper);
            addressBookServices = new AddressBookService(_config, _context, _logger, _mapper);
            addressBookRepositories = new AddressBookRepository(_config, _context, _logger);


            /* The below code is creating a new list of claims, which includes a claim for the user's
            name and a claim for the user's ID. It then creates a new ClaimsIdentity object using
            the list of claims and a specified authentication type. Finally, it creates a new
            ClaimsPrincipal object using the ClaimsIdentity object. This code is likely used for
            authentication and authorization purposes in a C# application. */
            List<Claim> claims = new List<Claim>
            {
                new Claim("NameIdentifier", name),
                new Claim("NameId",userId.ToString()),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuthType");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);


            /* The below code is setting up a controller context for an address book controller in C#.
            It creates a new instance of the DefaultHttpContext class and sets the user property to
            a given principal. It then creates a new instance of the ControllerContext class and
            sets its HttpContext property to the previously created httpContext. Finally, it sets
            the ControllerContext property of the addressBookController to the newly created
            controllerContext. This allows the controller to access the user information and other
            context information during its execution. */
            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.User = principal;
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            addressBookController.ControllerContext = controllerContext;

        }

    }
}