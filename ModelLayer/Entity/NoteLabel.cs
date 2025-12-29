using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    [Table("note_labels")]
    public class NoteLabel
    {
        public int NotesId { get; set; }
        public Note Note { get; set; }
        public int LabelId { get; set; }
        public Label1 Label { get; set; }
    }
}
