using AutoMapper;
using Contacts.Models.Api;
using Contacts.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Contacts.AutoMapper
{
    /// <summary>
    /// Creates a new AutoMapper configuration.
    /// </summary>
    public class AutoMapperConfiguration : Profile
    {
        /// <summary>
        /// This is Mapping Entity Method
        /// </summary>
        public AutoMapperConfiguration()
        {
            CreateMap<ContactData, Contact>()
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.ContactId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ReverseMap();
			
        }
    }
}
