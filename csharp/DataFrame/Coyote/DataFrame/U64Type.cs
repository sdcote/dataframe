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




    ///<summary>This is a type equivalent to a <code>ulong</code>; representing an unsigned, 64-bit value in the range of 0 to 18,446,744,073,709,551,615 </summary> 
    public class U64Type : FieldType
    {
        private const int _size = 8;

        private const string _name = "U64";




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
            return (obj is ulong);
        }




        public virtual object Decode(byte[] value)
        {
            return ByteUtil.RetrieveUnsignedLong(value, 0);
        }




        public virtual byte[] Encode(object obj)
        {
            return ByteUtil.RenderUnsignedLong(((ulong)obj));

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