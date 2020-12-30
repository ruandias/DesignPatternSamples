using System;
using DesignPatternSamples.WebAPI.Models.Detran.Enums;

namespace DesignPatternSamples.WebAPI.Models.Detran
{
    public class PontuacaoModel
    {
        public int Pontos { get; set; }
        public DateTime DataInfracao { get; set; }
        public ETipoInfracao TipoInfracao { get; set; }
    }
}