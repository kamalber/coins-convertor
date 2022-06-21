namespace CoinConvertor.DTO.Responses
{
    public class CurrencyData
    {
        public string Currency { get; set; }
        public double Amount { get; set; }

        override public string ToString()
        {
            return "Curreny : " + Currency + ", Amount : " + Amount;
        }
    }
}
