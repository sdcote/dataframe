/*
 * Copyright (c) 2015 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial concept and initial implementation
 */
package coyote.dataframe.marshal.xml;

import java.io.IOException;
import java.io.Reader;

import coyote.commons.SimpleReader;
import coyote.commons.StringParser;
import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;
import coyote.dataframe.FieldType;
import coyote.dataframe.marshal.ParseException;


/**
 * 
 */
public class XmlFrameParser extends StringParser {
  private static final String XML_DELIMS = " \t\n><";

  private static final int OPEN = (int)'<';
  private static final int CLOSE = (int)'>';
  private static final int QM = (int)'?';
  private static final int SLASH = (int)'/';
  public static final String TYPE_ATTRIBUTE_NAME = "type";




  public XmlFrameParser( String string ) {
    super( new SimpleReader( string ), XML_DELIMS );
  }




  public XmlFrameParser( Reader reader ) {
    super( reader, XML_DELIMS );
  }




  private String readValue() {
    StringBuffer b = new StringBuffer();
    try {
      if ( OPEN == peek() )
        return null;

      // keep reading characters until the next character is an open marker
      while ( OPEN != peek() ) {
        b.append( (char)read() );
      }

    } catch ( IOException e ) {
      e.printStackTrace();
    }
    return b.toString();
  }




  /**
   * Generate a parse exception with the given message.
   * 
   * <p>All the position information is populated in the exception based on the 
   * readers current counters.</p>
   * 
   * @param message The text message to include in the exception 
   * 
   * @return a parse exception with the given message.
   */
  private ParseException error( final String message ) {
    return new ParseException( message, getOffset(), getCurrentLineNumber(), getColumnNumber(), getLastCharacterRead() );
  }




  private ParseException expected( final String expected ) {
    if ( isEndOfText() ) {
      return error( "Unexpected input" );
    }
    return error( "Expected " + expected );
  }




  private boolean isEndOfText() {
    return getLastCharacterRead() == -1;
  }




  private Tag readTag() {
    Tag retval = null;
    String token = null;

    // read to the next open character
    try {
      readTo( OPEN );
    } catch ( Exception e ) {
      // assume there are no more tags
      return null;
    }

    try {
      // read everything up to the closing character into the token
      token = readTo( CLOSE );
      //log.debug( "Read tag of '{}'", token );

      if ( token != null ) {
        token = token.trim();

        if ( token.length() > 0 ) {
          retval = new Tag( token );
        }
      }
    } catch ( IOException e ) {
      throw error( "Could not read a complete tag: IO error" );
    }

    return retval;
  }




  /**
   * Parse the reader into a dataframe
   * 
   * @return a dataframe parsed from the data set in this parser.
   * 
   * @throws ParseException if there are problems parsing the XML
   */
  public DataFrame parse() throws ParseException {

    DataFrame retval = null;
    DataField field = null;

    Tag tag = null;
    Tag gat = null;

    // Start reading tags until we pass the preamble and comments
    do {
      tag = readTag();
      if ( tag == null ) {
        break;
      }
    }
    while ( tag.isComment() || tag.isPreamble() );

    // We have a tag which is not a preamble or comment; start looping through the tags 
    while ( tag != null ) {

      if ( tag.isOpenTag() ) {

        field = readField( tag.getName(), tag.getAttribute( TYPE_ATTRIBUTE_NAME ) );

        // add the parsed field into the dataframe
        if ( field != null ) {
          // create it if this is the first field
          if ( retval == null ) {
            retval = new DataFrame();
          }
          retval.add( field );
        }

        // read the closing tag
        gat = readTag();

        if ( gat != null && gat.isCloseTag() ) {
          // make sure it matches
          if ( !tag.getName().equals( gat.getName() ) ) {
            throw error( "Malformed XML detected: wrong closing tag '" + gat.getName() + "'" );
          }
        } else {
          throw error( "Malformed XML detected: EOF before close of tag '" + tag.getName() + "'" );
        }

        // read the next opening tag
        tag = readTag();

      } else {
        throw error( "Expected opening tag but read in a closing tag" );
      }

    } // while we have tags

    return retval;
  }




  /**
   * Read in the value of a tag creating a data field containing the value as 
   * of the requested data type.
   * 
   * @param name Name of the data field to create
   * @param type the type of data it is supposed to be defaults to String (STR) if null, empty or invalid.
   * 
   * @return a data field constructed from the XML value at the current position in the reader's stream
   */
  private DataField readField( String name, String type ) {
    DataField retval = null;
    FieldType fieldType = DataField.getFieldType( type );

    Object value = readValue();

    if ( value != null ) {
      if ( fieldType != null ) {
        // TODO try to convert the string data into an object

        retval = new DataField( name, value ); // string for now
      } else {
        // not valid field type, just use string (or DataFrame depending on what readValue returned)
        retval = new DataField( name, value );
      }
    }

    return retval;
  }




  /**
   * Start reading fields of a frame and return all the retrieved fields inside 
   * a frame.
   * 
   * <p>This is a recursive call allowing for fields containing frames.</p>
   * @return
   */
  private DataFrame readFrame() {
    DataFrame retval = null;

    return retval;
  }

}
