using Checkpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.Singletons
{
    public class CheckpointAdditionalAccess
    {
        public int IDAdditionalAccess { get; set; }
        public int IDCheckpoint { get; set; }
        public DateTime DateAdd { get; set; }
    }
    public class AllCheckpointAdditionalAccessSingleton
    {
        private static AllCheckpointAdditionalAccessSingleton _instance;
        private List<CheckpointAdditionalAccessWithTitleID> _checkpointAdditionalAccesses;

        private AllCheckpointAdditionalAccessSingleton()
        {
            _checkpointAdditionalAccesses = new List<CheckpointAdditionalAccessWithTitleID>();
        }

        public static AllCheckpointAdditionalAccessSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AllCheckpointAdditionalAccessSingleton();
                }
                return _instance;
            }
        }

        public List<CheckpointAdditionalAccessWithTitleID> CheckpointAdditionalAccesses
        {
            get { return _checkpointAdditionalAccesses; }
            set { _checkpointAdditionalAccesses = value; }
        }
    }
}
