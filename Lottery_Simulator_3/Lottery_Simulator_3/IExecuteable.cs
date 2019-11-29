//-----------------------------------------------------------------------
// <copyright file="IExecuteable.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the IExecuteable interface.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// This is an interface for the executing modes.
    /// </summary>
    public interface IExecuteable
    {
        /// <summary>
        /// This method should start the modes purpose.
        /// </summary>
        void Execute();
    }
}
