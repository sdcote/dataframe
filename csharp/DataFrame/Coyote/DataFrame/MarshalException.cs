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

    public class MarshalException : SystemException
    {



        public MarshalException()
        {
        }




        public MarshalException(string msg) : base(msg)
        {
        }




        public MarshalException(string msg, System.Exception t) : base(msg, t)
        {
        }

    }
}