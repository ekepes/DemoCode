﻿using System;

namespace MessageContracts
{
    public class ItemPicked
    {
        public string AtDeviceId { get; set; }

        public string ByUserId { get; set; }

        public DateTime EventTime { get; set; }

        public string ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
