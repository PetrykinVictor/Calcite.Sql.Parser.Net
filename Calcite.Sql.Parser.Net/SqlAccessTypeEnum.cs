using System;

namespace Calcite.Sql.Parser.Net
{
	/// <summary>
	/// Enumeration representing different access types
	/// </summary>
	[Flags]
	public enum SqlAccessEnum
	{
		NONE = 0,
		SELECT = 1,
		UPDATE = 2,
		INSERT = 4,
		DELETE = 8
	}
}
