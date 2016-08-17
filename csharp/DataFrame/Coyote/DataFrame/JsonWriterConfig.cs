#region license
//
// Linkage - a multi-broker .NET messaging API
// Copyright (c) 2004, 2011 Stephan D. Cote
//
// All rights reserved. This program and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html
//
// Contributors:
//   Stephan D. Cote 
//      - Initial API and implementation
//
// Contact Information
//   https://code.google.com/p/linker/
//
#endregion
using System;

namespace Coyote.DataFrame.JSON
{

    /// <summary>
    /// Controls the formatting of the JSON output. Use one of the available constants.
    /// </summary>
    public abstract class JsonWriterConfig
    {
        public static JsonWriterConfig MINIMAL = new MinCfg();
        public static JsonWriterConfig FORMATTED = new FmtCfg();
        /// <summary>
        /// Return a JSON Writer with the appropriate configuration.
        /// </summary>
        /// <param name="writer">The writer to use to write output.</param>
        /// <returns></returns>
        public abstract JsonWriter CreateWriter(Writer writer);
    }

    /// <summary>
    /// Write JSON in its minimal form, without any additional whitespace.
    /// </summary>
    private sealed class MinCfg : JsonWriterConfig
    {
        public JsonWriter createWriter(final Writer writer)
        {
            return new JsonWriter(writer);
        }
    }

    /// <summary>
    /// Generate a nicely formatted (and indented) JSON string from the given data frame.
    /// </summary>
    private sealed class FmtCfg : JsonWriterConfig
    {
        public JsonWriter createWriter(final Writer writer)
        {
            return new FormattedJsonWriter(writer);
        }
    }

}



