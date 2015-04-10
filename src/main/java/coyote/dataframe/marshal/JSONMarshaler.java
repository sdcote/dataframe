/*
 * Copyright (c) 2014 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial concept and initial implementation
 */
package coyote.dataframe.marshal;

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.StringWriter;
import java.util.List;

import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.json.JsonFrameParser;
import coyote.dataframe.marshal.json.JsonWriter;
import coyote.dataframe.marshal.json.WriterConfig;


/**
 * 
 */
public class JSONMarshaler {

  /**
   * Marshal the given JSON into a dataframe.
   * 
   * @param json
   * 
   * @return Data frame containing the JSON represented data
   */
  public static List<DataFrame> marshal( final String json ) throws MarshalException {
    List<DataFrame> retval = null;

    try {
      retval = new JsonFrameParser( json ).parse();
    } catch ( final Exception e ) {
      System.out.println( "oops: " + e.getMessage() );
      throw new MarshalException( "Could not marshal JSON to DataFrame", e );
    }

    return retval;
  }




  /**
   * Generate a JSON string from the given data frame.
   * 
   * @param frame The frame to marshal
   * 
   * @return A JSON formatted string which can be marshaled back into a frame
   */
  public static String marshal( final DataFrame frame ) {
    return write( frame, WriterConfig.MINIMAL );
  }




  /**
   * Generate a nicely formatted (and indented) JSON string from the given data frame.
   * 
   * @param frame The frame to marshal
   * 
   * @return A JSON formatted string which can be marshaled back into a frame
   */
  public static String toFormattedString( final DataFrame frame ) {
    return write( frame, WriterConfig.PRETTY_PRINT );
  }




  /**
   * @param frame
   * @param config
   * @return
   */
  private static String write( final DataFrame frame, final WriterConfig config ) {

    // create string writer
    final StringWriter sw = new StringWriter();
    final BufferedWriter bw = new BufferedWriter( sw );
    final JsonWriter writer = config.createWriter( bw );

    try {
      writeFrame( frame, writer );
      bw.flush();
    } catch ( IOException e ) {
      return "[\"" + e.getMessage() + "\"]";
    }
    return sw.getBuffer().toString();
  }




  /**
   * 
   * @param frame
   * @param config
   * 
   * @return
   * @throws IOException 
   */
  private static void writeFrame( final DataFrame frame, final JsonWriter writer ) throws IOException {

    if ( frame.size() > 0 ) {
      boolean isArray = frame.isArray();
      if ( isArray )
        writer.writeArrayOpen();
      else
        writer.writeObjectOpen();

      DataField field = null;
      for ( int i = 0; i < frame.size(); i++ ) {
        field = frame.getField( i );

        if ( !isArray ) {
          if ( field.getName() != null ) {
            writer.writeString( field.getName() );
          } else {
            writer.writeString( "" );
          }
          writer.writeMemberSeparator();
        }

        if ( field.getType() == DataField.NULLTYPE ) {
          writer.writeLiteral( "null" );
        } else if ( field.getType() == DataField.BOOLEANTYPE ) {
          if ( "true".equalsIgnoreCase( field.getStringValue() ) ) {
            writer.writeLiteral( "true" );
          } else {
            writer.writeLiteral( "false" );
          }
        } else if ( field.isNumeric() ) {
          writer.writeNumber( field.getStringValue() );
        } else if ( field.getType() == DataField.FRAMETYPE ) {
          writeFrame( (DataFrame)field.getObjectValue(), writer );
        } else {

          writer.writeString( field.getObjectValue().toString() );
        }
        if ( i + 1 < frame.size() ) {
          writer.writeObjectSeparator();
        }
      }

      if ( isArray )
        writer.writeArrayClose();
      else
        writer.writeObjectClose();

    } else {
      writer.writeObjectOpen();
      writer.writeObjectClose();
    }

    return;
  }
}
