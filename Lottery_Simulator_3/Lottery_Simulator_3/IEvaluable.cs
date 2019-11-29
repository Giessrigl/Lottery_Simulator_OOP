//-----------------------------------------------------------------------
// <copyright file="IEvaluable.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the IEvaluable interface.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// This is an interface for the evaluating modes.
    /// </summary>
    public interface IEvaluable
    {
        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        void PerformPreEvaluation();

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        void PerformEvaluation();
    }
}
