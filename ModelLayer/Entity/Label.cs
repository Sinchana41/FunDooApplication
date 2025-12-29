using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    [Table("labels")]
    public class Label1
    {
        [Key]
        public int LabelId { get; set; }

        [Required,MaxLength(100)]
        public string LabelName { get; set; }

        public DateTime CreateAT { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAT { get; set; } = DateTime.UtcNow;

        //Foreign Keys
        [ForeignKey("User")]
        public int UserId { get; set; }

        
        //Navigation
        public User User { get; set; }
        public ICollection<NoteLabel> NoteLabels { get; set; }
    }
}
