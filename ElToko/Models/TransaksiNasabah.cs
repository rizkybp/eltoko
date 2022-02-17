namespace ElToko.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransaksiNasabah")]
    public partial class TransaksiNasabah
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }
        public virtual Nasabah Nasabah { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string DebitCreditStatus { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
    }
}
