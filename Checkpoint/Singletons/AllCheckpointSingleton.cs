using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint.Models;

namespace Checkpoint.Singletons
{
    public class Checkpointt
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int IDOffice { get; set; }
        public bool IsActive { get; set; }
    }
    public class AllCheckpointsSingleton
    {
        private static AllCheckpointsSingleton _instance;
        private List<CheckpointWithOfficeName> _checkpoints;

        private AllCheckpointsSingleton()
        {
            _checkpoints = new List<CheckpointWithOfficeName>();
        }

        public static AllCheckpointsSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AllCheckpointsSingleton();
                }
                return _instance;
            }
        }

        public List<CheckpointWithOfficeName> Checkpoints
        {
            get { return _checkpoints; }
            set { _checkpoints = value; }
        }
    }

}
