using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.Models
{
    public class SocialMedia
    {
        public SocialMedia()
        {
            SocialMediaUsers = new HashSet<SocialMediaUser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SocialMediaId { get; set; }

        [Required]
        [StringLength(255)]
        public string SocialMediaName { get; set; }
        public virtual ICollection<SocialMediaUser> SocialMediaUsers { get; set; }
    }
}
