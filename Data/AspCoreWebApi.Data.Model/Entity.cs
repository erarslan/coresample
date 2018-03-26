using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCoreWebApi.Data.Model
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [DefaultValue(0)]
        public bool IsDeleted { get; set; }
    }
}
