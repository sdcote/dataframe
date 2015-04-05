package coyote.commons.json;

import java.io.IOException;
import java.io.Writer;


class PrettyPrinter extends JsonWriter {

  private final char[] indentChars = { ' ', ' ' };
  private int indent;

  PrettyPrinter( Writer writer ) {
    super( writer );
  }

  @Override
  protected void writeArrayOpen() throws IOException {
    indent++;
    writer.write( '[' );
    writeNewLine();
  }

  @Override
  protected void writeArrayClose() throws IOException {
    indent--;
    writeNewLine();
    writer.write( ']' );
  }

  @Override
  protected void writeArraySeparator() throws IOException {
    writer.write( ',' );
    writeNewLine();
  }

  @Override
  protected void writeObjectOpen() throws IOException {
    indent++;
    writer.write( '{' );
    writeNewLine();
  }

  @Override
  protected void writeObjectClose() throws IOException {
    indent--;
    writeNewLine();
    writer.write( '}' );
  }

  @Override
  protected void writeMemberSeparator() throws IOException {
    writer.write( ':' );
    writer.write( ' ' );
  }

  @Override
  protected void writeObjectSeparator() throws IOException {
    writer.write( ',' );
    writeNewLine();
  }

  private void writeNewLine() throws IOException {
    writer.write( '\n' );
    for( int i = 0; i < indent; i++ ) {
      writer.write( indentChars );
    }
  }

}
