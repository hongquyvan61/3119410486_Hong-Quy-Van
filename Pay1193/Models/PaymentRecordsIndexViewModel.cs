using Pay1193.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Pay1193.Models
{
    public class PaymentRecordsIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }
        public Employee Employee { get; set; }

        [Display(Name = "Date Pay")]
        public DateTime DatePay { get; set; }
        public string MonthPay { get; set; }

        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }

        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; }

        [Display(Name = "Total earnings")]
        public decimal TotalEarnings { get; set; }

        //[Display(Name = "Total earnings")]
        //public decimal TotalDeduction { get; set; }

        [Display(Name = "Earning deduction")]
        public decimal EarningDeduction { get; set; }
        [Display(Name = "Net payment")]
        public decimal NetPayment { get; set; }
    }
}
