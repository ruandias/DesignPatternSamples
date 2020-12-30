using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;
using DesignPatternSamples.Application.Repository;
using DesignPatternSamples.Application.Services;

namespace DesignPatternSamples.Application.Implementations
{
    public class DetranVerificadorPontuacaoServices : IDetranVerificadorPontuacaoService
    {
        private readonly IDetranVerificadorPontuacaoFactory _Factory;

        public DetranVerificadorPontuacaoServices(IDetranVerificadorPontuacaoFactory factory)
        {
            _Factory = factory;
        }

        public Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor)
        {
            IDetranVerificadorPontuacaoRepository repository = _Factory.Create(condutor.UF);

            return repository.ConsultarPontuacao(condutor);
        }
    }
}