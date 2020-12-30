using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPatternSamples.Application.DTO;

namespace DesignPatternSamples.Application.Services
{
    public interface IDetranVerificadorPontuacaoService
    {
        Task<IEnumerable<Pontuacao>> ConsultarPontuacao(Condutor condutor);
    }
}