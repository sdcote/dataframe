/*
 * Copyright (c) 2017 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 */
package coyote.commons;

import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;


/**
 * 
 */
public class DataFrameUtil {

  /**
   * Flatten any hierarchy into a single level.
   * 
   * <p>The field names of child frames are concatenated using the '.' character
   * as is common practice in property file key naming.
   * 
   * @param frame The source of the data
   * 
   * @return a dataframe with its hierarchy flattened to a single level.
   */
  public static DataFrame flatten(DataFrame frame) {
    DataFrame retval = new DataFrame();
    if (frame != null)
      recurse(frame, null, retval);
    return retval;
  }




  /**
   * Recurse into the a dataframe, building a target frame as it goes.
   * 
   * <p>The hierarchy of the dataframe is represented in the naming of the 
   * property values using the '.' to delimit each recursion into the frame.
   * 
   * @param source The frame being recursed into, providing data for the target 
   * @param token The current build of the name of the property
   * @param target The frame into which values are placed.
   */
  private static void recurse(DataFrame source, String token, DataFrame target) {
    for (int x = 0; x < source.getFieldCount(); x++) {
      final DataField field = source.getField(x);
      String fname = field.getName();

      if (fname == null)
        fname = Integer.toString(x);

      if (token != null)
        fname = token + "." + fname;

      if (field.isFrame()) {
        DataFrame childFrame = (DataFrame)field.getObjectValue();
        if (childFrame != null) {
          recurse(childFrame, fname, target);
        }
      } else {
        target.set(fname, field.getObjectValue());
      }
    } // for each frame
  }




  /**
   * Convenience method to perform a case insensitive search for a named field 
   * in a data frame and return its value as a string.
   * 
   * @param name the name of the field to search
   * @param frame the data frame in which to search
   * 
   * @return the string value of the first found field with that name or null 
   *         if the field is null, the name is null or the field with that 
   *         name was not found.
   */
  public static String findString(String name, DataFrame frame) {
    if (name != null) {
      for (DataField field : frame.getFields()) {
        if (equalsIgnoreCase(name, field.getName())) {
          return field.getStringValue();
        }
      }
    }
    return null;
  }




  /**
   * A null-tolerant version of the Java String method of the same name.
   * 
   * <p>This method will accept null values, returning true if both values are 
   * null and false if one is and the other is not.
   * 
   * @param source the source of the check
   * @param target the string against which to check
   * 
   * @return true if source and target are equal without considering case, 
   *         false otherwise.
   */
  private static boolean equalsIgnoreCase(String source, String target) {
    boolean retval;
    if (source == null) {
      if (target == null) {
        retval = true;
      } else {
        retval = false;
      }
    } else {
      if (target == null) {
        retval = false;
      } else {
        retval = source.equalsIgnoreCase(target);
      }
    }
    return retval;
  }


  /**
   * Convenience method to perform a case insensitive search for a named field
   * in a data frame and return its value as a string or the default value if
   * it is not found
   *
   * @param name         the name of the field to search
   * @param frame        the data frame in which to search
   * @param defaultValue the default value to return if a field with that name
   *                     was not found
   * @return the string value of the first found field with that name or the
   * given default value if the field is null, the name is null or the field
   * with that name was not found.
   */
  public static String findString(String name, DataFrame frame, String defaultValue) {
    String retval = defaultValue;
    if (name != null) {
      for (DataField field : frame.getFields()) {
        if (equalsIgnoreCase(name, field.getName())) {
          retval = field.getStringValue();
          break;
        }
      }
    }
    return retval;
  }

}
