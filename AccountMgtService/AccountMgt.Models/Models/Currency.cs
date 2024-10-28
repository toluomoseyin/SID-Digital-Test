using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Models.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
