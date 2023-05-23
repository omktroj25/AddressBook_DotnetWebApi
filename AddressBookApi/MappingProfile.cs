using AutoMapper;
using Entity.Dto;
using Entity.Model;

namespace AddressBookApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {   

            //SignupDto to user model
            CreateMap<SignupDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));


            //Create new address book

            CreateMap<AddressBookDto, AddressBook>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AddressBookDtoEmails, Email>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.EmailTypeId, opt => opt.MapFrom(src => src.Type!.Id))
                .ForMember(dest => dest.AddressBookId, opt => opt.MapFrom(src => src.AddressBookId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AddressBookDtoPhones, Phone>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneTypeId, opt => opt.MapFrom(src => src.Type!.Id))
                .ForMember(dest => dest.AddressBookId, opt => opt.MapFrom(src => src.AddressBookId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UserId));

            CreateMap<AddressBookDtoAddresses, Address>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Line1, opt => opt.MapFrom(src => src.Line1))
                .ForMember(dest => dest.Line2, opt => opt.MapFrom(src => src.Line2))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.StateName))
                .ForMember(dest => dest.AddressBookId, opt => opt.MapFrom(src => src.AddressBookId))
                .ForMember(dest => dest.AddressTypeId, opt => opt.MapFrom(src => src.Type!.Id))
                .ForMember(dest => dest.CountryTypeId, opt => opt.MapFrom(src => src.Country!.Id))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UserId));


        }
        
    }
}