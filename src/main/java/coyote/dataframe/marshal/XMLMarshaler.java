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
package coyote.dataframe.marshal;

import java.util.List;

import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.xml.XmlFrameParser;


/**
 * 
 */
public class XMLMarshaler {

  /**
   * Generate a XML string from the given data frame.
   * 
   * @param frame The frame to marshal
   * 
   * @return A XML formatted string which can be marshaled back into a frame
   */
  public static String marshal( final DataFrame frame ) {
    return "";
  }




  /**
   * Marshal the given XML into a dataframe.
   * 
   * @param xml
   * 
   * @return Data frame containing the XML represented data
   */
  public static List<DataFrame> marshal( final String xml ) throws MarshalException {
    List<DataFrame> retval = null;

    try {
      retval = new XmlFrameParser( xml ).parse();
    } catch ( final Exception e ) {
      System.out.println( "oops: " + e.getMessage() );
      throw new MarshalException( "Could not marshal XML to DataFrame", e );
    }

    return retval;
  }

}
