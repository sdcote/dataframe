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

import java.util.List;

import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.json.JsonFrameParser;


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
  public static List<DataFrame> marshal( String json ) throws MarshalException {
    List<DataFrame> retval = null;

    try {
      retval = new JsonFrameParser( json ).parse();
    } catch ( Exception e ) {
      System.out.println( "oops: " + e.getMessage() );
      throw new MarshalException( "Could not marshal JSON to DataFrame", e );
    }

    return retval;
  }

}
