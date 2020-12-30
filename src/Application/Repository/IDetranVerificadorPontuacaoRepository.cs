using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;

namespace DesignPatternSamples.Application.Repository
{
    public interface IDetranVerificadorPontuacaoRepository
    {
        Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor);
    }
}