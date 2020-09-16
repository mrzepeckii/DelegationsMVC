using System;
using System.Collections.Generic;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }
    }
}
