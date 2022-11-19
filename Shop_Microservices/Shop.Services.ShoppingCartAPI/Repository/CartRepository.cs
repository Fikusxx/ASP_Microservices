using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services.ShoppingCartAPI;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext db;
    private IMapper mapper;

    public CartRepository(ApplicationDbContext db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public async Task<bool> ClearCart(int id)
    {
        var cartHeader = await db.CartHeaders.FirstOrDefaultAsync(x => x.Id == id);

        if (cartHeader != null)
        {
            db.CartDetails.RemoveRange(db.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id));
            db.CartHeaders.Remove(cartHeader);
            await db.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<CartDTO> CreateUpdateCart(CartDTO cartDTO)
    {
        var cart = mapper.Map<Cart>(cartDTO);

        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == cartDTO.CartDetails.FirstOrDefault().ProductId);

        if (product == null)
        {
            db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await db.SaveChangesAsync();
        }

        var cartHeader = await db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == cart.CartHeader.Id);

        if (cartHeader == null)
        {
            db.CartHeaders.Add(cart.CartHeader);
            await db.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
            cart.CartDetails.FirstOrDefault().Product = null;
            db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await db.SaveChangesAsync();
        }
        else
        {
            var cartDetails = await db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                x => x.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                x.CartHeaderId == cartHeader.Id);

            if (cartDetails == null)
            {
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await db.SaveChangesAsync();
            }
            else
            {
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetails.Count;
                db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await db.SaveChangesAsync();
            }
        }

        return mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> GetCartById(int id)
    {
        var cartHeader = await db.CartHeaders.FirstOrDefaultAsync(x => x.Id == id);
        var cartDetails = db.CartDetails.Where(x => x.CartHeaderId == cartHeader.Id)
            .Include(x => x.Product).ToList();

        var cart = new Cart()
        {
            CartHeader = cartHeader,
            CartDetails = cartDetails
        };

        return mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveFromCart(int cartDetailsId)
    {
        try
        {
            var cartDetails = await db.CartDetails.FirstOrDefaultAsync(x => x.Id == cartDetailsId);

            if (cartDetails != null)
            {
                var totalCartItems = db.CartDetails
                    .Where(x => x.CartHeaderId == cartDetails.CartHeaderId).Count();

                db.CartDetails.Remove(cartDetails);

                if (totalCartItems == 1)
                {
                    var cartHeaderToRemove = await db.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartDetails.CartHeaderId);
                    db.CartHeaders.Remove(cartHeaderToRemove);
                }

                await db.SaveChangesAsync();
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ApplyCoupon(int id, string code)
    {
        var cartHeader = await db.CartHeaders.FirstOrDefaultAsync(x => x.Id == id);

        if (cartHeader != null)
        {
            cartHeader.CouponCode = code;
            db.CartHeaders.Update(cartHeader);
            await db.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> RemoveCoupon(int id)
    {
        var cartHeader = await db.CartHeaders.FirstOrDefaultAsync(x => x.Id == id);

        if (cartHeader != null)
        {
            cartHeader.CouponCode = "";
            db.CartHeaders.Update(cartHeader);
            await db.SaveChangesAsync();

            return true;
        }

        return false;
    }
}
