using Calcite.Sql.Parser.Net.Util;
using System;

namespace Calcite.Sql.Parser.Net.Parser
{
	class SqlParseException : Exception, ICalciteParserException
	{
		private SqlParserPos pos;
		public int[][] ExpectedTokenSequences { get; private set; }
		public string[] TokenImages { get; private set; }
			   
		[NonSerialized]
		private readonly Exception parserException;


		//override Exception
		public override Exception GetBaseException()
		{
			return parserException;
		}
	}
}
