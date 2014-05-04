/*
 * Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe;

import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.util.ArrayList;

import coyote.util.ByteUtil;


/**
 * Type Length Value data structure.
 * 
 * <p>This is an Abstract Data Type that represents itself in a fairly self-
 * describing format where each attribute of the instance is named and typed in
 * its native binary format.</p>
 * 
 * <p>The first octet is unsigned integer (0-255) indicating the length of the 
 * name of the field. If non-zero, the given number of octets are read and 
 * parsed into a UTF-8 string.</p>
 * 
 * <p>Next, another byte representing an unsigned integer (0-255) is read in 
 * and used to indicate the type of the field. If it is a numeric or other 
 * fixed type, the appropriate number of bytes are read in. If a variable type 
 * is indicated then the next U16 integer (2-bytes) is read as the length of 
 * the data.</p>
 * 
 * <p>This utility class packages up a tagged value pair with a length field so 
 * as to allow for reliable reading of data from various transport streams.</p>
 */
public class DataField implements Cloneable
{

  /** array of data types supported */
  private static final ArrayList<FieldType> _types = new ArrayList<FieldType>();

  /** (0) Type code representing a byte array */
  public static final short FRAMETYPE = 0;

  static final String ENC_UTF8 = "UTF8";

  public static String DEFAULT_ENCODING = DataField.ENC_UTF8;
  protected static String strEnc = DataField.DEFAULT_ENCODING;

  // setup the string encoding of field names
  static
  {
    try
    {
      DataField.DEFAULT_ENCODING = System.getProperty( "file.encoding", DataField.ENC_UTF8 );
    }
    catch( final SecurityException _ex )
    {
      DataField.DEFAULT_ENCODING = DataField.ENC_UTF8;
      System.err.println( "Security settings preclude accessing Java System Property \"file.encoding\" - Using default string encoding of " + DataField.DEFAULT_ENCODING + " instead." );
    }
    catch( final Exception _ex )
    {
      DataField.DEFAULT_ENCODING = DataField.ENC_UTF8;
    }

    /** (0) Type code representing a nested data frame */
    DataField.addType( FRAMETYPE, new FrameType() );
    /** (1) Type code representing a NULL value - undefined type and a therefore empty value */
    DataField.addType( 1, new NullType() );
    /** (2) Type code representing a byte array */
    DataField.addType( 2, new ByteArrayType() );
    /** (3) Type code representing a String object */
    DataField.addType( 3, new StringType() );
    /** (4) Type code representing an signed, 8-bit value in the range of -128 to 127 */
    DataField.addType( 4, new S8Type() );
    /** (5) Type code representing an unsigned, 8-bit value in the range of 0 to 255 */
    DataField.addType( 5, new U8Type() );
    /** (6) Type code representing an signed, 16-bit value in the range of -32,768 to 32,767 */
    DataField.addType( 6, new S16Type() );
    /** (7) Type code representing an unsigned, 16-bit value in the range of 0 to 65,535 */
    DataField.addType( 7, new U16Type() );
    /** (8) Type code representing a signed, 32-bit value in the range of -2,147,483,648 to 2,147,483,647 */
    DataField.addType( 8, new S32Type() );
    /** (9) Type code representing an unsigned, 32-bit value in the range of 0 to 4,294,967,295 */
    DataField.addType( 9, new U32Type() );
    /** (10) Type code representing an signed, 64-bit value in the range of -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 */
    DataField.addType( 10, new S64Type() );
    /** (11) Type code representing an unsigned, 64-bit value in the range of 0 to 18,446,744,073,709,551,615 */
    DataField.addType( 11, new U64Type() );
    /** (12) Type code representing a 32-bit floating point value in the range of +/-1.4013e-45 to +/-3.4028e+38. */
    DataField.addType( 12, new FloatType() );
    /** (13) Type code representing a 64-bit floating point value in the range of +/-4.9406e-324 to +/-1.7977e+308. */
    DataField.addType( 13, new DoubleType() );
    /** (14) Type code representing a boolean value */
    DataField.addType( 14, new BooleanType() );
    /** (15) Type code representing a unsigned 32-bit epoch time in milliseconds */
    DataField.addType( 15, new DateType() );
    /** (16) Type code representing a uniform resource identifier */
    DataField.addType( 16, new UriType() );
    /** (17) Type code representing an ordered array of values (DataFields) */
    DataField.addType( 17, new ArrayType() );
  }

  /** Field name */
  String name = null;
  short type;
  byte[] value;




  /**
   * Add a Data type for fields
   * 
   * @param indx where in the array to add the
   * @param type the object handling the type
   */
  static void addType( int indx, FieldType type )
  {
    _types.add( indx, type );
  }




  /**
   * Private no-arg constructor used for cloning
   */
  private DataField()
  {
  }




  /**
   * Create a DataField with a specified type and value.
   * 
   * Used by the ArrayType in decoding arrays of values.
   * 
   * @param type the type code representing the type of data held.
   * @param value the encoded value of the field.
   */
  protected DataField( short type, byte[] value )
  {
    this.type = type;
    this.value = value;
  }




  /**
   * Create a DataField for the specific object.
   *
   * @param obj THe object to use as the value of the field
   */
  public DataField( final Object obj )
  {
    type = DataField.getType( obj );
    value = DataField.encode( obj, type );
  }




  /**
   * Constructor DataField
   *
   * @param name The name of this DataField
   * @param obj The object value to encode
   *
   * @throws IllegalArgumentException
   */
  public DataField( final String name, final Object obj ) throws IllegalArgumentException
  {
    this.name = DataField.nameCheck( name );
    type = DataField.getType( obj );
    value = DataField.encode( obj, type );
  }




  /**
   * Create a deep-copy of this DataField.
   * 
   * <p>The name and type references are shared and the value is copied to an 
   * new byte array.</p>
   *
   * @return A mutable copy of this DataField.
   */
  public Object clone()
  {
    final DataField retval = new DataField();

    // strings are immutable
    retval.name = name;
    retval.type = type;

    if( value != null )
    {
      retval.value = new byte[value.length];

      System.arraycopy( value, 0, retval.value, 0, value.length );
    }

    return retval;
  }




  /**
   * Checks to see if the name is valid.
   * 
   * <p>Right now only a check for size is performed. The size of a name must 
   * be less than 256 characters.</p>
   *
   * @param name The name to check
   *
   * @return The validated name.
   *
   * @throws IllegalArgumentException
   */
  private static String nameCheck( final String name ) throws IllegalArgumentException
  {
    if( name != null && name.length() > 255 )
    {
      throw new IllegalArgumentException( "Name too long - 255 char limit" );
    }

    return name;
  }




  /**
   * Construct the data field from data read in from the given input stream.
   *
   * @param dis The input stream from which the data field will be read
   *
   * @throws IOException if there was a problem reading the stream.
   */
  public DataField( final DataInputStream dis ) throws IOException
  {
    // The first octet is the length of the name to read in
    final int nameLength = dis.readUnsignedByte();

    // If there is a name of any length, read it in as a String
    if( nameLength > 0 )
    {
      final int i = dis.available();

      if( i < nameLength )
      {
        throw new IOException( "value underflow: name length specified as " + nameLength + " but only " + i + " octets are available" );
      }

      final byte[] nameData = new byte[nameLength];
      dis.readFully( nameData );

      name = new String( nameData, DataField.strEnc );
    }

    // the next field we read is the data type
    type = dis.readByte();

    FieldType datatype = null;
    try
    {
      // get the proper field type
      datatype = getDataType( type );
    }
    catch( Throwable ball )
    {
      throw new IOException( "non supported type: '" + type + "'" );
    }

    // if the file type is a variable length (i.e. size < 0), read in the length
    if( datatype.getSize() < 0 )
    {
      final int length = dis.readUnsignedShort();

      if( length < 0 )
      {
        throw new IOException( "read length bad value: length = " + length + " type = " + type );
      }

      final int i = dis.available();

      if( i < length )
      {
        throw new IOException( "value underflow: length specified as " + length + " but only " + i + " octets are available" );
      }

      value = new byte[length];

      if( length > 0 )
      {
        dis.read( value, 0, length );
      }
    }
    else
    {
      value = new byte[datatype.getSize()];
      dis.read( value );
    }
  }




  /**
   * Get the numeric code representing the type of the passed object
   *
   * @param obj THe object to check
   *
   * @return the numeric type as it would be encoded in the field
   *
   * @throws IllegalArgumentException if the passed object is an unsupported type.
   */
  public static short getType( final Object obj ) throws IllegalArgumentException
  {
    for( short x = 0; x < _types.size(); x++ )
    {
      if( _types.get( x ).checkType( obj ) )
        return x;
    }

    throw new IllegalArgumentException( "Unsupported Object Type" );

  }




  /**
   * Convert the object into a binary representation of a DataField
   *
   * @param obj The object to encode.
   *
   * @return The bytes representing the object in DataFrame format.
   *
   * @throws IllegalArgumentException If the object is not supported.
   */
  public static byte[] encode( final Object obj ) throws IllegalArgumentException
  {
    return DataField.encode( obj, DataField.getType( obj ) );
  }




  /**
   * Return an array of bytes representing the given object using the given 
   * type specification.
   *
   * @param obj The object to encode.
   * @param type The type encoding to use.
   *
   * @return the value of the given object using the given type specification.
   *
   * @throws IllegalArgumentException
   */
  public static byte[] encode( final Object obj, final short type ) throws IllegalArgumentException
  {
    FieldType datatype = getDataType( type );
    return datatype.encode( obj );
  }




  /**
   * @return The numeric type of this field.
   */
  public short getType()
  {
    return type;
  }




  /**
   * @return The number of octets this fields value uses.
   */
  public int getLength()
  {
    return value.length;
  }




  /**
   * @return The encoded value of this field.
   */
  public byte[] getValue()
  {
    return value;
  }




  /**
   * @return The value of this field as an object.
   */
  public Object getObjectValue()
  {
    return getObjectValue( type, value );
  }




  /**
   * Decode the field into an object reference.
   * 
   * @return the object value of the data encoded in the value attribute.
   */
  private Object getObjectValue( final short typ, final byte[] val )
  {
    FieldType datatype = getDataType( typ );
    return datatype.decode( val );
  }




  /**
   * Write the field to the output stream.
   *
   * @param dos The DataOutputStream on which the field is to be written.
   *
   * @throws IOException if there is a problem writing to the output stream.
   */
  public void write( final DataOutputStream dos ) throws IOException
  {
    // If we have a name...
    if( name != null )
    {
      // write the length and name fields
      final byte[] nameField = name.getBytes( DataField.strEnc );
      final int nameLength = nameField.length;
      dos.write( ByteUtil.renderShortByte( (short)nameLength ) );
      dos.write( nameField );
    }
    else
    {
      // indicate a name field length of 0
      dos.write( ByteUtil.renderShortByte( (short)0 ) );
    }

    // Write the type field
    dos.write( ByteUtil.renderShortByte( type ) );

    if( value != null )
    {

      FieldType datatype = getDataType( type );

      // If the value is variable in length
      if( datatype.getSize() < 0 )
      {
        // write the length
        dos.writeShort( value.length );
      }

      // write the value itself
      dos.write( value );
    }
    else
    {
      dos.writeShort( 0 );
    }

    return;
  }




  /**
   * Get the wire format of the Data.
   *
   * @return binary representation of the field.
   */
  public byte[] getBytes()
  {
    final ByteArrayOutputStream out = new ByteArrayOutputStream();
    final DataOutputStream dos = new DataOutputStream( out );

    try
    {
      write( dos );
    }
    catch( final IOException ioe )
    {
    }

    return out.toByteArray();
  }




  /**
   * Access the name of this field.
   * 
   * @return The name of this field.
   */
  public String getName()
  {
    return name;
  }




  /**
   * Set the name of this field.
   * 
   * @param string Then name of this field.
   */
  public void setName( final String string )
  {
    name = string;
  }




  /**
   * Get the name of the type for the given code
   *
   * @param code THe code representing the data field type
   *
   * @return The name of the type represented by the code
   */
  private static String getTypeName( final short code )
  {
    return getDataType( code ).getTypeName();
  }




  /**
   * Get the size of the given type.
   * 
   * @param code the type code used in encoded fields
   * 
   * @return the number of octets used to represent the data type in its 
   * encoded form.
   */
  protected static int getTypeSize( final short code )
  {
    return getDataType( code ).getSize();
  }




  protected static FieldType getDataType( short typ )
  {
    FieldType retval = null;

    try
    {
      // get the proper field type
      retval = _types.get( typ );
    }
    catch( Throwable ball )
    {
      throw new IllegalArgumentException( "Unsupported data type of '" + typ + "'" );
    }

    if( retval == null )
      throw new IllegalArgumentException( "Null type field for type: '" + typ + "'" );
    else
      return retval;
  }




  /**
   * Return the name of the data type this field contains/
   *
   * @return The name of the data type for this instance
   */
  public String getTypeName()
  {
    return DataField.getTypeName( type );
  }




  /**
   * @return True if the value is numeric, false otherwise.
   */
  public boolean isNumeric()
  {
    return getDataType( type ).isNumeric();
  }




  /**
   * @return True if the value is a frame, false otherwise.
   */
  public boolean isFrame()
  {
    return type == FRAMETYPE;
  }




  /**
   * Human readable format of the data field.
   *
   * @return a string representation of the data field instance
   */
  @Override
  public String toString()
  {
    final StringBuffer buf = new StringBuffer( "DataField:" );
    buf.append( " name='" + name + "'" );
    buf.append( " type=" + type );
    if( value.length > 40 )
    {
      byte[] sample = new byte[40];
      System.arraycopy( value, 0, sample, 0, sample.length );
      buf.append( " value=[" + ByteUtil.bytesToHex( sample ) + " ...]" );
    }
    else
      buf.append( " value=[" + ByteUtil.bytesToHex( value ) + "]" );

    return buf.toString();
  }




  static int typeCount()
  {
    return _types.size();
  }

}
