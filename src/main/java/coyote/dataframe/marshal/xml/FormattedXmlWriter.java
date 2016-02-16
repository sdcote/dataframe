package coyote.dataframe.marshal.xml;

import java.io.IOException;
import java.io.Writer;


class FormattedXmlWriter extends XmlWriter {

  private final char[] indentChars = { ' ', ' ' };
  private int indent;




  FormattedXmlWriter( final Writer writer ) {
    super( writer );
  }




  public void writeIndent() throws IOException {
    for ( int i = 0; i < indent; i++ ) {
      writer.write( indentChars );
    }
  }




  public void writeFieldOpen() throws IOException {
    writeIndent();
  }




  private void writeNewLine() throws IOException {
    writer.write( '\n' );
  }




  @Override
  public void writeFieldClose() throws IOException {
    writeNewLine();
  }




  @Override
  public void writeFrameClose() throws IOException {
    indent--;
    writeIndent();
  }




  @Override
  public void writeFrameOpen() throws IOException {
    indent++;
    writeNewLine();
  }

}
