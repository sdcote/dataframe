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
using System.Collections.Generic;

using Coyote.DataFrame;


namespace Coyote.DataFrame.JSON
{




    /// <summary>
    /// Parse JSON text into DataFrames.
    /// </summary>
    public class JsonFrameParser
    {

        private const int MIN_BUFFER_SIZE = 10;
        private const int DEFAULT_BUFFER_SIZE = 1024;

        private readonly TextReader reader;
        private readonly char[] buffer;
        private int bufferOffset;

        private int fill;
        private int line;
        private int lineOffset;

        // where we are in the current buffer
        private int index;

        // The current character under consideration
        private int current;

        // What has been captured so far
        private System.Text.StringBuilder captureBuffer;

        // index into the current buffer where we start capturing our value
        private int captureStart;




        public JsonFrameParser(TextReader reader) : this(reader, DEFAULT_BUFFER_SIZE)
        {
        }




        public JsonFrameParser(TextReader reader, int buffersize)
        {
            this.reader = reader;
            buffer = new char[buffersize];
            line = 1;
            captureStart = -1;
        }




        public JsonFrameParser(string json) : this(new StringReader(json), Math.Max(MIN_BUFFER_SIZE, Math.Min(DEFAULT_BUFFER_SIZE, json.Length)))
        {
        }



        /// <summary>
        /// Stop capturing the string value from the buffer and return what has been captured so far.
        /// </summary>
        /// <remarks><para>Essentially, return the string from the point we started capturing to the character prior to the one we just read in.</para></remarks>
        /// <returns>the string in the buffer from the point of capture to now.</returns>
        private string endCapture()
        {
            int end = current == -1 ? index : index - 1;
            string captured;
            if (captureBuffer.Length > 0)
            {
                captureBuffer.Append(buffer, captureStart, end - captureStart);
                captured = captureBuffer.ToString();
                captureBuffer.Length = 0;
            }
            else
            {
                captured = new string(buffer, captureStart, end - captureStart);
            }
            captureStart = -1;
            return captured;
        }




        /// <summary>
        /// Generate a parse exception with the given message.
        /// </summary>
        /// <param name="message">The message to place in the exception</param>
        /// <returns>a parse exception with the given message.</returns>
        private ParseException error(string message)
        {
            int absIndex = bufferOffset + index;
            int column = absIndex - lineOffset;
            int offset = isEndOfText() ? absIndex : absIndex - 1;
            return new ParseException(message, offset, line, column, current);
        }




        private ParseException expected(string expected)
        {
            if (isEndOfText())
            {
                return error("Unexpected end of input");
            }
            return error("Expected " + expected);
        }




        private bool isDigit()
        {
            return (current >= '0') && (current <= '9');
        }




        private bool isEndOfText()
        {
            return current == -1;
        }




        private bool isHexDigit()
        {
            return ((current >= '0') && (current <= '9')) || ((current >= 'a') && (current <= 'f')) || ((current >= 'A') && (current <= 'F'));
        }




        private bool isWhiteSpace()
        {
            return (current == ' ') || (current == '\t') || (current == '\n') || (current == '\r');
        }



        /// <summary>
        /// Parse the data and return a list of DataFrames containing the data.
        /// </summary>
        /// <remarks>
        /// <para>Normally, there will only be one root value, but some applications may have data which represents multiple arrays or objects. This method will continue parsing until all objects (or arrays) are consumed.</para>
        /// </remarks>
        /// <returns>The data represented by the currently set string as one or more DataFrames</returns>
        public virtual List<DataFrame> Parse()
        {
            List<DataFrame> retval = new List<DataFrame>();
            read();
            skipWhiteSpace();

            while ((current == '{') || (current == '['))
            {
                retval.Add(readRootValue());
                skipWhiteSpace();
            }

            if (!isEndOfText())
            {
                throw error("Unexpected character");
            }
            return retval;

        }




        private void pauseCapture()
        {
            int end = current == -1 ? index : index - 1;
            captureBuffer.Append(buffer, captureStart, end - captureStart);
            captureStart = -1;
        }




        private void read()
        {
            if (index == fill)
            {
                if (captureStart != -1)
                {
                    captureBuffer.Append(buffer, captureStart, fill - captureStart);
                    captureStart = 0;
                }
                bufferOffset += fill;
                fill = reader.Read(buffer, 0, buffer.Length);
                index = 0;
                if (fill == -1)
                {
                    current = -1;
                    return;
                }
            }
            if (current == '\n')
            {
                line++;
                lineOffset = bufferOffset + index;
            }
            current = buffer[index++];
        }




        private DataFrame readArray()
        {
            read();
            DataFrame array = new DataFrame();
            skipWhiteSpace();
            if (readChar(']'))
            {
                return array;
            }
            do
            {
                skipWhiteSpace();
                DataField field = readFieldValue(null);
                array.Add(field);
                skipWhiteSpace();
            } while (readChar(','));
            if (!readChar(']'))
            {
                throw expected("',' or ']'");
            }
            return array;
        }




        private bool readChar(char ch)
        {
            if (current != ch)
            {
                return false;
            }
            read();
            return true;
        }




        private bool readDigit()
        {
            if (!isDigit())
            {
                return false;
            }
            read();
            return true;
        }




        private void readEscape()
        {
            read();
            switch (current)
            {
                case '"':
                case '/':
                case '\\':
                    captureBuffer.Append((char)current);
                    break;
                case 'b':
                    captureBuffer.Append('\b');
                    break;
                case 'f':
                    captureBuffer.Append('\f');
                    break;
                case 'n':
                    captureBuffer.Append('\n');
                    break;
                case 'r':
                    captureBuffer.Append('\r');
                    break;
                case 't':
                    captureBuffer.Append('\t');
                    break;
                case 'u':
                    char[] hexChars = new char[4];
                    for (int i = 0; i < 4; i++)
                    {
                        read();
                        if (!isHexDigit())
                        {
                            throw expected("hexadecimal digit");
                        }
                        hexChars[i] = (char)current;
                    }
                    captureBuffer.Append((char)Convert.ToInt32(new string(hexChars), 16));
                    break;
                default:
                    throw expected("valid escape sequence");
            }
            read();
        }




        private bool readExponent()
        {
            if (!readChar('e') && !readChar('E'))
            {
                return false;
            }
            if (!readChar('+'))
            {
                readChar('-');
            }
            if (!readDigit())
            {
                throw expected("digit");
            }
            while (readDigit())
            {
            }
            return true;
        }




        private bool readFalse()
        {
            read();
            readRequiredChar('a');
            readRequiredChar('l');
            readRequiredChar('s');
            readRequiredChar('e');
            return false;
        }




        private DataField readFieldValue(string name)
        {
            switch (current)
            {
                case 'n':
                    return new DataField(name, readNull());
                case 't':
                    return new DataField(name, readTrue());
                case 'f':
                    return new DataField(name, readFalse());
                case '"':
                    return new DataField(name, readString());
                case '[':
                    return new DataField(name, readArray());
                case '{':
                    return new DataField(name, readObject());
                case ']':
                case ',':
                    return new DataField(name, null);
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return new DataField(name, readNumber());
                default:
                    throw expected("value");
            }
        }




        private bool readFraction()
        {
            if (!readChar('.'))
            {
                return false;
            }
            if (!readDigit())
            {
                throw expected("digit");
            }
            while (readDigit())
            {
            }
            return true;
        }




        /// <summary>
        /// Read a quoted string.
        /// </summary>
        /// <returns>The value within the quotes</returns>
        /// <exception cref="IOException">if not in quotes</exception>
        private string readName()
        {
            if (current != '"')
            {
                throw expected("name");
            }
            return readStringInternal();
        }




        private object readNull()
        {
            read();
            readRequiredChar('u');
            readRequiredChar('l');
            readRequiredChar('l');
            return null;
        }




        private object readNumber()
        {
            startCapture();
            readChar('-');
            int firstDigit = current;
            if (!readDigit())
            {
                throw expected("digit");
            }
            if (firstDigit != '0')
            {
                while (readDigit())
                {
                }
            }
            bool isFraction = readFraction();
            readExponent();

            string @value = endCapture();
            // TODO: support more types like exponents
            if (isFraction)
            {
                try
                {
                    return Convert.ToDouble(@value);
                }
                catch (Exception)
                {
                    // Ignore...just return the string if all else fails
                }
            }
            else
            {
                try
                {
                    return Convert.ToInt64(@value);
                }
                catch (Exception)
                {
                    // Ignore...just return the string if all else fails
                }
            }
            return @value; // for now, just return it as a string
        }




        /// <summary>
        /// Read the JSON object into a dataFrame
        /// </summary>
        /// <returns>A dataframe containing the JSON object</returns>
        /// <exception cref="IOException"></exception>
        private DataFrame readObject()
        {
            read();
            DataFrame @object = new DataFrame();
            skipWhiteSpace();
            if (readChar('}'))
            {
                return @object; // return an empty frame
            }

            do
            {
                // try to read the name
                skipWhiteSpace();
                string name = readName();
                skipWhiteSpace();
                if (!readChar(':'))
                {
                    throw expected("':'");
                }
                // next, read the value for this named field
                skipWhiteSpace();
                DataField @value = readFieldValue(name);
                @object.Add(@value);
                skipWhiteSpace();
            } while (readChar(','));
            if (!readChar('}'))
            {
                throw expected("',' or '}'");
            }
            return @object;
        }




        private void readRequiredChar(char ch)
        {
            if (!readChar(ch))
            {
                throw expected("'" + ch + "'");
            }
        }




        private DataFrame readRootValue()
        {
            switch (current)
            {
                case 'n':
                    return new DataFrame(new DataField(readNull()));
                case 't':
                    return new DataFrame(new DataField(readTrue()));
                case 'f':
                    return new DataFrame(new DataField(readFalse()));
                case '"':
                    return new DataFrame(new DataField(readString()));
                case '[':
                    return readArray();
                case '{':
                    return readObject();
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return new DataFrame(new DataField(readNumber()));
                default:
                    throw expected("value");
            }
        }




        private string readString()
        {
            return readStringInternal();
        }




        private string readStringInternal()
        {
            read();
            startCapture();
            while (current != '"')
            {
                if (current == '\\')
                {
                    pauseCapture();
                    readEscape();
                    startCapture();
                }
                else if (current < 0x20)
                {
                    throw expected("valid string character");
                }
                else
                {
                    read();
                }
            }
            string @string = endCapture();
            read();
            return @string;
        }




        private bool readTrue()
        {
            read();
            readRequiredChar('r');
            readRequiredChar('u');
            readRequiredChar('e');
            return true;
        }




        /// <summary>
        /// Consume all whitespace.
        /// </summary>
        private void skipWhiteSpace()
        {
            while (isWhiteSpace())
            {
                read();
            }
        }




        private void startCapture()
        {
            if (captureBuffer == null)
            {
                captureBuffer = new System.Text.StringBuilder();
            }
            captureStart = index - 1;
        }

    }

}