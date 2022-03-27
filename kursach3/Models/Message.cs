using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kursach3.Models
{
    public partial class Message
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FromUserId { get; set; }
        [Required]
        [ForeignKey("FromUserId")]
        public virtual ApplicationUser FromUser { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ToRoomId { get; set; }
        [Required]
        [ForeignKey("ToRoomId")]
        public virtual Room ToRoom { get; set; }
        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int ToRolePlayId { get; set; }
        //[Required]
        //[ForeignKey("ToRolePlayId")]
        //public virtual RolePlay ToRolePlay { get; set; }
    }
}
