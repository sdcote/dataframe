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

namespace Coyote.DataFrame {

    /// <summary>
    /// Exception thrown when there is a problem with DataFrame operations.
    /// </summary>
    public sealed class DataFrameException : Exception {



        /// <summary>
        /// Constructor with no message.
        /// </summary>
        public DataFrameException() : base() {
        }




        /// <summary>
        /// Constructor with a user message
        /// </summary>
        /// <param name="message">The text of the message.</param>
        public DataFrameException( string message ) : base( message ) {
        }




        /// <summary>
        /// Constructor with a user message and a nested throwable object.
        /// </summary>
        /// <param name="message">The text of the message.</param>
        /// <param name="excptn">The throwable object (exception?) to nest in this exception.</param>
        public DataFrameException( string message, System.Exception excptn ) : base( message, excptn ) {
        }

    }


}