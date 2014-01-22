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

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.util.ArrayList;

import coyote.util.ByteUtil;


/** 
 * Type representing an ordered array of values.
 * 
 * <p>The current design involves encoding a name, type (array), length (number 
 * of elements) and a set of Type, Length, Value (TLV) triplets for each array 
 * element.</p>
 */
public class ArrayType implements FieldType
{
  private static final int _size = -1;

  private final static String _name = "ARY";




  public boolean checkType( Object obj )
  {
    return obj instanceof Object[];
  }




  public Object decode( byte[] value )
  {
    // as long as there is data to read, convert them into object values
    final ArrayList<DataField> retval = new ArrayList<DataField>();

    final DataInputStream dis = new DataInputStream( new ByteArrayInputStream( value ) );

    //    try
    //    {
    //      // keep parsing DataFields from the byte array until a match is made
    //      while( dis.available() > 0 )
    //      {
    //        // get the type
    //        final short tipe = dis.readByte();
    //
    //        // Figure out the length of the data to read
    //        short valen = 0;
    //        if( ( tipe <= 2 ) || ( tipe == DataField.ARRAY ) || ( tipe == DataField.URI ) )
    //        {
    //          valen = dis.readShort();
    //        }
    //        else if( ( tipe == DataField.U8 ) || ( tipe == DataField.S8 ) || ( tipe == DataField.BOOLEAN ) )
    //        {
    //          valen = 1;
    //        }
    //        else if( ( tipe == DataField.U16 ) || ( tipe == DataField.S16 ) )
    //        {
    //          valen = 2;
    //        }
    //        else if( ( tipe == DataField.U32 ) || ( tipe == DataField.S32 ) || ( tipe == DataField.FLOAT ) )
    //        {
    //          valen = 4;
    //        }
    //        else if( ( tipe == DataField.U64 ) || ( tipe == DataField.S64 ) || ( tipe == DataField.DATE ) || ( tipe == DataField.DOUBLE ) )
    //        {
    //          valen = 8;
    //        }
    //
    //        //create a file the size we need
    //        final byte[] fld = new byte[valen];
    //
    //        // fill it from the input stream
    //        dis.readFully( fld );
    //
    //        // convert it into an object through a recursive call
    //        retval.add( (DataField)getObjectValue( tipe, fld ) );
    //      } // while
    //
    //      if( retval.size() > 0 )
    //      {
    //        return retval.toArray();
    //      }
    //      else
    //      {
    return new Object[0];
    //      }
    //    }
    //    catch( final Exception e )
    //    {
    //      return null;
    //    }
    //    finally
    //    {
    //      try
    //      {
    //        if( dis != null )
    //        {
    //          dis.close();
    //        }
    //      }
    //      catch( final IOException ioe2 )
    //      {
    //      }
    //    }
  }




  /**
   * Encode the payload portion of the array (i.e., no name or type)
   */
  public byte[] encode( Object obj )
  {
    final Object[] ary = (Object[])obj;

    final ByteArrayOutputStream out = new ByteArrayOutputStream();
    final DataOutputStream dos = new DataOutputStream( out );

    for( int x = 0; x < ary.length; x++ )
    {
      try
      {
        // This will throw an exception for any unsupported data types.
        final short tipe = DataField.getType( ary[x] );
        final byte[] data = DataField.encode( ary[x], tipe );
        final int size = DataField.getDataType( tipe ).getSize();

        // Write the type field
        dos.write( ByteUtil.renderShortByte( tipe ) );

        if( data != null )
        {
          // If the value is variable in length
          if( size < 0 )
          {
            // write the length
            dos.writeShort( data.length );
          }

          // write the value itself
          dos.write( data );
        }
        else
        {
          dos.writeShort( 0 );// null value
        }
      }
      catch( final Throwable t )
      {
        System.err.println( "Array object of type " + ary[x].getClass().getSimpleName() + " is not supported in DataFrames" );
        // just skip the offending object and add the rest.
      }
    } // for each

    return out.toByteArray();
  }




  public String getTypeName()
  {
    return _name;
  }




  public boolean isNumeric()
  {
    return false;
  }




  public int getSize()
  {
    return _size;
  }

}
