using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Pay1193.Entity;
using Pay1193.Models;
using Pay1193.Services;
using Pay1193.Services.Implement;
using System.Text;

namespace Pay1193.Controllers
{
    public class PaymentRecordsController : Controller
    {
        private readonly IPayService _paymentService;
        private readonly IEmployee _employeeService;
        private readonly INationalInsuranceService _nationalInsuranceService;
        private readonly ITaxService _taxService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private decimal overtimehours;
        private decimal overtimeearnings;
        private decimal contractualearnings;
        private decimal totalearnings;
        private decimal taxamount;
        private decimal nic;
        private decimal slc;
        private decimal unionfee;

        public PaymentRecordsController(IPayService paymentService, IEmployee employeeService, INationalInsuranceService nationalInsuranceService, ITaxService taxService, IWebHostEnvironment webHostEnvironment)
        {
            _paymentService = paymentService;
            _employeeService = employeeService;
            _nationalInsuranceService = nationalInsuranceService;
            _taxService = taxService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var paymentrecord = _paymentService.GetAll().Select(paymentrecord => new PaymentRecordsIndexViewModel
            {
                Id = paymentrecord.Id,
                EmployeeId = paymentrecord.EmployeeId,
                EmployeeFullName = _employeeService.GetNameById(paymentrecord.EmployeeId),
                Employee = paymentrecord.Employee,
                DatePay = paymentrecord.DatePay,
                MonthPay = paymentrecord.MonthPay,
                TaxYearId = paymentrecord.TaxYearId,
                TaxCode = paymentrecord.TaxCode,
                TotalEarnings = paymentrecord.TotalEarnings,
                NetPayment = paymentrecord.NetPayment

            }).ToList();
            return View(paymentrecord);
        }

        public IActionResult Detail(int id)
        {
            var paymentrecord = _paymentService.GetById(id);
            if (paymentrecord == null)
            {
                return NotFound();
            }
            PaymentRecordsDetailsViewModel model = new PaymentRecordsDetailsViewModel()
            {
                Id = paymentrecord.Id,
                EmployeeId = paymentrecord.EmployeeId,
                DatePay = paymentrecord.DatePay,
                MonthPay = paymentrecord.MonthPay,
                TaxYearId = paymentrecord.TaxYearId,
                TaxCode = paymentrecord.TaxCode,
                HourlyRate = paymentrecord.HourlyRate,
                HourWorked = paymentrecord.HourWorked,
                ContractualHours = paymentrecord.ContractualHours,
                OvertimeHours = _paymentService.OverTimeHours(paymentrecord.HourWorked, paymentrecord.ContractualHours),
                OvertimeRate = _paymentService.OvertimeRate(paymentrecord.HourlyRate),
                ContractualEarnings = paymentrecord.ContractualEarnings,
                OvertimeEarnings = paymentrecord.OvertimeEarnings,
                NiC = paymentrecord.NiC,
                Tax = paymentrecord.Tax,
                UnionFee = paymentrecord.UnionFee,
                SLC = paymentrecord.SLC,
                TotalEarnings = paymentrecord.TotalEarnings,
                NetPayment = paymentrecord.NetPayment
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.FillSelectBoxWithEmployees();
            ViewBag.taxYears = _paymentService.GetAllTaxYear();
            var model = new PaymentRecordsCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentRecordsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var paymentrecord = new PaymentRecord
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    DatePay = model.DatePay,
                    MonthPay = model.MonthPay,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HourWorked = model.HourWorked,
                    ContractualHours = model.ContractualHours,
                    OvertimeHours = overtimehours = _paymentService.OverTimeHours(model.HourWorked, model.ContractualHours),
                    ContractualEarnings = contractualearnings = _paymentService.ContractualEarning(model.ContractualHours, model.HourWorked, model.HourlyRate),
                    OvertimeEarnings = overtimeearnings = _paymentService.OvertimeEarnings(_paymentService.OvertimeRate(model.HourlyRate), _paymentService.OverTimeHours(model.HourWorked, model.ContractualHours)),
                    TotalEarnings = totalearnings = _paymentService.TotalEarnings(overtimeearnings, contractualearnings),
                    NiC = nic = _nationalInsuranceService.NIContribution(totalearnings),
                    Tax = taxamount = _taxService.TaxAmount(totalearnings),
                    UnionFee = unionfee = _employeeService.UnionFee(model.EmployeeId),
                    SLC = _employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalearnings),
                    NetPayment = _paymentService.NetPay(totalearnings, _paymentService.TotalDeduction(taxamount,nic,slc,unionfee)),
                };
                await _paymentService.CreateAsync(paymentrecord);
                return RedirectToAction("Index");
            }
            ViewBag.employees = _employeeService.FillSelectBoxWithEmployees();
            ViewBag.taxYears = _paymentService.GetAllTaxYear();
            return View(model);
        }
    }
}
