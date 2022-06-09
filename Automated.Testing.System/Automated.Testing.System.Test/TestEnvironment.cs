using System;
using System.Net;
using System.Net.Http;
using Automated.Testing.System.Core.Core;
using Automated.Testing.System.Test.Interfaces;

namespace Automated.Testing.System.Test
{
    /// <inheritdoc />
    public sealed class TestEnvironment : ITestEnvironment
    {
        private readonly ApplicationFactory _applicationFactory;
        private readonly string _controller;

        /// <summary>
        /// Конструктор <see cref="TestEnvironment"/>.
        /// </summary>
        public TestEnvironment(
            ApplicationFactory applicationFactory,
            string controller)
        {
            Guard.NotNull(applicationFactory, nameof(applicationFactory));
            Guard.NotNullOrWhiteSpace(controller, nameof(controller));

            _applicationFactory = applicationFactory;
            _controller = controller;
        }

        /// <inheritdoc />
        public HttpClient CreateClient()
        {
            var httpClient = _applicationFactory.CreateClient();

            httpClient.BaseAddress = new Uri(httpClient.BaseAddress, _controller);
            
            return httpClient;
        }
    }
}