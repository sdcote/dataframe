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
import java.util.ArrayList;
import java.util.List;

import coyote.commons.SimpleReader;
import coyote.commons.StringParser;
import coyote.dataframe.DataFrame;


/**
 * 
 */
public class XmlFrameParser extends StringParser {
  private static final String XML_DELIMS = " \t\n><";

  private static final int OPEN = (int)'<';
  private static final int CLOSE = (int)'>';
  private static final int QM = (int)'?';
  private static final int SLASH = (int)'/';




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




  private Tag readTag() {
    Tag retval = null;
    String token = null;

    try {
      // read to the next open character
      readTo( OPEN );

      // read everything up to the closing character into the token
      token = readTo( CLOSE );
      //log.debug( "Read tag of '{}'", token );

      if ( token != null ) {
        token = token.trim();

        if ( token.length() > 0 ) {
          retval = new Tag();
          String name = null;

          if ( token.endsWith( "/" ) ) {
            retval.setEmptyTag( true );
            token = token.substring( 0, token.length() - 1 );
          }
          if ( token.startsWith( "/" ) ) {
            retval.setEndTag( true );
            token = token.substring( 1 );
          }

          // See if there are attributes
          if ( token.indexOf( ' ' ) > -1 ) {
            name = token.substring( 0, token.indexOf( ' ' ) );
          } else {
            name = token;
          }

          // split the name into namespace and name
          if ( name.indexOf( ':' ) > -1 ) {
            retval.setNamespace( name.substring( 0, token.indexOf( ':' ) ) );
            retval.setName( name.substring( token.indexOf( ':' ) + 1 ) );
          } else {
            retval.setName( name );
          }

        }
      }
    } catch ( IOException e ) {
      e.printStackTrace();
    }

    return retval;
  }




  /**
   * Parse the reader into data frames
   * 
   * @return a list of data frames parsed from the data set in this parser.
   */
  public List<DataFrame> parse() {
    final List<DataFrame> retval = new ArrayList<DataFrame>();
    try {
      skipWhitespace();
      
      
    } catch ( IOException e ) {
      e.printStackTrace();
    }
    

    // do stuff
    return retval;
  }

 
}
