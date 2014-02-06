/*
 * Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe;

/**
 * FrameFormatter - Interface
 * 
 * RFC 4627 data formatting
 * public static String toJSON(final DataFrame packet, final String name)
 */
public interface FrameFormatter {
	
	public String format(DataFrame frame);

}
