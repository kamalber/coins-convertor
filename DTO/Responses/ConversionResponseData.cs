namespace CoinConvertor.DTO.Responses
{
    public class ConversionResponseData
    {
        public CurrencyData Source { get; set; }
        public CurrencyData Conversion { get; set; }

        override public string ToString()
        {
            return "{ Source :{" + Source.ToString() + "}, Conversion:{" + Conversion.ToString() + "}}";
        }

    }
}
