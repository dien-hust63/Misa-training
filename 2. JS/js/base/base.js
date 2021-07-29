class Base{

    constructor(tableID){
        //load data table
        this.loadData(tableID);
    }

    
    /**
     * load dũ liệu nhân viên từ api và đổ dữ liệu ra bảng
     */
     loadData(tableID) {
        //reset table
        $(`.${tableID} tbody `).empty();
        //call api
        $.ajax({
            url: 'http://cukcuk.manhnv.net/v1/Employees',
            method: 'GET'
        })
            .done(function (res) {
                let tableEmployeeContent = $(`.${tableID} tbody `);
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
