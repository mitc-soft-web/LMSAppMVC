using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Loan;
using LMSAppMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.Implementation.Services
{
    public class LoanService(ILoanRepository loanRepository, 
        IBookRepository bookRepository, IMemberRepository memberRepository,
        IUnitOfWork unitOfWork, ILogger<LoanService> logger) : ILoanService
    {
        private readonly ILoanRepository _loanRepository = loanRepository ?? throw new ArgumentNullException(nameof(loanRepository));
        private readonly IBookRepository _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        private readonly IMemberRepository _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ILogger<LoanService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<BaseResponse<IReadOnlyList<PendingLoansResponse>>> AllPendingLoansAsync()
        {
            var pendingLoans = await _loanRepository.GetPendingLoans();

            if(pendingLoans is null || !pendingLoans.Any())
            {
                return new BaseResponse<IReadOnlyList<PendingLoansResponse>>
                {
                    Message = "No pending loans",
                    Status = false
                };
            }

            return new BaseResponse<IReadOnlyList<PendingLoansResponse>>
            {
                Message = $"{pendingLoans.Count()} pending loans retrieved successfully",
                Status = true,
                Data = pendingLoans.Select(l => new PendingLoansResponse
                {
                    Id = l.Id,
                    Author = l.Book?.Author != null ? l.Book.Author.FullName : "",
                    BookTitle = l.Book != null ? l.Book.Title : "",
                    CategoryName = l.Book?.Category != null ? l.Book.Category.Name : "",
                    Borrower = l.Member != null ? l.Member.FullName : "",
                    DateInitiated = l.BorrowDate,
                    ReturnDate = l.ReturnDate,
                    Status = l.LoanStatus

                }).ToList()
            };
        }

        public async Task<BaseResponse<bool>> ApproveBookLoanAsync(Guid id, Guid librarianId)
        {
            var loan = await _loanRepository.Get<Loan>(l => l.Id == id);
            if (loan is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Loan not found",
                    Status = false
                };
            }


            var book = await _bookRepository.Get<Book>(b => b.Id == loan.BookId);

            if (book is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Book not found",
                    Status = false
                };
            }

            if (book.AvailableCopies < 2)
            {
                return new BaseResponse<bool>
                {
                    Message = "This book is not available at the moment",
                    Status = false
                };
            }

            if(loan.LoanStatus == Contracts.Enums.LoanStatus.Approved && !loan.IsReturned)
            {
                return new BaseResponse<bool>
                {
                    Message = "This loan already approved",
                    Status = false
                };
            }

            var dateApproved = DateTime.UtcNow;
            var dateDifference = dateApproved - loan.BorrowDate;

            var strategy = _unitOfWork.CreateExecutionStrategy();

            BaseResponse<bool> response = await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    book.AvailableCopies -= 1;

                    await _bookRepository.Update<Book>(book);
                    await _unitOfWork.SaveChangesAsync();

                    loan.ApprovedDate = dateApproved;
                    loan.ReturnDate = loan.BorrowDate + dateDifference;
                    loan.DueDate += dateDifference;
                    loan.LibrarianId = librarianId;
                    loan.LoanStatus = Contracts.Enums.LoanStatus.Approved;

                    await _loanRepository.Update<Loan>(loan);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new BaseResponse<bool>
                    {
                        Message = "Loan approved successfully",
                        Status = true
                    };

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error approving book loan");
                    return new BaseResponse<bool>
                    {
                        Message = "An error occurred while approving loan: " + ex.Message,
                        Status = false
                    };
                }


            });
            return response;
        }

        public async Task<BaseResponse<PendingLoansResponse>> GetPendingLoanDetailsAsync(Guid loanId)
        {
            var pendingLoan = await _loanRepository.GetPendingLoanDetails(loanId);

            if (pendingLoan is null)
            {
                return new BaseResponse<PendingLoansResponse>
                {
                    Message = "Loan couldn't be found",
                    Status = false
                };
            }

            return new BaseResponse<PendingLoansResponse>
            {
                Message = $"Pending loan retrieved successfully",
                Status = true,
                Data = new PendingLoansResponse
                {
                    Id = pendingLoan.Id,
                    Author = pendingLoan.Book?.Author != null ? pendingLoan.Book.Author.FullName : "",
                    BookTitle = pendingLoan.Book != null ? pendingLoan.Book.Title : "",
                    CategoryName = pendingLoan.Book?.Category != null ? pendingLoan.Book.Category.Name : "",
                    Borrower = pendingLoan.Member != null ? pendingLoan.Member.FullName : "",
                    DateInitiated = pendingLoan.BorrowDate,
                    ReturnDate = pendingLoan.ReturnDate,
                    Status = pendingLoan.LoanStatus

                }
            };
        }

        public async Task<BaseResponse<bool>> InitiateBookLoanAsync(Guid bookId,Guid memberId, InitiateBookLoanRequestModel request)
        {
            var book = await _bookRepository.Get<Book>(b => b.Id == bookId);
            if(book is null)
            {
                _logger.LogError($"Book with Id {bookId} doesn't exist");
                return new BaseResponse<bool>
                {
                    Message = "Book doesn't exist",
                    Status = false
                };
            }

            if(book.AvailableCopies < 2)
            {
                return new BaseResponse<bool>
                {
                    Message = "This book is not available at the moment",
                    Status = false
                };
            }

            var member = await _memberRepository.Get<Member>(m => m.Id == memberId);

            if(member is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Member cannot found",
                    Status = false
                };
            }

            if(member.MembershipNumber != request.MembershipNumber)
            {
                return new BaseResponse<bool>
                {
                    Message = "Sorry, You're not eligible to loan book",
                    Status = false
                };
            }

            var memberPendingBookLoan = await _loanRepository.Get<Loan>(l => l.MemberId == member.Id && l.BookId == book.Id && !l.IsReturned);
            if(memberPendingBookLoan is not null)
            {
                return new BaseResponse<bool>
                {
                    Message = "You have already initiated loan for this book. Kindly wait for admin approval",
                    Status = false
                };
            }

            if(request.ReturnDate < DateTime.UtcNow)
            {
                return new BaseResponse<bool>
                {
                    Message = "The returning date cannot be past date",
                    Status = false
                };
            }

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                MembershipNumber = request.MembershipNumber,
                BorrowDate = DateTime.UtcNow,
                ReturnDate = request.ReturnDate,
                LoanStatus = Contracts.Enums.LoanStatus.Pending,
                DueDate = DateTime.UtcNow.AddDays(7),
              
            };

            await _loanRepository.Add<Loan>(loan);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse<bool>
            {
                Message = "Loan initiated successfully. Keep checking your email for update on your loan status",
                Status = true
            } :
            new BaseResponse<bool>
            {
                Message = "Loan couldn't be initiated",
                Status = false
            };
        }

        public async Task<BaseResponse<bool>> ReturnBookAsync(Guid loanId, Guid memberId)
        {
            var loan = await _loanRepository.Get<Loan>(l => l.Id == loanId && l.MemberId == memberId);
            if (loan is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Loan not found",
                    Status = false
                };
            }


            var book = await _bookRepository.Get<Book>(b => b.Id == loan.BookId);
            if (book is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Book not found",
                    Status = false
                };
            }


            if (loan.LoanStatus == Contracts.Enums.LoanStatus.Returned && loan.IsReturned)
            {
                return new BaseResponse<bool>
                {
                    Message = "This book already returned",
                    Status = false
                };
            }

            var strategy = _unitOfWork.CreateExecutionStrategy();

            BaseResponse<bool> response = await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    book.AvailableCopies += 1;

                    await _bookRepository.Update<Book>(book);
                    await _unitOfWork.SaveChangesAsync();

                    loan.IsReturned = true;
                    loan.LoanStatus = Contracts.Enums.LoanStatus.Returned;

                    await _loanRepository.Update<Loan>(loan);
                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new BaseResponse<bool>
                    {
                        Message = "Book returned successfully",
                        Status = true
                    };

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error returning book");
                    return new BaseResponse<bool>
                    {
                        Message = "An error occurred while returning book: " + ex.Message,
                        Status = false
                    };
                }


            });
            return response;
        }
    }
}
