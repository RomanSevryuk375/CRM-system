using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using system.DataAccess;


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
        var userEntyties = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        var users = userEntyties
            .Select(u => User.Create(u.UserId, u.UserRoleId, u.UserLogin, u.UserPasswordHash).user)
            .ToList();

        return users;
    }
    public async Task<int> Create(User user)
    {
        var userEntities = new UserEntity
        {
            UserRoleId = user.RoleId,
            UserLogin = user.Login,
            UserPasswordHash = user.PasswordHash
        };

        await _context.Users.AddAsync(userEntities);
        await _context.SaveChangesAsync();

        return userEntities.UserId;
    }
}
