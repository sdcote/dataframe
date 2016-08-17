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



    ///
    // * Type representing a Uniform Resource Identifier
    // 
    public class UriType : FieldType
    {
        /// negative size indicates a variable length value is to be expected. 
        private const int _size = -1;

        private const string _name = "URI";

        /// US standard default encoding, also known as Latin-1 
        public static Encoding DEFAULT_ENCODING = Encoding.GetEncoding("iso-8859-1");
        protected internal static Encoding strEnc = UriType.DEFAULT_ENCODING;



        // setup the string encoding of field names
        static UriType()
        {
            // Make this configurable...ConfigurationManager
            //try{
            //    DataField.DEFAULT_ENCODING = System.getProperty("field.encoding", DataField.ENC_UTF8);
            //} catch (Exception _ex) {
            strEnc = DataField.DEFAULT_ENCODING;
            //}
        }




        public virtual string TypeName
        {
            get
            {
                return _name;
            }
        }




        public virtual bool IsNumeric
        {
            get
            {
                return false;
            }
        }




        public virtual int Size
        {
            get
            {
                return _size;
            }
        }

        public virtual bool CheckType(object obj)
        {
            return (obj is Uri);
        }




        public virtual object Decode(byte[] value)
        {
            return new Uri(StringType.strEnc.GetString(value));
        }



        public virtual byte[] Encode(object obj)
        {
            return StringType.strEnc.GetBytes(((Uri)obj).ToString());
        }




        public virtual string StringValue(byte[] val)
        {
            object obj = Decode(val);
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }

    }

}