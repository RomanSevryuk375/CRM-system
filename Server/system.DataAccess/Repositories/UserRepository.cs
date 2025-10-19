using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SystemDbContext _context;

    public UserRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> Get()
    {
        var userEntyries = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        var users = userEntyries
            .Select(u => User.Create(u.Id, u.RoleId, u.Login, u.PasswordHash).user)
            .ToList();

        return users;
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
        var userEntyties = new UserEntity
        {
            RoleId = user.RoleId,
            Login = user.Login,
            PasswordHash = user.PasswordHash
        };

        await _context.Users.AddAsync(userEntyties);
        await _context.SaveChangesAsync();

        return userEntyties.Id;
    }
}
