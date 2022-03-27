using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.Models
{
    public class Character
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [Key]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RolePlayId { get; set; }
        [Key]
        [ForeignKey("RolePlayId")]
        public virtual RolePlay? RolePlay { get; set; }
        [Required]
        [StringLength(255)]
        public string? СharacterName { get; set; }
    }
}
