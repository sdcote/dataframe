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
using System.Text;

namespace Coyote.DataFrame {


    public class ByteArrayType : FieldType {

        /// negative size indicates a variable length value is to be expected. 
        private const int _size = -1;

        private const string _name = "BYTE";




        public virtual bool CheckType( object obj ) {
            return (obj is sbyte[]);
        }




        public virtual byte[] Encode( object obj ) {
            return (byte[])obj;
        }




        public virtual object Decode( byte[] @value ) {
            return @value;
        }




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




        public virtual string StringValue( byte[] val ) {
            object obj = Decode( val );
            if ( obj != null )
                return obj.ToString();
            else
                return "";
        }

    }

}