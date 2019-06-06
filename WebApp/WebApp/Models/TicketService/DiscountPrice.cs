using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models.TicketService
{
    public static class DiscountPrice
    {
        private static Dictionary<PassengerType, int> discounts = new Dictionary<PassengerType, int>()
        {
            {PassengerType.Penzioner, 30 },
            {PassengerType.Student,  20},
            {PassengerType.Regular, 0}
        };

        public static int GetDiscount(PassengerType type)
        {
            return discounts[type];
        }

    }
}