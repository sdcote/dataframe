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
package coyote.dataframe.marshal;

import java.util.Map;
import java.util.Properties;

import coyote.dataframe.DataFrame;


/**
 * This class marshals between Java properties and DataFrames.
 * 
 * @author Steve Cote
 */
public class PropertyFrame
{

  /**
   * 
   * @param source
   * 
   * @return
   */
  public DataFrame marshal( Properties source )
  {
    DataFrame retval = new DataFrame();

    if( source != null ) {

      for( Map.Entry<?, ?> entry : source.entrySet() ) {

        String key = entry.getKey().toString();

        Object value = entry.getValue();

        if( value != null ) {
          
          // Could this really happen?
          if( value instanceof Properties ) value = marshal( (Properties)value );
          
          retval.add( key, value );
        }

      }
    }
    return retval;
  }




  /**
   * 
   * @param frame
   * 
   * @return
   */
  public Properties marshal( DataFrame frame )
  {
    return null;
  }

}
