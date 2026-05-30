using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStore.Shared.Models.Dynamics
{
    public class DynamicDetailDto
    {
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Total { get; set; }

        public bool Active { get; set; }

        public DynamicResultsDto Results { get; set; } = new();

        public List<DynamicLineDto> Lines { get; set; } = new();
    }
}