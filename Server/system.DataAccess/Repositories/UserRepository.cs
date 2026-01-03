using CRMSystem.Core.DTOs.User;
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

    public async Task<UserItem?> GetByLogin(string login)
    {
        return await _context.Users.AsNoTracking()
            .Where(u => u.Login == login)
            .Select(u => new UserItem(
            u.Id,
            u.Role == null
                ? string.Empty
                : u.Role.Name,
            u.RoleId,
            u.Login,
            u.PasswordHash))
            .FirstOrDefaultAsync();
    }

    public async Task<long> Create(User user)
    {
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

    public async Task<long> Update(long id, UserUpdateModel model)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new ArgumentException("User not found");

        if (!string.IsNullOrWhiteSpace(model.Login)) entity.Login = model.Login;
        if (!string.IsNullOrWhiteSpace(model.Password)) entity.PasswordHash = model.Password;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists (long id)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == id);
    }
}


