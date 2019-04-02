using Calcite.Sql.Parser.Net.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calcite.Sql.Parser.Net
{
	public abstract class SqlNode
	{
		public static readonly SqlNode[] EMPTY_ARRAY = new SqlNode[0];

		protected SqlParserPos pos;

		protected SqlNode(SqlParserPos pos)
		{
			this.pos = pos ?? throw new NullReferenceException();
		}

		/**
		 * Returns the type of node this is, or
		 * <see cref="SqlKind.OTHER"/> if it's nothing special.
		 *
		 * <value see cref="SqlKind"/> 
		 * <seealso cref="IsA(HashSet{SqlKind})"/>
		 */
		public SqlKind Kind => SqlKind.OTHER;

		/**
		 * Returns whether this node is a member of an aggregate category.
		 *
		 * <p>For example, {@code node.isA(SqlKind.QUERY)} returns {@code true}
		 * if the node is a SELECT, INSERT, UPDATE etc.
		 *
		 * <p>This method is shorthand: {@code node.isA(category)} is always
		 * equivalent to {@code node.getKind().belongsTo(category)}.
		 *
		 * @param category Category
		 * @return Whether this node belongs to the given category.
		 */
		public bool IsA(ICollection<SqlKind> category)
		{
			return Kind.BelongsTo(category);
		}

		/// <summary>
		///  Clones a SqlNode with a different position.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public abstract SqlNode Clone(SqlParserPos pos);

		/** Creates a copy of a SqlNode. */
		public static E Clone<E>(E e) where E : SqlNode
		{
			//noinspection unchecked
			return (E)e.Clone(e.pos);
		}

		public SqlParserPos GetParserPosition()
		{
			return pos;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
