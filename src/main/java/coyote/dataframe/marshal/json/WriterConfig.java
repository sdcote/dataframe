package coyote.dataframe.marshal.json;

import java.io.Writer;


/**
 * Controls the formatting of the JSON output. Use one of the available constants.
 */
public abstract class WriterConfig {

  /**
   * Write JSON in its minimal form, without any additional whitespace. This is the default.
   */
  public static WriterConfig MINIMAL = new WriterConfig() {
    @Override
    public JsonWriter createWriter( final Writer writer ) {
      return new JsonWriter( writer );
    }
  };

  /**
   * Write JSON in pretty-print, with each value on a separate line and an indentation of two
   * spaces.
   */
  public static WriterConfig PRETTY_PRINT = new WriterConfig() {
    @Override
    public JsonWriter createWriter( final Writer writer ) {
      return new PrettyPrinter( writer );
    }
  };




  public abstract JsonWriter createWriter( Writer writer );

}