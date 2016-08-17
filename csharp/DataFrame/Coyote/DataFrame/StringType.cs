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
using System.Text;

namespace Coyote.DataFrame
{



    /// <summary>
    /// Type representing a string of characters.
    /// </summary>
    public class StringType : FieldType
    {
        /// negative size indicates a variable length value is to be expected. 
        private const int _size = -1;

        private const string _name = "STR";

        /// US standard default encoding, also known as Latin-1 
        public static Encoding DEFAULT_ENCODING = Encoding.GetEncoding("iso-8859-1");
        protected internal static Encoding strEnc = UriType.DEFAULT_ENCODING;


        // setup the string encoding of field names
        static StringType()
        {
            // Make this configurable...ConfigurationManager
            //try{
            //    DataField.DEFAULT_ENCODING = System.getProperty("field.encoding", DataField.ENC_UTF8);
            //} catch (Exception _ex) {
            strEnc = DataField.DEFAULT_ENCODING;
            //}
        }






        public int Size
        {
            get { return _size; }

        }




        public string TypeName
        {
            get { return _name; }
        }



        public virtual bool CheckType(object obj)
        {
            return (obj is string);
        }




        public virtual object Decode(byte[] @value)
        {
            return StringType.strEnc.GetString(@value);
        }




        public virtual byte[] Encode(object obj)
        {
            return StringType.strEnc.GetBytes((string)obj);
        }




        public virtual bool IsNumeric
        {
            get
            {
                return false;
            }
        }



        public string StringValue(byte[] val)
        {
            if (val == null)
            {
                return "";
            }
            else
            {
                object obj = Decode(val);
                if (obj != null)
                    return obj.ToString();
                else
                    return "";
            }
        }

    }

}