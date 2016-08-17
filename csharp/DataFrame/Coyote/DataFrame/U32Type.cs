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



    /// <summary>This is a type equivalent to the <code>uint</code> type, representing an unsigned, 32-bit value in the range of 0 to 4,294,967,295 </summary>
    public class U32Type : FieldType
    {
        private const int _size = 4;

        private const string _name = "U32";



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
            return (obj is uint);
        }




        public virtual object Decode(byte[] value)
        {
            return ByteUtil.RetrieveUnsignedInt(value, 0);
        }




        public virtual byte[] Encode(object obj)
        {
            return ByteUtil.RenderUnsignedInt((uint)obj);
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