namespace DesignPatternSamples.WebAPI.Models.Detran.Enums
{
    public enum ETipoInfracao
    {
        // Gravíssima: R$ 293,47 e 7 pontos no prontuário.
        // Grave: R$ 195,23 e 5 pontos no prontuário.
        // Média: R$ 130,16 e 4 pontos no prontuário.
        // Leve: R$ 88,38 e 3 pontos no prontuário.
        
        Leve = 1,
        Media = 2,
        Grave = 3,
        Gravissima = 4
    }
}