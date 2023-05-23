using Microsoft.EntityFrameworkCore;
using Entity.Data;
using Entity.Model;
using Entity.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Contract.IRepository;

namespace Repository
{
    public class AddressBookRepository : IAddressBookRepository
    {
        /* The below code is declaring three private fields in a C# class: `_context` of type
        `ApiDbContext`, `_config` of type `IConfiguration`, and `_logger` of type
        `ILogger<AddressBookRepositories>`. These fields are likely used within the class to
        access a database context, configuration settings, and logging functionality. */
        private readonly ApiDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<AddressBookRepository> _logger;

        /* The below code is defining a constructor for the `AddressBookRepositories` class that
        takes in three parameters: an `IConfiguration` object, an `ApiDbContext` object, and an
        `ILogger<AddressBookRepositories>` object. These parameters are then assigned to private
        fields within the class. */
        public AddressBookRepository(IConfiguration config, ApiDbContext context, ILogger<AddressBookRepository> logger)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        //To register the new user
        /// <summary>
        /// This function adds a new user to the database in C#.
        /// </summary>
        /// <param name="User">User is a class representing a user in the system. It likely contains
        /// properties such as username, password, email, and other relevant information about the
        /// user. The CreateUser method takes an instance of this class as a parameter and adds it
        /// to the database using Entity Framework.</param>
        public void CreateUser(User user)
        {
            _logger.LogInformation("Adding new user to the database");
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        //To Login into AddressBook
        /// <summary>
        /// This function retrieves a user object from the database based on their username.
        /// </summary>
        /// <param name="username">A string parameter representing the username of the user to
        /// retrieve from the database.</param>
        /// <returns>
        /// The method `GetByUsername` returns a `User` object that matches the given `username`
        /// parameter. If no matching user is found, it returns `null` since the return type is
        /// nullable (`User?`).
        // / </returns>
        public User? GetByUsername(string username)
        {
            _logger.LogInformation("Getting username and password from the users database for validation");
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        //To CreateAddressBook
        /// <summary>
        /// The code contains two methods, one for creating an address book and uploading it to a
        /// database, and another for retrieving a type ID from metadata based on a given key.
        /// </summary>
        /// <param name="AddressBook">An object representing an address book that is being created
        /// and uploaded to a database.</param>
        public void CreateAddressBook(AddressBook addressBook)
        {
            _logger.LogInformation("Address book is uploading to the database");
            _context.AddressBooks.Add(addressBook);
            _context.SaveChanges();
        }

        public Guid GetTypeId(string key)
        {
            _logger.LogInformation("Getting type id from the metadata to assign");
            return _context.RefTerms.FirstOrDefault(t => t.Key.ToLower() == key.ToLower())!.Id;
        }


        //To get the Meta-Data details
        /// <summary>
        /// These C# functions retrieve information from a database related to reference sets and
        /// terms.
        /// </summary>
        /// <param name="key">A string value used to search for a specific RefSet in the RefSets
        /// database.</param>
        /// <returns>
        /// The `GetRefSetId` method returns a nullable `Guid` representing the ID of a reference
        /// set retrieved from the database based on a given key. The `GetRefTermIdList` method
        /// returns a list of `Guid` representing the IDs of reference terms associated with a given
        /// reference set ID. The `GetRefTermDetailsList` method returns a list of
        /// `MetaResponseDto`
        /// </returns>
        public Guid? GetRefSetId(string key)
        {
            _logger.LogInformation("Getting the refSetId from the refsets database");
            return _context.RefSets.FirstOrDefault(refSet => refSet.Key.ToLower() == key.ToLower())?.Id;
        }
        public List<Guid> GetRefTermIdList(Guid? refSetId)
        {
            _logger.LogInformation("Getting the RefTermIdList from the setrefterms database");
            return _context.SetRefTerms.Where(a => a.RefSetId == refSetId).Select(a => a.RefTermId).ToList();
        }
        public List<MetaResponseDto> GetRefTermDetailsList(List<Guid> refTermIds)
        {
            _logger.LogInformation("Getting the RefTermDetailsList from the RefTerms database");
            return _context.RefTerms.Where(rt => refTermIds.Contains(rt.Id)).Select(rt => new MetaResponseDto
            {
                Id = rt.Id,
                Key = rt.Key,
                Description = rt.Description,
            }).ToList();
        }

        //To Get All the AddressBook in the database with respect to the given rowSize and the startIndex
        /// <summary>
        /// This function retrieves a list of active address book details with optional sorting and
        /// pagination.
        /// </summary>
        /// <param name="rowSize">The number of records to be returned in a single page or
        /// request.</param>
        /// <param name="startIndex">The index of the first record to retrieve from the
        /// database.</param>
        /// <param name="orderBy">The field by which the results should be ordered. It can be either
        /// "first_name" or "last_name".</param>
        /// <param name="sortOrder">The order in which the results should be sorted, either
        /// ascending ("asc") or descending ("desc").</param>
        /// <returns>
        /// The method is returning a list of AddressBook objects with the specified row size,
        /// starting index, and sorting order based on the orderBy and sortOrder parameters. The
        /// list includes related Email, Phone, and Address objects for each AddressBook.
        /// </returns>
        public List<AddressBook> GetAllAddressBookDetails(int rowSize, int startIndex, string orderBy, string sortOrder)
        {
            IQueryable<AddressBook> query = _context.AddressBooks
                .Include(a => a.Emails)
                .Include(a => a.Phones)
                .Include(a => a.Addresses)
                .Where(a => a.IsActive == true);

            switch (orderBy.ToLower())
            {
                case "first_name":
                    if (sortOrder.ToLower() == "asc")
                        query = query.OrderBy(a => a.FirstName);
                    else
                        query = query.OrderByDescending(a => a.FirstName);
                    break;

                case "last_name":
                    if (sortOrder.ToLower() == "asc")
                        query = query.OrderBy(a => a.LastName);
                    else
                        query = query.OrderByDescending(a => a.LastName);
                    break;

                default:
                    query = query.OrderBy(a => a.FirstName);
                    break;
            }

            return query.Skip(startIndex - 1 > 0 ? startIndex : 0).Take(rowSize > 0 ? rowSize : 5).ToList();

        }


        //To Count the number of AddressBook
        /// <summary>
        /// This function calculates the number of active address books present in the database.
        /// </summary>
        /// <returns>
        /// The method `GetCount()` returns an integer value which represents the number of active address books
        /// present in the database.
        /// </returns>
        public int GetCount()
        {
            _logger.LogInformation("Calculating the number of address book present in the database");
            return _context.AddressBooks.Where(a => a.IsActive == true).Count();
        }

        //To Get the details of specific AddressBook
        /// <summary>
        /// The code includes two functions, one to retrieve details of an address book from a
        /// database based on its ID, and another to retrieve a key value from a database based on a
        /// type ID.
        /// </summary>
        /// <param name="addressBookId">A unique identifier for an address book.</param>
        /// <returns>
        /// The code snippet includes two methods.
        /// </returns>
        public AddressBook? GetAddressBookDetails(Guid? addressBookId)
        {
            _logger.LogInformation("Getting address book details from the database for addressbooksId {addressBookId}", addressBookId);
            return _context.AddressBooks
            .Include(a => a.Emails)
            .Include(a => a.Phones)
            .Include(a => a.Addresses)
            .Include(a => a.Asset)
            .FirstOrDefault(a => a.Id == addressBookId && a.IsActive == true);
        }
        public string GetKeyById(Guid TypeId)
        {
            _logger.LogInformation("Getting the key value by using typeid {TypeId}", TypeId);
            return _context.RefTerms.FirstOrDefault(a => a.Id == TypeId)!.Key;
        }

        //To update the addressbook
        /// <summary>
        /// This function saves changes made to an address book to a database and logs the success
        /// using a logger.
        /// </summary>
        /// <param name="AddressBook">AddressBook is an object that represents an address book. It
        /// likely contains information such as the name of the address book, a list of contacts,
        /// and other relevant details. The method UpdateChanges takes an AddressBook object as a
        /// parameter, indicating that it will update the changes made to the address book
        /// in</param>
        public void UpdateChanges(AddressBook addressBook)
        {
            _logger.LogInformation("Address book changes saved to the database successfully");
            _context.SaveChanges();
        }


        public bool CheckEmails(string email)
        {
            _logger.LogInformation("Checking all email in the database for conflict validation");
            if (_context.Emails.FirstOrDefault(e => e.EmailId == email) != null)
                return false;
            return true;
        }
        public bool CheckPhones(string phone)
        {
            _logger.LogInformation("Checking all phone number in the database for conflict validation");
            if (_context.Phones.FirstOrDefault(p => p.PhoneNumber == phone) != null)
                return false;
            return true;
        }

        // To delete the addressbook
        /// <summary>
        /// This function retrieves the status of an asset based on its address book ID.
        /// </summary>
        /// <param name="addressBookId">A nullable Guid parameter representing the unique identifier
        /// of an address book. It is used to retrieve the status of an asset associated with the
        /// given address book.</param>
        /// <returns>
        /// A boolean value indicating whether the asset with the given address book ID is active or
        /// not.
        /// </returns>
        public bool GetAssetStatus(Guid? addressBookId)
        {
            _logger.LogInformation("Detecting the asset for the addressbookid {addressBookId}", addressBookId);
            return _context.Assets.FirstOrDefault(a => a.AddressBookId == addressBookId)?.IsActive ?? false;
        }

        // To upload the asset to the database
        /// <summary>
        /// This function uploads an asset to a database and logs the action using a logger.
        /// </summary>
        /// <param name="Asset">The parameter "Asset" is an object of the Asset class that contains
        /// information about an asset that needs to be uploaded to a database.</param>
        public void UploadAsset(Asset asset)
        {
            _logger.LogInformation("Uploading {asset} to the database", asset);
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        //To download the asset from the database
        /// <summary>
        /// This C# function retrieves an asset from a database based on its address book ID.
        /// </summary>
        /// <param name="addressBookId">A nullable Guid parameter representing the unique identifier
        /// of an asset in the database.</param>
        /// <returns>
        /// The method is returning an object of type `Asset` or `null` if the `addressBookId`
        /// parameter is `null` or if no asset is found in the database that matches the given
        /// `addressBookId` and is active.
        /// </returns>
        public Asset? GetAssetById(Guid? addressBookId)
        {
            _logger.LogInformation("Getting the asset from the database for the {addressBookId}", addressBookId);
            return _context.Assets.FirstOrDefault(a => a.AddressBookId == addressBookId && a.IsActive == true)!;
        }

    }
}