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



    /// Type representing a 32-bit floating point value in the range of +/-1.4013e-45 to +/-3.4028e+38. 
    public class FloatType : FieldType
    {
        private const int _size = 4;

        private const string _name = "FLT";


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
            return obj is float;
        }




        public virtual object Decode(byte[] value)
        {
            return BitConverter.ToSingle(value, 0);
        }




        public virtual byte[] Encode(object obj)
        {
            return BitConverter.GetBytes((float)obj);
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