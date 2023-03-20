using System.ComponentModel.DataAnnotations;

namespace POC_ANTO.Models
{
    public class Invoice
    {
        
        [Key]
        [Display(Name = "Invoice Number")]
        public int InvoiceId { get; set; }
        [Display(Name = "Bill To")]

        public string? Billto { get; set; }

        public string? Contact { get; set; }
        public string? Date { get; set; }
        public float? Amount { get; set; }
        [Display(Name = "Sub Total")]
        public float? SubTotal { get; set; }
        public float? Tax { get; set; }
        [Display(Name = "From")]
        public string? From { get; set; }
        public List<Item>? Item { get; set; }
    }
}
