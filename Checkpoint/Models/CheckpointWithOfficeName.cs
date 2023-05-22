using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint.Singletons;

namespace Checkpoint.Models
{
    public class CheckpointWithOfficeName : Checkpoint_
    {
        public string OfficeTitle { get; set; }
    }
}
