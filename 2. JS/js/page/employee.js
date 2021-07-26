
$(document).ready(function () {
    new EmployeePage();
})

class EmployeePage {

    constructor() {
        //load dữ liệu
        this.loadData();
        //khởi tạo các sự kiện
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
        $(".formstaff .button-cancel").click(function (){
            console.log("dadadfds");
            $(".formstaff-overlay").hide();
        })
    }

    /**
     * load dũ liệu nhân viên từ api và đổ dữ liệu ra bảng
     */
    loadData() {
        //reset table
        $(".table-employee tbody ").empty();
        //call api
        $.ajax({
            url: 'http://cukcuk.manhnv.net/v1/Employees',
            method: 'GET'
        })
            .done(function (res) {
                let tableEmployeeContent = $('.table-employee tbody');
                let employeeData = res;
                let tableEmployeeContentTag = ``;
                let dateOfBirth = '';
                let employeeSalary = '';
                for (let i = 0; i < employeeData.length; i++) {
                    dateOfBirth = CommonFunction.formatDate(employeeData[i]["DateOfBirth"], "/");
                    employeeSalary = CommonFunction.formatMoney(employeeData[i]["Salary"]);
                    tableEmployeeContentTag += `<tr row-id= "${employeeData[i]["EmployeeId"]}">
                                        <td>
                                            <div class="checkbox">
                                                <i class="fas fa-check"></i>
                                            </div>
                                        </td>
                                        <td>1</td>
                                        <td>${employeeData[i]["EmployeeCode"]}</td>
                                        <td>${employeeData[i]["FullName"]}</td>
                                        <td>${employeeData[i]["GenderName"]}</td>
                                        <td class="text-align-center">${dateOfBirth}</td>
                                        <td>${employeeData[i]["PhoneNumber"]}</td>
                                        <td >
                                            <div class="text-inline-description" title="${employeeData[i]["Email"]}">
                                            ${employeeData[i]["Email"]}
                                            </div>
                                        </td>
                                        <td>${employeeData[i]["PositionName"]}</td>
                                        <td>${employeeData[i]["DepartmentName"]}</td>
                                        <td class="text-align-right">${employeeSalary}</td>
                                        <td>${employeeData[i]["WorkStatus"]}</td>
                                    </tr>`;
                }

                tableEmployeeContent.append(tableEmployeeContentTag);

            })
            .fail(function () {
                alert("Get API fail");
            })
    }
}