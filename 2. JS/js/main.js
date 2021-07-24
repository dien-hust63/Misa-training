
var tableEmployee = $('.table-employee')[0];
var tableEmployeeContent = $('.table-employee tbody');
var employeeData;
var employeeId;
var typeMethod;
/**
 * lấy dữ liệu đổ ra bảng khi load trang
 */
$(document).ready(function () {
    loadData();
});

/**
 * load dũ liệu nhân viên từ api và đổ dữ liệu ra bảng
 */
function loadData() {
    //reset table
    $(".table-employee tbody ").empty();
    //call api
    $.ajax({
        url: 'http://cukcuk.manhnv.net/v1/Employees',
        method: 'GET'
    })
        .done(function (res) {
            let employeeData = res;
            let tableEmployeeContentTag = ``;
            let dateOfBirth = '';
            let employeeSalary = '';
            for (let i = 0; i < employeeData.length; i++) {
                dateOfBirth = formatDate(employeeData[i]["DateOfBirth"], "/");
                employeeSalary = formatMoney(employeeData[i]["Salary"]);
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

/**
 * hiển thị form popup khi ấn vào từng hàng trong bảng
 * gọi đến api nhân viên, đổ dữ liệu ứng với từng hàng trong bảng lên form popup
 */
$(".table-employee").on("dblclick", "tbody tr", function () {
    employeeId = $(this).attr("row-id");
    $.ajax({
        url: `http://cukcuk.manhnv.net/v1/Employees/${employeeId}`,
        method: 'GET'
    })
        .done(function (res) {
            console.log(res);
            employeeId = res["EmployeeId"];
            $('#employeeId').val(res["EmployeeCode"]);
            $('#employeeFullName').val(res["FullName"]);
            $('#employeeDateOfBirth').val(formatDate(res["DateOfBirth"], "-"));
            $('#employeeIdentityNumber').val(res["IdentityNumber"]);
            $('#employeeIdentityDate').val(formatDate(res["IdentityDate"], "-"));
            $('#employeeIdentityPlace').val(res["IdentityPlace"]);
            $('#employeeEmail').val(res["Email"]);
            $('#employeePhone').val(res["PhoneNumber"]);
            $('#employeeTaxCode').val(res["PersonalTaxCode"]);
            $('#employeeSalary').val(formatMoney(res["Salary"]));
            $('#employeeJoinDate').val(formatDate(res["JoinDate"], "-"));
            $('.formstaff-overlay').show();
            typeMethod = 1 //PUT khi ấn nút lưu
        })
        .fail(function (res) {
            alert("fail");
        })
})

/**
 * Hiển thị form thêm mới nhân viên khi ấn vào nút thêm mới
 */
$('.button-employee').click(function () {
    //reset form nhân viên
    $(".formstaff-overlay input").val(null);
    $(".formstaff-overlay input[required]").removeClass("border-red");
    //tô đỏ viền những input bắt buộc phải nhập dữ liệu khi người dùng không nhập
    $(".formstaff-overlay input[required]").blur(function () {
        console.log($(this).val());
        if ($(this).val() == "") {
            $(this).addClass("border-red");
        }
    })

    //sinh mã nhân viên tự động khi thêm mới
    $.ajax({
        url: 'http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode',
        method: 'GET'
    })
        .done(function (res) {
            let employeeId = $('#employeeId');
            employeeId.val(res);
            employeeId.focus();
        })
        .fail(function (res) {
            alert("Get Api fail");
        })
    $(".formstaff-overlay").show();
    typeMethod = 0; //có thể Post khi ấn nút lưu
})


/**
 * thêm hoặc chỉnh sửa thông tin nhân viên
 * typeMethod = 0 : POST
 * typeMethod = 1 : PUT
 * @param {int} typeMethod 
 * created by nvdien (22/7/2021)
 */
function saveEmployee(typeMethod) {
    let exampleData = {
        "EmployeeCode": "MF888668",
        "FirstName": null,
        "LastName": null,
        "FullName": "Nguyễn Văn Diện 2",
        "Gender": null,
        "DateOfBirth": null,
        "PhoneNumber": "0123456",
        "Email": "lqnhat@gmail.com",
        "Address": null,
        "IdentityNumber": "09151",
        "IdentityDate": null,
        "IdentityPlace": "",
        "JoinDate": null,
        "MartialStatus": null,
        "EducationalBackground": null,
        "QualificationId": null,
        "DepartmentId": null,
        "PositionId": null,
        "WorkStatus": null,
        "PersonalTaxCode": "",
        "Salary": null,
        "PositionCode": null,
        "PositionName": null,
        "DepartmentCode": null,
        "DepartmentName": null,
        "QualificationName": null,
        "GenderName": null,
        "EducationalBackgroundName": null,
        "MartialStatusName": null,
    }
    exampleData["EmployeeCode"] = $('#employeeId').val();
    exampleData["FullName"] = $('#employeeFullName').val();
    exampleData["PhoneNumber"] = $('#employeePhone').val();
    exampleData["Email"] = $('#employeeEmail').val();
    exampleData["IdentityNumber"] = $('#employeeIdentityNumber').val();
    let method = 'POST';
    let url = 'http://cukcuk.manhnv.net/v1/Employees'
    if (typeMethod == 1) {
        method = 'PUT';
        url = `http://cukcuk.manhnv.net/v1/Employees/${employeeId}`;
    }
    $.ajax({
        url: url,
        method: method,
        data: JSON.stringify(exampleData),
        dataType: 'json',
        contentType: "application/json",
    })
        .done(function (res) {
            alert("success");
            $(".formstaff-overlay").hide();
            loadData();
        })
        .fail(function () {
            alert("get api fail");
        })
}
//Lưu thông tin nhân viên
$(".formstaff .button-save").click(function () {
    saveEmployee(typeMethod);
});

//Đóng form nhân viên khi ấn Hủy
$(".formstaff .button-cancel").click(function(){
    $(".formstaff-overlay").hide();
})
//Mặc định ẩn form nhân viên
$(".formstaff-overlay").css("display", "none");

//Đóng form nhân viên
$(".formstaff-header .close").click(function () {
    $(".formstaff-overlay").css("display", "none");
})
/**
 * refresh lại dữ liệu trên table
 */
$(".controls-right-refresh").click(loadData);

/**
 * Xử lí định dạng tiền khi người dùng nhập vào ô input trong form nhân viên
 */
$("#employeeSalary").on('keyup', function () {
    let currentSalary = formatMoneytoDouble($(this).val());
    $(this).val(formatMoney(currentSalary));
});

/**
 * xử lí sự kiến khi ấn vào checkbox trên table
 */

$(".table-employee").on("click", "tbody tr", function () {
    $(this).find(".checkbox").toggleClass("checkbox-active");
    $(this).toggleClass("row-selected");
})

/**
 * test
 */
$('html').keyup(function (e) {
    if (e.keyCode == 46) {
        let selectedRows = $(".table-employee").find(".row-selected");
        let numberSelectedRows = selectedRows.length;
        if (numberSelectedRows > 0) {
            //xác dịnh nhân viên chuẩn bị xóa và thông báo cho người dùng
            let employeeCode = '';
            let warningText = `Bạn có chắc chắn muốn xóa nhân viên <b></b> không?`;
            console.log($('.formwarning-overlay'));
            if (numberSelectedRows == 1) {
                employeeCode = selectedRows.find("td:nth-child(3)").text();
                warningText = `Bạn có chắc chắn muốn xóa nhân viên <b>${employeeCode}</b> không?`;
            }
            if (numberSelectedRows == 2) {
                employeeCode = '';
                $.each(selectedRows, function (index, row) {
                    if (index == 1) {
                        employeeCode += ' và ' + row.querySelector("td:nth-child(3)").textContent;
                    }
                    else {
                        employeeCode += row.querySelector("td:nth-child(3)").textContent;
                    }
                })
                warningText = `Bạn có chắc chắn muốn xóa <b>${numberSelectedRows}</b> nhân viên <b>${employeeCode}</b> không?`;
            }
            if (numberSelectedRows > 2) {
                warningText = `Bạn có chắc chắn muốn xóa <b>${numberSelectedRows}</b> nhân viên đã chọn không?`;
            }
            $('.warning-content-text').html(warningText);
            //Hiển thị form cảnh báo
            $('.formwarning-overlay').show();

            //Đóng form khi ấn nút đóng hoặc hủy
            $('.warning-del .close').click(function () {
                $('.formwarning-overlay').hide();
            })
            $('.warning-del .button-cancel').click(function () {
                $('.formwarning-overlay').hide();
            })

            //Xác nhận xóa nhân viên
            let selectedEmployeeId = '';
            $('.warning-del .button-del').click(function () {
                selectedRows.each(function () {
                    selectedEmployeeId = $(this).attr("row-id");
                    $.ajax({
                        url: `http://cukcuk.manhnv.net/v1/Employees/${selectedEmployeeId}`,
                        method: 'DELETE',
                        async: false
                    })
                        .done(function (res) {
                        })
                        .fail(function () {
                            alert("call api delete fail");
                        })
                })
                $('.formwarning-overlay').hide();
                loadData();
               
            })


        }

    }
});

/**
 * Format dữ liệu ngày tháng sang định dạng khác mong muốn
 * seperator = "-" : year/month/day
 * seperator = "/" : day/month/year
 * @param {string} dateString xâu dạng date
 * @param {string} seperator dấu phân cách để chia theo định dạng
 * @returns xâu rỗng hoặc xâu dạng date theo định dạng 
 * Created by: nvdien (20/7/2021)
 */

function formatDate(dateString, seperator) {
    var dateObj = new Date(dateString);
    if (Number.isNaN(dateObj.getTime())) {
        return "";
    }
    else {
        var month = dateObj.getUTCMonth() + 1;
        var day = dateObj.getUTCDate() + 1;
        var year = dateObj.getUTCFullYear();
        if (month < 10) {
            month = "0" + month;
        }
        if (day < 10) {
            day = "0" + day;
        }
        let newdate = '';
        if (seperator == "-") {
            newdate = year + seperator + month + seperator + day;
        }
        if (seperator == "/") {
            newdate = day + seperator + month + seperator + year;
        }
        return newdate;
    }

}

/**
 * chuyển từ dạng số sang dạng ngăn cách bởi dấu '.'
 * @param {string} money string tiền tệ
 * @returns string tiền tệ theo đúng định dạng
 * Created by nvdien (20/7/2021)
 */
function formatMoney(money) {
    if (money) {
        return money.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ".");
    }
    return '';
}

/**
 * chuyển từ dạng ngăn cách bởi dấu '.' sang dạng số
 * @param {string} money 
 */
function formatMoneytoDouble(money){
    return money.split('.').join('');
}

