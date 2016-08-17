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



    /// <summary>This is a numeric type equivalent to the <code>sbyte</code>type, representing an signed, 8-bit value in the range of -128 to 127.</summary>
    public class S8Type : FieldType
    {
        private const int _size = 1;

        private const string _name = "S8";



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
                return true;
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
            return obj is sbyte;
        }




        public virtual object Decode(byte[] value)
        {
            return (sbyte)value[0];
        }




        public virtual byte[] Encode(object obj)
        {
            byte[] retval = new byte[1];
            retval[0] = (byte)obj;
            return retval;
        }





        public virtual string StringValue(byte[] val)
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