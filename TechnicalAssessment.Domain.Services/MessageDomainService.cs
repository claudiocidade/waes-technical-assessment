﻿//  <copyright file="MessageDomainService.cs" company="WAES">
//  Copyright (c) All rights reserved.
//  </copyright>
namespace TechnicalAssessment.Domain.Services
{
    using System;
    using System.Linq;
    using System.Text;
    using TechnicalAssessment.Domain.Services.Contracts;

    /// <summary>
    /// Manages <see cref="Message"/> domain logic implementation.
    /// </summary>
    public class MessageDomainService : IMessageDomainService
    {
        /// <summary>
        /// Analyzes the messages and provide comparison diagnostics.
        /// </summary>
        /// <param name="left">Left side message.</param>
        /// <param name="right">Right side message.</param>
        /// <returns>A string containing information about the comparison process.</returns>
        string IMessageDomainService.AnalyzeMessages(Message left, Message right)
        {
            if (left == null || right == null)
            {
                return "Session must have both sides";
            }

            byte[] leftSide = Convert.FromBase64String(left.Data);

            byte[] rightSide = Convert.FromBase64String(right.Data);

            if (leftSide.SequenceEqual(rightSide))
            {
                return "Content is the same";
            }

            if (!leftSide.Length.Equals(rightSide.Length))
            {
                return "Sizes are different";
            }
            else
            {
                int diffStart = -1;

                int diffEnd = -1;

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < leftSide.Length; i++)
                {
                    if (!leftSide[i].Equals(rightSide[i]))
                    {
                        if (diffStart == -1)
                        {
                            diffStart = i;
                        }
                    }
                    else
                    {
                        if (diffStart >= 0)
                        {
                            diffEnd = i;
                        }
                    }

                    if (diffStart >= 0 && diffEnd >= 0)
                    {
                        sb.AppendLine($"Differences found starting at {diffStart} ending at {diffEnd - 1}");

                        diffStart = diffEnd = -1;
                    }
                }

                return sb.ToString();
            }
        }
    }
}