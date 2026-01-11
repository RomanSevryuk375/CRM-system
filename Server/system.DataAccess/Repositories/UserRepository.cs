// Ignore Spelling: Hasher

using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SystemDbContext _context;
    private readonly IMyPasswordHasher _myPasswordHasher;
    private readonly IMapper _mapper;

    public UserRepository(
        SystemDbContext context, 
        IMyPasswordHasher myPasswordHasher,
        IMapper mapper)
    {
        _context = context;
        _myPasswordHasher = myPasswordHasher;
        _mapper = mapper;
    }

    public async Task<UserItem?> GetByLogin(string login, CancellationToken ct)
    {
        return await _context.Users.AsNoTracking()
            .Where(u => u.Login == login)
            .ProjectTo<UserItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<long> Create(User user, CancellationToken ct)
    {
        var hashedPassword = _myPasswordHasher.Generate(user.PasswordHash);

        var userEntyties = new UserEntity
        {
            RoleId = user.RoleId,
            Login = user.Login,
            PasswordHash = hashedPassword
        };

        await _context.Users.AddAsync(userEntyties, ct);
        await _context.SaveChangesAsync(ct);

        return userEntyties.Id;
    }

    public async Task<long> Update(long id, UserUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, ct)
            ?? throw new NotFoundException("User not found");

        if (!string.IsNullOrWhiteSpace(model.Login)) entity.Login = model.Login;
        if (!string.IsNullOrWhiteSpace(model.Password)) entity.PasswordHash = model.Password;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var user = await _context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists (long id, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == id, ct);
    }
}


