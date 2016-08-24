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



    /// This is an Integer, representing a signed, 32-bit value in the range of -2,147,483,648 to 2,147,483,647 
    public class S32Type : FieldType {
        private const int _size = 4;

        private const string _name = "S32";



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
            return obj is int;
        }




        public virtual object Decode( byte[] @value ) {
            return ByteUtil.RetrieveInt( @value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            return ByteUtil.RenderInt( (int)obj );
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