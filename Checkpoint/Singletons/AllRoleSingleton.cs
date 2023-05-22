using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.Singletons
{
    public class Role
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }

    }
    public class AllRoleSingleton
    {
        private static AllRoleSingleton _instance;
        private List<Role> _roles;

        private AllRoleSingleton()
        {
            _roles = new List<Role>();
        }

        public static AllRoleSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AllRoleSingleton();
                }
                return _instance;
            }
        }

        public List<Role> Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
    }
}
