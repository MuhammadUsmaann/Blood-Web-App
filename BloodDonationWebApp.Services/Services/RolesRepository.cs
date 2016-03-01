
using BloodDonationWebApp.Database;using BloodDonationWebApp.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BloodDonationWebApp.Services
{
	public partial class RolesRepository : IRepository<Roles>
	{
		private const string SqlTableName = "Roles";
		private const string SqlSelectCommand = " SELECT * FROM " + SqlTableName + " Where isActive = 1 ";
		private const string SqlInsertCommand = " INSERT INTO " + SqlTableName + " ( Description, Code) OUTPUT Inserted.RoleID Values(@Description, @Code)";
		private const string SqlUpdateCommand = " UPDATE " + SqlTableName + " Set Description = @Description, Code = @Code where ( RoleID = @RoleID ) AND  isActive = 1 ";
		private const string SqlDeleteCommand = " Update " + SqlTableName + " Set isActive = 0 where ( RoleID = @RoleID ) AND isActive = 1 ";

		public override int Insert(Roles model)
		{
			return Query<int>(SqlInsertCommand, model).Single();
		}
		public override int Remove(int id)
		{
			return Execute(SqlDeleteCommand, new { RoleID = id });
		}
		public override  Roles FindByID(int id)
		{
			return Query<Roles>(SqlSelectCommand + " AND RoleID = @RoleID ", new { RoleID = id }).FirstOrDefault();
		}
		public override IEnumerable<Roles> FindByQuery(string query)
		{
			return Query<Roles>(SqlSelectCommand + " AND " + query);
		}
		public override IEnumerable<Roles> GetAll()
		{
			return Query<Roles>(SqlSelectCommand);
		}
		public override IEnumerable<Roles> GetTop(int count)
		{
			return Query<Roles>(string.Format("SELECT TOP {0} * FROM {1}", count, SqlTableName)).ToList();
		}
		public override int Update(Roles item)
		{
			return Execute(SqlUpdateCommand, item);
		}
	}
}