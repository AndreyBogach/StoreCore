namespace StoreCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class inf_legal_entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int client_id { get; set; }

        public long tin { get; set; }

        public int address { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime record_updated { get; set; }

        public int record_state { get; set; }

        public virtual inf_address inf_address { get; set; }

        public virtual inf_client inf_client { get; set; }
    }
}
