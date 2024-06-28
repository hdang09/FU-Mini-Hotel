using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();

        public Customer? GetCustomerByEmail(string email) => CustomerDAO.GetCustomerByEmail(email);

        public void CreateCustomer(Customer customer) => CustomerDAO.SaveCustomer(customer);

        public void UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);

        public void DeleteCustomer(int id) => CustomerDAO.DeleteCustomer(id);

        public Customer GetCustomerById(int id) => CustomerDAO.GetCustomerById(id);
    }
}
