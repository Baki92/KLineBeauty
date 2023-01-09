using CustomerManager.Models;

namespace CustomerManager.Web.Services.Interface
{
    public interface ICustomerService
    {
        public Task Create(Customer customer);
        public Task Update(Customer customer);
        public Task Delete(int customerId);
        public Task List();
    }
}
