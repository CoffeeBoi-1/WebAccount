﻿using AccountLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
	public class AccountContext : DbContext
	{
		public DbSet<AccountModel> Accounts { get; set; }
		public DbSet<MemberModel> Members { get; set; }
		public DbSet<MemberAccountRelation> MemberAccountRelations { get; set; }
		public DbSet<AddressModel> Addresses { get; set; }

		public AccountContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Определение промежуточной таблицы
			modelBuilder.Entity<MemberAccountRelation>()
				.HasKey(cpr => new { cpr.AccountId, cpr.MemberId });

			// Определение связи с родительской сущностью (ParentModel)
			modelBuilder.Entity<MemberAccountRelation>()
				.HasOne(cpr => cpr.Account)
				.WithMany(p => p.AccountRelations)
				.HasForeignKey(cpr => cpr.AccountId);

			// Определение связи с дочерней сущностью (ChildModel)
			modelBuilder.Entity<MemberAccountRelation>()
				.HasOne(cpr => cpr.Member)
				.WithMany(c => c.MemberRelations)
				.HasForeignKey(cpr => cpr.MemberId);

			modelBuilder.Entity<AccountModel>()
					.HasOne(account => account.Address)
					.WithOne(address => address.Account)
					.HasForeignKey<AccountModel>(account => account.AddressId);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=Accounts.db");
		}
	}
}
