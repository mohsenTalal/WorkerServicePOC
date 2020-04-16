﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WorkerServicePOC.ScopeProcess
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private int _executionCount = 0;
        private readonly ILogger _logger;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", _executionCount);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}