using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Entity.Model;
using Entity.Dto;
using Service;
using Contract.IService;
using Repository;
using Entity.Data;
using AutoMapper;

namespace AddressBookApi.Controller
{
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        
        
        /* The below code is declaring private fields for an AddressBookService, IConfiguration,
        ApiDbContext, ILogger, and IMapper in a C# class. These fields are likely used for
        dependency injection and accessing external services and resources within the class. */
        private readonly IAddressBookService addressBookService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private readonly ILogger<AddressBookRepository> _logger;
        private readonly IMapper _mapper;


        /* The below code is a constructor for an AddressBookController class in C#. It takes in four
        parameters: IConfiguration config, ApiDbContext context, ILogger<AddressBookRepositories>
        logger, and IMapper mapper. It initializes private fields _mapper, _logger, _config, and
        _context with the corresponding parameter values. It also creates an instance of
        addressBookService class and assigns it to the addressBookService field. */
        public AddressBookController(IConfiguration config, ApiDbContext context, ILogger<AddressBookRepository> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _context = context;
            addressBookService = new AddressBookService(_config, _context, _logger,_mapper);
        }

        /// <summary>
        /// Send Signup details to the server
        /// </summary>
        /// <remarks>To signup using Username &amp; Password &amp; ConfirmPassword</remarks>
        /// <param name="body">Define the http content type</param>
        /// <response code="201">Created</response>
        /// <response code="404">Not Found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/auth/signup")]
        [SwaggerOperation("Signup")]
        [SwaggerResponse(statusCode: 201, type: typeof(ResponseIdDto), description: "Created")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public IActionResult Signup([FromBody] SignupDto signupDto)
        {
            _logger.LogInformation("Signup method called");
            return CreatedAtAction(nameof(Signup),addressBookService.CreateUser(signupDto));
        }

        /// <summary>
        /// Send login details to the server
        /// </summary>
        /// <remarks>To login using Username &amp; Password</remarks>
        /// <param name="body">Define the http content type</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/auth/login")]
        [SwaggerOperation("Login")]
        [SwaggerResponse(statusCode: 200, type: typeof(TokenResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Login method called");
            User? user = addressBookService.ValidateUser(loginDto.UserName, loginDto.Password);
            _logger.LogInformation("User {UserName} logged in successfully.", loginDto.UserName);
            return Ok(addressBookService.GenerateToken(user));

        }


        /// <summary>
        /// Query the reference set metadata to retrieve the members
        /// </summary>
        /// <remarks>To fetch the refterm details with the refset</remarks>
        /// <param name="key"></param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/meta-data/ref-set/{key}")]
        [Authorize]
        [SwaggerOperation("GetMetaData")]
        [SwaggerResponse(statusCode: 200, type: typeof(MetaResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Resource not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult GetMetaData([FromRoute] string key)
        {
            _logger.LogInformation("Getting Metadata method called");
            return Ok(addressBookService.GetMetaDataList(key));
        }


        /// <summary>
        /// To create or add the addressbook by the user
        /// </summary>
        /// <remarks>To Create addressbook</remarks>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Not Found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/address-book")]
        [Authorize]
        [SwaggerOperation("CreateAddressBook")]
        [SwaggerResponse(statusCode: 201, type: typeof(ResponseIdDto), description: "Created")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not Found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public IActionResult CreateAddressBook([FromBody] AddressBookDto addressBookDto)
        {
            _logger.LogInformation("CreateAddressBook method called");
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return CreatedAtAction(nameof(CreateAddressBook),addressBookService.CreateAddressBookServices(addressBookDto, userId));
        }


        /// <summary>
        /// To view the list of addressbook added by the user
        /// </summary>
        /// <remarks>To Get all AddressBook</remarks>
        /// <param name="rowSize">No of address books to be returned</param>
        /// <param name="startIndex">Cursor position to fetch the address book</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/address-book")]
        [Authorize]
        [SwaggerOperation("FetchAllAddressBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<AddressBookDto>), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Resource not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult FetchAllAddressBook([FromQuery] int rowSize = 5, [FromQuery] int startIndex =1, [FromQuery] string? sortBy ="first_name", [FromQuery] string? sortOrder="asc" )
        {
            _logger.LogInformation("Fetch all address book method called");
            return Ok(addressBookService.GetAllAddressBook( rowSize, startIndex, sortBy!, sortOrder! ));
        }


        /// <summary>
        /// To show the number of addressbook created by the user
        /// </summary>
        /// <remarks>To Get total count of addressbook</remarks>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/address-book/count")]
        [Authorize]
        [SwaggerOperation("ShowAddressBookCount")]
        [SwaggerResponse(statusCode: 200, type: typeof(NumberOfAddressBookDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult ShowAddressBookCount()
        {
            _logger.LogInformation("Get total count of address book method called");
            return Ok(addressBookService.GetAddressBookCount());
        }


        /// <summary>
        /// To view the specific addressbook requested by the user
        /// </summary>
        /// <remarks>To Get specific addressbook</remarks>
        /// <param name="addressBookId">Type specific ID of address book to display</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal server error</response>
        // [HttpGet("{address-book-id}")]
        [HttpGet]
        [Route("/api/address-book/{address-book-id}")]
        [Authorize]
        [SwaggerOperation("FetchSpecificAddressBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(AddressBookDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Resource not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult FetchSpecificAddressBook([FromRoute(Name = "address-book-id")] Guid addressBookId)
        {
            _logger.LogInformation("Fetch specific address book method called");
            return Ok(addressBookService.GetSpecificAddressBook(addressBookId));
        }


        /// <summary>
        /// To update the added addressbook by the user
        /// </summary>
        /// <remarks>To update addressbook</remarks>
        /// <param name="addressBookId">ID of address book to update the details</param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Not Found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("/api/address-book/{address-book-id}")]
        [Authorize]
        [SwaggerOperation("UpdateAddressBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not Found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UpdateAddressBook([FromRoute(Name = "address-book-id")] Guid? addressBookId, [FromBody] AddressBookDto addressBookDto)
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(addressBookService.UpdateAddressBookById(addressBookId, addressBookDto, userId));
        }


        /// <summary>
        /// To remove the unwanted addressbook by the user
        /// </summary>
        /// <remarks>To delete an AddressBook</remarks>
        /// <param name="id">ID of the AddressBook to get the details</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("/api/address-book/{id}")]
        [Authorize]
        [SwaggerOperation("DeleteAddressBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Resource not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult DeleteAddressBook([FromRoute(Name = "id")] Guid? id)
        {
            _logger.LogInformation("Delete address book method called");
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(addressBookService.DeleteAddressBookById(id, userId));
        }


        /// <summary>
        /// To upload the resources by the user
        /// </summary>
        /// <remarks>To uploade the image or any file and then map it to an account</remarks>
        /// <param name="addressBookId">ID of the AddressBook</param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/asset/")]
        [Authorize]
        [SwaggerOperation("Upload")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseIdDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UploadAsset([FromQuery(Name = "address-book-id")] Guid addressBookId, [FromForm] UploadFileDto uploadFileDto)
        {
            _logger.LogInformation("Upload asset method called");
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(addressBookService.UploadAssetById(userId, addressBookId, uploadFileDto));
        }


        /// <summary>
        /// To download the required resources by the user
        /// </summary>
        /// <remarks>To download the file</remarks>
        /// <param name="addressBookId">ID of the Asset</param>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Resource not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/asset/{address-book-id}")]
        [Authorize]
        [SwaggerOperation("DownloadAsset")]
        [SwaggerResponse(statusCode: 200, type: typeof(DownloadFileDto), description: "Success")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Resource not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult DownloadAsset([FromRoute(Name = "address-book-id")] Guid? addressBookId)
        {
            _logger.LogInformation("Download asset method called");
            return addressBookService.DownloadFileById(addressBookId);
        }

    }

}


