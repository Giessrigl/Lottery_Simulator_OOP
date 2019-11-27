//-----------------------------------------------------------------------
// <copyright file="Mode.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This file contains an abstract class for Mode objects.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// Super Class for the mode objects initialized in the mode admin.
    /// </summary>
    public abstract class Mode : IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the Mode class.
        /// </summary>
        /// <param name="title">The name of the Mode object.</param>
        /// <param name="abbreviation">The key for the Mode object (has to be unique among them).</param>
        /// <param name="uniqueChars">The keys that are already in use to prevent not calling the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        /// <exception cref="ArgumentNullException">If the title has no actual name or the unique char array is null.</exception>
        /// <exception cref="ArgumentException">If the abbreviation has already be used.</exception>
        public Mode(string title, char abbreviation, char[] uniqueChars, Lottery lotto)
        {
            if (uniqueChars == null)
            {
                throw new ArgumentNullException(nameof(uniqueChars));
            }

            for (int i = 0; i < uniqueChars.Length; i++)
            {
                if (uniqueChars[i] == abbreviation)
                {
                    throw new ArgumentException("The same abbreviation must not be used multiple times!", nameof(abbreviation));
                }
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title), $"The title of the mode with the character {abbreviation} must have a name.");
            }

            for (int i = 0; i < uniqueChars.Length; i++)
            {
                if (uniqueChars[i] == '\0')
                {
                    uniqueChars[i] = abbreviation;
                    break;
                }
            }

            this.Title = title;
            this.Abbreviation = abbreviation;
            this.Lotto = lotto;
            this.Render = new ModeRenderer();
        }

        /// <summary>
        /// Gets or sets the Title of a Mode object.
        /// </summary>
        /// <value>
        /// The title of the mode object.
        /// </value>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Abbreviation of a Mode object.
        /// The Abbreviations have to be unique among themselves.
        /// </summary>
        /// <value>
        /// The key that opens the modes execute method.
        /// </value>
        public char Abbreviation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Lotto property.
        /// </summary>
        /// <value>
        /// The Lottery variable.
        /// </value>
        public Lottery Lotto
        {
            get;
            private set;
        }

        public ModeRenderer Render
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the InputChecker property.
        /// </summary>
        /// <value>
        /// The CheckUserInput variable.
        /// </value>
        public NumbersChecker NumberChecker
        {
            get;
            private set;
        }

        public virtual void Execute()
        {

        }
    }
}
