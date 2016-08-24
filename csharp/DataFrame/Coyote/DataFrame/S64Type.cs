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



    /// Type representing an signed, 64-bit value in the range of -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 
    public class S64Type : FieldType {
        private const int _size = 8;

        private const string _name = "S64";



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
            return obj is long;
        }




        public virtual object Decode( byte[] value ) {
            return ByteUtil.RetrieveLong( value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            return ByteUtil.RenderLong( (long)obj );
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