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
namespace Coyote.DataFrame
{

    /// <summary>
    /// An unchecked exception to indicate that an input does not qualify as valid.
    /// </summary>
    public class ParseException : SystemException
    {

        private readonly int offset;
        private readonly int line;
        private readonly int column;
        private readonly int character;




        public ParseException(string message, int offset, int line, int column, int character) : base(message + " at line:" + line + " column:" + column + " offset:" + offset + " char: '" + (char)character + "' (" + character + ")")
        {
            this.offset = offset;
            this.line = line;
            this.column = column;
            this.character = character;
        }



        /// <summary>
        /// The integer value of the character which caused the error. This is usually the 'current' character the parser is considering.
        /// </summary>
        public int Character { get { return character; } }




        /// <summary>
        /// The index of the character at which the error occurred, relative to the line. The index of the first character of a line is 1.
        /// </summary>
        public int Column { get { return column; } }


        

        /// <summary>
        /// The number of the line in which the error occurred. The first line counts as 1.
        /// </summary>
        public int Line { get { return line; } }




        /// <summary>
        /// The absolute index of the character at which the error occurred. The index of the first character of a document is 0.
        /// </summary>
        public int Offset { get { return offset; } }

    }
}