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
package coyote.dataframe.marshal;

import java.io.BufferedWriter;
import java.io.StringWriter;
import java.util.List;

import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.json.JsonFrameParser;
import coyote.dataframe.marshal.json.JsonWriter;
import coyote.dataframe.marshal.json.WriterConfig;

/**
 * 
 */
public class JSONMarshaler {

	/**
	 * Marshal the given JSON into a dataframe.
	 * 
	 * @param json
	 * 
	 * @return Data frame containing the JSON represented data
	 */
	public static List<DataFrame> marshal(final String json) throws MarshalException {
		List<DataFrame> retval = null;

		try {
			retval = new JsonFrameParser(json).parse();
		} catch (final Exception e) {
			System.out.println("oops: " + e.getMessage());
			throw new MarshalException("Could not marshal JSON to DataFrame", e);
		}

		return retval;
	}




	public static String marshal(final DataFrame frame) {
		return writeFrame(frame, WriterConfig.MINIMAL);
	}




	/**
	 * Use the JSON Writer to output a nicely formatted JSON string.
	 * 
	 * @param frame The frame to marshal
	 * 
	 * @return A JSON formatted string which can be marshaled back into a frame
	 */
	public static String prettyPrint(final DataFrame frame) {
		return writeFrame(frame, WriterConfig.PRETTY_PRINT);
	}




	/**
	 * 
	 * @param frame
	 * @param config
	 * 
	 * @return
	 */
	private static String writeFrame(final DataFrame frame, final WriterConfig config) {

		// create string writer
		final StringWriter sw = new StringWriter();
		final JsonWriter writer = config.createWriter(new BufferedWriter(sw));

		//

		// do stuff with the writer

		//

		// return sw.getBuffer().toString();

		return frame.toString();
	}
}
