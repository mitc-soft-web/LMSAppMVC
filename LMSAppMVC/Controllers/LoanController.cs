using LMSAppMVC.Implementation.Services;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMSAppMVC.Controllers
{
    public class LoanController(ILoanService loanService) : Controller
    {
        private readonly ILoanService _loanService = loanService ?? throw new ArgumentNullException(nameof(loanService));

        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public async Task<IActionResult> PendingLoans()
        {
            var pendingLoans = await _loanService.AllPendingLoansAsync();

            if (pendingLoans.Status)
            {
                ViewBag.Message = pendingLoans.Message;
                return View(pendingLoans);
            }
            ViewBag.Message = pendingLoans.Message;

            return View(pendingLoans);
        }

        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public async Task<IActionResult> LoanDetails(Guid loanId)
        {
            var loan = await _loanService.GetPendingLoanDetailsAsync(loanId);

            if (loan.Status)
            {
                return View(loan);
            }

            ViewBag.Message = loan.Message;
            return View(loan);
        }

        [Authorize(Roles = "Librarian")]
        [HttpGet("Approve/{loanId}")]
        public async Task<IActionResult> Approve(Guid loanId)
        {
            var librarianIdString = User?.FindFirst("LibrarianId")?.Value;

            if (!Guid.TryParse(librarianIdString, out var librarianId))
                return BadRequest("Invlaid librarian Id");

            var approveResponse = await _loanService.ApproveBookLoanAsync(loanId, librarianId);
            if (approveResponse.Status)
            {
                TempData["SuccessMessage"] = approveResponse.Message;
                return RedirectToAction("PendingLoans");
            }
            ViewBag.Message = approveResponse.Message;

            return View(approveResponse);
        }

        [HttpGet(("InitiateBookLoan/{bookId}"))]
        public IActionResult InitiateBookLoan(Guid bookId)
        {
            ViewBag.BookId = bookId;

            return View(new InitiateBookLoanRequestModel());
        }

        [Authorize(Roles = "Member")]
        [HttpPost("InitiateBookLoan/{bookId}")]
        public async Task<IActionResult> InitiateBookLoan(Guid bookId, InitiateBookLoanRequestModel request)
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            ViewBag.NameAvatar = startName;

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var memberIdString = User?.FindFirst("MemberId")?.Value;

            if (!Guid.TryParse(memberIdString, out var memberId))
                return BadRequest("Invlaid Member Id");

            var loanResponse = await _loanService.InitiateBookLoanAsync(new Guid("23827dbc-e1a4-4170-a280-b38f451a9001"), memberId, request);

            if (loanResponse.Status)
            {
                TempData["SuccessMessage"] = loanResponse.Message;
                return RedirectToAction("MemberDashboard", "User");
            }

            ViewBag.BookId = bookId;
            ViewBag.Message = loanResponse.Message;
            return View(request);
        }
    }
}
