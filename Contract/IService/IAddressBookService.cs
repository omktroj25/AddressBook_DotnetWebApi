using Entity.Model;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Contract.IService
{
    public interface IAddressBookService 
        {
            /// <summary>
            /// The function creates a user based on the provided signup data and returns a response
            /// object.
            /// </summary>
            /// <param name="SignupDto">SignupDto is a data transfer object (DTO) that contains the
            /// information required to create a new user account. It may include fields such as
            /// username, email, password, and any other relevant user information. The CreateUser
            /// method takes in a SignupDto object as a parameter and returns a ResponseDto
            /// object,</param>
            ResponseIdDto CreateUser(SignupDto signupDto);

           /// <summary>
           /// This function generates a token for a given user.
           /// </summary>
           /// <param name="User">User is an object that represents a user in the system. It typically
           /// contains information such as the user's username, password, email address, and any other
           /// relevant details. The GenerateToken method takes a User object as input and generates a
           /// token that can be used to authenticate the user for subsequent requests to</param>
            TokenResponseDto GenerateToken(User user);

            /// <summary>
            /// The function takes in a username and password as parameters and returns a User object
            /// after validating the user's credentials.
            /// </summary>
            /// <param name="username">A string variable that represents the username entered by the
            /// user for authentication.</param>
            /// <param name="password">The "password" parameter is a string variable that represents the
            /// password entered by the user during the login process. It is used to authenticate the
            /// user's identity and grant access to the system if the password is correct.</param>
            User ValidateUser(string username, string password);

            /// <summary>
            /// The function takes an input string and returns its hash value.
            /// </summary>
            /// <param name="input">The input parameter is a string that represents the data for which
            /// we want to compute a hash value. A hash function takes an input (in this case, a string)
            /// and produces a fixed-size output (also a string) that represents a unique "fingerprint"
            /// of the input data. The hash</param>
            string ComputeHash(string input);

            /// <summary>
            /// This function checks for type conflicts in a list of strings representing keys.
            /// </summary>
            /// <param name="key">The parameter "key" is a list of strings that represents a set of keys
            /// or identifiers. The function "TypeConflict" likely checks if there are any conflicting
            /// data types associated with the given keys. The return value of the function is a
            /// boolean, which would be true if there is a type conflict</param>
            bool TypeConflict(List<string> key);

            /// <summary>
            /// The function MetadataValidate takes in a key-value pair and returns a unique identifier
            /// (GUID) after validating the metadata.
            /// </summary>
            /// <param name="key">The key is a string parameter that represents the metadata key that
            /// needs to be validated. It is used to identify the specific metadata field that needs to
            /// be validated.</param>
            /// <param name="value">The value parameter is a string that represents the value of a
            /// metadata key-value pair. It is the data that needs to be validated.</param>
            Guid MetadataValidate(string key, string value);

            /// <summary>
            /// This function returns a list of MetaResponseDto objects based on a given key.
            /// </summary>
            /// <param name="key">The key parameter is a string that represents the key for which the
            /// metadata list is being retrieved. It is used to identify the specific metadata list that
            /// is being requested.</param>
            List<MetaResponseDto>? GetMetaDataList(string key);

            /// <summary>
            /// This function creates address book services for a user with a given address book data
            /// and user ID.
            /// </summary>
            /// <param name="AddressBookDto">AddressBookDto is a data transfer object (DTO) that
            /// contains the information needed to create an address book. It may include fields such as
            /// the name of the address book, the owner of the address book, and the contacts within the
            /// address book.</param>
            /// <param name="Guid">A Guid (Globally Unique Identifier) is a 128-bit value that is used
            /// to identify objects in a unique way. It is often used in software development as a way
            /// to generate unique identifiers for various entities such as users, transactions, or
            /// resources. In the context of the method signature you</param>
            ResponseIdDto CreateAddressBookServices(AddressBookDto addressBookDto, Guid userId);

            /* The `GetAllAddressBook` method is retrieving a list of `AddressBookDto` objects from the
            system. It takes in three parameters: `rowSize`, `startIndex`, and `sortOption`. */
            List<AddressBookDto> GetAllAddressBook(int rowSize, int startIndex, string sortBy, string sortOrder);

            /// <summary>
            /// The function returns the number of address books as a DTO (data transfer object).
            /// </summary>
            NumberOfAddressBookDto GetAddressBookCount();

            /// <summary>
            /// The function returns an AddressBookDto object for a specific address book identified by
            /// its GUID.
            /// </summary>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer
            /// value used to identify resources. In this case, it is used to identify a specific
            /// address book.</param>
            AddressBookDto? GetSpecificAddressBook(Guid addressBookId);

            /// <summary>
            /// The function converts an AddressBook object to an AddressBookDto object.
            /// </summary>
            /// <param name="AddressBook">AddressBook is likely a class or data structure that
            /// represents a collection of contacts or addresses. It may contain properties such as
            /// name, phone number, email address, and other relevant information for each contact. The
            /// method SetToAddressBookDto takes an instance of this class and converts it into an
            /// AddressBook</param>
            AddressBookDto SetToAddressBookDto(AddressBook addressBook);

            /// <summary>
            /// This function updates an address book entry with the provided ID and data, and returns a
            /// response object.
            /// </summary>
            /// <param name="addressBookId">A unique identifier for the address book that needs to be
            /// updated.</param>
            /// <param name="AddressBookDto">AddressBookDto is a data transfer object (DTO) that
            /// contains the updated information for an address book. It typically includes fields such
            /// as the name of the address book, the owner of the address book, and a list of contacts
            /// associated with the address book. This object is used as a parameter in</param>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer
            /// value that is used to identify a unique object or entity in a system. In this case, the
            /// Guid parameter is used to identify a specific address book in the system.</param>
            ResponseDto UpdateAddressBookById(Guid? addressBookId, AddressBookDto addressBookDto, Guid userId);

           /// <summary>
           /// The function validates the input of an address book.
           /// </summary>
           /// <param name="AddressBook">The AddressBook parameter is likely an object or data structure
           /// that contains information about a collection of addresses, such as names, phone numbers,
           /// and email addresses. The ValidateInput function likely takes this AddressBook as input
           /// and performs some kind of validation or error checking on the data within it.</param>
            void ValidateInput(AddressBook addressBook);

            /// <summary>
            /// The function deletes an address book by its ID and returns a response DTO for a specific
            /// user.
            /// </summary>
            /// <param name="addressBookId">The unique identifier of the address book that needs to be
            /// deleted.</param>
            /// <param name="Guid">A Guid (Globally Unique Identifier) is a 128-bit value that is used
            /// to identify objects in a unique way. In this case, the Guid is being used to identify
            /// the specific address book that the user wants to delete.</param>
            ResponseDto DeleteAddressBookById(Guid? addressBookId, Guid userId);


            /// <summary>
            /// This is a generic function in C# that changes the active status of a given entity object
            /// based on a provided user ID.
            /// </summary>
            /// <param name="Model">The "Model" parameter is a generic type parameter that represents
            /// the type of entity that is being passed as an argument to the method. It is constrained
            /// to be a subtype of the "BaseEntity" class.</param>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer
            /// value that is used to identify a unique object or entity. In this context, the Guid
            /// parameter is used to identify a specific user who is making changes to the
            /// entity.</param>
            void ChangeActiveStatus<Model>(Model entity, Guid userId) where Model : BaseEntity;

            /// <summary>
            /// This function uploads an asset by ID for a specific user and address book, returning a
            /// response ID DTO.
            /// </summary>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer
            /// value used to identify resources such as objects, entities, or components in a system.
            /// In this case, the Guid parameters UserId and addressBookId are used to identify specific
            /// users and address books in the system.</param>
            /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer
            /// value used to identify resources such as objects, entities, or components in a system.
            /// In this case, the Guid parameters UserId and addressBookId are used to identify specific
            /// users and address books in the system.</param>
            /// <param name="UploadFileDto">UploadFileDto is a data transfer object (DTO) that contains
            /// information about the file being uploaded. It typically includes properties such as the
            /// file name, file size, file type, and the actual file data in binary format. This DTO is
            /// used as a parameter in the UploadAssetById method, which</param>
            ResponseIdDto UploadAssetById(Guid UserId, Guid addressBookId, UploadFileDto uploadFileDto);
            
            /// <summary>
            /// The function downloads a file by its address book ID and returns it as a
            /// FileStreamResult.
            /// </summary>
            /// <param name="addressBookId">The addressBookId parameter is a nullable Guid data type
            /// that represents the unique identifier of the address book file that needs to be
            /// downloaded. It is used to retrieve the specific file from the database or file system
            /// and return it as a FileStreamResult to the user for downloading.</param>
            FileStreamResult DownloadFileById(Guid? addressBookId);


        }
}
