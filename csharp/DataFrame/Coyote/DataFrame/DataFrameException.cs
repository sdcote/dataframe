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

namespace Coyote.DataFrame
{

    ///
    // * Exception thrown when there is a problem with DataFrame operations.
    // 
    public sealed class DataFrameException : Exception
    {



        //  *
        //   * Constructor with no message.
        //   
        public DataFrameException() : base()
        {
        }




        //  *
        //   * Constructor with a user message
        //   *
        //   * @param message The text of the message.
        //   
        public DataFrameException(string message) : base(message)
        {
        }




        //  *
        //   * Constructor with a user message and a nested throwable object.
        //   *
        //   * @param message The text of the message.
        //   * @param excptn The throwable object (exception?) to nest in this exception
        //   
        public DataFrameException(string message, System.Exception excptn) : base(message, excptn)
        {
        }

    }


}