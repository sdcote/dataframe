package coyote.dataframe.marshal.xml;

import java.io.Writer;


/**
 * Controls the formatting of the XML output. Use one of the available constants.
 */
public abstract class XmlWriterConfig {

  /**
   * Write XML in its minimal form, without any additional whitespace. 
   * 
   * This is the default.
   */
  public static XmlWriterConfig MINIMAL = new XmlWriterConfig() {
    @Override
    public XmlWriter createWriter( final Writer writer ) {
      return new XmlWriter( writer );
    }
  };

  /**
   * Write formated XML, with each value on a separate line and an indentation 
   * of two spaces.
   */
  public static XmlWriterConfig PRETTY_PRINT = new XmlWriterConfig() {
    @Override
    public XmlWriter createWriter( final Writer writer ) {
      return new FormattedXmlWriter( writer );
    }
  };




  public abstract XmlWriter createWriter( Writer writer );

}
