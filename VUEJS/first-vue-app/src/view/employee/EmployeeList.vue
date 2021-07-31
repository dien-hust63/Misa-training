<template>
  <div class="employee-list">
    <the-control />
    <table-employee
      :urlAPI="employeesAPI"
      :tableHeaders="tableEmployeeHeader"
    />
    <the-paging />
  </div>
</template>

<style scoped>
@import url("../../css/page/employee-list.css");
</style>

<script>
import TheControl from "../common/TheControl.vue";
import TableEmployee from "../../components/base/BaseTable.vue";
import ThePaging from "../common/ThePaging.vue";
export default {
  name: "EmployeeList",
  components: {
    TheControl,
    TableEmployee,
    ThePaging,
  },
  data() {
    return {
      employeesAPI: "http://cukcuk.manhnv.net/v1/Employees",
      tableEmployeeHeader: [
        { EmployeeCode: "Mã nhân viên", type: "0" },
        { FullName: "Họ và tên", type: "0" },
        { GenderName: "Giới tính", type: "0" },
        { DateOfBirth: "Ngày sinh", type: "1" },
        { PhoneNumber: "Điện thoại", type: "0" },
        { Email: "Email", type: "0" },
        { PositionName: "Chức vụ", type: "0" },
        { DepartmentName: "Phòng ban", type: "0" },
        { Salary: "Mức lương cơ bản", type: "2" },
        { WorkStatus: "Tình trạng công việc", type: "0" },
      ],
    };
  },
  
};
</script>