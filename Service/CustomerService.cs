using BusinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository iCustomerRepository;

        public CustomerService()
        {
            iCustomerRepository = new CustomerRepository();
        }

        public List<Customer> GetCustomers() => iCustomerRepository.GetCustomers();

        public Customer? Login(string email, string password) => iCustomerRepository.GetCustomerByEmail(email);

        public void CreateCustomer(Customer customer) => iCustomerRepository.CreateCustomer(customer);

        public void UpdateCustomer(Customer customer) => iCustomerRepository.UpdateCustomer(customer);

        public void DeleteCustomer(int id) => iCustomerRepository.DeleteCustomer(id);

        public Customer GetCustomerById(int id) => iCustomerRepository.GetCustomerById(id);
    }
}
