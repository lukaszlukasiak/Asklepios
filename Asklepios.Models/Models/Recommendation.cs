using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class Recommendation
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public VisitSummary VisitSummary { get; set; }

    }
}
