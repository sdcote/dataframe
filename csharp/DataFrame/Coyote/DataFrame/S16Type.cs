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



    /// This is a Short; a signed, 16-bit value in the range of -32,768 to 32,767 
    public class S16Type : FieldType
    {
        private const int _size = 2;

        private const string _name = "S16";




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
            return (obj is short);
        }




        public virtual object Decode(byte[] @value)
        {
            return ByteUtil.RetrieveShort(@value, 0);
        }




        public virtual byte[] Encode(object obj)
        {
            return ByteUtil.RenderShort((short)obj);
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