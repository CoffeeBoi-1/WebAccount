﻿using AccountLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Services
{
	public class FilterService
	{
		private FilterModel _filter;
		private IQueryable<AccountModel> _accounts;

		public FilterService(FilterModel filter, IQueryable<AccountModel> accounts)
		{
			_filter = filter;
			_accounts = accounts;
		}

		public void FilterByNotEmptyMembers()
		{
			_accounts = _accounts
				.Where(account => account.AccountRelations.Any());
		}

		public void FilterBySureaname()
		{
			string surename = _filter.Surename.ToLower();
			_accounts = _accounts.Where(account => account.AccountRelations
			.Any(relation => relation.Member.Surename.ToLower() == surename));
		}

		public void FilterByName()
		{
			string name = _filter.Name.ToLower();
			_accounts = _accounts.Where(account => account.AccountRelations
			.Any(relation => relation.Member.Name.ToLower() == name));
		}

		public void FilterByPatronymic()
		{
			string patronymic = _filter.Patronymic.ToLower();
			_accounts = _accounts.Where(account => account.AccountRelations
			.Any(relation => relation.Member.Patronymic.ToLower() == patronymic));
		}

		public void FilterByCertainDate()
		{
			_accounts = _accounts
				.Where(account => account.StartDate == _filter.CertainDate);
		}

		public void FilterByAddress()
		{
			string street = _filter.Address.Street.ToLower();
			string house = _filter.Address.House.ToLower();
			string apartment = string.IsNullOrEmpty(_filter.Address.Apartment) ? "" : _filter.Address.Apartment.ToLower();

			_accounts = _accounts
				.Include(account => account.Address)
				.Where(joined => joined.Address.Street.ToLower().Contains(street) &&
							 joined.Address.House.ToLower() == house &&
							 joined.Address.Apartment.ToLower().Contains(apartment));
		}

		public void FilterByRoomArea()
		{
			_accounts = _accounts
				.Where(account => account.RoomArea == _filter.RoomArea);
		}

		public void FilterByAccountNumber()
		{
			_accounts = _accounts
				.Where(account => account.AccountNumber == _filter.AccountNumber);
		}

		public IQueryable<AccountModel> GetAccounts()
		{
			return _accounts;
		}
	}
}