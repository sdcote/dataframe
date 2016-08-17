#region license
// Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
// 
// This program and the accompanying materials are made available under the 
// terms of the MIT License which accompanies this distribution, and is 
// available at http://creativecommons.org/licenses/MIT/
// *
// Contributors:
//   Stephan D. Cote 
//      - Initial API and implementation
#endregion
using System;
using System.IO;

namespace Coyote.DataFrame.JSON
{



    internal class FormattedJsonWriter : JsonWriter
	{

	  private readonly char[] indentChars = { ' ', ' ' };
	  private int indent;




	  internal FormattedJsonWriter(TextWriter writer) : base(writer)
	  {
	  }




	  public override void writeArrayClose()
	  {
		indent--;
		writeNewLine();
		writer.Write(']');
	  }




	  public override void writeArrayOpen()
	  {
		indent++;
		writer.Write('[');
		writeNewLine();
	  }




	  public override void writeArraySeparator()
	  {
		writer.Write(',');
		writeNewLine();
	  }




	  public override void writeMemberSeparator()
	  {
		writer.Write(':');
		writer.Write(' ');
	  }




	  private void writeNewLine()
	  {
		writer.Write('\n');
		for (int i = 0; i < indent; i++)
		{
		  writer.Write(indentChars);
		}
	  }




	  public override void writeObjectClose()
	  {
		indent--;
		writeNewLine();
		writer.Write('}');
	  }




	  public override void writeObjectOpen()
	  {
		indent++;
		writer.Write('{');
		writeNewLine();
	  }




	  public override void writeObjectSeparator()
	  {
		writer.Write(',');
		writeNewLine();
	  }

	}

}