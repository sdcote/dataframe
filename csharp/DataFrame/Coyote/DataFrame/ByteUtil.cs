#region license
// DataFrame - a data marshaling toolkit
// Copyright(c) 2006 Stephan D.Cote' - All rights reserved.
//
// This program and the accompanying materials are made available under the
// terms of the MIT License which accompanies this distribution, and is 
// available at http://creativecommons.org/licenses/MIT/
//
// Contributors:
//   Stephan D. Cote
//     - Initial API and implementation
#endregion
using System;
using System.Text;

namespace Coyote.DataFrame
{
    /// <summary>Various utilities for manipulating data at the byte level.</summary>
    /// <remarks>
    ///	<para>"Overlay" methods place data into a byte array buffer.</para>
    ///	<para>"Render" methods convert values into byte arrays</para>
    ///	<para>"Retrieve" methods remove values from byte arrays.</para>
    ///	<para>"Show" methods convert data into strings such as a binary or hex string.</para>
    ///	<para>"Dump" methods convert bytes into easily viewable partitioned data.</para>
    /// </remarks>
    public sealed class ByteUtil
    {

        #region Attributes

        ///All possible chars for representing a number as a String
        static char[] digits = {
                             '0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f',
                             'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v',
                             'w','x','y','z'
                           };

        /// <summary>
        /// The tick offset for the epoch
        /// </summary>
        public const long EPOCH_OFFSET = 621355968000000000L;

        private const string PF = "{0:D3}";

        #endregion




        #region Constructors

        ///Private constructor because everything is static
        private ByteUtil()
        {
        }

        #endregion




        #region Methods


        #region Overlaying
        /// <summary>
        /// Put a long into a byte[] buffer.
        ///	<para>This stores a binary number. Assuming that only four bytes are 
        /// written down, here are some examples of what is written:
        /// <pre>
        ///		-259 : 0xFF 0xFF 0xFE 0xFD
        ///		-258 : 0xFF 0xFF 0xFE 0xFE
        ///		-257 : 0xFF 0xFF 0xFE 0xFF
        ///		-256 : 0xFF 0xFF 0xFF 0x00
        ///		-255 : 0xFF 0xFF 0xFF 0x01
        ///		 -10 : 0xFF 0xFF 0xFF 0xF6
        ///		  -1 : 0xFF 0xFF 0xFF 0xFF
        ///		   1 : 0x00 0x00 0x00 0x01
        ///		  10 : 0x00 0x00 0x00 0x0A
        ///		 258 : 0x00 0x00 0x01 0x02
        ///		 259 : 0x00 0x00 0x01 0x03
        ///		 260 : 0x00 0x00 0x01 0x04
        ///		</pre></para>
        /// </summary>
        /// 
        /// <param name="x"></param>
        /// <param name="buf"></param>
        /// <param name="offset">at which to place the number.</param>
        public static void Overlay(long x, byte[] buf, uint offset)
        {
            buf[offset++] = (byte)(x >> 56);
            buf[offset++] = (byte)(x >> 48);
            buf[offset++] = (byte)(x >> 40);
            buf[offset++] = (byte)(x >> 32);
            buf[offset++] = (byte)(x >> 24);
            buf[offset++] = (byte)(x >> 16);
            buf[offset++] = (byte)(x >> 8);
            buf[offset++] = (byte)(x);
        }





        /// <summary>
        /// Put an unsigned integer(4-byte value) into a byte[] buffer.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="buf"></param>
        /// <param name="offset">at which to place the number.</param>
        public static void OverlayUnsignedInt(uint x, byte[] buf, uint offset)
        {
            buf[offset++] = (byte)((x >> 24) & 0xFF);
            buf[offset++] = (byte)((x >> 16) & 0xFF);
            buf[offset++] = (byte)((x >> 8) & 0xFF);
            buf[offset++] = (byte)((x >> 0) & 0xFF);
        }





        /// <summary>
        /// Put an int (4 byte value) into a byte[] buffer.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="buf"></param>
        /// <param name="offset">at which to place the number.</param>
        public static void Overlay(int x, byte[] buf, uint offset)
        {
            buf[offset++] = (byte)(x >> 24);
            buf[offset++] = (byte)(x >> 16);
            buf[offset++] = (byte)(x >> 8);
            buf[offset++] = (byte)(x);
        }




        /// <summary>
        /// Put a short (2 byte value) into a byte[] buffer.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="buf"></param>
        /// <param name="offset">at which to place the number.</param>
        public static void Overlay(short x, byte[] buf, uint offset)
        {
            buf[offset++] = (byte)(x >> 8);
            buf[offset++] = (byte)(x);
        }




        /// <summary>
        /// Put a short (2 byte value) into a byte[] buffer.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="buf"></param>
        /// <param name="offset">at which to place the number.</param>
        public static void OverlayUnsignedShort(int x, byte[] buf, uint offset)
        {
            buf[offset++] = (byte)((x >> 8) & 0xFF);
            buf[offset++] = (byte)((x >> 0) & 0xFF);
        }

        #endregion


        #region Rendering

        /// <summary>
        /// Return a byte (1 octet) from an unsigned short
        /// </summary>
        /// <param name="valu">Short 0-255 (anything higher will wrap/overflow)</param>
        /// <returns>a byte representing the short value to render</returns>
        public static byte RenderShortByte(ushort valu)
        {
            return (byte)((valu >> 0) & 0xFF);
        }




        /// <summary>
        /// Render a byte[2] value from an unsigned short (U16)
        /// </summary>
        /// <param name="valu"></param>
        /// <returns></returns>
        public static byte[] RenderUnsignedShort(ushort valu)
        {
            byte[] retval = new byte[2];
            retval[0] = (byte)((valu >> 8) & 0xFF);
            retval[1] = (byte)((valu >> 0) & 0xFF);
            return retval;
        }




        /// <summary>
        /// Return a 2-byte array from a S16 short value.
        /// <para>This encodes the short in network byte order (big-endian).</para>
        /// </summary>
        /// <param name="valu">Short from -32,768 to 32,767 to render into the 2-byte array</param>
        /// <returns>a 2-byte array representing the S16 short value</returns>
        public static byte[] RenderShort(short valu)
        {
            byte[] retval = new byte[2];

            retval[0] = (byte)((valu >> 8) & 0xFF);
            retval[1] = (byte)((valu >> 0) & 0xFF);
            return retval;
        }




        /// <summary>
        /// Return a 4-byte array representing an unsigned integer (U32) in network byte order.
        /// </summary>
        public static byte[] RenderUnsignedInt(uint valu)
        {
            byte[] retval = new byte[4];
            retval[0] = (byte)((valu >> 24) & 0xFF);
            retval[1] = (byte)((valu >> 16) & 0xFF);
            retval[2] = (byte)((valu >> 8) & 0xFF);
            retval[3] = (byte)((valu >> 0) & 0xFF);

            return retval;
        }





        /// <summary>
        /// Return a 4-byte array representing a signed integer (S32) in network byte order.
        /// </summary>
        /// <param name="valu">integer from -2,147,483,648 to 2,147,483,647 to render into the 4-byte array</param>
        public static byte[] RenderInt(int valu)
        {
            byte[] retval = new byte[4];
            retval[0] = (byte)((valu >> 24) & 0xFF);
            retval[1] = (byte)((valu >> 16) & 0xFF);
            retval[2] = (byte)((valu >> 8) & 0xFF);
            retval[3] = (byte)((valu >> 0) & 0xFF);

            return retval;
        }



        /// <summary>
        /// Return a 8-byte array representing a signed long (S64) in network byte order.
        /// </summary>
        /// <param name="valu">long from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 to render into the 8-byte array</param>
        public static byte[] RenderLong(long valu)
        {
            byte[] retval = new byte[8];

            retval[0] = (byte)((valu >> 56) & 0xFF);
            retval[1] = (byte)((valu >> 48) & 0xFF);
            retval[2] = (byte)((valu >> 40) & 0xFF);
            retval[3] = (byte)((valu >> 32) & 0xFF);
            retval[4] = (byte)((valu >> 24) & 0xFF);
            retval[5] = (byte)((valu >> 16) & 0xFF);
            retval[6] = (byte)((valu >> 8) & 0xFF);
            retval[7] = (byte)((valu >> 0) & 0xFF);

            return retval;
        }




        /// <summary>
        /// Return a 8-byte array representing an unsigned long (U64) in network byte order.
        /// </summary>
        /// <param name="valu">long from 0 to 18,446,744,073,709,551,615 to render into the 8-byte array</param>
        public static byte[] RenderUnsignedLong(ulong valu)
        {
            byte[] retval = new byte[8];
            retval[0] = (byte)((valu >> 56) & 0xFF);
            retval[1] = (byte)((valu >> 48) & 0xFF);
            retval[2] = (byte)((valu >> 40) & 0xFF);
            retval[3] = (byte)((valu >> 32) & 0xFF);
            retval[4] = (byte)((valu >> 24) & 0xFF);
            retval[5] = (byte)((valu >> 16) & 0xFF);
            retval[6] = (byte)((valu >> 8) & 0xFF);
            retval[7] = (byte)((valu >> 0) & 0xFF);

            return retval;
        }




        /// <summary>
        /// Return an 4-byte array from a float value.
        /// <para>This encodes the float in network byte order (big-endian).</para>
        /// </summary>
        /// <param name="valu">float from �1.4013e-45 to �3.4028e+38 to render into the 4-byte array</param>
        /// <returns>a 4-byte array representing the float value</returns>
        public static byte[] RenderFloat(float valu)
        {
            return RenderInt(Decimal.ToByte(Decimal.Parse(valu.ToString())));
        }




        /// <summary>
        /// Return an 8-byte array from a double precision value.
        /// <para>This encodes the long in  network byte order (big-endian).</para>
        /// </summary>
        /// <param name="valu">double precision value from �4.9406e-324 to �1.7977e+308 to render into the 8-byte array</param>
        /// <returns>a 8-byte array representing the double precision value</returns>
        public static byte[] RenderDouble(double valu)
        {
            return RenderLong((long)valu);
        }





        /// <summary>
        /// Return a byte representing a boolean value.
        /// </summary>
        /// <param name="valu">either true or false</param>
        /// <returns>a single byte representing 1 for true and 0 for false</returns>
        public static byte RenderBooleanByte(bool valu)
        {
            return (byte)(valu ? 1 : 0);
        }





        /// <summary>
        /// Return a 1-byte array representing a boolean value.
        /// </summary>
        /// <param name="arg">either true or false</param>
        /// <returns>a single byte representing 1 for true and 0 for false</returns>
        public static byte[] RenderBoolean(bool arg)
        {
            byte[] retval = new byte[1];
            retval[0] = (byte)(arg ? 1 : 0);
            return retval;
        }




        /// <summary>
        /// Return a 8-byte array representing the epoch time in milliseconds for the
        /// given date.
        /// <para>This encodes the time as the number of milliseconds since the current epoch
        /// (Brady-bunch time) as a long value in network byte order (big-endian) after
        /// adjusting the time to UTC.</para>
		/// <para>This will result in a loss of precision as .NET measures time 
		/// more finely (in ticks) than more compact epoch millisecond 
		/// timestamp. If greater precision is needed, consider using an 
		/// unsigned long.</para>
        /// </summary>
        /// <param name="date">any valid Date object</param>
        /// <returns>a 8-byte array representing the epoch time in milliseconds, if value is null, then the date will be encoded as zero</returns>
        public static byte[] RenderDate(DateTime date)
        {
            if (date.Equals(null))
            {
                return RenderLong(0);
            }
            else
            {
                // return the UTC as ticks, subtract the epoch offset and convert ticks to milliseconds
                return RenderLong((date.ToUniversalTime().Ticks - EPOCH_OFFSET) / 10000);
            }
        }




        #endregion


        #region Retrieve


        /// <summary>
        /// Get a signed short (S8) from 1 byte in a byte[] buffer.
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the unsigned short number (0 to 255) stored at the offset.</returns>
        public static short RetrieveShortByte(byte[] buf, uint offset)
        {
            return (short)buf[offset];
        }




        /// <summary>
        /// Get an unsigned short (U8) from 1 byte in a byte[] buffer.
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the unsigned short number (0 to 255) stored at the offset.</returns>
        public static ushort RetrieveUnsignedShortByte(byte[] buf, uint offset)
        {
            return (ushort)((ushort)buf[offset] & 0xFF);
        }




        /// <summary>
        /// Get a signed short (S16) from 2 bytes in a byte[] buffer.
        /// <para>This decodes the short using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed short number (-32,768 to 32,767) stored at the offset.</returns>
        public static short RetrieveShort(byte[] buf, uint offset)
        {
            //return (short)( (byte)( (short)( Convert.ToInt16( buf[offset++] & 0xff ) << 8 ) ) | Convert.ToByte( (short)buf[offset++] & 0xff ));
            return (short)((short)(((short)buf[offset++] & 0xff) << 8) | (short)((short)buf[offset++] & 0xff));
        }




        /// <summary>
        /// Get an unsigned short (U16) from 2 bytes in a byte[] buffer.
        /// <para>This decodes the short using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns> the unsigned short number (0 to 65,535) stored at the offset.</returns>
        public static int RetrieveUnsignedShort(byte[] buf, uint offset)
        {
            return (int)((int)(((int)buf[offset++] & 0xff) << 8) | (int)((int)buf[offset++] & 0xff));
        }




        /// <summary>
        /// Get a signed integer (S32) from 4 bytes in a byte[] buffer.
        /// <para>This decodes the integer using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed integer number (-2,147,483,648 to 2,147,483,647) stored at the offset.</returns>
        public static int RetrieveInt(byte[] buf, uint offset)
        {
            return (int)(((int)buf[offset++] & 0xff) << 24) |
              (int)(((int)buf[offset++] & 0xff) << 16) |
              (int)(((int)buf[offset++] & 0xff) << 8) |
              (int)((int)buf[offset++] & 0xff);
        }




        /// <summary>
        /// Get an unsigned integer (U32) from 4 bytes in a byte[] buffer.
        /// <para>This decodes the integer using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the unsigned integer number (0 to 4,294,967,295) stored at the offset.</returns>
        public static uint RetrieveUnsignedInt(byte[] buf, uint offset)
        {
            return (uint)(((uint)buf[offset++] & 0xff) << 24) |
              (uint)(((uint)buf[offset++] & 0xff) << 16) |
              (uint)(((uint)buf[offset++] & 0xff) << 8) |
              (uint)((uint)buf[offset++] & 0xff);
        }




        /// <summary>
        /// Get a signed long (S64) from 8 bytes in a byte[] buffer.
        /// <para>This decodes the long using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed integer number (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807) stored at the offset.</returns>
        public static long RetrieveLong(byte[] buf, uint offset)
        {
            return (long)(((long)buf[offset++] & 0xff) << 56) |
              (long)(((long)buf[offset++] & 0xff) << 48) |
              (long)(((long)buf[offset++] & 0xff) << 40) |
              (long)(((long)buf[offset++] & 0xff) << 32) |
              (long)(((long)buf[offset++] & 0xff) << 24) |
              (long)(((long)buf[offset++] & 0xff) << 16) |
              (long)(((long)buf[offset++] & 0xff) << 8) |
              (long)((long)buf[offset++] & 0xff);
        }




        /// <summary>
        /// Get an unsigned long (U64) from 8 bytes in a byte[] buffer.
        /// <para>This decodes the long using network byte order (big-endian).</para>
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed integer number (0 to 18,446,744,073,709,551,615) stored at the offset.</returns>
        public static ulong RetrieveUnsignedLong(byte[] buf, uint offset)
        {
            return (ulong)(((ulong)buf[offset++] & 0xff) << 56) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 48) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 40) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 32) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 24) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 16) |
              (ulong)(((ulong)buf[offset++] & 0xff) << 8) |
              (ulong)((ulong)buf[offset++] & 0xff);
        }




        /// <summary>
        /// Method RetrieveDate
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static DateTime RetrieveDate(byte[] buf, uint offset)
        {
            long ticks = (RetrieveLong(buf, offset) * 10000) + EPOCH_OFFSET;
            return new DateTime(ticks);
        }




        /// <summary>
        /// Get a floating point value from 4 bytes in a byte[] buffer.
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed floating point number (�1.4013e-45 to �3.4028e+38) stored at the offset.</returns>
        public static float RetrieveFloat(byte[] buf, uint offset)
        {
            return (float)RetrieveInt(buf, offset);
        }




        /// <summary>
        /// Get a double precision value from 8 bytes in a byte[] buffer.
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the number.</param>
        /// <returns>the signed floating point number (�4.9406e-324 to �1.7977e+308) stored at the offset.</returns>
        public static double RetrieveDouble(byte[] buf, uint offset)
        {
            return (double)RetrieveLong(buf, offset);
        }





        /// <summary>
        /// Get a boolean value from a single byte in a byte[] buffer.
        /// </summary>
        /// <param name="buf">The buffer from which to retrieve the value</param>
        /// <param name="offset">from which to get the value.</param>
        /// <returns>True if the value in the byte is > 0 false otherwise.</returns>
        public static bool RetrieveBoolean(byte[] buf, uint offset)
        {
            if ((byte)buf[offset] > 0)
            {
                return true;
            }

            return false;
        }




        /// <summary>
        /// Retrieve an ASCII encoded string from the given buffer.
        /// </summary>
        /// <param name="buf">The byte array from which we read</param>
        /// <param name="offset">The position in the byte array where the string begins.</param>
        /// <param name="length">The length of the string in bytes.</param>
        /// <returns>The specified section of the given byte array decoded in ASCII.</returns>
        public static string RetrieveAsciiString(byte[] buf, uint offset, int length)
        {
            byte[] textData = new byte[length];
            Array.Copy(buf, offset, textData, 0, textData.Length);
            return System.Text.Encoding.ASCII.GetString(textData);
        }


        #endregion


        #region Other Methods

        /// <summary>
        /// Find the first occurrence of a specified byte.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="buf"></param>
        /// <param name="offset">from which to start</param>
        /// <param name="len"></param>
        /// <returns>offset of first occurrence or -1 if not found.</returns>
        public static uint FindNextByte(byte c, byte[] buf, uint offset, uint len)
        {
            long rc = -1;

            if (buf != null)
            {
                rc = offset;

                while ((rc < len) && (buf[rc] != c))
                {
                    ++rc;
                }

                if (rc >= len)
                {
                    rc = -1;
                }
            }

            return (uint)rc;
        }





        /// <summary>
        /// Find the first occurrence of a specified pair of bytes.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="buf"></param>
        /// <param name="offset">from which to start</param>
        /// <param name="len"></param>
        /// <returns>offset of first occurrence or -1 if not found.</returns>
        public static uint FindNextTwoBytes(byte c1, byte c2, byte[] buf, uint offset, int len)
        {
            long rc = -1;

            if (buf != null)
            {
                rc = offset + 1;

                while ((rc < len) && (buf[rc] != c2) && (buf[rc - 1] != c1))
                {
                    ++rc;
                }

                if (rc >= len)
                {
                    rc = -1;
                }
            }

            return (uint)rc;
        }





        /// <summary>
        /// Convert a byte to a character representation.
        /// <para>Sorta misnamed as this method will return the character repersentation
        /// of the given byte in the current / default encoding for the locale.</para>
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static char ByteToASCII(byte b)
        {
            if ((b < 32) || (b > 126))
            {
                return ' ';
            }
            else
            {
                return (char)b;
            }
        }




        /// <summary>
        /// Convert a byte to a 2-character string representing its hexadecimal value,
        /// with leading zero if needed.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ByteToHex(byte b)
        {
            return Show(b, 4);
        }




        /// <summary>
        /// Convert a byte array to a string of 2-character hexadecimal values, with
        /// leading zero if needed.
        /// </summary>
        /// <param name="barray"></param>
        /// <returns></returns>
        public static string BytesToHex(byte[] barray)
        {
            if (barray == null)
            {
                return null;
            }

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < barray.Length; i++)
            {
                result.Append(Show((byte)barray[i], 4));
            }

            return result.ToString().ToUpper();
        }




        /// <summary>
        /// Convert the entered number into a string. This works based upon number of bits.
        ///   
        /// <para>This can be used to produce many standard outputs.
        /// <pre>
        ///   1 bit  = binary, base two
        ///   2 bits = base four
        ///   3 bits = octal, base eight
        ///   4 bits = hex, base sixteen
        ///   5 bits = base 32
        ///  </pre>
        ///  Leading digits are included.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num_bits"></param>
        /// <returns></returns>
        public static string Show(long i, int num_bits)
        {
            int num_chars = 64 / num_bits;

            if ((64 % num_bits) == 0)
            {
                --num_chars;
            }

            char[] buf = new char[num_chars + 1];
            int radix = 1 << num_bits;
            long mask = radix - 1;

            for (int charPos = num_chars; charPos >= 0; --charPos)
            {
                buf[charPos] = digits[(int)(i & mask)];

                i >>= num_bits;
            }

            return new string(buf);
        }




        /// <summary>
        /// Convert the entered number into a string. This works based upon number of bits.
        ///   
        /// <para>This can be used to produce many standard outputs.
        ///  <pre>
        ///    1 bit  = binary, base two
        ///    2 bits = base four
        ///    3 bits = octal, base eight
        ///    4 bits = hex, base sixteen
        ///    5 bits = base 32
        ///  </pre>
        ///  Leading digits are included.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num_bits"></param>
        /// <returns></returns>
        public static string Show(int i, int num_bits)
        {
            int num_chars = 32 / num_bits;

            if (32 % num_bits == 0)
            {
                --num_chars;
            }

            char[] buf = new char[num_chars + 1];
            int radix = 1 << num_bits;
            long mask = radix - 1;

            for (int charPos = num_chars; charPos >= 0; --charPos)
            {
                buf[charPos] = digits[(int)(i & mask)];

                i >>= num_bits;
            }

            return new string(buf);
        }




        /// <summary>
        /// Convert the entered number into a string. This works based upon number of bits.
        ///   
        ///   <para>This can be used to produce many standard outputs.
        ///   <pre>
        ///      1 bit  = binary, base two
        ///      2 bits = base four
        ///      3 bits = octal, base eight
        ///      4 bits = hex, base sixteen
        ///      5 bits = base 32
        ///    </pre>
        ///   Leading digits are included.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num_bits"></param>
        /// <returns></returns>
        public static string Show(short i, int num_bits)
        {
            int num_chars = 16 / num_bits;

            if (16 % num_bits == 0)
            {
                --num_chars;
            }

            char[] buf = new char[num_chars + 1];
            int radix = 1 << num_bits;
            long mask = radix - 1;

            for (int charPos = num_chars; charPos >= 0; --charPos)
            {
                buf[charPos] = digits[(int)(i & mask)];

                i >>= num_bits;
            }

            return new string(buf);
        }




        /// <summary>
        /// Convert the entered number into a string.
        ///  
        ///   <para>This works based upon number of bits. This can be used to produce many
        ///   standard outputs.
        ///   <pre>
        ///     1 bit  = binary, base two
        ///     2 bits = base four
        ///     3 bits = octal, base eight
        ///     4 bits = hex, base sixteen
        ///     5 bits = base 32
        ///   </pre>
        ///   Leading digits are included.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="num_bits"></param>
        /// <returns></returns>
        public static string Show(byte i, int num_bits)
        {
            int num_chars = 8 / num_bits;

            if (8 % num_bits == 0)
            {
                --num_chars;
            }

            char[] buf = new char[num_chars + 1];

            int radix = 1 << num_bits;
            long mask = radix - 1;

            for (int charPos = num_chars; charPos >= 0; --charPos)
            {
                buf[charPos] = digits[(int)(i & mask)];

                i >>= num_bits;
            }

            return new string(buf);
        }




        /// <summary>
        /// Return this byte as a binary string.
        /// </summary>
        /// <param name="octet"></param>
        /// <returns></returns>
        public static string Show(byte octet)
        {
            return Show((byte)octet, 1);
        }




        /// <summary>
        /// This will dump one long into a string for examination.
        /// Dump produces three lines of output including an index, binary, decimal, hex, and character output.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Dump(long data)
        {
            byte[] temp = new byte[8];
            Overlay(data, temp, 0);
            return Dump(temp);
        }




        /// <summary>
        /// This will dump one integer into a string for examination.
        /// <para>Dump produces three lines of output including an index, binary,
        /// decimal, hex, and character output.</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Dump(int data)
        {
            byte[] temp = new byte[4];
            Overlay(data, temp, 0);
            return Dump(temp);
        }




        /// <summary>
        /// This will dump one short into a string for examination.
        /// <para>Dump produces three lines of output including an index, binary,
        /// decimal, hex, and character output.</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Dump(short data)
        {
            byte[] temp = new byte[2];
            Overlay(data, temp, 0);
            return Dump(temp);
        }




        /// <summary>
        /// This will dump one byte into a string for examination
        /// <para>The byte will be compartmentalized where the cell contains the offset
        ///	(always zero) into the byte (first line), the binary representation of
        ///	the byte (second line), and the decimal, hex, and corresponding character
        ///	(third line).</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Dump(byte data)
        {
            byte[] temp = new byte[1];
            temp[0] = data;
            return Dump(temp);
        }




        /// <summary>
        /// This will dump up to 255 bytes of an array into a string for examination.
        /// <para>The individual bytes will be compartmentalized where each cell contains
        /// the offset into the byte array (first line), the binary representation of
        /// the byte (second line), and the decimal, hex, and corresponding character
        /// (third line).</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Dump(byte[] data)
        {
            if ((data == null) || (data.Length == 0))
            {
                return "";
            }

            return Dump(data, (data.Length > 256) ? 256 : data.Length);
        }




        /// <summary>
        /// This will dump bytes of an array into a string for examination.
        /// <para>The individual bytes will be compartmentalized where each cell contains
        /// the offset into the byte array (first line), the binary representation of
        /// the byte (second line), and the decimal, hex, and corresponding character
        /// (third line).</para>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string Dump(byte[] data, int size)
        {
            // First, a little error checking
            if ((data == null) || (data.Length == 0))
            {
                return "";
            }

            if (data.Length < size)
            {
                size = data.Length;
            }

            StringBuilder result = new StringBuilder();
            StringBuilder line1 = new StringBuilder("+");
            StringBuilder line2 = new StringBuilder("|");
            StringBuilder line3 = new StringBuilder("|");
            StringBuilder line4 = new StringBuilder("+");
            int mark = 0;

            while (mark < size)
            {
                // Get the unsigned value of this byte
                int val = (data[mark] & 0xFF);

                // Print the little box that represents this byte
                line1.Append(string.Format(PF, mark) + ":" + ByteToHex((byte)mark) + "--+");
                line2.Append(Show(data[mark]) + "|");
                line3.Append(string.Format(PF, val) + ":" + ByteToHex(data[mark]) + ":" + ByteToASCII(data[mark]) + "|");
                line4.Append("--------+");

                mark++;

                if ((mark > 0) && ((mark % 8) == 0))
                {
                    line1.Append("\r\n");
                    line2.Append("\r\n");
                    line3.Append("\r\n");
                    line4.Append("\r\n");
                    result.Append(line1.ToString() + line2.ToString() + line3.ToString() + line4.ToString());
                    line1.Remove(0, line1.Length);
                    line2.Remove(0, line2.Length);
                    line3.Remove(0, line3.Length);
                    line4.Remove(0, line4.Length);

                    if (mark < size)
                    {
                        line1.Append("+");
                        line2.Append("|");
                        line3.Append("|");
                        line4.Append("+");
                    }
                }
            }

            if (line1.Length > 0)
            {
                line1.Append("\r\n");
                line2.Append("\r\n");
                line3.Append("\r\n");
                result.Append(line1.ToString() + line2.ToString() + line3.ToString() + line4.ToString());
            }

            return result.ToString();
        }




        /// <summary>
        /// Converts a long to a little-endian four-byte array
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] IntToLittleEndian(int val)
        {
            byte[] b = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                b[i] = (byte)(val % 256);

                val = val / 256;
            }

            return b;
        }




        /// <summary>
        /// Converts a long to a little-endian two-byte array
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte[] ShortToLittleEndian(short val)
        {
            byte[] b = new byte[2];

            for (int i = 0; i < 2; i++)
            {
                b[i] = (byte)(val % 256);

                val = (short)(val / 256);
            }

            return b;
        }




        /// <summary>
        /// Converts a little-endian four-byte array to a long, represented as a
        /// double, since long is signed.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Vax_to_long(byte[] b)
        {
            return FixByte(b[0]) + (FixByte(b[1]) * 256) + (FixByte(b[2]) * (256 ^ 2)) + (FixByte(b[3]) * (256 ^ 3));
        }




        /// <summary>
        /// Converts a little-endian four-byte array to a short, represented as an int,
        /// since short is signed.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int vax_to_short(byte[] b)
        {
            return (int)(FixByte(b[0]) + (FixByte(b[1]) * 256));
        }




        /// <summary>
        /// bytes are signed; let's fix them
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static short FixByte(byte b)
        {
            if (b < 0)
            {
                return (short)(b + 256);
            }

            return b;
        }




        /// <summary>
        /// Method ToBase64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToBase64(byte[] data)
        {

            int i = data.Length / 3;
            int j = data.Length % 3;
            byte[] barray = new byte[((j == 0) ? i : i + 1) * 4];
            int k = 0;
            int l = 0;

            for (int i1 = 0; i1 < i; i1++)
            {
                barray[k++] = ToBase64((byte)(data[l] >> 2 & 0x3f));

                barray[k++] = ToBase64((byte)((data[l] & 3) << 4 | (data[l + 1] & 0xf0) >> 4 & 0xf));
                barray[k++] = ToBase64((byte)((data[l + 1] & 0xf) << 2 | (data[l + 2] & 0xc0) >> 6 & 3));
                barray[k++] = ToBase64((byte)(data[l + 2] & 0x3f));
                l += 3;
            }

            if (j == 1)
            {
                barray[k++] = ToBase64((byte)(data[l] >> 2 & 0x3f));

                barray[k++] = ToBase64((byte)((data[l] & 3) << 4));
                barray[k++] = 61;
                barray[k++] = 61;
            }
            else
            {
                if (j == 2)
                {
                    barray[k++] = ToBase64((byte)(data[l] >> 2 & 0x3f));

                    barray[k++] = ToBase64((byte)((data[l] & 3) << 4 | (data[l + 1] & 0xf0) >> 4 & 0xf));
                    barray[k++] = ToBase64((byte)((data[l + 1] & 0xf) << 2));
                    barray[k++] = 61;
                }
            }

            return Encoding.ASCII.GetString(barray);
        }




        /// <summary>
        /// Method GetBase64Length
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetBase64Length(string str)
        {
            int len = (str.Length / 4) * 3;

            if ((str.Length > 0) && (str[str.Length - 1] == '='))
            {
                len -= (str[str.Length - 2] != '=') ? 1 : 2;
            }

            return len;
        }




        /// <summary>
        ///  Method FromBase64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] FromBase64(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);

            int i = data.Length / 4;
            int j = i * 3;
            byte octet = 0;

            if ((data.Length > 0) && (data[data.Length - 1] == 61))
            {
                octet = ((byte)((data[data.Length - 2] != 61) ? 1 : 2));

                j -= octet;

                i--;
            }

            byte[] barray = new byte[j];
            int k = 0;
            int l = 0;

            for (int i1 = 0; i1 < i; i1++)
            {
                byte byte3 = FromBase64(data[l++]);

                byte byte6 = FromBase64(data[l++]);
                byte byte8 = FromBase64(data[l++]);
                byte byte9 = FromBase64(data[l++]);
                barray[k++] = (byte)(byte3 << 2 | (byte6 & 0x30) >> 4);
                barray[k++] = (byte)((byte6 & 0xf) << 4 | (byte8 & 0x3c) >> 2);
                barray[k++] = (byte)((byte8 & 3) << 6 | byte9 & 0x3f);
            }

            if (octet == 1)
            {
                byte byte1 = FromBase64(data[l++]);

                byte byte4 = FromBase64(data[l++]);
                byte byte7 = FromBase64(data[l++]);
                barray[k++] = (byte)(byte1 << 2 | (byte4 & 0x30) >> 4);
                barray[k++] = (byte)((byte4 & 0xf) << 4 | (byte7 & 0x3c) >> 2);
            }
            else
            {
                if (octet == 2)
                {
                    byte byte2 = FromBase64(data[l++]);

                    byte byte5 = FromBase64(data[l++]);
                    barray[k++] = (byte)(byte2 << 2 | (byte5 & 0x30) >> 4);
                }
            }

            return barray;
        }




        /// <summary>
        /// Method ToBase64
        /// </summary>
        /// <param name="octet"></param>
        /// <returns></returns>
        public static byte ToBase64(byte octet)
        {
            if (octet <= 25)
            {
                return (byte)(65 + octet);
            }

            if (octet <= 51)
            {
                return (byte)((97 + octet) - 26);
            }

            if (octet <= 61)
            {
                return (byte)((48 + octet) - 52);
            }

            if (octet == 62)
            {
                return 43;
            }

            return ((byte)((octet != 63) ? 61 : 47));
        }




        /// <summary>
        /// Method FromBase64
        /// </summary>
        /// <param name="octet"></param>
        /// <returns></returns>
        public static byte FromBase64(byte octet)
        {
            if ((octet >= 65) && (octet <= 90))
            {
                return (byte)(octet - 65);
            }

            if ((octet >= 97) && (octet <= 122))
            {
                return (byte)((26 + octet) - 97);
            }

            if ((octet >= 48) && (octet <= 57))
            {
                return (byte)((52 + octet) - 48);
            }

            if (octet == 43)
            {
                return 62;
            }

            return ((byte)((octet != 47) ? 64 : 63));
        }




        /// <summary>
        /// Swap bytes
        /// </summary>
        /// <param name="s"></param>
        /// <returns>The byte swapped version of <code>s</code>.</returns>
        public static short SwapBytes(short s)
        {
            return (short)((s << 8) | ((s >> 8) & 0x00ff));
        }




        /// <summary>
        /// Swap bytes
        /// </summary>
        /// <param name="i"></param>
        /// <returns>The byte swapped version of <code>i</code>.</returns>
        public static int SwapBytes(int i)
        {
            return (i << 24) | ((i << 8) & 0x00ff0000) | (i >> 24) | ((i >> 8) & 0x0000ff00);
        }




        /// <summary>
        /// Works like String.Substring()
        /// </summary>
        /// <param name="source">Source of the byte array</param>
        /// <param name="start">starting byte</param>
        /// <param name="length">length of the array to return</param>
        /// <returns></returns>
        public static byte[] SubArray(byte[] source, int start, int length)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("Start index: " + start);
            }

            if (source == null)
            {
                throw new ArgumentException("Source array was null");
            }

            if ((start + length) > source.Length)
            {
                throw new ArgumentOutOfRangeException("length index: " + length);
            }

            byte[] retval = new byte[length];
            Array.Copy(source, start, retval, 0, length);
            return retval;
        }




        /// <summary>
        /// Method HexToBytes
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string hex)
        {
            byte[] retval = null;

            if ((hex != null) && (hex.Length > 0))
            {
                retval = new byte[(hex.Length / 2) + (hex.Length % 2)];

                for (int i = 0; i < hex.Length; i++)
                {
                    if (i + 1 < hex.Length)
                    {
                        retval[(i + 1) / 2] = (byte)Int32.Parse(hex.Substring(i, i + 2), System.Globalization.NumberStyles.HexNumber);

                        i++;
                    }
                    else
                    {
                        retval[(i + 1) / 2] = (byte)Int32.Parse(hex.Substring(i), System.Globalization.NumberStyles.HexNumber);
                    }
                }
            }

            return retval;
        }




        /// <summary>reverse byte order (16-bit)</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt16 ReverseBytes(UInt16 value)
        {
            return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }




        /// <summary>reverse byte order (32-bit)</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }




        /// <summary>reverse byte order (64-bit)</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 ReverseBytes(UInt64 value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }
        #endregion


        #endregion
    }
}
