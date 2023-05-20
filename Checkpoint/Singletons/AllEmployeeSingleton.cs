using Checkpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint.Singletons
{
    public class AllEmployeesSingleton
    {
        private static AllEmployeesSingleton _instance;
        private List<EmployeeWithRoleName> _employees;

        private AllEmployeesSingleton()
        {
            // Приватный конструктор для предотвращения создания экземпляров извне
            _employees = new List<EmployeeWithRoleName>();
        }

        public static AllEmployeesSingleton Instance
        {
            get
            {
                // Ленивая инициализация экземпляра синглтона
                if (_instance == null)
                {
                    _instance = new AllEmployeesSingleton();
                }
                return _instance;
            }
        }

        public List<EmployeeWithRoleName> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
    }
}
