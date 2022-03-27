using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3.Models
{
    public partial class Friend
    {
        [Key]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [Key]
        [ForeignKey("UserFriendId")]
        public virtual ApplicationUser UserFriend { get; set; }
        [Key]
        [Column(Order = 0)]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserFriendId { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public bool Confirmed { get; set; }
    }
}
