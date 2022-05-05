//using System.Threading.Tasks;
//using Automated.Testing.System.ApplicationServices.Interfaces;
//using Automated.Testing.System.Common.User.Dto.Request;
//using NUnit.Framework;

//public class Tests
//{
//    private readonly IAccountService _accountService;
//    private readonly IUserService _userService;

//    public Tests(IAccountService accountService, IUserService userService)
//    {
//        _accountService = accountService;
//        _userService = userService;
//    }

//    [SetUp]
//    public void Setup()
//    {
        
//    }

//    [Test]
//    public async Task Test1()
//    {
//        var result = await _accountService.RegisterUserAsync(new RegisterUserRequest()
//        {
//            Login = "test",
//            Password = "test",
//        }, null);
//        Assert.True(result);
//    }
//}