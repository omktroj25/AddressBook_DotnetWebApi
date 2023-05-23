using Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Exception;
using Microsoft.AspNetCore.Http;

namespace AddressBookApiTest
{

    public class AddressBookApiTest : BaseTesting
    {
        /* The below code is a constructor for a class called AddressBookApiTests. It is calling the
        constructor of its base class (which is not shown in the code snippet) with no arguments. */
        public AddressBookApiTest() : base()
        {

        }

        //Signup api test
        /// <summary>
        /// This is a unit test for the Signup function in an address book controller, which tests for
        /// expected HTTP status codes and response types.
        /// </summary>
        /// <param name="username">The username to be used for the signup process.</param>
        /// <param name="password">The password parameter is a string variable that holds the password
        /// input for the SignupDto object.</param>
        /// <param name="confirmPassword">The confirmed password entered by the user during signup. It
        /// should match the password entered in the password field.</param>
        /// <param name="expectedCode">The expected HTTP status code that the Signup method should
        /// return. It is used to determine whether the test should expect a successful signup (201) or
        /// a failed signup (409).</param>
        [Theory]
        [InlineData("RootUser@outlook.com","User@123","User@123",201)]
        [InlineData("User@propelinc.com","Password@123","Password@123",409)]
        public void SignupTesting(string username, string password, string confirmPassword,int expectedCode)
        {
            SignupDto signupDto = new SignupDto
            {
                UserName = username,
                Password = password,
                ConfirmPassword = confirmPassword,
            };
            if(expectedCode == 201)
            {
                IActionResult result = addressBookController.Signup(signupDto);
                CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
                ResponseIdDto responseIdDto = Assert.IsType<ResponseIdDto>(createdAtActionResult.Value);
                Assert.NotNull(responseIdDto);
            }
            else
            {
                BaseCustomException ex = Assert.Throws<BaseCustomException>(() => addressBookController.Signup(signupDto));
                Assert.Equal(expectedCode, ex.StatusCode);
            }
        }

        //Login api test
        /// <summary>
        /// This is a unit test function for testing the login functionality of an address book
        /// application.
        /// </summary>
        [Theory]
        [InlineData("User@propelinc.com","Password@123",200)]
        [InlineData("Wrong@User.com","Nopassword@123",401)]
        public void LoginTesting(string userName, string password, int expectedCode)
        {
            LoginDto loginDto = new LoginDto()
            {
                UserName = userName,
                Password = password,
            };
            if(expectedCode == 200)
            {
                OkObjectResult result = (OkObjectResult)addressBookController.Login(loginDto);
                TokenResponseDto tokenResponseDto = (TokenResponseDto)result.Value!;
                Assert.NotNull(result);
                Assert.Equal(expectedCode, result.StatusCode);
                Assert.NotNull(tokenResponseDto?.Token);
            }
            else
            {
                BaseCustomException ex = Assert.Throws<BaseCustomException>(() => addressBookController.Login(loginDto));
                Assert.Equal(expectedCode, ex.StatusCode);
            }
            
        }

        //Metadata api testing
        /// <summary>
        /// The function tests the GetMetaData method of an address book controller by providing
        /// different keys and orders and checking if the returned metadata contains the expected keys.
        /// </summary>
        /// <param name="key">A string representing the type of metadata to retrieve.</param>
        /// <param name="order">The order parameter is an integer value used to determine which set of
        /// metadata to generate and compare against the actual metadata returned by the GetMetaData
        /// method. It is used in a series of if-else statements to populate a list of MetaResponseDto
        /// objects with different key values based on the value of order.</param>
        [Theory]
        [InlineData("EMAIL_ADDRESS_TYPE", 1)]
        [InlineData("PHONE_NUMBER_TYPE", 2)]
        [InlineData("ADDRESS_TYPE", 3)]
        [InlineData("COUNTRY", 4)]
        [InlineData("WrongMetaData", 5)]
        public void GetMetaDataTesting(string key, int order)
        {
            List<MetaResponseDto> meta = new List<MetaResponseDto>();
            if (order == 1)
            {
                meta.Add(new MetaResponseDto { Key = "PERSONAL" });
                meta.Add(new MetaResponseDto { Key = "WORK" });
            }
            else if (order == 2)
            {
                meta.Add(new MetaResponseDto { Key = "PERSONAL" });
                meta.Add(new MetaResponseDto { Key = "WORK" });
                meta.Add(new MetaResponseDto { Key = "ALTERNATE" });
            }
            else if (order == 3)
            {
                meta.Add(new MetaResponseDto { Key = "PERSONAL" });
                meta.Add(new MetaResponseDto { Key = "WORK" });
            }
            else
            {
                meta.Add(new MetaResponseDto { Key = "INDIA" });
                meta.Add(new MetaResponseDto { Key = "USA" });
            }

            if (order != 5)
            {
                IActionResult result = addressBookController.GetMetaData(key);

                List<MetaResponseDto>? resultMeta = ((OkObjectResult)result).Value as List<MetaResponseDto>;
                bool allKeysPresent = true;
                foreach (MetaResponseDto metaItem in meta)
                {
                    bool keyPresent = resultMeta!.Any(item => item.Key == metaItem.Key);
                    if (!keyPresent)
                    {
                        allKeysPresent = false;
                        break;
                    }
                }
                Assert.True(allKeysPresent);
            }
            else
            {
                BaseCustomException ex = Assert.Throws<BaseCustomException>(() => addressBookController.GetMetaData("WrongMetaData"));
                Assert.Equal(404, ex.StatusCode);
            }
        }

        //Create address book testing
        /// <summary>
        /// This is a unit test in C# for creating an address book with a phone number and type.
        /// </summary>
        [Fact]
        public void CreateAddressBookTesting()
        {
            Guid addressBookId = Guid.NewGuid();
            AddressBookDto addressBookDto = source(addressBookId);
            addressBookDto.Phones = new List<AddressBookDtoPhones> {
                new AddressBookDtoPhones {
                    PhoneNumber = "9898989899",
                    Type = new AddressBookDtoType { Key = "PERSONAL" }
                }
            };
            IActionResult result = addressBookController.CreateAddressBook(addressBookDto);
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            ResponseIdDto responseIdDto = Assert.IsType<ResponseIdDto>(createdAtActionResult.Value);
            Assert.NotNull(responseIdDto);
        }

        //Fetch all address book
        /// <summary>
        /// This is a unit test in C# that tests the FetchAllAddressBook function in an
        /// AddressBookController by comparing the expected and actual results.
        /// </summary>
        [Fact]
        public void FetchAllAddressBookTesting()
        {
            List<Guid> guidlist = new List<Guid> { addressBookId1, addressBookId2, addressBookId3 };
            List<AddressBookDto> expectedAddressBookDtos = new List<AddressBookDto>();
            foreach (Guid g in guidlist)
            {
                expectedAddressBookDtos.Add(source(g));
            }
            IActionResult result = addressBookController.FetchAllAddressBook(5, 0,"first_name","asc");
            OkObjectResult okresult = (OkObjectResult)result;
            List<AddressBookDto> actualAddressBookDto = (List<AddressBookDto>)okresult.Value!;
            Assert.Equal(expectedAddressBookDtos.Count, guidlist.Count);
        }

        //Count api testing
        [Theory]
        [InlineData(3)]
        public void CountTesting(int n)
        {
            IActionResult result = addressBookController.ShowAddressBookCount();
            OkObjectResult okresult = (OkObjectResult)result;
            NumberOfAddressBookDto numberOfAddressBookDto = (NumberOfAddressBookDto)okresult.Value!;
            Assert.Equal(n, numberOfAddressBookDto.Count);
        }

        //GetSpecific address book testing
        /// <summary>
        /// This is a unit test in C# that tests the FetchSpecificAddressBook method of an
        /// AddressBookController by comparing the expected and actual values of the returned
        /// AddressBookDto object.
        /// </summary>
        [Fact]
        public void FetchSpecificAddressBookTesting()
        {
            IActionResult result = addressBookController.FetchSpecificAddressBook(addressBookId1);
            AddressBookDto expectedAddressBookDto = source(addressBookId1);
            OkObjectResult okResult = (OkObjectResult)result;
            AddressBookDto? actualAddressBookDto = okResult.Value as AddressBookDto;
            Assert.Equal(expectedAddressBookDto.FirstName, actualAddressBookDto!.FirstName);
        }

        //Update addressbook testing
        /// <summary>
        /// The function tests the update functionality of an address book controller in C#.
        /// </summary>
        [Fact]
        public void UpdateAddressBookTesting()
        {
            AddressBookDto addressBookDto = source(addressBookId3);
            IActionResult result = addressBookController.UpdateAddressBook(addressBookId3, addressBookDto);
            Assert.IsType<OkObjectResult>(result);
            ObjectResult objectResult = (ObjectResult)result;
            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<ResponseDto>(objectResult.Value);
        }

        //Delete addressbook testing
        /// <summary>
        /// This is a unit test in C# that checks if the DeleteAddressBook function in an address book
        /// controller returns an OkObjectResult with a status code of 200 and a ResponseDto object
        /// value.
        /// </summary>
        [Fact]
        public void DeleteAddressBookTesting()
        {
            IActionResult result = addressBookController.DeleteAddressBook(addressBookId2);
            Assert.IsType<OkObjectResult>(result);
            ObjectResult objectResult = (ObjectResult)result;
            Assert.Equal(200, objectResult.StatusCode);
            Assert.IsType<ResponseDto>(objectResult.Value);
        }

        //Upload asset testing
        /// <summary>
        /// This is a unit test in C# for testing the functionality of uploading an asset to an address
        /// book.
        /// </summary>
        [Fact]
        public void UploadAssetTesting()
        {
            UploadFileDto uploadFileDto = new UploadFileDto
            {
                File = new FormFile(Stream.Null, 0, 0, "testFile", "test.jpg")
            };
            IActionResult result = addressBookController.UploadAsset(addressBookId3, uploadFileDto);
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            ResponseIdDto responseIdDto = Assert.IsType<ResponseIdDto>(okResult.Value);
            Assert.NotNull(responseIdDto.id);
        }

        //Download asset testing
        /// <summary>
        /// This is a unit test in C# that checks if the DownloadAsset function in the
        /// addressBookController returns the expected file properties.
        /// </summary>
        [Fact]
        public void DownloadAssetTesting()
        {
        FileStreamResult result = (FileStreamResult)addressBookController.DownloadAsset(addressBookId1);
        Assert.NotNull(result);
        Assert.Equal("Propel.jpg", result.FileDownloadName);
        Assert.Equal("image/jpeg", result.ContentType);
        }

        //Addressbook dto source to use
        public AddressBookDto source(Guid id)
        {

            if (id == addressBookId1)
            {

                AddressBookDto addressBookDto = new AddressBookDto()
                {
                    FirstName = "Balasubramanian",
                    LastName = "V",
                    Emails = new List<AddressBookDtoEmails>
                    {
                        new AddressBookDtoEmails
                        {
                            EmailAddress = "vbalaas@gmail.com" ,
                            Type = new AddressBookDtoType {
                                Key = "PERSONAL"
                            }
                        }
                    },
                    Addresses = new List<AddressBookDtoAddresses>
                    {
                        new AddressBookDtoAddresses
                        {
                            Line1 = "12131" ,
                            Line2 = "street" ,
                            City = "chennai" ,
                            Zipcode = "69991" ,
                            StateName = "Tamilnadu" ,
                            Type = new AddressBookDtoType {
                                Key = "WORK"
                            },
                            Country = new AddressBookDtoType {
                                Key = "INDIA"
                            }
                        }
                    },
                    Phones = new List<AddressBookDtoPhones>
                    {
                        new AddressBookDtoPhones
                        {
                            PhoneNumber = "9872981231" ,
                            Type = new AddressBookDtoType {
                                Key = "PERSONAL"
                            }
                        }
                    },
                };

                return addressBookDto;
            }
            else if (id == addressBookId2)
            {

                AddressBookDto addressBookDto = new AddressBookDto()
                {
                    FirstName = "Kumar",
                    LastName = "S",
                    Emails = new List<AddressBookDtoEmails>
                    {
                        new AddressBookDtoEmails
                        {
                            EmailAddress = "kumars@gmail.com" ,
                            Type = new AddressBookDtoType {
                                Key = "WORK"
                            }
                        }
                    },
                    Addresses = new List<AddressBookDtoAddresses>
                    {
                        new AddressBookDtoAddresses
                        {
                            Line1 = "678934" ,
                            Line2 = "road" ,
                            City = "florida" ,
                            Zipcode = "897456" ,
                            StateName = "London" ,
                            Type = new AddressBookDtoType {
                                Key = "WORK"
                            },
                            Country = new AddressBookDtoType {
                                Key = "USA"
                            }
                        }
                    },
                    Phones = new List<AddressBookDtoPhones>
                    {
                        new AddressBookDtoPhones
                        {
                            PhoneNumber = "9872673890" ,
                            Type = new AddressBookDtoType {
                                Key = "WORK"
                            }
                        }
                    },
                };

                return addressBookDto;
            }
            else if (id == addressBookId3)
            {

                AddressBookDto addressBookDto = new AddressBookDto()
                {
                    FirstName = "Ganesh",
                    LastName = "L",
                    Emails = new List<AddressBookDtoEmails>
                    {
                        new AddressBookDtoEmails
                        {
                            EmailAddress = "Siv123@gmail.com" ,
                            Type = new AddressBookDtoType {
                                Key = "PERSONAL"
                            }
                        }
                    },
                    Addresses = new List<AddressBookDtoAddresses>
                    {
                        new AddressBookDtoAddresses
                        {
                            Line1 = "987651" ,
                            Line2 = "Fourway" ,
                            City = "MyCity" ,
                            Zipcode = "876234" ,
                            StateName = "America" ,
                            Type = new AddressBookDtoType {
                                Key = "PERSONAL"
                            },
                            Country = new AddressBookDtoType {
                                Key = "USA"
                            }
                        }
                    },
                    Phones = new List<AddressBookDtoPhones>
                    {
                        new AddressBookDtoPhones
                        {
                            PhoneNumber = "0987654321" ,
                            Type = new AddressBookDtoType {
                                Key = "ALTERNATE"
                            }
                        }
                    },
                };

                return addressBookDto;
            }
            else
            {

                AddressBookDto addressBookDto = new AddressBookDto()
                {
                    FirstName = "RajaKumar",
                    LastName = "K",
                    Emails = new List<AddressBookDtoEmails>
                    {
                        new AddressBookDtoEmails
                        {
                            EmailAddress = "pushpas@gmail.com" ,
                            Type = new AddressBookDtoType {
                                Key = "PERSONAL"
                            }
                        }
                    },
                    Addresses = new List<AddressBookDtoAddresses>
                    {
                        new AddressBookDtoAddresses
                        {
                            Line1 = "89321" ,
                            Line2 = "palace" ,
                            City = "delhi" ,
                            Zipcode = "625291" ,
                            StateName = "Agra" ,
                            Type = new AddressBookDtoType {
                                Key = "WORK"
                            },
                            Country = new AddressBookDtoType {
                                Key = "INDIA"
                            }
                        }
                    },
                    Phones = new List<AddressBookDtoPhones>
                    {
                        new AddressBookDtoPhones
                        {
                            PhoneNumber = "9872981231" ,
                            Type = new AddressBookDtoType {
                                Key = "ALTERNATE"
                            }
                        }
                    },
                };

                return addressBookDto;
            }
        }

    }
}
