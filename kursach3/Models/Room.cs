using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kursach3.Models
{
    public partial class Room
    {
        public Room()
        {
            Messages = new HashSet<Message>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("RolePlayId")]
        public int RolePlayId { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual RolePlay RolePlay { get; set; }
        [Required]
        [StringLength(450)]
        [ForeignKey("AdminId")]
        public string AdminId { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual ApplicationUser Admin { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
