using System;

namespace DesignPatternSamples.Application.Repository
{
    public interface IDetranVerificadorPontuacaoFactory
    {
        public IDetranVerificadorPontuacaoFactory Register(string UF, Type repository);
        public IDetranVerificadorPontuacaoFactory Create(string UF);
    }
}