namespace StoreCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class inf_order
    {
        public int id { get; set; }

        public int product_id { get; set; }

        public int client_id { get; set; }

        public int count { get; set; }

        public decimal amount { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime record_updated { get; set; }

        public int record_state { get; set; }

        public virtual inf_client inf_client { get; set; }

        public virtual inf_product inf_product { get; set; }
    }
}
