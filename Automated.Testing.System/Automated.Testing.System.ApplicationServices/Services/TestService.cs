using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Automated.Testing.System.ApplicationServices.Interfaces;
using Automated.Testing.System.Common.Test;
using Automated.Testing.System.Common.Test.Dto;
using Automated.Testing.System.Common.Test.Dto.Request;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.DataAccess.Abstractions.Interfaces;
using Microsoft.Extensions.Primitives;
using NotImplementedException = System.NotImplementedException;

namespace Automated.Testing.System.ApplicationServices.Services
{
    /// <inheritdoc />
    public class TestService : ITestService
    {
        private readonly IUserService _userService;
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository testRepository, IMapper mapper, IUserService userService)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _userService = userService;
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
        public async Task<TestDto[]> GetTestsAsync(int[] categoryIds)
        {
            return _mapper.Map<TestDto[]>(await _testRepository.GetTestsAsync(categoryIds));
        }

        public Task<TestDto[]> GetTestAnswerAsync(int testId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TestPassedResultDto> CheckTestResultsAsync(CheckPassTestRequest request)
        {
            var userInfo = await _userService.GetCurrentUserInfo();
            var testAnswers = await _testRepository.GetTestTaskAnswersAsync(request.TestId);
            var countCorrect = 0;
            var allTaskInTest = testAnswers.Select(x => x.taskId).Distinct().Count();
            foreach (var taskId in testAnswers.Select(x => x.taskId).Distinct())
            {
                var userAnswers = request.ExecuteTasks.FirstOrDefault(task => task.TaskId == taskId)?.Answer;
                var correctAnswers = testAnswers.Where(x => x.taskId == taskId).Select(x => x.answer).ToArray();
                var correctAnswer = string.Join(",", correctAnswers);
                var isCorrect = correctAnswers.Any(x => userAnswers != null && userAnswers.Contains(x));
                countCorrect += isCorrect ? 1 : 0;
                await _testRepository.WriteUserTestResultAsync(userInfo.Id, request.TestId, taskId, userAnswers, correctAnswer, isCorrect);
            }

            return new TestPassedResultDto
            {
                CountTask = allTaskInTest,
                CountCorrectAnswer = countCorrect,
            };
        }

        public async Task<bool> CreateTestAsync(CreateTestRequest request)
        {
            Guard.NotNull(request, nameof(request));
            var userInfo = await _userService.GetCurrentUserInfo();

            var testId = await _testRepository.CreateTestAsync(request.TestName, userInfo.Id);
            await _testRepository.CreateTestCategoryAsync(testId, request.CategoryIds);
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