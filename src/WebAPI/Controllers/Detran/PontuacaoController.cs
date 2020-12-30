using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DesignPatternSamples.Application.DTO;
using DesignPatternSamples.Application.Services;
using DesignPatternSamples.WebAPI.Models;
using DesignPatternSamples.WebAPI.Models.Detran;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatternSamples.WebAPI.Controllers.Detran
{
    [Route("api/Detran/[controller]")]
    [ApiController]
    public class PontuacaoController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly IDetranVerificadorPontuacaoService _DetranVerificadorPontuacaoServices;

        public PontuacaoController(IMapper mapper,
            IDetranVerificadorPontuacaoService detranVerificadorPontuacaoServices)
        {
            _Mapper = mapper;
            _DetranVerificadorPontuacaoServices = detranVerificadorPontuacaoServices;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(SuccessResultModel<IEnumerable<PontuacaoModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromQuery] CondutorModel model)
        {
            var pontuacoes = await _DetranVerificadorPontuacaoServices.ConsultarPontuacao(_Mapper.Map<Condutor>(model));

            var result =
                new SuccessResultModel<IEnumerable<PontuacaoModel>>(
                    _Mapper.Map<IEnumerable<PontuacaoModel>>(pontuacoes));

            return Ok(result);
        }
    }
}