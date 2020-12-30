using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;
using DesignPatternSamples.Application.Repository;

namespace DesignPatternSamples.Infra.Repository.Detran
{
    public abstract class DetranVerificadorPontuacaoRepositoryCrawlerBase : IDetranVerificadorPontuacaoRepository
    {
        public async Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor)
        {
            var html = await RealizarAcesso(condutor);
            return await PadronizarResultado(html);
        }

        protected abstract Task<string> RealizarAcesso(Condutor condutor);
        protected abstract Task<IEnumerable<Pontuacao>> PadronizarResultado(string html);
    }
}