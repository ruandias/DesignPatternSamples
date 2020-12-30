using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;
using DesignPatternSamples.Application.Services;
using Microsoft.Extensions.Logging;

namespace DesignPatternSamples.Application.Decorators
{
    public class DetranVerificadorPontuacaoDecoratorLogger : IDetranVerificadorPontuacaoService
    {
        private readonly IDetranVerificadorPontuacaoService _Inner;
        private readonly ILogger<DetranVerificadorPontuacaoDecoratorLogger> _Logger;

        public DetranVerificadorPontuacaoDecoratorLogger(
            IDetranVerificadorPontuacaoService inner,
            ILogger<DetranVerificadorPontuacaoDecoratorLogger> logger)
        {
            _Inner = inner;
            _Logger = logger;
        }

        public async Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor)
        {
            Stopwatch watch = Stopwatch.StartNew();
            _Logger.LogInformation($"Iniciando a execução do método ConsultarPontuacap({condutor})");
            var result = await _Inner.ConsultarPontuacao(condutor);
            watch.Stop(); 
            _Logger.LogInformation($"Encerrando a execução do método ConsultarPontuacao({condutor}) {watch.ElapsedMilliseconds}ms");
            return result;
        }
    }
}