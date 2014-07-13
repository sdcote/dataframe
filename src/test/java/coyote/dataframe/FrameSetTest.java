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

import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.ArrayList;
import java.util.List;

import org.junit.Test;


/**
 * 
 */
public class FrameSetTest {

  /**
   * Test method for {@link coyote.dataframe.FrameSet#add(coyote.dataframe.DataFrame)}.
   */
  @Test
  public void testAdd() {

    FrameSet set = new FrameSet();

    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", "first" );
    frame1.add( "beta", "second" );
    set.add( frame1 );
    DataFrame frame2 = new DataFrame();
    frame2.add( "alpha", "third" );
    frame2.add( "beta", "fourth" );
    set.add( frame2 );

    assertTrue( set.size() == 2 );
  }




  /**
   * Test method for {@link coyote.dataframe.FrameSet#getColumns()}.
   */
  @Test
  public void testGetColumns() {
    FrameSet set = new FrameSet();

    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", "first" );
    frame1.add( "beta", "second" );
    set.add( frame1 );
    DataFrame frame2 = new DataFrame();
    frame2.add( "gamma", "third" );
    frame2.add( "delta", "fourth" );
    set.add( frame2 );
    assertTrue( set.size() == 2 );
    List<String> columnNames = set.getColumns();
    assertNotNull( columnNames );
    assertTrue( columnNames.size() == 4 );
  }




  /**
   * Test method for {@link coyote.dataframe.FrameSet#get()}.
   */
  @Test
  public void testGet() {
    FrameSet set = new FrameSet();
    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", "first" );
    set.add( frame1 );
    DataFrame frame2 = new DataFrame();
    frame2.add( "beta", "second" );
    set.add( frame2 );
    assertTrue( set.size() == 2 );

    assertTrue( set.get( 0 ) == frame1 );
    assertTrue( set.get( 1 ) == frame2 );

  }




  /**
   * Test the addAll method
   */
  @Test
  public void testAddAll() {
    List<DataFrame> list = new ArrayList<DataFrame>();

    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", "first" );
    list.add( frame1 );
    DataFrame frame2 = new DataFrame();
    frame2.add( "beta", "second" );
    list.add( frame2 );

    // addAll(Collection<DataFrame>) is called by the constructor
    FrameSet set = new FrameSet( list );
    assertTrue( set.size() == 2 );
    assertTrue( set.get( 0 ) == frame1 );
    assertTrue( set.get( 1 ) == frame2 );

    // try addAll on it own
    set = new FrameSet();
    set.addAll( list );
    assertTrue( set.size() == 2 );
    assertTrue( set.get( 0 ) == frame1 );
    assertTrue( set.get( 1 ) == frame2 );
  }
}
