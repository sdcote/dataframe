/*
 * Copyright (c) 2016 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 */
package cookbook;

import coyote.commons.ByteUtil;
import coyote.dataframe.DataFrame;

/**
 * 
 */
public class IntegerField {

  /**
   * @param args
   */
  public static void main( String[] args ) {
    DataFrame frame = new DataFrame("MSG", 42);
    byte[] bytes = frame.getBytes();
    System.out.println( ByteUtil.dump(bytes));
  }

}
