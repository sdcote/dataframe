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

    /// <summary>
    /// Type representing an undefined value.
    /// </summary>
    public class UndefinedType : FieldType {

        private static readonly byte[] NULLVALUE = new byte[0];

        private const string _name = "UDEF";




        /// <summary>
        /// zero size implies null type
        /// </summary>
        public virtual int Size {
            get {
                return 0;
            }
        }




        public virtual bool CheckType( object obj ) {
            if ( obj == null )
                return true;
            else
                return false;
        }




        public virtual byte[] Encode( object obj ) {
            return NULLVALUE;
        }




        public virtual object Decode( byte[] @value ) {
            return null;
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

        

      
        public string StringValue( byte[] val ) {
            object obj = Decode( val );
            if ( obj != null )
                return obj.ToString();
            else
                return "";
        }




    }

}