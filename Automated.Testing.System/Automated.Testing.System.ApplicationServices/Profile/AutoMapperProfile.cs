using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Test.Dto;
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
            // User
            CreateMap<User, UserDto>();
            CreateMap<User, RegisterUserRequest>();
            
            //Dictionary
            CreateMap<Dictionary, DictionaryDto>();
            CreateMap<DictionaryItem, DictionaryItemDto>();

            // Test
            CreateMap<TestTask, TestTaskDto>();
            CreateMap<Test, TestDto>();
        }
    }
}