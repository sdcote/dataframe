#region license
// Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
// 
// This program and the accompanying materials are made available under the 
// terms of the MIT License which accompanies this distribution, and is 
// available at http://creativecommons.org/licenses/MIT/
// *
// Contributors:
//   Stephan D. Cote 
//      - Initial API and implementation
#endregion
using System;
using System.IO;

namespace Coyote.DataFrame.JSON
{



    /// <summary>
    /// Controls the formatting of the JSON output. Use one of the available constants.
    /// </summary>
    public abstract class JsonWriterConfig
    {

        public static JsonWriterConfig MINIMAL = new MinCfg();
        public static JsonWriterConfig FORMATTED = new FmtCfg();

        public abstract JsonWriter createWriter(TextWriter writer);
    }

    sealed class MinCfg : JsonWriterConfig
    {
        /// <summary>
        /// Write JSON in its minimal form, without any additional whitespace.
        /// </summary>
        /// <param name="writer">The writer to use for output.</param>
        /// <returns></returns>
        public override JsonWriter createWriter(TextWriter writer)
        {
            return new JsonWriter(writer);
        }

    }

    sealed class FmtCfg : JsonWriterConfig
    {
        /// <summary>
        /// Generate a nicely formatted (and indented) JSON string from the given data frame.
        /// </summary>
        /// <param name="writer">The writer to use for output.</param>
        /// <returns></returns>
        public override JsonWriter createWriter(TextWriter writer)
        {
            return new FormattedJsonWriter(writer);
        }

    }
}
