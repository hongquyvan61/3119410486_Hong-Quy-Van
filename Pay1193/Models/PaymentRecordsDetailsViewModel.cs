using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pay1193.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pay1193.Models
{
    public class PaymentRecordsDetailsViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        [Display(Name = "Nhan Vien")]
        public Employee Employee { get; set; }
        

        [DataType(DataType.Date), Display(Name = "Date Pay")]
        public DateTime DatePay { get; set; }

        [Display(Name = "Month Pay")]
        public string MonthPay { get; set; }
        
        [Display(Name = "Tax Year ID")]
        public int TaxYearId { get; set; }

        //[Display(Name = "Year of Tax")]
        //public string Year { get; set; }

        [Display(Name = "Tax Year")]
        public TaxYear TaxYear { get; set; }

        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; }
        [Display(Name = "Hourly rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Hourly worked")]
        public decimal HourWorked { get; set; }

        [Display(Name = "Contractual hours")]
        public decimal ContractualHours { get; set; }

        [Display(Name = "Overtime hours")]
        public decimal OvertimeHours { get; set; }

        [Display(Name = "Overtime rate")]
        public decimal OvertimeRate { get; set; }

        [Display(Name = "Contractual earnings")]
        public decimal ContractualEarnings { get; set; }

        [Display(Name = "Overtime earnings")]
        public decimal OvertimeEarnings { get; set; }
        public decimal NiC { get; set; }
        public decimal Tax { get; set; }

        [Display(Name = "Union fee")]
        public decimal UnionFee { get; set; }
        public Nullable<decimal> SLC { get; set; }

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
