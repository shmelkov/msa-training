using System;
using HealthApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthApp.Core
{
	public interface IUserDbContext
	{
        public DbSet<Core.Entities.User> Users { get; set; }
    }
}

