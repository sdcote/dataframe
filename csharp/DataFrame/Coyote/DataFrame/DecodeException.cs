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
    public class DecodeException : SystemException {


        private readonly int position;
        private readonly int previous;
        private readonly int fieldIndex;
        private readonly DataField field;
        private readonly byte[] bytes;

        private const string MESSGE = "Decode exception";



        public DecodeException() : this( MESSGE, null, true, true, 0, -1, -1, null, null ) {
        }




        public DecodeException( string message ) : this( message, null, true, true, 0, -1, -1, null, null ) {
        }




        public DecodeException( System.Exception cause ) : this( MESSGE, cause, true, true, 0, -1, -1, null, null ) {
        }




        public DecodeException( string message, System.Exception cause ) : this( message, cause, true, true, 0, -1, -1, null, null ) {
        }




        public DecodeException( string message, byte[] bytes ) : this( message, null, true, true, 0, -1, -1, null, bytes ) {
        }




        public DecodeException( string message, System.Exception cause, bool enableSuppression, bool writableStackTrace ) : this( message, cause, enableSuppression, writableStackTrace, 0, -1, -1, null, null ) {
        }




        public DecodeException( string message, System.Exception cause, int pos, int prev, int indx, DataField fld ) : this( message, cause, true, true, pos, prev, indx, fld, null ) {
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="enableSuppression"></param>
        /// <param name="writableStackTrace"></param>
        /// <param name="pos"></param>
        /// <param name="prev"></param>
        /// <param name="indx"></param>
        /// <param name="fld"></param>
        /// <param name="data"></param>
        public DecodeException( string message, System.Exception cause, bool enableSuppression, bool writableStackTrace, int pos, int prev, int indx, DataField fld, byte[] data ) : base( message, cause ) {
            position = pos;
            previous = prev;
            fieldIndex = indx;
            field = fld;
            bytes = data;
        }


        public override string Message {
            get {
                StringBuilder b = new StringBuilder();
                b.Append( base.Message );
                b.Append( " - pos:" );
                b.Append( position );
                b.Append( " prev:" );
                b.Append( previous );
                b.Append( " fld#:" );
                b.Append( fieldIndex );
                return b.ToString();
            }
        }

        /// <summary>
        /// The offset in the stream where the error occurred, more accurately, the start of reading the field where the error occurred
        /// </summary>
        public virtual int Position {
            get {
                return position;
            }
        }



        /// <summary>
        /// Location of the last correctly decoded field in the stream
        /// </summary>
        public virtual int PreviousPosition {
            get {
                return previous;
            }
        }




        /// <summary>
        /// Current field index
        /// </summary>
        public virtual int FieldIndex {
            get {
                return fieldIndex;
            }
        }




        /// <summary>
        /// The last field correctly decoded
        /// </summary>
        public virtual DataField LastField {
            get {
                return field;
            }
        }





        public virtual byte[] Bytes {
            get {
                return bytes;
            }
        }

    }

}