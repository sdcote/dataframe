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
package coyote.dataframe;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;


/**
 * This class models a set of rows and allows for their uniform treatment.
 */
public class FrameSet {

  private List<DataFrame> rows = new ArrayList<DataFrame>();
  private Set<String> columns = new HashSet<String>();




  /**
   * Add the given frame to this set.
   * 
   * @param frame The frame to add
   */
  public void add( DataFrame frame ) {
    // add the frame to the collection
    rows.add( frame );

    // Add all the named fields to the 
    for ( String name : frame.getNames() ) {
      columns.add( name );
    }
  }




  /**
   * Access a list of all the named fields in all the frames in this set.
   * 
   * <p>Note that not all fields may be represented in all rows. It is 
   * possible that a frame in the set may have no named fields and its data 
   * will be inaccessible by name. This method attempts to provide uniform 
   * columnar access to a set of frames for those frames with names.</p>
   * 
   * <p>No assertion can be made as to the order of the names in the returned 
   * list.</p>
   * 
   * @return A list of unique names for all the named fields of all the frames 
   * in this set.
   */
  public List<String> getColumns() {
    List<String> retval = new ArrayList<String>();
    retval.addAll( columns );
    return retval;
  }




  /**
   * @return the number of frames in this set
   */
  public int size() {
    return rows.size();
  }




  /**
   * Returns the DataFrame at the specified position in this set.
   * 
   * @param index - index of the element to return
   * 
   * @return the element at the specified position in this list 
   * 
   * @throws IndexOutOfBoundsException - if the index is out of range (index < 0 || index >= size())
   */
  public DataFrame get( int index ) {
    return rows.get( index );
  }

}
