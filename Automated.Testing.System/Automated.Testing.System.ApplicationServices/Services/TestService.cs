using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Test;
using Automated.Testing.System.Common.Test.Dto;
using Automated.Testing.System.Common.Test.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using NotImplementedException = System.NotImplementedException;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        public async Task<TestTaskDto[]> GetTestTaskAsync(int testId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            
            var testTasks = _mapper.Map<TestTaskDto[]>(await _testRepository.GetTestTaskAsync(testId));
            var options = await _testRepository.GetTestTaskResponseOptionAsync(testTasks.Select(x => x.TestTaskId).ToArray());
            foreach (var task in testTasks)
            {
                task.ResponseOptions = options.Where(x => x.taskId == task.TestTaskId).Select(x => new ResponseOption
                {
                    Id = x.optionId,
                    Option = x.option
                }).ToArray();
            }

            return testTasks;
        }

        /// <inheritdoc />
        public async Task<TestDto[]> GetTestsAsync(int? categoryId)
        {
            return _mapper.Map<TestDto[]>(await _testRepository.GetTestsAsync(categoryId));
        }

        public Task<TestDto[]> GetTestAnswerAsync(int testId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<dynamic> CheckTestResultsAsync(dynamic request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateTestAsync(CreateTestRequest request)
        {
            Guard.NotNull(request, nameof(request));

            var testId = await _testRepository.CreateTestAsync(request.TestName, request.CategoryId);
            foreach (var task in request.Task)
            {
                var taskId = await _testRepository.CreateTestTaskAsync(task.Description, testId, task.TypeId);
                await Task.WhenAll(_testRepository.CreateTestTaskResponseOptionAsync(task.ResponseOptions, taskId),
                    _testRepository.CreateTestTaskAnswersAsync(task.Answers, taskId));
            }


            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveTestAsync(int testId)
        {
            Guard.GreaterThanZero(testId, nameof(testId));
            
            return await _testRepository.RemoveTestAsync(testId);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateTestAsync(UpdateTestRequest request)
        {
            Guard.NotNull(request, nameof(request));

            await _testRepository.UpdateTestAsync(request.TestId, request.TestName, request.CategoryId);
            await _testRepository.DeleteTestInformationTaskAsync(request.TestId);
            foreach (var task in request.Task)
            {
                var taskId = await _testRepository.CreateTestTaskAsync(task.Description, request.TestId, task.TypeId);
                await Task.WhenAll(_testRepository.CreateTestTaskResponseOptionAsync(task.ResponseOptions, taskId),
                    _testRepository.CreateTestTaskAnswersAsync(task.Answers, taskId));
            }

            return true;
        }
    }
}