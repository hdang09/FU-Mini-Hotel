using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();

        Customer? GetCustomerByEmail(string email);

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void DeleteCustomer(int id);

        Customer GetCustomerById(int id);

    }
}
