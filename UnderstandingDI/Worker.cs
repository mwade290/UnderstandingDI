using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UnderstandingDI
{
    public class Worker
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly UnderstandingDIContext _dbContext;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, UnderstandingDIContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public void Run()
        {
            _logger.LogInformation("Inside Worker Class");
            var settings = new Settings()
            {
                Secret1 = _configuration["Settings:Secret1"],
                Secret2 = _configuration["Settings:Secret2"]
            };

            _logger.LogInformation($"Secret 1 is '{settings.Secret1}'");
            _logger.LogInformation($"Secret 2 is '{settings.Secret2}'");

            _dbContext.Add(new UnderstandingDIModel()
            {
                Message = "Adding a message to the database."
            });
            _dbContext.SaveChanges();
        }
    }
}
