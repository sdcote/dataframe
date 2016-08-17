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




    /// Type representing a unsigned 64-bit epoch time in milliseconds 
    public class DateType : FieldType
    {
        private const int _size = 8;

        private const string _name = "DAT";
        public const string DATEFORMAT = "{0:yyyy-MM-dd'T'HH:mm:ss.fffK}";




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
            return obj is DateTime;
        }




        public virtual object Decode(byte[] value)
        {
            return ByteUtil.RetrieveDate(value, 0);
        }




        public virtual byte[] Encode(object obj)
        {
            return ByteUtil.RenderDate((DateTime)obj);
        }




        public virtual string StringValue(byte[] val)
        {
            if (val == null || val.Length == 0)
            {
                return "";
            }
            else
            {
                object obj = Decode(val);
                if (obj != null)
                    return String.Format(DATEFORMAT, (DateTime)obj);
                else
                    return "";
            }
        }

    }

}