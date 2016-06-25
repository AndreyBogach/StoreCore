namespace StoreCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class inf_address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inf_address()
        {
            inf_legal_entity = new HashSet<inf_legal_entity>();
            inf_physical_entity = new HashSet<inf_physical_entity>();
        }

        public int id { get; set; }

        public int client_id { get; set; }

        [Required]
        [StringLength(100)]
        public string country { get; set; }

        [Required]
        [StringLength(100)]
        public string city { get; set; }

        [Required]
        [StringLength(100)]
        public string street { get; set; }

        [Required]
        [StringLength(100)]
        public string home_number { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime record_updated { get; set; }

        public int record_state { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_legal_entity> inf_legal_entity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_physical_entity> inf_physical_entity { get; set; }
    }
}
