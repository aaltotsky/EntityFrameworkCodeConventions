using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCode
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        [Column(TypeName = "DateTime2")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // this will prevent the column to be updated
        public DateTime CreatedDate { get; internal set; }
    }
}
