using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.Models
{
    public class SocialMediaUser
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SocialMediaId { get; set; }

        [Required]
        [StringLength(255)]
        public string UserReference { get; set; }
        [Key]
        [ForeignKey("UserId")]

        public virtual ApplicationUser User { get; set; }
        [Key]
        [ForeignKey("SocialMediaId")]

        public virtual SocialMedia SocialMedia { get; set; }
    }
}
