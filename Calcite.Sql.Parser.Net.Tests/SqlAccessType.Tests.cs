using System;
using Xunit;
using Calcite.Sql.Parser;
using Calcite.Sql.Parser.Net.Parser;

namespace Calcite.Sql.Parser.Net.Tests
{
	public class SqlAccessTypeTests
	{
		[Fact]
		public void CheckContains()
		{
			var all = SqlAccessType.ALL;
			Assert.True(all.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.True(all.AllowsAccess(SqlAccessEnum.DELETE));

			var readOnly = SqlAccessType.READ_ONLY;
			Assert.True(readOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var writeOnly = SqlAccessType.WRITE_ONLY;
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(writeOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.DELETE));
		}

		[Fact]
		public void CheckCreate()
		{
			var all = SqlAccessType.Create(new[] { "Select", "INSERT", "update", "DeLeTe" });
			Assert.True(all.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.True(all.AllowsAccess(SqlAccessEnum.DELETE));

			var readOnly = SqlAccessType.Create(new[] { "Select" });
			Assert.True(readOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var writeOnly = SqlAccessType.Create(new[] { "INSERT" });
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(writeOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var readWriteOnly = SqlAccessType.Create(new[] { "select", "INSERT" });
			Assert.True(readWriteOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(readWriteOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(readWriteOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(readWriteOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var bad = SqlAccessType.Create(new[] { "blah", "create", "remove" });
			Assert.False(bad.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.DELETE));
		}

		[Fact]
		public void CheckCreateString()
		{
			var all = SqlAccessType.Create("Select,INSERT,update,DeLeTe");
			Assert.True(all.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.True(all.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.True(all.AllowsAccess(SqlAccessEnum.DELETE));

			var readOnly = SqlAccessType.Create("[Select]");
			Assert.True(readOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(readOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var writeOnly = SqlAccessType.Create("INSERT");
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(writeOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(writeOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var readWriteOnly = SqlAccessType.Create("select,[INSERT]");
			Assert.True(readWriteOnly.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.True(readWriteOnly.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(readWriteOnly.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(readWriteOnly.AllowsAccess(SqlAccessEnum.DELETE));

			var bad = SqlAccessType.Create("blah,create,remove");
			Assert.False(bad.AllowsAccess(SqlAccessEnum.SELECT));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.INSERT));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.UPDATE));
			Assert.False(bad.AllowsAccess(SqlAccessEnum.DELETE));
		}
	}
}
