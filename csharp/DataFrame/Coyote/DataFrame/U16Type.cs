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

namespace Coyote.DataFrame {



    /// <summary>This is a numeric type equivalent to the <code>ushort</code> type, representing an unsigned, 16-bit value in the range of 0 to 65,535 </summary>
    public class U16Type : FieldType {
        private const int _size = 2;

        private const string _name = "U16";


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
            return (obj is ushort);
        }




        public virtual object Decode( byte[] value ) {
            return ByteUtil.RetrieveUnsignedShort( value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            return ByteUtil.RenderUnsignedShort( (ushort)obj );
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