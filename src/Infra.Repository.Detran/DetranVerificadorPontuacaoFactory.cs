using System;
using System.Collections.Generic;
using DesignPatternSamples.Application.Repository;

namespace DesignPatternSamples.Infra.Repository.Detran
{
    public class DetranVerificadorPontuacaoFactory : IDetranVerificadorPontuacaoFactory
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly IDictionary<string, Type> _Repositories = new Dictionary<string, Type>();

        public DetranVerificadorPontuacaoFactory(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        public IDetranVerificadorPontuacaoRepository Create(string UF)
        {
            IDetranVerificadorPontuacaoRepository result = null;

            if (_Repositories.TryGetValue(UF, out Type type))
            {
                result = _ServiceProvider.GetService(type) as IDetranVerificadorPontuacaoRepository;
            }

            return result;
        }

        public DetranVerificadorPontuacaoFactory Register(string UF, Type repository)
        {
            if (!_Repositories.TryAdd(UF, repository))
            {
                _Repositories[UF] = repository;
            }

            return this;
        }
    }
}