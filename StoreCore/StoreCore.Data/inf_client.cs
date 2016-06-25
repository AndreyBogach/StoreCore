namespace StoreCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class inf_client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inf_client()
        {
            inf_order = new HashSet<inf_order>();
        }

        public int id { get; set; }

        public string name { get; set; }

        public int type_id { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime record_updated { get; set; }

        public int record_state { get; set; }

        public virtual inf_type inf_type { get; set; }

        public virtual inf_legal_entity inf_legal_entity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_order> inf_order { get; set; }

        public virtual inf_physical_entity inf_physical_entity { get; set; }
    }
}
