using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Template.Config.DbContext;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Infra.Entities;

namespace Users.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(User user)
        {
            UserEntity entity = _mapper.Map<UserEntity>(user);
            await _dbContext.Set<UserEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();            
        }

        public async Task DeleteAsync(Guid id)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>().FindAsync(id);
            if (entity != null) { 
                _dbContext.Set<UserEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<User> users = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return users ?? [];
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(s => s.Id.Equals(id));

            return entity == null ? null : _mapper.Map<User>(entity);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .Where(w =>
                    w.Email.Equals(email)
                    )
                .FirstOrDefaultAsync();

            return entity == null ? null : _mapper.Map<User>(entity);
        }

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password) {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .Where(w => 
                    w.Email.Equals(email) &&
                    w.Password.Equals(password)
                    )
                .FirstOrDefaultAsync();

            return entity == null ? null : _mapper.Map<User>(entity);
        }

        public async Task UpdateAsync(User user)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(s => s.Id.Equals(user.Id));

            if (entity != null) { 

                _mapper.Map(user, entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
