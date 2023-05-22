using Checkpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.Singletons
{
    public class CheckpointRole
    {
        public int IDRole { get; set; }
        public int IDCheckpoint { get; set; }
        public DateTime DateAdd { get; set; }
    }
    public class AllCheckpointRoleSingleton
    {
        private static AllCheckpointRoleSingleton _instance;
        private List<CheckpointRoleWithTitleID> _checkpointRoles;

        private AllCheckpointRoleSingleton()
        {
            _checkpointRoles = new List<CheckpointRoleWithTitleID>();
        }

        public static AllCheckpointRoleSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AllCheckpointRoleSingleton();
                }
                return _instance;
            }
        }

        public List<CheckpointRoleWithTitleID> CheckpointRoles
        {
            get { return _checkpointRoles; }
            set { _checkpointRoles = value; }
        }
    }
}
