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



    /// <summary>
    /// Type code representing a 64-bit floating point value in the range of +/-4.9406e-324 to +/-1.7977e+308. 
    /// </summary>
    public class DoubleType : FieldType {
        private const int _size = 8;

        private const string _name = "DBL";


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
            return obj is double;
        }




        public virtual object Decode( byte[] value ) {
            return BitConverter.ToDouble( value, 0 );
        }




        public virtual byte[] Encode( object obj ) {
            return BitConverter.GetBytes( (double)obj );
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