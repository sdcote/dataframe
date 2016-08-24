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
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.Generic;
namespace Coyote.DataFrame {


    /// <summary>
    /// Type representing an ordered array of values.
    /// </summary>
    /// <remarks>
    /// <para>The current design involves encoding a name, type (array), length (number of elements) and a set of Type, Length, Value (TLV) triplets for each array element.</para>
    /// </remarks>
    public class ArrayType : FieldType {

        private static readonly object[] EMPTY_ARRAY = new object[0];

        private const int _size = -1;

        private const string _name = "ARY";


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
            return obj is object[];
        }




        public virtual object Decode( byte[] value ) {
            object[] retval = EMPTY_ARRAY;
            List<object> elements = new List<object>();
            byte type = 0;
            byte[] data = null;
            FieldType datatype = null;

            if ( value != null ) {

                try {
                    using ( MemoryStream stream = new MemoryStream( value ) ) {
                        using ( BinaryReader reader = new BinaryReader( stream ) ) {
                            while ( reader.BaseStream.Position != reader.BaseStream.Length ) {
                                // the next field we read is the data type
                                type = reader.ReadByte();

                                // get the proper field type
                                try {
                                    datatype = DataField.GetDataType( type );
                                } catch ( System.Exception ) {
                                    throw new IOException( "non supported type: '" + type + "'" );
                                }

                                // if the file type is a variable length (i.e. size < 0), read in the length
                                if ( datatype.Size < 0 ) {

                                    // Read the next 4 bytes
                                    byte[] u32 = reader.ReadBytes( 4 );

                                    // ...and convert it to a length using big-endian encoding
                                    uint length = ByteUtil.RetrieveUnsignedInt( u32, 0 );

                                    if ( length < 0 ) {
                                        throw new IOException( "read length bad value: length = " + length + " type = " + type );
                                    }

                                    data = new byte[length];

                                    if ( length > 0 ) {
                                        reader.Read( data, 0, data.Length );
                                    }
                                } else {
                                    data = new byte[datatype.Size];
                                    reader.Read( data, 0, data.Length );
                                }

                                // now get the object value of the data
                                elements.Add( datatype.Decode( data ) );

                            } // while there is data available to read

                            retval = elements.ToArray();

                        }
                    }
                } catch ( Exception e ) {
                    throw new ArgumentException( "Could not decode value", e );
                }

            }

            return retval;

        }




        public virtual byte[] Encode( object obj ) {
            object[] ary = (object[])obj;

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter( ms );


            for ( int x = 0; x < ary.Length; x++ ) {
                try {
                    // This will throw an exception for any unsupported data types.
                    byte tipe = DataField.GetType( ary[x] );
                    byte[] data = DataField.Encode( ary[x], tipe );
                    int size = DataField.GetDataType( tipe ).Size;

                    // Write the type field
                    bw.Write( tipe );

                    if ( data != null ) {
                        // If the value is variable in length
                        if ( size < 0 ) {
                            // write the length as a u32 like ither variable field length types
                            bw.Write( ByteUtil.RenderUnsignedInt( (uint)data.Length ) );
                        }

                        // write the value itself
                        bw.Write( data );
                    } else {
                        bw.Write( 0 ); // null value
                    }
                } catch ( System.Exception ) {
                    //System.err.println("Array object of type " + ary[x].getClass().getSimpleName() + " is not supported in DataFrames");
                    // just skip the offending object and add the rest.
                }
            } // for each

            return ms.ToArray();
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