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
namespace Coyote.DataFrame {



    /// <summary>This a numeric type equivalent to a <code>byte</code>; representing an unsigned, 8-bit value in the range of 0 to 255.</summary>
    public class U8Type : FieldType {
        private const int _size = 1;

        private const string _name = "U8";




        public virtual string TypeName {
            get {
                return _name;
            }
        }




        public virtual bool IsNumeric {
            get {
                return true;
            }
        }




        public virtual int Size {
            get {
                return _size;
            }
        }



        public virtual bool CheckType( object obj ) {
            return (obj is byte);
        }




        public virtual object Decode( byte[] value ) {
            return ByteUtil.RetrieveUnsignedByte( value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            byte[] retval = new byte[1];
            retval[0] = (byte)obj;
            return retval;
        }




        public virtual string StringValue( byte[] val ) {
            if ( val == null ) {
                return "";
            } else {
                object obj = Decode( val );
                if ( obj != null )
                    return obj.ToString();
                else
                    return "";
            }
        }

    }

}