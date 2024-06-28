using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            try
            {
                using var context = new FuminiHotelManagementContext();
                customers = context.Customers.ToList();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        } 

        public static void SaveCustomer(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                customer.CustomerStatus = 1;
                context.Customers.Add(customer);
                context.SaveChanges();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCustomer(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var deletedCustomer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
                if (deletedCustomer != null)
                {
                    context.Customers.Remove(deletedCustomer);
                }
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Customer? GetCustomerById(int id)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(customer => customer.CustomerId.Equals(id));
        }

        public static Customer? GetCustomerByEmail(string email)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(customer => customer.EmailAddress.Equals(email));
        }
    }
}
