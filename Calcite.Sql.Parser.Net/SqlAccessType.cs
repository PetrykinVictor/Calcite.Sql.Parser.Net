using System;

namespace Calcite.Sql.Parser.Net
{
	public class SqlAccessType
	{
		public static SqlAccessType ALL = new SqlAccessType(SqlAccessEnum.SELECT | SqlAccessEnum.UPDATE | SqlAccessEnum.INSERT | SqlAccessEnum.DELETE);
		public static SqlAccessType READ_ONLY = new SqlAccessType(SqlAccessEnum.SELECT);
		public static SqlAccessType WRITE_ONLY = new SqlAccessType(SqlAccessEnum.INSERT);

		private readonly SqlAccessEnum accessTypes;
		
		public SqlAccessType(SqlAccessEnum accessTypes)
		{
			this.accessTypes = accessTypes;
		}

		public bool AllowsAccess(SqlAccessEnum access)
		{
			return (accessTypes & access) == access;
		}

		public override string ToString()
		{
			return accessTypes.ToString();
		}

		public static SqlAccessType Create(string[] accessNames)
		{
			var access = SqlAccessEnum.NONE;
			foreach (string accessName in accessNames)
			{
				if (Enum.TryParse(accessName, true, out SqlAccessEnum accessValue))
					access = access | accessValue;
			}
			
			return new SqlAccessType(access);
		}

		public static SqlAccessType Create(string accessString)
		{
			accessString = accessString.Replace('[', ' ');
			accessString = accessString.Replace(']', ' ');
			string[] accessNames = accessString.Split(',');
			return Create(accessNames);
		}
	}
}
