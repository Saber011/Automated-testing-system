using System.Threading;
using System.Threading.Tasks;
using Automated.Testing.System.ApplicationServices.Interfaces;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class DictionaryService : IDictionaryService
    {
        /// <inheritdoc />
        public async Task GetAllAsync()
        {
            // todo any BL logic
            Thread.Sleep(4000);
        }
    }
}