using SelSnab.Domain.Commons.Request;
using SelSnab.Domain.Models;
using Npgsql;
using SqlKata.Execution;

namespace SelSnab.Infrastructure.Services.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Создание нового пользователя в системе
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<int> CreatedUserAsync(UserModel model);
    /// <summary>
    /// Получение данных о пользователи по его email
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<UserModel> GetUserAsync(string login);
    /// <summary>
    /// Проверка наличия пользователя по email
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<bool> CheckedUserByLoginAsync(string login);
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<bool> LoginUserAsync(Domain.Commons.Request.LoginRequest request);
    
    /// <summary>
    /// Удаление пользователя по email
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task DeleteUserAsync(string login);
    /// <summary>
    /// Удаление пользователя по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<int> DeleteUserAsync(int id);
    /// <summary>
    /// Получение id пользователя по его email
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public Task<int> GetUserIdAsync(string login);
    /// <summary>
    /// Получение соли по почте пользователя
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<string?> GetSaltByEmail(string email);

    public void BlockedUser(string email);
}
