using Xunit;
using Calcite.Sql.Parser.Net.Parser;

namespace Calcite.Sql.Parser.Net.Tests
{
	public class SqlParserPosTests
	{
		[Fact]
		public void Test1()
		{
			var zeroPos = SqlParserPos.ZERO;			

			var largePos = new SqlParserPos(9876, 231);
			
			var smallPos = new SqlParserPos(4, 4);

			var plus = smallPos + largePos;

			var lessOrEqual = SqlKind.LESS_THAN_OR_EQUAL;
			var name = lessOrEqual.GetName();
			Assert.True(name == "<=");
		}
	}
}
