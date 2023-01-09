using CustomerManager.Models;
using CustomerManager.Web.Services.Interface;
using Radzen;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace CustomerManager.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;
        private readonly NotificationService notificationService;
        private readonly IConfiguration configuration;
        public List<Customer> customers { get; set; }
        public readonly IEnumerable<CustomerCategory> customerCategories;

        public CustomerService(
                HttpClient _httpClient, 
                NotificationService _notificationService, 
                IConfiguration _configuration)
        {
            configuration = _configuration;
            httpClient = _httpClient;
            httpClient.BaseAddress = new Uri(configuration["CustomerApiBaseUrl"]);
            notificationService = _notificationService;
            customerCategories = GetCustomerOptions();
            
        }

        public async Task Create(Customer customer)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Customer/create", customer);
            await ShowNotification(response, "Customer successfully created");
            await List();
        }
        public async Task Update(Customer customer)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync("Customer/update", customer);
            await ShowNotification(response, "Customer successfully updated");
            await List();
        }
        public async Task Delete(int customerId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"Customer/delete?customerId={customerId}");
            await ShowNotification(response, "Customer successfully deleted");
            await List();
        }
        public async Task List()
        {
            customers = await httpClient.GetFromJsonAsync<List<Customer>>("Customer/list");
        }

        private async Task ShowNotification(HttpResponseMessage response, string successDetail)
        {
            if (response.IsSuccessStatusCode)
            {
                var msg = new NotificationMessage { 
                                Style = configuration["NotificationStyle"], 
                                Severity = NotificationSeverity.Success,
                                Summary = "Success", 
                                Detail = successDetail, 
                                Duration = Convert.ToDouble(configuration["NotificationDuration"]) };

                notificationService.Notify(msg);
            }
            else
            {
                var msg = new NotificationMessage {
                                Style = configuration["NotificationStyle"],
                                Severity = NotificationSeverity.Error,
                                Summary = "Error",
                                Detail = await response.Content.ReadAsStringAsync(),
                                Duration = Convert.ToDouble(configuration["NotificationDuration"]) };

                notificationService.Notify(msg);
            }
        }

        private List<CustomerCategory> GetCustomerOptions()
        {
            var values = (CustomerCategory[])Enum.GetValues(typeof(CustomerCategory));
            return values.ToList();
        }
    }
}
