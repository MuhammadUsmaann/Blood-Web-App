
using BloodDonationWebApp.Database;using BloodDonationWebApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonationWebApp.Services
{
	public partial class UsersRepository : IRepository<Users>
	{
		private const string SqlTableName = "Users";
		private const string SqlSelectCommand = " SELECT * FROM " + SqlTableName + " Where isActive = 1 ";
		private const string SqlInsertCommand = " INSERT INTO " + SqlTableName + " ( UserName, Email, Password, ContactNo, Role, Country, State, City, isDonor, RegistrationDate, UpdationDate) OUTPUT Inserted.UserID Values(@UserName, @Email, @Password, @ContactNo, @Role, @Country, @State, @City, @isDonor, @RegistrationDate, @UpdationDate)";
		private const string SqlUpdateCommand = " UPDATE " + SqlTableName + " Set UserName = @UserName, Email = @Email, Password = @Password, ContactNo = @ContactNo, Role = @Role, Country = @Country, State = @State, City = @City, isDonor = @isDonor, isActive = @isActive, RegistrationDate = @RegistrationDate, UpdationDate = @UpdationDate where ( UserID = @UserID ) AND  isActive = 1 ";
		private const string SqlDeleteCommand = " Update " + SqlTableName + " Set isActive = 0 where ( UserID = @UserID ) AND isActive = 1 ";

		public override int Insert(Users model)
		{
			return Query<int>(SqlInsertCommand, model).Single();
		}
		public override int Remove(int id)
		{
			return Execute(SqlDeleteCommand, new { UserID = id });
		}
		public override  Users FindByID(int id)
		{
			return Query<Users>(SqlSelectCommand + " AND UserID = @UserID ", new { UserID = id }).FirstOrDefault();
		}
		public override IEnumerable<Users> FindByQuery(string query)
		{
			return Query<Users>(SqlSelectCommand + " AND " + query);
		}
		public override IEnumerable<Users> GetAll()
		{
			return Query<Users>(SqlSelectCommand);
		}
		public override IEnumerable<Users> GetTop(int count)
		{
			return Query<Users>(string.Format("SELECT TOP {0} * FROM {1}", count, SqlTableName)).ToList();
		}
		public override int Update(Users item)
		{
			return Execute(SqlUpdateCommand, item);
		}
	}
}