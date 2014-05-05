/*
 * Copyright (c) 2014 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe.selector;

import java.util.ArrayList;
import java.util.List;

import coyote.commons.SegmentFilter;
import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;


/**
 * Field Selectors allow for the quick retrieval of data from the hierarchy of
 * DataFrames.
 */
public class FieldSelector {

  /** The segment filter used to search for fields. */
  private SegmentFilter filter = null;




  /**
   * Create a selector with the given expression.
   * 
   * @param expression The segment filter expression to use for all selections
   */
  public FieldSelector( String expression ) {

    filter = new SegmentFilter( expression );
  }




  /**
   * Return a list of DataFields  from the given DataFrame matching the 
   * currently set expression.
   * 
   * @param frame The dataframe containing the source of the data
   * 
   * @return a non-null list of DataFields which match the currently set expression
   */
  public List<DataField> select( DataFrame frame )
  {
    List<DataField> retval = new ArrayList<DataField>();
    if( frame != null )
      recurse( frame, null, retval );
    return retval;
  }




  /**
   * Recurse onto the frame concatenating the field names according to their 
   * hierarchy and performing a check on the name to see if they match the set 
   * filter.
   * 
   * @param frame The current frame to check
   * @param token the current value of the concatenated field name
   * 
   * @param results The current set of field found to have matched the filter
   */
  private void recurse( DataFrame frame, String token, List<DataField> results )
  {
    for( int x = 0; x < frame.getFieldCount(); x++ ) {
      final DataField field = frame.getField( x );
      String fname = field.getName();

      if( fname == null )
        fname = "field" + x;

      if( token != null )
        fname = token + "." + fname;

      if( field.isFrame() )
        recurse( (DataFrame)field.getObjectValue(), fname, results );
      else {
        if( filter.matches( fname ) )
          results.add( field );
      }

    } // for each frame

  }

}
