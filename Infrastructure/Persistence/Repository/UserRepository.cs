using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUserByIsApproved()
        {
            return await _context.Users
           .Where(u => !u.IsApproved && u.DeletedDate == null)
            .OrderByDescending(x => x.CreatedDate)
           .ToListAsync();
           
        }

        public async Task<User?> GetUserWithRoleAsync(string email, string password)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);
        }
    }
}
