using Automated.Testing.System.Core.Execute.models;

namespace Automated.Testing.System.Core.Execute
{
    public static class ServiceResponseHelper
    {
        public static ServiceResponse<T> ConvertToServiceResponse<T> (T response)
        {
            return new()
            {
                Content = response,
                ResponseInfo = new ResponseInfo(),
            };
        }
    }
}