using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    [Table("collaborators")]
    public class Collaborator
    {
        [Key]
        public int CollaboratorId { get; set; }

        [Required, EmailAddress, MaxLength(225)]
        public string Email { get; set; }

        //ForeignKeys
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        //Novigation
        public User User { get; set; }
        public Note Note { get; set; }
    }
}
