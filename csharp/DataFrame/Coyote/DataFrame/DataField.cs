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
using System.Text;

using System.Collections.Generic;

namespace Coyote.DataFrame {

    /// <summary>
    /// Type-Length-Value (TLV) data structure. 
    /// </summary>
    /// <remarks>
    /// <para>This is an Abstract Data Type that represents itself in a self-describing format where each attribute of the instance is named and typed in a binary format.</para>
    /// <para>The first octet is unsigned integer (0-255) indicating the length of the name of the field. If non-zero, the given number of octets are read and parsed into a UTF-8 string.</para>
    /// <para>Next, another byte representing an unsigned integer (0-255) is read in and used to indicate the type of the field. If it is a numeric or other fixed type, the appropriate number of bytes are read in. If a variable type is indicated then the next U32 integer (4-bytes) is read as the length of the data. U32 is used to support nesting of frames within frames which can quickly exceed U16 values of 32767 bytes in length.</para>
    /// <para>This utility class packages up a tagged value pair with a length field so as to allow for reliable reading of data from various transport streams.</para>
    /// </remarks>
    public class DataField : ICloneable {

        #region Attributes

        /// Name of this field 
        internal string name = null;

        /// The type of data being held (0-255). 
        internal byte type;

        /// The actual value being held. Empty arrays are equivalent to a null value. 
        internal byte[] value;

        /// array of data types supported 
        private static readonly List<FieldType> _types = new List<FieldType>();

        /// (0) Type code representing a nested data frame 
        public const byte FRAME = 0;
        /// (1) Type code representing an undefined type 
        public const byte UDEF = 1;
        /// (2) Type code representing a byte array 
        public const byte BYTEARRAY = 2;
        /// (3) Type code representing a String object 
        public const byte STRING = 3;
        /// (4) Type code representing an signed, 8-bit value in the range of -128 to 127 
        public const byte S8 = 4;
        /// (5) Type code representing an unsigned, 8-bit value in the range of 0 to 255 
        public const byte U8 = 5;
        /// (6) Type code representing an signed, 16-bit value in the range of -32,768 to 32,767 
        public const byte S16 = 6;
        /// (7) Type code representing an unsigned, 16-bit value in the range of 0 to 65,535 
        public const byte U16 = 7;
        /// (8) Type code representing a signed, 32-bit value in the range of -2,147,483,648 to 2,147,483,647 
        public const byte S32 = 8;
        /// (9) Type code representing an unsigned, 32-bit value in the range of 0 to 4,294,967,295 
        public const byte U32 = 9;
        /// (10) Type code representing an signed, 64-bit value in the range of -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 
        public const byte S64 = 10;
        /// (11) Type code representing an unsigned, 64-bit value in the range of 0 to 18,446,744,073,709,551,615 
        public const byte U64 = 11;
        /// (12) Type code representing a 32-bit floating point value in the range of +/-1.4013e-45 to +/-3.4028e+38. 
        public const byte FLOAT = 12;
        /// (13) Type code representing a 64-bit floating point value in the range of +/-4.9406e-324 to +/-1.7977e+308. 
        public const byte DOUBLE = 13;
        /// (14) Type code representing a boolean 
        public const byte BOOLEAN = 14;
        /// (15) Type code representing a unsigned 32-bit epoch time in milliseconds 
        public const byte DATE = 15;
        /// (16) Type code representing a uniform resource identifier 
        public const byte URI = 16;
        /// (17) Type code representing an ordered array of values (DataFields) 
        public const byte ARRAY = 17;

        public static Encoding DEFAULT_ENCODING = Encoding.UTF8;
        protected internal static Encoding strEnc = DataField.DEFAULT_ENCODING;

        #endregion


        #region Properties

        /// <summary>
        /// The name of this field.
        /// </summary>
        public string Name {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// The type code of the data in this field.
        /// </summary>
        public byte Type {
            get { return type; }
            internal set { type = value; }
        }


        /// <summary>
        /// Access the encoded length of this fields value in octets(bytes).
        /// </summary>
        public virtual int Length {
            get {
                return value.Length;
            }
        }


        /// <summary>
        /// Access the encoded (binary) value of this field.
        /// </summary>
        public virtual byte[] Value {
            get {
                return value;
            }
        }


        /// <summary>
        /// Get a new reference of the object represented by this data field.
        /// </summary>
        /// <returns>The value of this field as an object.</returns>
        public virtual object ObjectValue {
            get {
                return GetObjectValue( type, value );
            }
        }


        /// <summary>
        /// Get the wire format of this field.
        /// </summary>
        public virtual byte[] Bytes {
            get {
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter( ms );
                Write( bw );
                return ms.ToArray();
            }
        }


        /// <summary>
        /// Return the name of the data type this field contains/
        /// </summary>
        /// <returns>The name of the data type for this instance</returns>
        public virtual string TypeName {
            get {
                return DataField.GetTypeName( type );
            }
        }


        /// <summary>
        /// Determine if this field is numeric
        /// </summary>
        /// <returns>True if the value is numeric, false otherwise.</returns>
        public virtual bool IsNumeric {
            get {
                return GetDataType( type ).IsNumeric;
            }
        }


        /// <summary>
        /// Determine if this field is not numeric
        /// </summary>
        /// <returns>True if the value is not numeric, false if it is numeric.</returns>
        public virtual bool IsNotNumeric {
            get {
                return !IsNumeric;
            }
        }


        /// <summary>
        /// Determine if this field contains a DataFrame.
        /// </summary>
        /// <returns>True if the value is a frame, false otherwise.</returns>
        public virtual bool IsFrame {
            get {
                return type == FRAME;
            }
        }


        /// <summary>
        /// Determine if this field contains anything other than a DataFrame.
        /// </summary>
        /// <returns>True if the value is not a frame, false if it is a frame.</returns>
        public virtual bool IsNotFrame {
            get {
                return type != FRAME;
            }
        }


        /// <summary>
        /// Determine if this field type is undefined
        /// </summary>
        /// <remarks>
        /// <para>This helps with the use case of a DataField receiving a NULL as a value. It is encoded as an undefined type with no value.</para>
        /// </remarks>
        /// <returns>True if the value represents an un-typed value, false otherwise.</returns>
        public virtual bool IsUndefined {
            get {
                return type == UDEF;
            }
        }


        /// <summary>
        /// Determine how many different types are supported
        /// </summary>
        /// <returns>the number of types currently supported.</returns>
        internal static int TypeCount {
            get {
                return _types.Count;
            }
        }


        /// <summary>
        /// Get the string representation of this field.
        /// </summary>
        /// <returns>The value of this field as a String.</returns>
        public virtual string StringValue {
            get {
                return GetStringValue( type, value );
            }
        }


        /// <summary>
        /// Determines if this field has no value. 
        /// (Empty frames are not considered a null value)
        /// </summary>
        /// <returns>true if there is no value, false if there is data in this field</returns>
        public virtual bool IsNull {
            get {
                return (value == null || (type != DataField.FRAME && value.Length == 0));
            }
        }


        /// <summary>
        /// Test to see if this field has a value.
        /// </summary>
        /// <returns>true if there is a value, false if there is no data in this field</returns>
        public virtual bool IsNotNull {
            get {
                return !IsNull;
            }
        }

        #endregion


        #region Constructors

        // setup the string encoding of field names
        static DataField() {
            // Make this configurable...ConfigurationManager
            //try{
            //    DataField.DEFAULT_ENCODING = System.getProperty("field.encoding", DataField.ENC_UTF8);
            //} catch (Exception _ex) {
            strEnc = DataField.DEFAULT_ENCODING;
            //}

            // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
            // Adding support for a new type involves creating a new field type and adding it here
            // Further, all types must be sequential due to the nature of how they are stored in an array
            DataField.AddType( FRAME, new FrameType() );
            DataField.AddType( UDEF, new UndefinedType() );
            DataField.AddType( BYTEARRAY, new ByteArrayType() );
            DataField.AddType( STRING, new StringType() );
            DataField.AddType( S8, new S8Type() );
            DataField.AddType( U8, new U8Type() );
            DataField.AddType( S16, new S16Type() );
            DataField.AddType( U16, new U16Type() );
            DataField.AddType( S32, new S32Type() );
            DataField.AddType( U32, new U32Type() );
            DataField.AddType( S64, new S64Type() );
            DataField.AddType( U64, new U64Type() );
            DataField.AddType( FLOAT, new FloatType() );
            DataField.AddType( DOUBLE, new DoubleType() );
            DataField.AddType( BOOLEAN, new BooleanType() );
            DataField.AddType( DATE, new DateType() );
            DataField.AddType( URI, new UriType() );
            DataField.AddType( ARRAY, new ArrayType() );
            // / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / /
        }




        /// <summary>
        /// Protected no-arg constructor used for cloning
        /// </summary>
        protected internal DataField() {
        }




        /// <summary>
        /// Create a DataField with a specified type and value.
        /// </summary>
        /// <param name="type">the type code representing the type of data held.</param>
        /// <param name="value">the encoded value of the field.</param>
        protected internal DataField( byte type, byte[] value ) {
            this.type = type;
            this.value = value;
        }




        /// <summary>
        /// Create a DataField with a specified name, type and value.
        /// </summary>
        /// <remarks>
        /// <para>It is possible to use this to force a data field into a specific type even if the value is null.</para>
        /// </remarks>
        /// <param name="name">The name of this DataField</param>
        /// <param name="type">The type code representing the type of data held.</param>
        /// <param name="value">The encoded value of the field. An empty array is equivalent to a null value.</param>
        /// <exception cref="ArgumentException">If there are problems with the name</exception>
        public DataField( string name, byte type, byte[] value ) {
            this.name = DataField.NameCheck( name );
            this.type = type;
            this.value = value;
        }




        /// <summary>
        /// Create a DataField for the specific object.
        /// </summary>
        /// <param name="obj">The object to use as the value of the field</param>
        /// <exception cref="ArgumentException">If the object type is not supported</exception>
        public DataField( object obj ) {
            type = DataField.GetType( obj );
            value = DataField.Encode( obj, type );
        }




        /// <summary>
        /// The constructor for the most common use case, Name-Value pair
        /// </summary>
        /// <param name="name">The name of this DataField</param>
        /// <param name="obj">The object value to encode</param>
        /// <exception cref="ArgumentException">If the name is not valid or the object type is not supported</exception>
        public DataField( string name, object obj ) {
            this.name = DataField.NameCheck( name );
            type = DataField.GetType( obj );
            value = DataField.Encode( obj, type );
        }




        /// <summary>
        /// Construct the data field from data read in from the given input stream.
        /// </summary>
        /// <param name="br">The input stream from which the data field will be read</param>
        /// <exception cref="IOException">if there are problems reading from the stream</exception>
        public DataField( BinaryReader br ) {
            // The first octet is the length of the name to read in
            byte nameLength = br.ReadByte();

            // If there is a name of any length, read it in as a String
            if ( nameLength > 0 ) {
                byte[] nameData = new byte[nameLength];
                br.Read( nameData, 0, nameData.Length );
                name = Encoding.ASCII.GetString( nameData );
            }

            // the next octet we read is the data type
            type = br.ReadByte();

            FieldType datatype = null;
            try {
                // get the proper data type for the field
                datatype = GetDataType( type );
            } catch ( Exception ) {
                if ( nameLength > 0 ) {
                    throw new DecodeException( "non supported type: '" + type + "' for field: '" + name + "'" );
                } else {
                    throw new DecodeException( "non supported type: '" + type + "'" );
                }
            }


            // if the file type is a variable length (i.e. size < 0), read in the length
            if ( datatype.Size < 0 ) {
                // Read the next 4 bytes
                byte[] u32 = br.ReadBytes( 4 );

                // ...and convert it to a length using big-endian encoding
                uint length = ByteUtil.RetrieveUnsignedInt( u32, 0 );

                if ( length < 0 ) {
                    throw new DecodeException( "read length bad value: length = " + length + " type = " + type );
                }

                value = new byte[length];
                if ( length > 0 ) {
                    br.Read( value, 0, value.Length );
                }

            } else {
                value = br.ReadBytes( datatype.Size );
            }
        }

        #endregion


        #region Methods


        /// <summary>
        /// Add a Data type for fields
        /// </summary>
        /// <param name="indx">where in the array to add the</param>
        /// <param name="type">the object handling the type</param>
        internal static void AddType( int indx, FieldType type ) {
            _types.Insert( indx, type );
        }




        /// <summary>
        ///  Create a deep-copy of this DataField.
        /// </summary>
        /// The name and type references are shared and the value is copied to a
        /// new byte array.
        /// <returns>A mutable copy of this DataField.</returns>
        public virtual object Clone() {
            DataField retval = new DataField();

            // strings are immutable
            retval.name = name;
            retval.type = type;

            if ( value != null ) {
                retval.value = new byte[value.Length];

                System.Array.Copy( value, 0, retval.value, 0, value.Length );
            }

            return retval;
        }




        /// <summary>
        /// Checks to see if the name is valid.
        /// </summary>
        /// <remarks>
        /// <para>Right now only a check for size is performed. The size of a name must be less than 256 characters.</para>
        /// </remarks>
        /// <param name="name">The name to check</param>
        /// <returns>The validated name.</returns>
        /// <exception cref="ArgumentException">There is a problem with the name</exception>
        private static string NameCheck( string name ) {
            if ( name != null && name.Length > 255 ) {
                throw new ArgumentException( "Name too long - 255 char limit" );
            }

            return name;
        }




        /// <summary>
        /// Get the numeric code representing the type of the passed object
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>the numeric type as it would be encoded in the field</returns>
        /// <exception cref="ArgumentException">if the passed object is an unsupported type.</exception>
        public static byte GetType( object obj ) {
            for ( byte x = 0; x < _types.Count; x++ ) {
                if ( _types[x].CheckType( obj ) )
                    return x;
            }
            throw new ArgumentException( "Unsupported Object Type" );
        }




        /// <summary>
        /// Get a list of all the type names
        /// </summary>
        /// <returns>the list of supported type names</returns>
        public static List<string> GetTypeNames() {
            List<string> retval = new List<string>();
            for ( short x = 0; x < _types.Count; x++ ) {
                retval.Add( _types[x].TypeName );
            }
            return retval;
        }




        /// <summary>
        /// Return the field type with the given name
        /// </summary>
        /// <param name="name">The name of the type to retrieve</param>
        /// <returns>the FieldType with the given name or null if not found</returns>
        public static FieldType GetFieldType( string name ) {
            for ( short x = 0; x < _types.Count; x++ ) {
                if ( _types[x].TypeName.Equals( name ) ) {
                    return _types[x];
                }
            }
            return null;
        }




        /// <summary>
        /// Convert the object into a binary representation of a DataField
        /// </summary>
        /// <param name="obj">The object to encode.</param>
        /// <returns>The bytes representing the object in DataFrame format.</returns>
        /// <exception cref="ArgumentException">if the object cannot be encoded - type not supported</exception>
        public static byte[] Encode( object obj ) {
            return DataField.Encode( obj, DataField.GetType( obj ) );
        }




        /// <summary>
        /// Encode the given object as the given type.
        /// </summary>
        /// <param name="obj">The object to encode</param>
        /// <param name="type">the type of object it is or it is to be encoded as</param>
        /// <returns>Return an array of bytes representing the given object using the given type specification.</returns>
        /// <exception cref="ArgumentException">if the object cannot be encoded using that type</exception>
        public static byte[] Encode( object obj, byte type ) {
            FieldType datatype = GetDataType( type );
            return datatype.Encode( obj );
        }




        /// <summary>
        /// Decode the field into an object reference.
        /// </summary>
        /// <param name="typ">the type of data in the value array</param>
        /// <param name="val">the binary representation of the data</param>
        /// <returns>the object value of the data encoded in the value attribute.</returns>
        private object GetObjectValue( byte typ, byte[] val ) {
            if ( val == null ) {
                return null;
            } else if ( val.Length == 0 ) {
                if ( typ == FRAME ) {
                    // empty value in a frame indicates an empty frame
                    return new DataFrame();
                } else {
                    // an empty value for any other type indicates a null object
                    return null;
                }
            } else {
                FieldType datatype = GetDataType( typ );
                return datatype.Decode( val );
            }
        }




        /// <summary>
        /// Write the field to the output stream.
        /// </summary>
        /// <param name="bw">The binary writer on which the field is to be written.</param>
        public virtual void Write( BinaryWriter bw ) {
            // If we have a name...
            if ( name != null ) {
                // write the length and name fields
                byte[] nameField = strEnc.GetBytes( name );
                int nameLength = nameField.Length;
                bw.Write( (byte)nameLength );
                bw.Write( nameField );
            } else {
                // indicate a name field length of 0
                bw.Write( (byte)0 );
            }

            // Write the type field
            bw.Write( (byte)type );

            if ( value != null ) {
                FieldType datatype = GetDataType( type );

                // If the value is variable in length
                if ( datatype.Size < 0 ) {
                    // write the length as a U32 bit value (even though it will
                    // never be larger than a S32)
                    bw.Write( ByteUtil.RenderUnsignedInt( (uint)value.Length ) );
                }

                // write the value itself
                bw.Write( value );
            } else {
                bw.Write( (ushort)0 );
            }

            return;
        }




        /// <summary>
        /// Get the name of the type for the given code
        /// </summary>
        /// <param name="code">The code representing the data field type</param>
        /// <returns>The name of the type represented by the code</returns>
        private static string GetTypeName( byte code ) {
            return GetDataType( code ).TypeName;
        }




        /// <summary>
        /// Get the size of the given type.
        /// </summary>
        /// <param name="code">the type code used in encoded fields</param>
        /// <returns>the number of octets used to represent the data type in its encoded form.</returns>
        protected internal static int GetTypeSize( byte code ) {
            return GetDataType( code ).Size;
        }




        /// <summary>
        /// Return the appropriate FieldType for the given type identifier.
        /// </summary>
        /// <param name="typ">the identifier of the type to retrieve</param>
        /// <returns>The FieldType object which handles the data of the identified type</returns>
        protected internal static FieldType GetDataType( byte typ ) {
            FieldType retval = null;

            try {
                // get the proper field type
                retval = _types[typ];
            } catch ( Exception ) {
                throw new ArgumentException( "Unsupported data type of '" + typ + "'" );
            }

            if ( retval == null )
                throw new ArgumentException( "Null type field for type: '" + typ + "'" );
            else
                return retval;
        }




        /// <summary>
        /// Human readable format of the data field.
        /// </summary>
        /// <returns>a string representation of the data field instance</returns>
        public override string ToString() {
            System.Text.StringBuilder buf = new System.Text.StringBuilder( "DataField:" );
            buf.Append( " name='" + name + "'" );
            buf.Append( " type=" + this.TypeName );
            buf.Append( "(" + type + ")" );
            if ( value.Length > 32 ) {
                byte[] sample = new byte[32];
                System.Array.Copy( value, 0, sample, 0, sample.Length );
                buf.Append( " value=[" + ByteUtil.BytesToHex( sample ) + " ...]" );
            } else
                buf.Append( " value=[" + ByteUtil.BytesToHex( value ) + "]" );

            return buf.ToString();
        }




        /// <summary>
        /// Decode the field into an string representation.
        /// </summary>
        /// <remarks>
        /// <para>This is useful when using this field as a value and needing to output it in human readable form. This is similar to the toString function except this represents the value carried/encapsulated in this field, not the field itself.</para>
        /// </remarks>
        /// <param name="typ">the type of data in the value array</param>
        /// <param name="val">binary representation of the value to represent as a string.</param>
        /// <returns>the string representation of the object value of the data encoded in the value attribute.</returns>
        private string GetStringValue( byte typ, byte[] val ) {
            FieldType datatype = GetDataType( typ );
            return datatype.StringValue( val );
        }

        #endregion

    }

}