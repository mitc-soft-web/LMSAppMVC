using LMSAppMVC.Contracts.Services;
using LMSAppMVC.Implementation.Repositories;
using LMSAppMVC.Interfaces.Repositories;
using LMSAppMVC.Interfaces.Services;
using LMSAppMVC.Models.DTOs;
using LMSAppMVC.Models.DTOs.Auth;
using LMSAppMVC.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Principal;

namespace LMSAppMVC.Implementation.Services
{
    public class UserService(IUserRespository userRespository, 
        ILibrarianRegistrationCodeRepository employeeGenerator, IRoleRepository roleRepository,
        UserManager<User> userManager, IIdentityService identityService, IUnitOfWork unitOfWork,
        ILogger<UserService> logger) : IUserService
    {
        private readonly IUserRespository _userRespository = userRespository ?? throw new ArgumentNullException(nameof(userRespository));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly ILibrarianRegistrationCodeRepository _employeeNumberGenerator = employeeGenerator ?? throw new ArgumentNullException(nameof(employeeGenerator));
        private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public Task<BaseResponse> DeleteMember(Guid memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> RegisterLibrarianAsync(RegisterLibrarianRequestModel request)
        {
            var invite = await _employeeNumberGenerator.Get<LibrarianRegistrationCodeGenerator>(e => e.LibrarianRegistrationCode == request.LibrarianRegistrationCode);
            if (invite is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Invalid librarian registration code!",
                    Status = false
                };
            }

            if (invite.IsUsed)
            {
                return new BaseResponse<bool>
                {
                    Message = "Registration code already used",
                    Status = false
                };
            }

            if (invite.Expiry < DateTime.UtcNow)
            {
                return new BaseResponse<bool>
                {
                    Message = "Registration code expired, contact Admin.",
                    Status = false
                };
            }

            if (invite.Email != request.Email)
            {
                return new BaseResponse<bool>
                {
                    Message = "Email provided doesn't match the email attached to the registration code",
                    Status = false
                };
            }

            var userExists = await _userRespository.Any<User>(u => u.Email == request.Email);
            if (userExists) return new BaseResponse<bool>
            {
                Message = $"User with email '{request.Email}' already exists",
                Status = false
            };

            var role = await _roleRepository.Get<Role>(r => r.Name == "Librarian");

            if (role is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Member role doesn't exist",
                    Status = false
                };
            }

            // Hash password
            var passwordHash = _identityService.GetPasswordHash(request.HashPassword);

            var userLibrarian = new User
            {
                Email = request.Email,
                HashPassword = passwordHash,
                RoleId = role.Id,
                DateCreated = DateTime.UtcNow,

            };

            var strategy = _unitOfWork.CreateExecutionStrategy();

            BaseResponse<bool> response = await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var newUserResult = await _userManager.CreateAsync(userLibrarian);
                    if (!newUserResult.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", newUserResult.Errors.Select(e => e.Description)));
                    }

                    var librarian = new Librarian
                    {
                        FullName = request.FullName,
                        UserId = userLibrarian.Id,
                        LibrarianRegistrationCode = request.LibrarianRegistrationCode,
                        DateCreated = DateTime.UtcNow
                    };

                    await _userRespository.Add<Librarian>(librarian);
                    var success = await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return success > 0 ? new BaseResponse<bool>
                    {
                        Message = "Librarian account created successfully",
                        Status = true
                    }
                    : new BaseResponse<bool>
                    {
                        Message = "Account creation failed",
                        Status = false
                    };



                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error creating librarian");
                    return new BaseResponse<bool>
                    {
                        Message = "An error occurred while creating librarian: " + ex.Message,
                        Status = false
                    };
                }
            });
            return response;
         }

        public async Task<BaseResponse<bool>> RegisterMemberAsync(RegisterMemberRequestModel request)
        {
            var userExists = await _userRespository.Any<User>(u => u.Email == request.Email);
            if (userExists) return new BaseResponse<bool>
            {
                Message = $"User with email '{request.Email}' already exists",
                Status = false
            };

            var role = await _roleRepository.Get<Role>(r => r.Name == "Member");

            if (role is null)
            {
                return new BaseResponse<bool>
                {
                    Message = "Member role doesn't exist",
                    Status = false
                };
            }

            //if(request.ConfirmPassword != request.HashPassword)
            //{
            //    return new BaseResponse<bool>
            //    {
            //        Message = "Confirm password must match the password",
            //        Status = false,
            //    };
            //}


            // Hash password
            var passwordHash = _identityService.GetPasswordHash(request.HashPassword);
            var userMember = new User
            {
                Email = request.Email,
                HashPassword = passwordHash,
                RoleId = role.Id,
                DateCreated = DateTime.UtcNow,

            };
            var strategy = _unitOfWork.CreateExecutionStrategy();

            BaseResponse<bool> response = await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var newUserResult = await _userManager.CreateAsync(userMember);
                    if (!newUserResult.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", newUserResult.Errors.Select(e => e.Description)));
                    }

                    var member = new Member
                    {
                        FullName = request.FullName,
                        Address = request.Address,
                        Phone = request.PhoneNumber,
                        UserId = userMember.Id,
                        DateCreated = DateTime.UtcNow,
                        MembershipNumber = GenerateMembershipNumber()
                    };
                    await _userRespository.Add<Member>(member);
                    var success = await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return success > 0 ? new BaseResponse<bool>
                    {
                        Message = "Member account created successfully",
                        Status = true
                    }
                    : new BaseResponse<bool>
                    {
                        Message = "Account creation failed",
                        Status = false
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error creating member");
                    return new BaseResponse<bool>
                    {
                        Message = "An error occurred while creating member: " + ex.Message,
                        Status = false
                    };
                }
            });
            return response;
                
        }

        public async Task<BaseResponse<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            var user = await _userRespository.GetUserByEmail(request.Email);
            if (user is null)
            {
                _logger.LogError("Invalid credentials");
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Invalid credentials",
                    Status = false
                };
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                _logger.LogError("Invalid credentials");
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Invalid credentials",
                    Status = false
                };
            }
            var userRole = await _userRespository.GetUserRole(user.Id);

            var role = userRole?.Role?.Name;

            if(role is null)
            {
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Role not found",
                    Status = false
                };
            }

            if (role == "Member")
            {
                return new BaseResponse<LoginResponseModel>
                {
                    Message = "Login successful",
                    Status = true,
                    Data = new LoginResponseModel
                    {

                        UserId = user.Id,
                        MembershipNo = user?.Member?.MembershipNumber,
                        Email = user.Email,
                        Role = role,
                        FullName = user.Member != null ? $"{user.Member?.FullName}" : string.Empty

                    }
                };
            }

            return new BaseResponse<LoginResponseModel>
            {
                Message = "Login successful",
                Status = true,

                Data = new LoginResponseModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = role,
                    FullName = user.Librarian != null ? $"{user.Librarian?.FullName}" : string.Empty,


                }
            };

           
        }

        private string GenerateMembershipNumber()
        {
            return $"Mem-{Guid.NewGuid().ToString().Substring(0, 5).Replace("-", "").ToUpper()}";
        }
            
    }
}
