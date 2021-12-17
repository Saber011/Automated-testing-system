using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.User.Dto;
using Automated.Testing.System.Common.User.Dto.Request;
using Automated.Testing.System.DataAccess.Abstractions.Entities;

namespace Automated.Testing.System.ApplicationServices.Profile
{
    /// <summary>
    /// Класс для маппинга обьектов.
    /// </summary>
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, RegisterUserRequest>();
            
            CreateMap<Dictionary, DictionaryDto>();
            
            CreateMap<DictionaryItem, DictionaryItemDto>();
        }
    }
}