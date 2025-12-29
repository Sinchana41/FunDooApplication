using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    [Table("notes")]
    public class Note
    {
            [Key]
            public int NotesId { get; set; }

            [Required]
            [MaxLength(255)]
            public string Title { get; set; }

            public string? Description { get; set; }

            public DateTime? Reminder { get; set; }

            [MaxLength(7)]
            public string Colour { get; set; } = "#FFFFFF";

            public string? Image { get; set; }

            public bool IsArchive { get; set; } = false;
            public bool IsPin { get; set; } = false;
            public bool IsTrash { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

           [ForeignKey("UserId")]
           public int UserId { get; set; }
           
          //Navigation
           public User User { get; set; }
           public ICollection<NoteLabel> NoteLabels { get; set; }

           public ICollection<Collaborator> Collaborators { get; set; }
        
    }
}