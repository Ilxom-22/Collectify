using System.Security.Authentication;
using AutoMapper;
using Collectify.Application.Identity.Brokers;
using Collectify.Application.Identity.Models.Dtos;
using Collectify.Application.Identity.Services;
using Collectify.Domain.Entities;
using Collectify.Domain.Enums;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Collectify.Infrastructure.Identity.Services;

public class AuthService(
    IMapper mapper,
    IRequestContextProvider contextProvider,
    IUserService userService,
    IAccessTokenService accessTokenService,
    IValidator<SignUpDetails> signUpDetailsValidator,
    IPasswordHasherService passwordHasherService) : IAuthService
{
    public async ValueTask<User> GetCurrentUser(
        CancellationToken cancellationToken = default)
    {
        var userId = contextProvider.GetUserId()
                     ?? throw new AuthenticationException("The current logged-in user could not be found!");
        
        var foundUser = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
            ?? throw new AuthenticationException("The current logged-in user could not be found!");

        return foundUser;
    }
    
    public async ValueTask<User> SignUpAsync(
        SignUpDetails details,
        CancellationToken cancellationToken = default)
    {
        await ValidateSignUpDetailsAsync(details, cancellationToken);

        var user = mapper.Map<User>(details);
        user.PasswordHash = passwordHasherService.HashPassword(details.Password);
        
        return await userService.CreateAsync(user, cancellationToken: cancellationToken);
    }

    public async ValueTask<AccessToken> SignInAsync(
        SignInDetails details,
        CancellationToken cancellationToken = default)
    {
        var foundUser = await userService.GetByUserName(details.UserName);
        ValidateSignInDetails(details, foundUser);

        var accessToken = await accessTokenService
            .CreateAsync(foundUser!, cancellationToken: cancellationToken);

        return accessToken;
    }

    public async ValueTask LogOutAsync(
        AccessToken accessToken,
        CancellationToken cancellationToken = default)
    {
        await accessTokenService.DeleteByUserIdAsync(accessToken.UserId, cancellationToken: cancellationToken);
    }

    private async ValueTask ValidateSignUpDetailsAsync(
        SignUpDetails details,
        CancellationToken cancellationToken = default)
    {
        if (await userService.UserExists(details.EmailAddress, cancellationToken))
            throw new ValidationException("User with this email address is already registered!");

        if (await userService.IsUserNameTaken(details.UserName, cancellationToken))
            throw new ValidationException(
                $"Username {details.UserName} is already taken by another user. Please try another one!");

        var validationResult = await signUpDetailsValidator.ValidateAsync(details, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors[0].ToString());
    }

    private void ValidateSignInDetails(SignInDetails details, User? foundUser)
    {
        if (foundUser is null 
            || !passwordHasherService.ValidatePassword(details.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Provided login details are invalid! Please, try again!");

        if (foundUser.Status == AccountStatus.Blocked)
            throw new AuthenticationException("Your account has been blocked. Please wait for an administrator to unblock it. We apologize for any inconvenience this may cause.");
    }
}