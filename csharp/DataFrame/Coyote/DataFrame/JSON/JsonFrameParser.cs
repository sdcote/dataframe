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


namespace Coyote.DataFrame.JSON {




    /// <summary>
    /// Parse JSON text into DataFrames.
    /// </summary>
    public class JsonFrameParser {

        private const int MIN_BUFFER_SIZE = 10;
        private const int DEFAULT_BUFFER_SIZE = 1024;

        /// <summary>The reader used to acquire text from the outside world.</summary>
        private readonly TextReader reader;

        /// <summary>The current buffer of characters</summary>
        private readonly char[] buffer;
        /// <summary>The current position in the buffer.</summary>
        private int bufferOffset;

        private int fill;
        private int line;
        private int lineOffset;

        /// <summary>Where we are in the current buffer.</summary>
        private int index;

        /// <summary>The current character under consideration.</summary>
        private int current;

        /// <summary>What has been captured so far.</summary>
        private System.Text.StringBuilder captureBuffer;

        /// <summary>Index into the current buffer where we start capturing our value.</summary>
        private int captureStart;




        public JsonFrameParser( TextReader reader ) : this( reader, DEFAULT_BUFFER_SIZE ) {
        }




        public JsonFrameParser( TextReader reader, int buffersize ) {
            this.reader = reader;
            buffer = new char[buffersize];
            line = 1;
            captureStart = -1;
        }




        public JsonFrameParser( string json ) : this( new StringReader( json ), Math.Max( MIN_BUFFER_SIZE, Math.Min( DEFAULT_BUFFER_SIZE, json.Length ) ) ) {
        }



        /// <summary>
        /// Stop capturing the string value from the buffer and return what has been captured so far.
        /// </summary>
        /// <remarks><para>Essentially, return the string from the point we started capturing to the character prior to the one we just read in.</para></remarks>
        /// <returns>the string in the buffer from the point of capture to now.</returns>
        private string EndCapture() {
            int end = current == -1 ? index : index - 1;
            string captured;
            if ( captureBuffer.Length > 0 ) {
                captureBuffer.Append( buffer, captureStart, end - captureStart );
                captured = captureBuffer.ToString();
                captureBuffer.Length = 0;
            } else {
                captured = new string( buffer, captureStart, end - captureStart );
            }
            captureStart = -1;
            return captured;
        }




        /// <summary>
        /// Generate a parse exception with the given message.
        /// </summary>
        /// <param name="message">The message to place in the exception</param>
        /// <returns>a parse exception with the given message.</returns>
        private ParseException Error( string message ) {
            int absIndex = bufferOffset + index;
            int column = absIndex - lineOffset;
            int offset = IsEndOfText() ? absIndex : absIndex - 1;
            return new ParseException( message, offset, line, column, current );
        }




        private ParseException Expected( string expected ) {
            if ( IsEndOfText() ) {
                return Error( "Unexpected end of input" );
            }
            return Error( "Expected " + expected );
        }




        private bool IsDigit() {
            return (current >= '0') && (current <= '9');
        }




        private bool IsEndOfText() {
            return current == -1;
        }




        private bool IsHexDigit() {
            return ((current >= '0') && (current <= '9')) || ((current >= 'a') && (current <= 'f')) || ((current >= 'A') && (current <= 'F'));
        }



        private bool IsWhiteSpace() {
            return (current == ' ') || (current == '\t') || (current == '\n') || (current == '\r');
        }



        /// <summary>
        /// Parse the data and return a list of DataFrames containing the data.
        /// </summary>
        /// <remarks>
        /// <para>Normally, there will only be one root value, but some applications may have data which represents multiple arrays or objects. This method will continue parsing until all objects (or arrays) are consumed.</para>
        /// </remarks>
        /// <returns>The data represented by the currently set string as one or more DataFrames</returns>
        public virtual List<DataFrame> Parse() {
            List<DataFrame> retval = new List<DataFrame>();
            Read();
            SkipWhiteSpace();

            while ( (current == '{') || (current == '[') ) {
                retval.Add( ReadRootValue() );
                SkipWhiteSpace();
            }

            if ( !IsEndOfText() ) {
                throw Error( "Unexpected character" );
            }
            return retval;

        }




        private void PauseCapture() {
            int end = current == -1 ? index : index - 1;
            captureBuffer.Append( buffer, captureStart, end - captureStart );
            captureStart = -1;
        }



        /// <summary>
        /// Read the next character
        /// </summary>
        /// <remarks><para>This actually just increments the index into the current buffer. If the index is at the end of the current buffer, a new "chunk" of data is read into the buffer from the reader and the index is set to the beginning of the buffer.</para></remarks>
        private void Read() {
            if ( index == fill ) {
                if ( captureStart != -1 ) {
                    captureBuffer.Append( buffer, captureStart, fill - captureStart );
                    captureStart = 0;
                }
                bufferOffset += fill;
                fill = reader.Read( buffer, 0, buffer.Length );
                index = 0;

                // check to see if we are at the end of the data
                // -1 is definitely EOF
                // 0 means we did not read anything into our buffer
                if ( fill <= 0 ) {
                    current = -1;
                    return;
                }
            }

            // if the current character is a new-line, 
            if ( current == '\n' ) {
                line++; // increment the line counter value
                lineOffset = bufferOffset + index;  // reset the line offset value
            }

            // set the current character to the current position in the buffer
            current = buffer[index++];
        }




        private DataFrame ReadArray() {
            Read(); // read past the '['
            DataFrame array = new DataFrame();
            SkipWhiteSpace();

            // if the next character is the array close, return the empty frame
            if ( CurrentCharacterIs( ']' ) ) {
                return array;
            }

            // Loop through all the values
            do {
                SkipWhiteSpace();
                DataField field = ReadFieldValue( null );
                array.Add( field );
                SkipWhiteSpace();
            } while ( CurrentCharacterIs( ',' ) );
            if ( !CurrentCharacterIs( ']' ) ) {
                throw Expected( "',' or ']'" );
            }
            return array;
        }



        /// <summary>
        /// This method is a sentinel check method designed to be used in loops.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>false if the character does not match, returns true after reading past it if it does.</returns>
        private bool CurrentCharacterIs( char ch ) {
            if ( current != ch ) {
                return false;
            }
            Read();
            return true;
        }




        private bool ReadDigit() {
            if ( !IsDigit() ) {
                return false;
            }
            Read();
            return true;
        }



        /// <summary>Read in an escaped character, placing the appropriate character in the capture buffer.</summary>
        private void ReadEscape() {
            Read();
            switch ( current ) {
                case '"':
                case '/':
                case '\\':
                    captureBuffer.Append( (char)current );
                    break;
                case 'b':
                    captureBuffer.Append( '\b' );
                    break;
                case 'f':
                    captureBuffer.Append( '\f' );
                    break;
                case 'n':
                    captureBuffer.Append( '\n' );
                    break;
                case 'r':
                    captureBuffer.Append( '\r' );
                    break;
                case 't':
                    captureBuffer.Append( '\t' );
                    break;
                case 'u':
                    char[] hexChars = new char[4];
                    for ( int i = 0; i < 4; i++ ) {
                        Read();
                        if ( !IsHexDigit() ) {
                            throw Expected( "hexadecimal digit" );
                        }
                        hexChars[i] = (char)current;
                    }
                    captureBuffer.Append( (char)Convert.ToInt32( new string( hexChars ), 16 ) );
                    break;
                default:
                    throw Expected( "valid escape sequence" );
            }
            Read();
        }




        private bool ReadExponent() {
            if ( !CurrentCharacterIs( 'e' ) && !CurrentCharacterIs( 'E' ) ) {
                return false;
            }
            if ( !CurrentCharacterIs( '+' ) ) {
                CurrentCharacterIs( '-' );
            }
            if ( !ReadDigit() ) {
                throw Expected( "digit" );
            }
            while ( ReadDigit() ) {
            }
            return true;
        }




        private bool ReadFalse() {
            Read();
            ReadRequiredChar( 'a' );
            ReadRequiredChar( 'l' );
            ReadRequiredChar( 's' );
            ReadRequiredChar( 'e' );
            return false;
        }



        /// <summary>
        /// Read the value into a DataField with the given name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <returns>A DataField with the given name containing the value read in from the reader.</returns>
        private DataField ReadFieldValue( string name ) {
            switch ( current ) {
                case 'n':
                    return new DataField( name, ReadNull() );
                case 't':
                    return new DataField( name, ReadTrue() );
                case 'f':
                    return new DataField( name, ReadFalse() );
                case '"':
                    return new DataField( name, ReadString() );
                case '[':
                    return new DataField( name, ReadArray() );
                case '{':
                    return new DataField( name, ReadObject() );
                case ']':
                case ',':
                    return new DataField( name, null );
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
                    return new DataField( name, ReadNumber() );
                default:
                    throw Expected( "value" );
            }
        }




        private bool ReadFraction() {
            if ( !CurrentCharacterIs( '.' ) ) {
                return false;
            }
            if ( !ReadDigit() ) {
                throw Expected( "digit" );
            }
            while ( ReadDigit() ) {
            }
            return true;
        }




        /// <summary>
        /// Read a quoted string.
        /// </summary>
        /// <returns>The value within the quotes</returns>
        /// <exception cref="IOException">if not in quotes</exception>
        private string ReadName() {
            if ( current != '"' ) {
                throw Expected( "attribute name" );
            }
            return ReadString();
        }




        private object ReadNull() {
            Read();
            ReadRequiredChar( 'u' );
            ReadRequiredChar( 'l' );
            ReadRequiredChar( 'l' );
            return null;
        }




        private object ReadNumber() {
            StartCapture();

            // if the current character is a negative sign, read past it adding it to the capture buffer
            CurrentCharacterIs( '-' );

            int firstDigit = current;
            if ( !ReadDigit() ) {
                throw Expected( "digit" );
            }
            if ( firstDigit != '0' ) {
                while ( ReadDigit() ) {
                    //keep reading into the capture buffer while the current character is a digit
                }
            }

            // we may have stopped because we reached a decimal point
            bool isFraction = ReadFraction();

            // read any exponent portion of the number (there may nt be anything to read)
            ReadExponent();

            // stop the capture and get the read-in text
            string retval = EndCapture();

            // if we found a decimal point...
            if ( isFraction ) {
                // convert the string into a double (DBL)
                try {
                    return Convert.ToDouble( retval );
                } catch ( Exception ) {
                    // Ignore...just return the string if all else fails
                }
            } else {
                // convert it into an integer (S64)
                try {
                    return Convert.ToInt64( retval );
                } catch ( Exception ) {
                    // Ignore...just return the string if all else fails
                }
            }

            // if we got here, the number could not be parsed...return it as a string (STR)
            return retval;
        }




        /// <summary>
        /// Read the JSON object into a dataFrame
        /// </summary>
        /// <returns>A dataframe containing the JSON object</returns>
        /// <exception cref="IOException"></exception>
        private DataFrame ReadObject() {
            Read();
            DataFrame @object = new DataFrame();
            SkipWhiteSpace();
            if ( CurrentCharacterIs( '}' ) ) {
                return @object; // return an empty frame
            }

            do {
                // try to read the name
                SkipWhiteSpace();
                string name = ReadName();
                SkipWhiteSpace();
                if ( !CurrentCharacterIs( ':' ) ) {
                    throw Expected( "':'" );
                }
                // next, read the value for this named field
                SkipWhiteSpace();
                DataField @value = ReadFieldValue( name );
                @object.Add( @value );
                SkipWhiteSpace();
            } while ( CurrentCharacterIs( ',' ) );
            if ( !CurrentCharacterIs( '}' ) ) {
                throw Expected( "',' or '}'" );
            }
            return @object;
        }



        /// <summary>
        /// Read the next character and throw an exception if it is not what is expected.
        /// </summary>
        /// <param name="expectedCharacter">The required/expected character.</param>
        private void ReadRequiredChar( char expectedCharacter ) {
            if ( !CurrentCharacterIs( expectedCharacter ) ) {
                throw Expected( "'" + expectedCharacter + "'" );
            }
        }



        /// <summary>
        /// Read the value into a dataframe.
        /// </summary>
        /// <returns></returns>
        private DataFrame ReadRootValue() {
            switch ( current ) {
                case 'n':
                    return new DataFrame( new DataField( ReadNull() ) );
                case 't':
                    return new DataFrame( new DataField( ReadTrue() ) );
                case 'f':
                    return new DataFrame( new DataField( ReadFalse() ) );
                case '"':
                    return new DataFrame( new DataField( ReadString() ) );
                case '[':
                    return ReadArray();
                case '{':
                    return ReadObject();
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
                    return new DataFrame( new DataField( ReadNumber() ) );
                default:
                    throw Expected( "value" );
            }
        }




        private string ReadString() {
            Read(); // read past quote
            StartCapture();

            // keep reading until the next doube-quote character
            while ( current != '"' ) {

                // check for the escape character
                if ( current == '\\' ) {
                    // pause capturing read-in characters
                    PauseCapture();
                    // place the escaped character in the buffer
                    ReadEscape();
                    // resume capturing read-in characters
                    StartCapture();
                } else if ( current < 0x20 ) {
                    throw Expected( "valid string character" );
                } else {
                    Read(); // read the next character
                }
            } // while not string delimiter

            // End the capture and get the captured string
            string retval = EndCapture();

            // read past the closing quotes
            Read();

            // return what was captured
            return retval;
        }




        private bool ReadTrue() {
            Read();
            ReadRequiredChar( 'r' );
            ReadRequiredChar( 'u' );
            ReadRequiredChar( 'e' );
            return true;
        }




        /// <summary>
        /// Consume all whitespace.
        /// </summary>
        private void SkipWhiteSpace() {
            while ( IsWhiteSpace() ) {
                Read();
            }
        }




        private void StartCapture() {
            if ( captureBuffer == null ) {
                captureBuffer = new System.Text.StringBuilder();
            }
            captureStart = index - 1;
        }

    }

}