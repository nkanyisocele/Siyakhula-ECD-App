using System;
using System.Collections.Generic;

namespace Siyakhula.Shared.Services
{
    public class SubsidyCalculator
    {
        // Strictly constrained system constant from the project spec
        private const decimal DAILY_SUBSIDY_PER_CHILD = 24.00m;

       
        /// Safely computes the payout amount based on total verified attendances.
        /// Implements proactive input boundary validation to eliminate logical errors.
        
        public decimal CalculateSubsidy(int presentCount)
        {
            if (presentCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(presentCount), "Attendance counts cannot evaluate to a negative value.");
            }

            return presentCount * DAILY_SUBSIDY_PER_CHILD;
        }
    }
}
