using Misa.ApplicationCore.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [MisaRequired]
        /// 
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và đệm
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tene
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [MisaRequired]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MisaRequired]
        public string Email { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        // <summary>
        /// Id của nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DebitAmount { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Trạng thái công việc trong công ty: đang làm hoặc dừng
        /// </summary>
        public bool IsStopFollow { get; set; }

        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [MisaNotMap]
        public string CustomerGroupName { get; set; }









        #endregion
    }
}
