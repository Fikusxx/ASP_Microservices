namespace Shop.Services.OrderAPI;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext db;

    public OrderRepository(IOrderRepository db)
    {
        this.db = db;
    }

    public Task<bool> AddOrder(OrderHeader orderHeader)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderPaymentStatus(int orderHeaderId, bool isPaid)
    {
        throw new NotImplementedException();
    }
}
