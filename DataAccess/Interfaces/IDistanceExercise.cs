using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IDistanceExercise
    {
        public int? Distance { get; set; }
        public int? AvgKmTempo { get; set; }
        public int? Steps { get; set; }
        public int? AvgStepLength { get; set; }
        public int? AvgStepPerMin { get; set; }
    }
}
