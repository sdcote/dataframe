/*
 * Copyright (c) 2014 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 */
package coyote.dataframe.selector;

import java.util.ArrayList;
import java.util.List;

import coyote.commons.SegmentFilter;
import coyote.dataframe.DataFrame;


/**
 * Frame selectors allow for the quick retrieval of data from the hierarchy of
 * DataFrames.
 * 
 * <p>In order not to lose the context of the frame and any data represeted by 
 * the frames position in the hierarchy, it is possible to add the selection 
 * path to the frame as a segmented token in a field name of the callers 
 * choosing.
 * 
 * <p>The selection path is simply a segmented name which represents where
 * in the heirarchy the returned frames existed in the parent. This is 
 * critical in determining the context of the frame. For example, each frame 
 * may represent a customer record, but where the record occurs determines if 
 * it is a corporate customer or a private customer. If the frame existing 
 * under the private node / frame, then that classifier can be included in 
 * the frame and it can be differentiated later by parsing the "private" 
 * token from the path.  
 */
public class FrameSelector extends AbstractSelector {
  private String pathName = null;




  /**
   * Create a selector with the given expression.
   * 
   * @param expression The segment filter expression to use for all selections
   */
  public FrameSelector(final String expression) {
    filter = new SegmentFilter(expression);
  }




  /**
   * Create a selector with the given expression and storing the selector path 
   * in a field with the given name.
   * 
   * @param expression The segment filter expression to use for all selections
   * @param pathName The name of the fiel in which to store the selector path 
   *        of the frame.
   */
  public FrameSelector(String expression, String pathName) {
    this(expression);
    this.pathName = pathName;
  }




  /**
   * Count how many frame matches there are.
   * 
   * @param frame The dataframe containing the source of the data
   * 
   * @return the number of frames matching the current expression
   */
  public int count(final DataFrame frame) {
    final List<DataFrame> retval = new ArrayList<DataFrame>();
    if (frame != null) {
      recurseFrames(frame, null, retval, pathName);
    }
    return retval.size();
  }




  /**
   * Return a list of DataFrames from the given DataFrame matching the 
   * currently set expression.
   * 
   * @param frame The dataframe containing the source of the data
   * 
   * @return a non-null list of DataFrames which match the currently set expression
   */
  public List<DataFrame> select(final DataFrame frame) {
    final List<DataFrame> retval = new ArrayList<DataFrame>();
    if (frame != null) {
      recurseFrames(frame, null, retval, pathName);
    }
    return retval;
  }




  /**
   * Return a list of DataFrames from the given DataFrame matching the 
   * currently set expression and publish the selection path in each returned
   * frame.
   * 
   * <p>The selection path is simply a segmented name which represents where
   * in the heirarchy the returned frames existed. This is critical in 
   * determining the context of the frame. For example, each frame may 
   * represent a customer record, but where the record occurs determines if it 
   * is a corporate customer or a private customer. If the frame existing 
   * under the private node / frame, then that classifier can be included in 
   * the frame and it can be differentiated later by parsing the "private" 
   * token from the path. 
   * 
   * @param frame The dataframe containing the source of the data
   * @param pathName The nme of the field into which the selection path name 
   *        is stored
   * 
   * @return a non-null list of DataFrames which match the currently set expression
   */
  public List<DataFrame> select(final DataFrame frame, String pathName) {
    final List<DataFrame> retval = new ArrayList<DataFrame>();
    if (frame != null) {
      recurseFrames(frame, null, retval, pathName);
    }
    return retval;
  }

}
