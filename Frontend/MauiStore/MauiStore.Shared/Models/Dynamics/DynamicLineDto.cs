using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStore.Shared.Models.Dynamics
{
    public class DynamicLineDto
    {
        public string Article { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public bool Active { get; set; }

        public bool Backorder { get; set; }
    }
}
