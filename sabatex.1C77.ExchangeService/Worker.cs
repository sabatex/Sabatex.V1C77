using Microsoft.Extensions.Logging;
using sabatex.V1C77;


namespace Sabatex.V1C77.ExchangeService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Start at: {DateTimeOffset.Now}");
                _logger.LogInformation($"Read config {DateTimeOffset.Now}");
                var connection1C77 = new sabatex.V1C77.Models.Connection();
                _configuration.GetSection("V1C77").Bind(connection1C77);
                _logger.LogInformation($"Start connection to {connection1C77.DataBasePath}");
                using (var global = COMObject1C77.CreateConnection(connection1C77).GlobalContext)
                {
                    _logger.LogInformation("Connected!");
                    _logger.LogInformation("Read metadata...");
                    var metadata = MetadataBuilder.GetMetadata(global);
                    _logger.LogInformation("Ok");

                }
                await Task.Delay(1000*60, stoppingToken);
            }
        }
    
        
    
    }
}