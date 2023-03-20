using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POC_ANTO.Models
{
    public class Item
    {
        [Key] 

        public int Id { get; set; }
        [Display(Name = "Item Name")]

        public string? ItemName { get; set; }

        public string? Quantity { get; set; }
        [Display(Name = "Unit Cost")]

        public float? UnitCost { get; set; }
        [Display(Name = "Item Amount")]

        public float? ItemAmount { get; set; }
        [Display(Name = "Invoice Number")]

        public int? InvoiceID { get; set; }

        [ForeignKey("InvoiceID")]
        public virtual Invoice? Invoice { get; set; }
    }
}
