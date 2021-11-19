using System.ComponentModel.DataAnnotations;

namespace Automated.Testing.System.Core.Execute.models
{
    /// <summary>
    /// Generic HTTP response
    /// </summary>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        public ServiceResponse()
        {
            ResponseInfo = new ResponseInfo();
        }
        
        /// <summary>
        /// Содержимое ответа
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// Информация об ответе
        /// </summary>
        [Required]
        public ResponseInfo ResponseInfo { get; set; }
    }
}