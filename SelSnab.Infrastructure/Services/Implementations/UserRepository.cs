using SelSnab.Domain.Commons.Request;
using SelSnab.Domain.Models;
using SelSnab.Infrastructure.Services.Interfaces;
using Npgsql;
using SqlKata;
using SqlKata.Execution;

namespace SelSnab.Infrastructure.Services.Implementations;

public class UserRepository : IUserRepository
{
    private readonly QueryFactory _query;
    private readonly ICryptographyService _cryptographyService;
    private readonly string TableName = "users";

    public UserRepository(IDbConnectionManager connectionManager, ICryptographyService cryptographyService)
    {
        _query = connectionManager.PostgresQueryFactory;
        _cryptographyService = cryptographyService;
    }

    public void BlockedUser(string email, bool block)
    {
        var query = _query.Query(TableName).Where("email", email).Update(new {is_blocked = block });
    }


    /// <inheritdoc />
    public async Task<bool> CheckedUserByLoginAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("email as Email");

        var result = await _query.FirstOrDefaultAsync<string>(query);

        if (result != null) return true;
        else return false;
    }
    
    /// <inheritdoc />
    public async Task<int> CreatedUserAsync(UserModel model)
    {
        string salt = _cryptographyService.GenerateSalt();
        string hashedPassword = _cryptographyService.HashPassword(model.Password, salt);
        model.Password = hashedPassword;
        model.Salt = salt;

        var query = _query.Query(TableName).AsInsert(model);
        
        return await _query.ExecuteAsync(query);
    }
    
    /// <inheritdoc />
    public async Task DeleteUserAsync(string login)
    {
        var query = _query.Query(TableName).Where("email", login).AsDelete();

        await _query.ExecuteAsync(query);
    }

    /// <inheritdoc />
    public async Task<int> DeleteUserAsync(int id)
    {
        var query = _query.Query(TableName).Where("id", id).AsDelete();

        return await _query.ExecuteAsync(query);
    }

    /// <inheritdoc />
    public async Task<string?> GetSaltByEmail(string email)
    {
        var query = _query.Query(TableName).Where("email", email).Select("salt");

        var result = await _query.FirstOrDefaultAsync<string?>(query);

        return result;
    }

    /// <inheritdoc />
    public Task<UserModel> GetUserAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("email as Email",
            "password as Password",
            "salt as Salt",
            "is_blocked as IsBlocked",
            "created_at as CreatedAt",
            "updated_at as UpdatedAt",
            "is_deleted as IsDeleted");

        var result = _query.FirstOrDefaultAsync<UserModel>(query);
        return result;
    }
    
    /// <inheritdoc />
    public async Task<int> GetUserIdAsync(string login)
    {
        var query = _query.Query(TableName)
            .Where("email", login)
            .Select("id as Id");

        var result = await _query.FirstAsync<int>(query);
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> LoginUserAsync(LoginRequest request)
    {
        var query = _query.Query(TableName)
            .Where("email", request.Email)
            .Where("is_blocked", false)
            .Select("password as Password",
            "salt as Salt");

        if(query == null) return false;
        var result = await _query.FirstOrDefaultAsync<CheckPasswordModel>(query);

        string hash = _cryptographyService.HashPassword(request.Password, result.Salt);

        if (hash == result.Password) return true;
        else return false;
    }
}
