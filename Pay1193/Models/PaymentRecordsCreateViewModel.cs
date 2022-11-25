using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pay1193.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Pay1193.Models
{
    public class PaymentRecordsCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Phai co payment ID")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [DataType(DataType.Date), Display(Name = "Date Pay")]
        public DateTime DatePay { get; set; } = DateTime.Now;
        public string MonthPay { get; set; } = DateTime.Now.Month.ToString();
        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public TaxYear? TaxYear { get; set; }

        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; } = "1250L";
        [Display(Name = "Hourly rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hour worked")]
        public decimal HourWorked { get; set; }
        [Display(Name = "Contractual hours")]
        public decimal ContractualHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeEarnings { get; set; }
        public decimal NiC { get; set; }
        public decimal Tax { get; set; }
        public decimal UnionFee { get; set; }
        public Nullable<decimal> SLC { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal EarningDeduction { get; set; }
        public decimal NetPayment { get; set; }
    }
}
