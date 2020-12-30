using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;
using DesignPatternSamples.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using Workbench.IDistributedCache.Extensions;

namespace DesignPatternSamples.Application.Decorators
{
    public class DetranVerificadorPontuacaoDecoratorCache : IDetranVerificadorPontuacaoService
    {
        private readonly IDetranVerificadorPontuacaoService _Inner;
        private readonly IDistributedCache _Cache;

        public DetranVerificadorPontuacaoDecoratorCache(
            IDetranVerificadorPontuacaoService inner,
            IDistributedCache cache)
        {
            _Inner = inner;
            _Cache = cache;
        }

        public Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor)
        {
            return Task.FromResult(_Cache.GetOrCreate($"{condutor.UF}_{condutor.CNH}", () => _Inner.ConsultarPontuacao(condutor).Result));
        }
    }
}