using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contract.IService;
using Contract.IRepository;
using System.Security.Cryptography;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Entity.Dto;
using Entity.Data;
using Microsoft.AspNetCore.StaticFiles;
using Repository;
using Exception;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Service
{
    public class AddressBookService : IAddressBookService
    {
        /* The below code is declaring a C# class with private fields for IConfiguration,
        IAddressBookRepository, ApiDbContext, ILogger<AddressBookRepository>, and IMapper. These
        fields are likely dependencies that will be injected into the class constructor. */
        private readonly IConfiguration _config;
        private readonly IAddressBookRepository addressBookRepository;
        private readonly ApiDbContext _context;
        private readonly ILogger<AddressBookRepository> _logger;
        private readonly IMapper _mapper;

        /* The below code is defining a constructor for the AddressBookServices class in C#. It takes
        in four parameters: IConfiguration config, ApiDbContext context,
        ILogger<AddressBookRepositories> logger, and IMapper mapper. It initializes private
        variables _mapper, _logger, _config, and _context with the values of the corresponding
        parameters. It also creates an instance of the AddressBookRepositories class and assigns it
        to the addressBookRepositories variable. */
        public AddressBookService(IConfiguration config, ApiDbContext context, ILogger<AddressBookRepository> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _context = context;
            addressBookRepository = new AddressBookRepository(_config, _context, _logger);

        }

        //User signup
        /// <summary>
        /// The function creates a new user in the address book database and returns a response
        /// indicating success or failure.
        /// </summary>
        /// <param name="SignupDto">A data transfer object that contains the user's signup information,
        /// including their username and password.</param>
        /// <returns>
        /// The method is returning a ResponseDto object.
        /// </returns>
        public ResponseIdDto CreateUser(SignupDto signupDto)
        {
            _logger.LogInformation("Password and confirm password validated successfully");
            if (addressBookRepository.GetByUsername(signupDto.UserName) != null)
            {
                _logger.LogWarning("User name already exists");
                throw new BaseCustomException(409, "User name already exists", "Username already taken. Please choose a different username");
            }
            signupDto.Password = ComputeHash(signupDto.Password);
            User user = _mapper.Map<User>(signupDto);
            _logger.LogInformation("Username and password assigned to the instance of user database");
            addressBookRepository.CreateUser(user);
            _logger.LogInformation("New user added to the database successfully");
            ResponseIdDto responseIdDto = new ResponseIdDto
            {
                id = user.Id,
                description = "User signedup successfully"
            };
            _logger.LogInformation("{responseIdDto}", responseIdDto);
            return responseIdDto;
        }

        //JWT generator service
        /// <summary>
        /// This function generates a JWT token for a given user using a symmetric security key and
        /// returns it as a TokenResponseDto object.
        /// </summary>
        /// <param name="User">The user object contains information about the user for whom the JWT
        /// token is being generated, such as their username and ID.</param>
        /// <returns>
        /// The method returns a TokenResponseDto object which contains the generated JWT token and its
        /// type.
        /// </returns>
        public TokenResponseDto GenerateToken(User user)
        {
            _logger.LogInformation("Generating JWT token for user {user.UserName}", user.UserName);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim("NameId",user.Id.ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);
            string tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("JWT token generated successfully");
            TokenResponseDto tokenResponseDto = new TokenResponseDto
            {
                TokenType = "Bearer Token",
                Token = tokenstring,
            };
            _logger.LogInformation("{tokenResponseDto}", tokenResponseDto);
            return tokenResponseDto;
        }

        //To validate the username and password.
        /// <summary>
        /// The function validates a user's login credentials by checking if the username and password
        /// match with the stored values in the database.
        /// </summary>
        /// <param name="username">The username entered by the user trying to log in.</param>
        /// <param name="password">The password entered by the user trying to log in.</param>
        /// <returns>
        /// The method is returning a User object if the validation is successful. If the validation
        /// fails, it throws a BaseCustomException with a 401 status code and an error message.
        /// </returns>
        public User ValidateUser(string username, string password)
        {
            string hashedPasswordEntered = ComputeHash(password);
            User? user = addressBookRepository.GetByUsername(username);
            _logger.LogInformation("Validating user with username {username} and password {password}", username, password);
            if (user == null)
            {
                _logger.LogWarning("Unauthorized access - user not found");
                throw new BaseCustomException(401, "Unauthorized access", "Access denied. You need to authenticate yourself before accessing this resource. Please check your login details and try again");
            }
            if (username == user.UserName && user.Password == hashedPasswordEntered)
            {
                _logger.LogInformation("User validation successful");
                return user;
            }
            _logger.LogWarning("Unauthorized access - invalid username or password");
            throw new BaseCustomException(401, "Unauthorized access", "Access denied. You need to authenticate yourself before accessing this resource. Please check your login details and try again");

        }

        //Password hashing
        /// <summary>
        /// The function computes the SHA256 hash of an input string and returns it as a Base64 encoded
        /// string.
        /// </summary>
        /// <param name="input">A string that represents the input to be hashed.</param>
        /// <returns>
        /// The method returns a string that represents the SHA-256 hash of the input string in Base64
        /// format.
        /// </returns>
        public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                _logger.LogInformation("Hashing the input {input}", input);
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        //Validate the meta data
        /// <summary>
        /// This function checks if there are any type conflicts in a list of strings.
        /// </summary>
        /// <param name="key">A list of strings representing keys that need to be validated for type
        /// conflict.</param>
        /// <returns>
        /// A boolean value is being returned.
        /// </returns>
        public bool TypeConflict(List<string> key)
        {
            _logger.LogInformation("validating {key} for type conflict", key);
            List<string> uniqueKey = key.Distinct().ToList();
            if (key.Count != uniqueKey.Count)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// This function validates a given email metadata key and returns its corresponding type ID.
        /// </summary>
        /// <param name="key">A string representing the key of the metadata to be validated.</param>
        /// <param name="value">The value is a string parameter that is used to retrieve a list of
        /// metadata.</param>
        /// <returns>
        /// The method is returning a Guid value.
        /// </returns>
        public Guid MetadataValidate(string key, string value)
        {
            _logger.LogInformation("Validating the email meta data key {key}", key);
            List<MetaResponseDto> metaDataList = GetMetaDataList(value)!;
            List<string> metaTypeList = metaDataList.Where(dto => dto.Key != null).Select(dto => dto.Key).ToList()!;
            if (!metaTypeList.Contains(key.ToUpper()))
            {
                _logger.LogInformation("Meta data key {key} not found", key);
                throw new BaseCustomException(404, "Meatdata not found", "Type the valid meta data key personal, work or alternate");
            }
            return addressBookRepository.GetTypeId(key);
        }


        //To get meta-data details
        /// <summary>
        /// This C# function retrieves a list of metadata based on a given key.
        /// </summary>
        /// <param name="key">A string parameter representing the key for which metadata needs to be
        /// retrieved.</param>
        /// <returns>
        /// The method returns a list of MetaResponseDto objects or null if the list is empty.
        /// </returns>
        public List<MetaResponseDto>? GetMetaDataList(string key)
        {
            Guid? refSetId = addressBookRepository.GetRefSetId(key);
            if (refSetId == null)
            {
                _logger.LogWarning("refSetId not found in the database");
                throw new BaseCustomException(404, "Meatdata not found", "Type the valid key (Personal, Work, Alternate..)");
            }
            else
            {
                return addressBookRepository.GetRefTermDetailsList(addressBookRepository.GetRefTermIdList(refSetId));
            }
        }

       
        /// <summary>
        /// This function creates an address book for a user with the given information and returns a
        /// response ID.
        /// </summary>
        /// <param name="AddressBookDto">A data transfer object (DTO) that contains information about an
        /// address book, including its ID, user ID, name, description, and lists of emails, phones, and
        /// addresses associated with it.</param>
        /// <param name="Guid">A globally unique identifier (GUID) is a 128-bit number used to identify
        /// resources. In this case, it is used to identify the user who is creating the address
        /// book.</param>
        /// <returns>
        /// The method is returning a ResponseIdDto object.
        /// </returns>
        public ResponseIdDto CreateAddressBookServices(AddressBookDto addressBookDto, Guid userId)
        {
            _logger.LogInformation("Getting data from the user");
            Guid id = Guid.NewGuid();
            addressBookDto.Id = id;
            addressBookDto.UserId = userId;
            foreach(AddressBookDtoEmails emailTypeId in addressBookDto.Emails!)
            {
                emailTypeId.UserId = userId;
                emailTypeId.AddressBookId = id;
                emailTypeId.Type!.Id = MetadataValidate(emailTypeId.Type.Key , "EMAIL_ADDRESS_TYPE" );
            }
            foreach(AddressBookDtoPhones phoneTypeId in addressBookDto.Phones!)
            {
                phoneTypeId.UserId = userId;
                phoneTypeId.AddressBookId = id;
                phoneTypeId.Type!.Id = MetadataValidate(phoneTypeId.Type.Key , "PHONE_NUMBER_TYPE" );
            }
            foreach(AddressBookDtoAddresses addressTypeId in addressBookDto.Addresses!)
            {
                addressTypeId.UserId = userId;
                addressTypeId.AddressBookId = id;
                addressTypeId.Type!.Id = MetadataValidate(addressTypeId.Type.Key , "Address_Type" );
                addressTypeId.Country!.Id = MetadataValidate(addressTypeId.Country.Key , "COUNTRY" );
            }
            AddressBook addressBook = _mapper.Map<AddressBook>(addressBookDto);
            ValidateInput(addressBook);
            addressBookRepository.CreateAddressBook(addressBook);
            _logger.LogInformation("Address book created successfully");
            ResponseIdDto responseIdDto = new ResponseIdDto
            {
                id = addressBook.Id,
                description = "Address book created successfully",
            };
            _logger.LogInformation("The id of the created address book is {responseIdDto.Id}", responseIdDto.id);
            return responseIdDto;
        }


        /// <summary>
        /// This function retrieves all address book details, converts them into DTOs, and returns them
        /// as a list.
        /// </summary>
        /// <param name="rowSize">The number of records to be returned in a single page or
        /// request.</param>
        /// <param name="startIndex">startIndex is the index of the first record to be retrieved from
        /// the database. It is used for pagination purposes.</param>
        /// <param name="sortBy">The field by which the address book should be sorted. For example, if
        /// sortBy is "name", the address book will be sorted by name.</param>
        /// <param name="sortOrder">The sort order parameter specifies whether the results should be
        /// sorted in ascending or descending order. It can have two possible values: "asc" for
        /// ascending order and "desc" for descending order.</param>
        /// <returns>
        /// The method `GetAllAddressBook` returns a list of `AddressBookDto` objects.
        /// </returns>
        public List<AddressBookDto> GetAllAddressBook(int rowSize, int startIndex, string sortBy, string sortOrder)
        {
            List<AddressBook> addressBook = addressBookRepository.GetAllAddressBookDetails(rowSize, startIndex, sortBy, sortOrder);
            List<AddressBookDto> addressBookDtos = new List<AddressBookDto>();
            _logger.LogInformation("All address book is converting into dto and storing as a dto list");
            foreach (AddressBook oneAddressBook in addressBook)
            {
                AddressBookDto dto = SetToAddressBookDto(oneAddressBook);
                addressBookDtos.Add(dto);
            }
            _logger.LogInformation("All address books are fetched");
            return addressBookDtos;
        }

        //To count addressbook
        /// <summary>
        /// This C# function returns the count of address books as a NumberOfAddressBookDto object.
        /// </summary>
        /// <returns>
        /// The method is returning an object of type NumberOfAddressBookDto, which contains the count
        /// of address books obtained from the addressBookRepository.
        /// </returns>
        public NumberOfAddressBookDto GetAddressBookCount()
        {
            _logger.LogInformation("Assign the count of address book in the NumberOfAddressBookDto");
            NumberOfAddressBookDto numberOfAddressBookDto = new NumberOfAddressBookDto
            {
                Count = addressBookRepository.GetCount(),
            };
            _logger.LogInformation("The number of active address book in the database is {numberOfAddressBoodDto.Count}", numberOfAddressBookDto.Count);
            return numberOfAddressBookDto;
        }

        //To get specific addressbook
        /// <summary>
        /// This C# function retrieves a specific address book by its ID and returns it as a DTO, or
        /// throws an exception if it is not found.
        /// </summary>
        /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer value
        /// used to identify resources such as objects, entities, or components in a distributed
        /// computing environment. In this code snippet, it is used to identify a specific address book
        /// by its unique identifier.</param>
        /// <returns>
        /// A nullable AddressBookDto object is being returned.
        /// </returns>
        public AddressBookDto? GetSpecificAddressBook(Guid addressBookId)
        {
            AddressBook? addressBook = addressBookRepository.GetAddressBookDetails(addressBookId);
            if (addressBook == null)
            {
                _logger.LogWarning("Address book not found for {addressBookId}", addressBookId);
                throw new BaseCustomException(404, "Address book not found", "Type the valid addressbook id");
            }
            else
            {
                return SetToAddressBookDto(addressBook);
            }
        }

        //To assign the values from addressbook to the addressbookdto
        /// <summary>
        /// This function converts an AddressBook object into an AddressBookDto object by mapping its
        /// properties and logging the process.
        /// </summary>
        /// <param name="AddressBook">An object representing an address book, containing information
        /// such as first name, last name, emails, phones, and addresses.</param>
        /// <returns>
        /// The method is returning an instance of the AddressBookDto class, which is created by mapping
        /// the properties of the input AddressBook object to the corresponding properties of the
        /// AddressBookDto object.
        /// </returns>
        public AddressBookDto SetToAddressBookDto(AddressBook addressBook)
        {
            _logger.LogInformation("Converting address book into address book dto");
            AddressBookDto addressBookDto = new AddressBookDto()
            {
                Id = addressBook.Id,
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                Emails = addressBook.Emails.Select(email => new AddressBookDtoEmails()
                {
                    EmailAddress = email.EmailId,
                    Type = new AddressBookDtoType()
                    {
                        Key = addressBookRepository.GetKeyById(email.EmailTypeId),
                    },
                }).ToList(),
                Phones = addressBook.Phones.Select(phone => new AddressBookDtoPhones()
                {
                    PhoneNumber = phone.PhoneNumber,
                    Type = new AddressBookDtoType()
                    {
                        Key = addressBookRepository.GetKeyById(phone.PhoneTypeId),
                    },
                }).ToList(),
                Addresses = addressBook.Addresses.Select(address => new AddressBookDtoAddresses
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    Zipcode = address.Zipcode,
                    StateName = address.State,
                    Type = new AddressBookDtoType()
                    {
                        Key = addressBookRepository.GetKeyById(address.AddressTypeId),
                    },
                    Country = new AddressBookDtoType()
                    {
                        Key = addressBookRepository.GetKeyById(address.CountryTypeId),
                    },
                }).ToList(),
            };

            _logger.LogInformation("Address book converted into address book dto successfully");
            return addressBookDto;
        }

        //To update addressbook
        /// <summary>
        /// This function updates an address book entry with the provided information and returns a
        /// success response.
        /// </summary>
        /// <param name="addressBookId">A unique identifier for the address book that needs to be
        /// updated.</param>
        /// <param name="AddressBookDto">A data transfer object that contains the updated information
        /// for an address book.</param>
        /// <param name="Guid">A globally unique identifier (GUID) is a 128-bit number used to identify
        /// resources. In this case, it is used to identify a specific address book.</param>
        /// <returns>
        /// A ResponseDto object is being returned.
        /// </returns>
        public ResponseDto UpdateAddressBookById(Guid? addressBookId, AddressBookDto addressBookDto, Guid userId)
        {
            AddressBook? addressBook = addressBookRepository.GetAddressBookDetails(addressBookId);
            if (addressBook == null)
            {
                _logger.LogWarning("Address book not found for {addressBookId}", addressBookId);
                throw new BaseCustomException(404, "Addressbook not found", "Type the valid addressbook id");
            }
            else
            {
                addressBook.FirstName = addressBookDto.FirstName;
                addressBook.LastName = addressBookDto.LastName;
                addressBook.UpdatedBy = userId;
                addressBook.UpdatedDate = DateTime.UtcNow;

                addressBook.Emails = addressBookDto.Emails!.Select(email => new Email
                {
                    EmailId = email.EmailAddress,
                    EmailTypeId = MetadataValidate(email.Type!.Key, "EMAIL_ADDRESS_TYPE"),
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = addressBook.CreatedBy,
                    CreatedDate = addressBook.CreatedDate,
                    IsActive = true,
                }).ToList();

                addressBook.Phones = addressBookDto.Phones!.Select(phone => new Phone
                {
                    PhoneNumber = phone.PhoneNumber,
                    PhoneTypeId = MetadataValidate(phone.Type!.Key, "PHONE_NUMBER_TYPE"),
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = addressBook.CreatedBy,
                    CreatedDate = addressBook.CreatedDate,
                    IsActive = true,
                }).ToList();

                addressBook.Addresses = addressBookDto.Addresses!.Select(addresses => new Address
                {
                    AddressTypeId = MetadataValidate(addresses.Type!.Key, "ADDRESS_TYPE"),
                    Line1 = addresses.Line1,
                    Line2 = addresses.Line2,
                    City = addresses.City,
                    Zipcode = addresses.Zipcode,
                    State = addresses.StateName,
                    CountryTypeId = MetadataValidate(addresses.Country!.Key, "COUNTRY"),
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = addressBook.CreatedBy,
                    CreatedDate = addressBook.CreatedDate,
                    IsActive = true,
                }).ToList();
                
                ValidateInput(addressBook);
                _logger.LogInformation("Address book validated successfully");
                addressBookRepository.UpdateChanges(addressBook);
            }
            ResponseDto responseDto = new ResponseDto
            {
                StatusCode = 200,
                Message = "Success",
                Description = "Addressbook updated successfully",
            };
            _logger.LogInformation("{responseDto}", responseDto);
            return responseDto;
        }

        //Validate the input and error response
        /// <summary>
        /// This function validates the input data for an address book by checking for duplicate email
        /// addresses and phone numbers, and ensuring that the types of email addresses, phone numbers,
        /// addresses, and countries are valid.
        /// </summary>
        /// <param name="AddressBook">An object that contains information about a person's address book,
        /// including their emails, phones, and addresses.</param>
        public void ValidateInput(AddressBook addressBook)
        {
            _logger.LogInformation("Validating the input data");
            List<string> typelist = new List<string>();
            List<string> ctypelist = new List<string>();
            foreach (Email email in addressBook.Emails)
            {
                if (!addressBookRepository.CheckEmails(email.EmailId))
                {
                    _logger.LogWarning("Email address already exist");
                    throw new BaseCustomException(409, "Emailaddress already exist", "Type the valid emailaddress");
                }
                typelist.Add(addressBookRepository.GetKeyById(email.EmailTypeId));
            }
            if (TypeConflict(typelist) == false)
            {
                _logger.LogWarning("Invalid email address type");
                throw new BaseCustomException(409, "Meatdata already exist", "Type the valid emailaddress key");
            }
            _logger.LogInformation("Email validation completed");
            typelist.Clear();
            foreach (Phone phone in addressBook.Phones)
            {
                if (!addressBookRepository.CheckPhones(phone.PhoneNumber))
                {
                    _logger.LogWarning("Phone number already exist");
                    throw new BaseCustomException(409, "PhoneNumber already exist", "Type the valid phonenumber");
                }
                typelist.Add(addressBookRepository.GetKeyById(phone.PhoneTypeId));
            }
            if (!TypeConflict(typelist))
            {
                _logger.LogWarning("Invalid phone number type");
                throw new BaseCustomException(409, "Meatdata already exist", "Type the valid phonenumber Key");
            }
            _logger.LogInformation("Phone number validation completed");
            typelist.Clear();
            foreach (Address address in addressBook.Addresses)
            {
                typelist.Add(addressBookRepository.GetKeyById(address.AddressTypeId));
                ctypelist.Add(addressBookRepository.GetKeyById(address.CountryTypeId));
            }
            if (!TypeConflict(ctypelist))
            {
                _logger.LogWarning("Invalid country type");
                throw new BaseCustomException(409, "Meatdata already exist", "Type the valid country key");
            }
            ctypelist.Clear();
            if (!TypeConflict(typelist))
            {
                _logger.LogWarning("Invalid address type");
                throw new BaseCustomException(409, "Meatdata already exist", "Type the valid address Key");
            }
            _logger.LogInformation("Address validation completed");
            typelist.Clear();
        }

        //To delete addressbook
        /// <summary>
        /// The function deletes an address book and its associated emails, phones, and addresses, and
        /// updates the asset status if necessary, returning a success response.
        /// </summary>
        /// <param name="addressBookId">A nullable Guid representing the unique identifier of the
        /// address book to be deleted.</param>
        /// <param name="Guid">A globally unique identifier (GUID) is a 128-bit integer value used to
        /// identify resources. In this case, it is used to identify a specific address book that needs
        /// to be deleted.</param>
        /// <returns>
        /// A ResponseDto object is being returned.
        /// </returns>
        public ResponseDto DeleteAddressBookById(Guid? addressBookId, Guid userId)
        {
            AddressBook? addressBook = addressBookRepository.GetAddressBookDetails(addressBookId);
            if (addressBook == null)
            {
                _logger.LogWarning("Address book not found for {addressBookId}", addressBookId);
                throw new BaseCustomException(404, "Addressbook not found", "Type the valid addressbook id");
            }
            else
            {
                _logger.LogInformation("Deleting the address book");
                addressBook.IsActive = false;
                addressBook.UpdatedBy = userId;
                addressBook.UpdatedDate = DateTime.UtcNow;
                if (addressBookRepository.GetAssetStatus(addressBookId) == true)
                {   
                    addressBook.Asset!.IsActive = false;
                    addressBook.Asset.UpdatedBy = userId;
                    addressBook.Asset.UpdatedDate = DateTime.UtcNow;
                }
                foreach (Email email in addressBook.Emails)
                {
                    ChangeActiveStatus(email , userId);
                }
                foreach (Phone phone in addressBook.Phones)
                {
                    ChangeActiveStatus(phone , userId);
                }
                foreach (Address address in addressBook.Addresses)
                {
                    ChangeActiveStatus(address , userId);
                }
                addressBookRepository.UpdateChanges(addressBook);
            }
            ResponseDto responseDto = new ResponseDto
            {
                StatusCode = 200,
                Message = "Success",
                Description = "Addressbook deleted successfully",
            };
            _logger.LogInformation("{responseDto}", responseDto);
            return responseDto;
        }
        /// <summary>
        /// This function changes the active status of a given entity to false and updates its updated
        /// by and updated date properties.
        /// </summary>
        /// <param name="Model">The generic type parameter that represents the type of the entity being
        /// passed in. It must inherit from the BaseEntity class.</param>
        /// <param name="Guid">Guid is a globally unique identifier, which is a 128-bit integer value
        /// that is used to identify a unique object or entity. In this code snippet, the Guid parameter
        /// represents the unique identifier of the user who is making the change to the entity's active
        /// status.</param>
        public void ChangeActiveStatus<Model>(Model entity, Guid userId) where Model : BaseEntity
        {
            entity.IsActive = false;
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;
        }

        //To upload the asset
        /// <summary>
        /// This function uploads a file as an asset and returns the ID of the uploaded asset.
        /// </summary>
        /// <param name="Guid">A globally unique identifier (GUID) is a 128-bit number used to identify
        /// resources. In this case, it is used to identify the user and the address book.</param>
        /// <param name="Guid">A globally unique identifier (GUID) is a 128-bit number used to identify
        /// resources. In this case, it is used to identify the user and the address book.</param>
        /// <param name="UploadFileDto">A data transfer object that contains information about the file
        /// being uploaded, including the file itself and its file name.</param>
        /// <returns>
        /// The method returns a ResponseIdDto object which contains the ID of the uploaded asset.
        /// </returns>
        public ResponseIdDto UploadAssetById(Guid UserId, Guid addressBookId, UploadFileDto uploadFileDto)
        {
            _logger.LogInformation("upload {uploadFileDto} for {addressBookId}", uploadFileDto, addressBookId);
            byte[] fileBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                uploadFileDto.File!.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            string mimeType = GetMimeType(uploadFileDto.File.FileName);
            _logger.LogInformation("The mimeType of the given file is {mimeType}", mimeType);
            _logger.LogInformation("File is converted into the byte array");
            Guid id = Guid.NewGuid();
            Asset asset = new Asset
            {
                Id = id,
                AddressBookId = addressBookId,
                FileName = uploadFileDto.File.FileName,
                FileType = mimeType,
                FileContent = fileBytes,
                IsActive = true,
                CreatedBy = UserId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = UserId,
                UpdatedDate = DateTime.UtcNow,
            };
            _logger.LogInformation("Details of the file is assigned to the asset data type");
            if (addressBookRepository.GetAssetStatus(addressBookId) == true)
            {
                throw new BaseCustomException(409, "Asset already exist", "Asset is already present in the database for this address book");
            }
            addressBookRepository.UploadAsset(asset);
            _logger.LogInformation("Asset file uploaded successfully");
            ResponseIdDto responseIdDto = new ResponseIdDto
            {
                id = id,
                description = "File uploaded successfully"
            };
            _logger.LogInformation("The Id of the uploaded asset file is {responseIdDto.Id}", responseIdDto);
            return responseIdDto;
        }
        /// <summary>
        /// This function returns the MIME type of a file based on its extension.
        /// </summary>
        /// <param name="fileName">A string representing the name of the file for which the MIME type
        /// needs to be determined.</param>
        /// <returns>
        /// The method `GetMimeType` returns a string representing the MIME type of a file, based on its
        /// file extension. If the MIME type cannot be determined, the method returns the default value
        /// of "application/octet-stream".
        /// </returns>
        private string GetMimeType(string fileName)
        {
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        //To downloade the asset
        /// <summary>
        /// This C# function downloads a file by its ID from an address book repository and returns it
        /// as a FileStreamResult.
        /// </summary>
        /// <param name="addressBookId">A nullable Guid representing the unique identifier of the
        /// address book to be downloaded.</param>
        /// <returns>
        /// A FileStreamResult object is being returned.
        /// </returns>
        public FileStreamResult DownloadFileById(Guid? addressBookId)
        {
            Asset? asset = addressBookRepository.GetAssetById(addressBookId);
            if (asset == null)
            {
                _logger.LogWarning("Address book not found for {addressBookId}", addressBookId);
                throw new BaseCustomException(404, "Addressbook not found", "Type the valid addressbook id");
            }
            byte[] fileByte = asset.FileContent!;
            MemoryStream memoryStream = new MemoryStream(fileByte);
            _logger.LogInformation("The Byte array format file is converted into memory stream");
            FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, asset.FileType);
            _logger.LogInformation("The memory stream is converted into filestreamresult");
            fileStreamResult.FileDownloadName = asset.FileName;
            _logger.LogInformation("The asset is downloaded successfully");
            return fileStreamResult;
        }

    }
}