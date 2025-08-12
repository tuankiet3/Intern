using RegisterAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterAPI.Dtos
{
    public class RegisterStudentDto
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        public string StudentName { get; set; } = null!;
        [Required(ErrorMessage = "Email là bắt buộc.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "CCCD là bắt buộc.")]
        public string Cccd { get; set; } = null!;
        public bool Prioritize { get; set; }
        [Required(ErrorMessage = "Tỉnh/Thành phố  là bắt buộc.")]
        public int ProvinceId { get; set; }
        [Required(ErrorMessage = "THTP là bắt buộc.")]
        public int HighSchoolId { get; set; }
        [Required(ErrorMessage = "Khu vực là bắt buộc.")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "Ngành là bắt buộc.")]
        public int MajorId { get; set; }
        public int? AspirationId { get; set; }
        public bool Gender { get; set; }
    }
}
