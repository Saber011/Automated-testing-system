using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Dictionary.Dto;
using Automated.Testing.System.Common.Dictionary.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Interfaces;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IMapper _mapper;

        public DictionaryService(IDictionaryRepository dictionaryRepository, IMapper mapper)
        {
            _dictionaryRepository = dictionaryRepository;
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        public async Task<DictionaryDto[]> GetAllAsync()
        {
            return _mapper.Map<DictionaryDto[]>(await _dictionaryRepository.GetAllDictionaryAsync());
        }

        /// <inheritdoc />
        public async Task<DictionaryItemDto[]> GetByIdAsync(int id)
        {
            Guard.GreaterThanZero(id, nameof(id));
            
            return _mapper.Map<DictionaryItemDto[]>(await _dictionaryRepository.GetDictionaryElementsByDictionaryIdAsync(id));
        }

        /// <inheritdoc />
        public async Task<bool> CreateDictionaryItemAsync(CreateDictionaryElementRequest request)
        {
            Guard.NotNull(request, nameof(request));
            Guard.NotNullOrWhiteSpace(request.Name, nameof(request.Name));
            Guard.GreaterThanZero(request.DictionaryId, nameof(request.DictionaryId));

            var dictionaryTable = await _dictionaryRepository.GetDictionaryTableName(request.DictionaryId);

            if (!string.IsNullOrWhiteSpace(dictionaryTable))
            {
                return await _dictionaryRepository.CreateDictionaryItemAsync(dictionaryTable, request.Name);
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateDictionaryItemAsync(UpdateDictionaryElementRequest request)
        {
            Guard.NotNull(request, nameof(request));
            Guard.NotNullOrWhiteSpace(request.Name, nameof(request.Name));
            Guard.GreaterThanZero(request.DictionaryId, nameof(request.DictionaryId));
            Guard.GreaterThanZero(request.ElementId, nameof(request.ElementId));

            var dictionaryTable = await _dictionaryRepository.GetDictionaryTableName(request.DictionaryId);

            if (!string.IsNullOrWhiteSpace(dictionaryTable))
            {
                return await _dictionaryRepository.UpdateDictionaryItemAsync(dictionaryTable,  request.ElementId,request.Name);
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteDictionaryItemAsync(DeleteDictionaryElementRequest request)
        {
            Guard.NotNull(request, nameof(request));
            Guard.GreaterThanZero(request.ElementId, nameof(request.ElementId));
            Guard.GreaterThanZero(request.DictionaryId, nameof(request.DictionaryId));

            var dictionaryTable = await _dictionaryRepository.GetDictionaryTableName(request.DictionaryId);

            if (!string.IsNullOrWhiteSpace(dictionaryTable))
            {
                return await _dictionaryRepository.DeleteDictionaryItemAsync(dictionaryTable, request.ElementId);
            }

            return false;
        }
    }
}