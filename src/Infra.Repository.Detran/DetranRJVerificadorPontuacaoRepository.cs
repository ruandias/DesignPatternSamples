using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;
using Microsoft.Extensions.Logging;

namespace DesignPatternSamples.Infra.Repository.Detran
{
    public class DetranRJVerificadorPontuacaoRepository : DetranVerificadorPontuacaoRepositoryCrawlerBase
    {
        private readonly ILogger _Logger;

        public DetranRJVerificadorPontuacaoRepository(ILogger<DetranRJVerificadorPontuacaoRepository> logger)
        {
            _Logger = logger;
        }

        protected override Task<IEnumerable<Pontuacao>> PadronizarResultado(string html)
        {
            _Logger.LogDebug($"Padronizando o Resultado {html}.");
            return Task.FromResult<IEnumerable<Pontuacao>>(new List<Pontuacao>() { new Pontuacao() });
        }

        protected override Task<string> RealizarAcesso(Condutor condutor)
        {
            _Logger.LogDebug($"Consultando pontuação do condutor possuinte da CNH {condutor.CNH} para o estado de RJ.");
            return Task.FromResult("CONTEUDO DO SITE DO DETRAN/RJ");
        }
    }
}