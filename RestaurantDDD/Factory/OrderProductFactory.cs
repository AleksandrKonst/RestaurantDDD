﻿using RestaurantConsole.Aggregate;
using RestaurantConsole.Data;

namespace RestaurantConsole.Factory;

public class OrderProductFactory
{
    private readonly RestaurantContext _context;

    public OrderProductFactory()
    {
        _context = new RestaurantContext();
    }

    public OrderProduct CreateClient(OrderProduct orderProduct)
    {
        var newOrderProduct = _context.OrderProducts.Add(orderProduct).Entity;
        _context.SaveChanges();
        return newOrderProduct;
    }
}