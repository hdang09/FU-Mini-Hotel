using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();

        Customer? Login(string email, string password);

        void CreateCustomer(Customer customer);

        void DeleteCustomer(int id);

        Customer GetCustomerById(int id);

        void UpdateCustomer(Customer customer);
    }
}
