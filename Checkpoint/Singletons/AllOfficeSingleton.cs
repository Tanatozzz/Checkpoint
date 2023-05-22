using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint.Models;

namespace Checkpoint.Singletons
{
    public class Office
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int IDCity { get; set; }
    }
    public class AllOfficesSingleton
    {
        private static AllOfficesSingleton _instance;
        private List<OfficeWithCityName> _offices;

        private AllOfficesSingleton()
        {
            // Приватный конструктор для предотвращения создания экземпляров извне
            _offices = new List<OfficeWithCityName>();
        }

        public static AllOfficesSingleton Instance
        {
            get
            {
                // Ленивая инициализация экземпляра синглтона
                if (_instance == null)
                {
                    _instance = new AllOfficesSingleton();
                }
                return _instance;
            }
        }

        public List<OfficeWithCityName> Offices
        {
            get { return _offices; }
            set { _offices = value; }
        }
    }

}
