using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoinConvertor.Entities
{
    public class ConversionRequest
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime PerfprmedAt { get; set; } = DateTime.UtcNow;
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        public string ResponseBody { get; set; }
        public User User { get; set; }
    }
}
