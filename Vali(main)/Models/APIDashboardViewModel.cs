namespace Vali_main_.Models
{
    public class APIDashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public decimal? TotalRevenue { get; set; }
        public int TotalProducts { get; set; }

        public List<MonthlyRevenueModel> MonthlyRevenue { get; set; }
    }
}
