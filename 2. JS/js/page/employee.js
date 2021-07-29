
$(document).ready(function () {
    new EmployeePage();
})

class EmployeePage extends Base{

    constructor() {
        super("table-employee");
        //khởi tạo các sự kiện
        this.initEvents();
        
    }

    /**
     * Khởi tạo các sự kiện
     */
    initEvents(){
        /**
         * hiển thị form popup khi ấn vào từng hàng trong bảng
         * gọi đến api nhân viên, đổ dữ liệu ứng với từng hàng trong bảng lên form popup
        */
         $(".table-employee").on("dblclick", "tbody tr", function () {
            let employeeId = $(this).attr("row-id");
            $.ajax({
                url: `http://cukcuk.manhnv.net/v1/Employees/${employeeId}`,
                method: 'GET'
            })
                .done(function (res) {
                    employeeId = res["EmployeeId"];
                    $('#employeeId').val(res["EmployeeCode"]);
                    $('#employeeFullName').val(res["FullName"]);
                    $('#employeeDateOfBirth').val(CommonFunction.formatDate(res["DateOfBirth"], "-"));
                    $('#employeeIdentityNumber').val(res["IdentityNumber"]);
                    $('#employeeIdentityDate').val(CommonFunction.formatDate(res["IdentityDate"], "-"));
                    $('#employeeIdentityPlace').val(res["IdentityPlace"]);
                    $('#employeeEmail').val(res["Email"]);
                    $('#employeePhone').val(res["PhoneNumber"]);
                    $('#employeeTaxCode').val(res["PersonalTaxCode"]);
                    $('#employeeSalary').val(CommonFunction.formatMoney(res["Salary"]));
                    $('#employeeJoinDate').val(CommonFunction.formatDate(res["JoinDate"], "-"));
                    $('.formstaff-overlay').show();
                    // typeMethod = 1 //PUT khi ấn nút lưu
                })
                .fail(function (res) {
                    alert("fail");
                })
        })
        //Mặc định ẩn form nhân viên
        $(".formstaff-overlay").css("display", "none");

        //Đóng form nhân viên khi ấn Hủy
        $(".formstaff .close").click(function (){
            console.log("dadadfds");
            $(".formstaff-overlay").hide();
        })

    }
    
}