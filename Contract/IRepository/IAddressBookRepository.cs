using Entity.Model;
using Entity.Dto;    

namespace Contract.IRepository
{
        public interface IAddressBookRepository
        {
            /// <summary>
            /// This function creates a new user.
            /// </summary>
            /// <param name="User">User is a data type that represents a user object. It may contain
            /// various properties such as username, password, email, etc. The CreateUser function takes
            /// an instance of the User object as a parameter and creates a new user in the system using
            /// the provided information.</param>
            void CreateUser(User user);

            /// <summary>
            /// The function returns a user object based on the provided username.
            /// </summary>
            /// <param name="username">The parameter "username" is a string that represents the username
            /// of the user that we want to retrieve from a database or some other data source. The
            /// method "GetByUsername" takes this parameter as input and returns the user object
            /// associated with the given username.</param>
            User? GetByUsername(string username);

            /// <summary>
            /// This function creates an address book.
            /// </summary>
            /// <param name="AddressBook">The parameter "AddressBook" is the name of a data structure or
            /// object that represents a collection of contacts or addresses. It could be a class,
            /// struct, or any other data type that is used to store and manage contact information. The
            /// CreateAddressBook function takes an instance of this data structure as</param>
            void CreateAddressBook(AddressBook addressBook);

            /// <summary>
            /// The function returns a unique identifier (GUID) for a given key.
            /// </summary>
            /// <param name="key">The key parameter is a string that represents the identifier or name
            /// of a type. The method GetTypeId takes this key as input and returns a Guid that
            /// represents the unique identifier of the type.</param>
            Guid GetTypeId(string key);

            /// <summary>
            /// The function returns a nullable Guid value based on a given string key.
            /// </summary>
            /// <param name="key">The "key" parameter is a string value that is used as a unique
            /// identifier to retrieve a reference set ID. The method "GetRefSetId" takes this key as
            /// input and returns the corresponding reference set ID.</param>
            Guid? GetRefSetId(string key);

            /* The `GetRefTermIdList` function is returning a list of `Guid` values that represent the
            term IDs associated with a given reference set ID. The function takes a nullable `Guid`
            parameter `refSetId` which represents the reference set ID for which the term IDs are to
            be retrieved. The function returns a list of `Guid` values that represent the term IDs
            associated with the given reference set ID. */
            List<Guid> GetRefTermIdList(Guid? refSetId);

            /* The `List<MetaResponseDto> GetRefTermDetailsList(List<Guid> refTermIds);` function
            is retrieving a list of `MetaResponseDto` objects that represent the details of the
            terms associated with a given list of term IDs. The function takes a list of `Guid`
            values `refTermIds` as input, which represent the term IDs for which the details are to
            be retrieved. The function returns a list of `MetaResponseDto` objects that contain
            the details of the terms associated with the given term IDs. */
            List<MetaResponseDto> GetRefTermDetailsList(List<Guid> refTermIds);

            //     /* The `GetAllAddressBookDetailsByFirstName` function is retrieving a list of `AddressBook`
            //     objects that contain details of all the address books in the system, sorted by first
            //     name. The function takes two parameters: `rowSize`, which represents the number of rows
            //     to retrieve, and `startIndex`, which represents the starting index of the rows to
            //     retrieve. The function returns a list of `AddressBook` objects that contain details of
            //     all the address books in the system, sorted by first name. */
            //     List<AddressBook> GetAllAddressBookDetailsByFirstName(int rowSize, int startIndex);
            //     /* The `GetAllAddressBookDetailsByLastName` function is retrieving a list of `AddressBook`
            //     objects that contain details of all the address books in the system, sorted by last
            //     name. It takes two parameters: `rowSize`, which represents the number of rows to
            //     retrieve, and `startIndex`, which represents the starting index of the rows to retrieve.
            //     The function returns a list of `AddressBook` objects that contain details of all the
            //     address books in the system, sorted by last name. */
            //     List<AddressBook> GetAllAddressBookDetailsByLastName(int rowSize, int startIndex);
            List<AddressBook> GetAllAddressBookDetails(int rowSize, int startIndex, string orderBy, string sortOrder);

            /// <summary>
            /// The function "GetCount" returns an integer value.
            /// </summary>
            int GetCount();

            /// <summary>
            /// The function retrieves the details of an address book identified by a unique identifier.
            /// </summary>
            /// <param name="addressBookId">The addressBookId parameter is a unique identifier (GUID)
            /// that is used to retrieve the details of a specific address book from a database or other
            /// data source. It is an optional parameter, indicated by the "?" symbol after the data
            /// type "Guid". If a valid GUID is provided, the method will</param>
            AddressBook? GetAddressBookDetails(Guid? addressBookId);

            /// <summary>
            /// This function takes a TypeId as input and returns a string key associated with that
            /// TypeId.
            /// </summary>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer used
            /// to identify resources. It is often used in software development to uniquely identify
            /// objects, such as files, database records, or network addresses. In this case, the TypeId
            /// parameter is a Guid that represents the unique identifier of a</param>
            string GetKeyById(Guid TypeId);

            /// <summary>
            /// The function updates changes made to an address book.
            /// </summary>
            /// <param name="AddressBook">AddressBook is likely a data structure or object that contains
            /// information about a collection of addresses, such as names, phone numbers, and email
            /// addresses. The UpdateChanges function likely takes this AddressBook as a parameter and
            /// updates it with any changes that have been made.</param>
            void UpdateChanges(AddressBook addressBook);
            
            /// <summary>
            /// The function checks if a given email is valid.
            /// </summary>
            /// <param name="email">The "email" parameter is a string that represents an email address.
            /// The function "CheckEmails" likely checks whether the email address is valid or meets
            /// certain criteria.</param>
            bool CheckEmails(string email);
            
            /// <summary>
            /// The function checks if a given phone number is valid.
            /// </summary>
            /// <param name="phone">The parameter "phone" is a string that represents a phone number.
            /// The function "CheckPhones" likely checks whether the phone number is valid or meets
            /// certain criteria.</param>
            bool CheckPhones(string phone);

            /// <summary>
            /// The function returns a boolean value indicating the status of an asset with a given
            /// address book ID.
            /// </summary>
            /// <param name="addressBookId">The addressBookId parameter is a nullable Guid data type
            /// that represents the unique identifier of an asset in the system. It is used as input to
            /// the GetAssetStatus method to retrieve the status of the asset. If the value of
            /// addressBookId is null, the method will return an error or throw</param>
            bool GetAssetStatus(Guid? addressBookId);

            /// <summary>
            /// The function "UploadAsset" takes an object of type "Asset" as a parameter and presumably
            /// uploads it to some sort of storage or server.
            /// </summary>
            /// <param name="Asset">"Asset" is the data type of the parameter being passed into the
            /// "UploadAsset" function. It is likely a custom data type that represents some kind of
            /// digital asset, such as an image, video, or audio file. The specific properties and
            /// methods of the "Asset" data type would depend</param>
            void UploadAsset(Asset asset);
            
            /// <summary>
            /// The function returns an asset based on its ID in the address book.
            /// </summary>
            /// <param name="addressBookId">The addressBookId parameter is a nullable Guid data type
            /// that represents the unique identifier of an asset in the system. The method GetAssetById
            /// takes this parameter as input and returns the asset object associated with the specified
            /// addressBookId.</param>
            Asset? GetAssetById(Guid? addressBookId);
    }

}