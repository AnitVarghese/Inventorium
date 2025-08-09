//using System;
//using System.ComponentModel.DataAnnotations;

//namespace Inventorium.Models
//{
//    public class Request
//    {
//        public int Id { get; set; }

//        [Required]
//        public string Message { get; set; } = string.Empty;

//        public string UserId { get; set; } = string.Empty;

//        public string UserEmail { get; set; } = string.Empty;

//        public DateTime SubmittedAt { get; set; } = DateTime.Now;
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;

namespace Inventorium.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public required string ItemName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public required string Unit { get; set; }


        public required string Reason { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.Now;

        // ✅ ADD THIS:
        public required string RequestedBy { get; set; }
        public string? UserId { get; internal set; }
        public string? UserEmail { get; internal set; }

    }
    

}
