/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to you under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Calcite.Sql.Parser.Net.Parser
{
	/**
	 * SqlParserPos represents the position of a parsed token within SQL statement
	 * text.
	 */
	public class SqlParserPos : ISerializable, IEquatable<SqlParserPos>
	{
		/**
		 * SqlParserPos representing line one, character one. Use this if the node
		 * doesn't correspond to a position in piece of SQL text.
		 */
		public static SqlParserPos ZERO = new SqlParserPos(0, 0);

		public int LineNumber { get; private set; }
		public int ColumnNumber { get; private set; }
		public int EndLineNumber { get; private set; }
		public int EndColumnNumber { get; private set; }

		public SqlParserPos(int lineNumber, int columnNumber)
			: this(lineNumber, columnNumber, lineNumber, columnNumber)
		{ }

		public SqlParserPos(int startLineNumber, int startColumnNumber, int endLineNumber, int endColumnNumber)
		{
			LineNumber = startLineNumber;
			ColumnNumber = startColumnNumber;
			EndLineNumber = endLineNumber;
			EndColumnNumber = endColumnNumber;

			Debug.Assert(startLineNumber < endLineNumber
						|| startLineNumber == endLineNumber
						&& startColumnNumber <= endColumnNumber);
		}

		public bool Equals(SqlParserPos other)
		{
			if (other == null)
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return this.LineNumber == other.LineNumber
				&& this.ColumnNumber == other.ColumnNumber
				&& this.EndLineNumber == other.EndLineNumber
				&& this.EndColumnNumber == other.EndColumnNumber;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as SqlParserPos);
		}

		public override int GetHashCode()
		{
			return (LineNumber, ColumnNumber, EndLineNumber, EndColumnNumber).GetHashCode();
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <returns>true if this SqlParserPos is quoted.</returns>
		public virtual bool IsQuoted()
		{
			return false;
		}

		public override string ToString()
		{
			return CalciteResources.ParserContext(LineNumber, ColumnNumber);
		}


		public SqlParserPos Plus(SqlParserPos pos)
		{
			return new SqlParserPos(
				LineNumber,
				ColumnNumber,
				pos.EndLineNumber,
				pos.EndColumnNumber);
		}

		public static SqlParserPos operator +(SqlParserPos first, SqlParserPos second)
		{
			return first.Plus(second);
		}


		private class QuotedParserPos : SqlParserPos
		{
			public QuotedParserPos(int startLineNumber, int startColumnNumber, int endLineNumber, int endColumnNumber) 
				: base(startLineNumber, startColumnNumber, endLineNumber, endColumnNumber)
			{
			}

			public override bool IsQuoted()
			{
				return true;
			}
		}

	}
}
