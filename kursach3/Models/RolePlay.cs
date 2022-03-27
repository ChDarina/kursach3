using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.Models
{
    public class RolePlay
    {
        public RolePlay()
        {
            //Rooms = new HashSet<Room>();
            //Messages = new HashSet<Message>();
            Characters = new HashSet<Character>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePlayId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MasterId { get; set; }
        [Required]
        [ForeignKey("MasterId")]
        public virtual ApplicationUser Master { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
