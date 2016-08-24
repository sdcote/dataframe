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
using System.Collections.Generic;

namespace Coyote.DataFrame {



    /// <summary>
    /// Type representing a boolean value 
    /// </summary>
    public class BooleanType : FieldType {
        private const int _size = 1;

        private const string _name = "BOL";


        public virtual string TypeName {
            get {
                return _name;
            }
        }




        public virtual bool IsNumeric {
            get {
                return false;
            }
        }




        public virtual int Size {
            get {
                return _size;
            }
        }




        public virtual bool CheckType( object obj ) {
            return obj is bool;
        }




        public virtual object Decode( byte[] value ) {
            return ByteUtil.RetrieveBoolean( value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            return ByteUtil.RenderBoolean( (bool)obj );
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