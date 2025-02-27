﻿using Core.Enums;

namespace Core.Entities
{
    public class Product
    {
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public ProductTypes Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }
        public List<StatusProduct> Statuses { get; set; }

        public StatusProduct GetLastStatus()
        {
            return Statuses.OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }
    }
}
