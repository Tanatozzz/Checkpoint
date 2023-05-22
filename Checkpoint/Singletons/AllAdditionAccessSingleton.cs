using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.Singletons
{
    public class AdditionAccess
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
    public class AllAdditionAccessSingleton
    {
        private static AllAdditionAccessSingleton _instance;
        private List<AdditionAccess> _additionalAccesses;

        private AllAdditionAccessSingleton()
        {
            _additionalAccesses = new List<AdditionAccess>();
        }

        public static AllAdditionAccessSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AllAdditionAccessSingleton();
                }
                return _instance;
            }
        }

        public List<AdditionAccess> AdditionalAccesses
        {
            get { return _additionalAccesses; }
            set { _additionalAccesses = value; }
        }
    }
}
