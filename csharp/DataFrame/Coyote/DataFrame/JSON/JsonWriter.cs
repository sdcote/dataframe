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
    

    public class JsonWriter
    {
        public readonly TextWriter writer;

        private const int CONTROL_CHARACTERS_END = 0x001f;

        private static readonly char[] QUOT_CHARS = { '\\', '"' };
        private static readonly char[] BS_CHARS = { '\\', '\\' };
        private static readonly char[] LF_CHARS = { '\\', 'n' };
        private static readonly char[] CR_CHARS = { '\\', 'r' };
        private static readonly char[] TAB_CHARS = { '\\', 't' };
        private static readonly char[] UNICODE_2028_CHARS = { '\\', 'u', '2', '0', '2', '8' };
        private static readonly char[] UNICODE_2029_CHARS = { '\\', 'u', '2', '0', '2', '9' };
        private static readonly char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };




        private static char[] getReplacementChars(char ch)
        {
            if (ch > '\\')
            {
                if ((ch < '\u2028') || (ch > '\u2029'))
                {
                    // The lower range contains 'a' .. 'z'. Only 2 checks required.
                    return null;
                }
                return ch == '\u2028' ? UNICODE_2028_CHARS : UNICODE_2029_CHARS;
            }
            if (ch == '\\')
            {
                return BS_CHARS;
            }
            if (ch > '"')
            {
                // This range contains '0' .. '9' and 'A' .. 'Z'. Need 3 checks to get here.
                return null;
            }
            if (ch == '"')
            {
                return QUOT_CHARS;
            }
            if (ch > CONTROL_CHARACTERS_END)
            {
                return null;
            }
            if (ch == '\n')
            {
                return LF_CHARS;
            }
            if (ch == '\r')
            {
                return CR_CHARS;
            }
            if (ch == '\t')
            {
                return TAB_CHARS;
            }
            return new char[] { '\\', 'u', '0', '0', HEX_DIGITS[(ch >> 4) & 0x000f], HEX_DIGITS[ch & 0x000f] };
        }






        internal JsonWriter(TextWriter writer)
        {
            this.writer = writer;
        }




        public virtual void writeArrayClose()
        {
            writer.Write(']');
        }




        public virtual void writeArrayOpen()
        {
            writer.Write('[');
        }




        public virtual void writeArraySeparator()
        {
            writer.Write(',');
        }



        
        /// <summary>
        /// Write the string replacing any non-standard characters as necessary.
        /// </summary>
        /// <param name="str"></param>
        public virtual void writeJsonString(string str)
        {
             int length = str.Length;
            int Start = 0;
            for (int index = 0; index < length; index++)
            {
                 char[] replacement = getReplacementChars(str[index]);
                if (replacement != null)
                {
                    writer.Write(str, Start, index - Start);
                    writer.Write(replacement);
                    Start = index + 1;
                }
            }
            writer.Write(str, Start, length - Start);
        }



        
        /// <summary>
        /// Writes the string to the underlying writer with no additional formatting.
        /// </summary>
        /// <param name="value">the value to write</param>
        public virtual void writeLiteral(string @value)
        {
            writer.Write(@value);
        }




        public virtual void writeMemberName(string name)
        {
            writer.Write('"');
            writeJsonString(name);
            writer.Write('"');
        }




        public virtual void writeMemberSeparator()
        {
            writer.Write(':');
        }



        
        /// <summary>
        /// Writes the number to the underlying writer with no additional formatting.
        /// </summary>
        /// <param name="value">the value to write</param>
        /// <exception cref="IOException">If writing encountered an error</exception>
        public virtual void writeNumber(string @value)
        {
            writer.Write(@value);
        }




        public virtual void writeObjectClose()
        {
            writer.Write('}');
        }




        public virtual void writeObjectOpen()
        {
            writer.Write('{');
        }




        public virtual void writeObjectSeparator()
        {
            writer.Write(',');
        }




        public virtual void writeString(string @string)
        {
            writer.Write('"');
            writeJsonString(@string);
            writer.Write('"');
        }

    }

}