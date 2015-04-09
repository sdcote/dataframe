package coyote.dataframe.marshal.json;

import java.io.IOException;
import java.io.Writer;

class PrettyPrinter extends JsonWriter {

	private final char[] indentChars = { ' ', ' ' };
	private int indent;




	PrettyPrinter(final Writer writer) {
		super(writer);
	}




	@Override
	public void writeArrayOpen() throws IOException {
		indent++;
		writer.write('[');
		writeNewLine();
	}




	@Override
	public void writeArrayClose() throws IOException {
		indent--;
		writeNewLine();
		writer.write(']');
	}




	@Override
	public void writeArraySeparator() throws IOException {
		writer.write(',');
		writeNewLine();
	}




	@Override
	public void writeObjectOpen() throws IOException {
		indent++;
		writer.write('{');
		writeNewLine();
	}




	@Override
	public void writeObjectClose() throws IOException {
		indent--;
		writeNewLine();
		writer.write('}');
	}




	@Override
	public void writeMemberSeparator() throws IOException {
		writer.write(':');
		writer.write(' ');
	}




	@Override
	public void writeObjectSeparator() throws IOException {
		writer.write(',');
		writeNewLine();
	}




	private void writeNewLine() throws IOException {
		writer.write('\n');
		for (int i = 0; i < indent; i++) {
			writer.write(indentChars);
		}
	}

}
