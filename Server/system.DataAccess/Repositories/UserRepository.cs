using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using CRMSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SystemDbContext _context;
    private readonly IMyPasswordHasher _myPasswordHasher;

    public UserRepository(SystemDbContext context, IMyPasswordHasher myPasswordHasher)
    {
        _context = context;
        _myPasswordHasher = myPasswordHasher;
    }

    public async Task<User> GetByLogin(string login)
    {
        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Login == login) ?? throw new Exception();

        return User.Create(userEntity.Id, userEntity.RoleId, userEntity.Login, userEntity.PasswordHash).user;
    }

    public async Task<int> Create(User user)
    {
        var (_, error) = User.Create(
        0,
        user.RoleId,
        user.Login,
        user.PasswordHash);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception User: {error}");

        var hashedPassword = _myPasswordHasher.Generate(user.PasswordHash);

        var userEntyties = new UserEntity
        {
            RoleId = user.RoleId,
            Login = user.Login,
            PasswordHash = hashedPassword
        };

        await _context.Users.AddAsync(userEntyties);
        await _context.SaveChangesAsync(); 

        return userEntyties.Id;
    }

    public async Task<int> Delete(int id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}


