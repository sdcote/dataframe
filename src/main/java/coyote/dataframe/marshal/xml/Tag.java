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

/**
 * 
 */
public class Tag {
  private String name = null;
  private String namespace = null;
  private boolean closetag = false;
  private boolean emptytag = false;




  /**
   * @return the name
   */
  public String getName() {
    return name;
  }




  public boolean isCloseTag() {
    return closetag;
  }




  public boolean isOpenTag() {
    return !closetag;
  }




  /**
   * @param name the name to set
   */
  public void setName( String name ) {
    this.name = name;
  }




  /**
   * @return the namespace
   */
  public String getNamespace() {
    return namespace;
  }




  /**
   * @param namespace the namespace to set
   */
  public void setNamespace( String namespace ) {
    this.namespace = namespace;
  }




  /**
   * @param flag true to represent the tag as a terminator
   */
  public void setEndTag( boolean flag ) {
    closetag = flag;
  }




  /**
   * @return true if there is a value following this tag, false if it is empty
   */
  public boolean isEmptyTag() {
    return emptytag;
  }




  public boolean isNotEmptyTag() {
    return !emptytag;
  }




  /**
   * @param flag true, the tag is empty, false, the tag is followed by a value and an end tag.
   */
  public void setEmptyTag( boolean flag ) {
    emptytag = flag;
  }

}
