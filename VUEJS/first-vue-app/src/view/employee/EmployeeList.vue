<template>
  <div class="employee-list">
    <the-control
      @addEmployee="addEmployee"
      @loadTable="loadTable"
      @showPopup="showPopup"
      @changeTableData="changeTableData"
    />
    <base-table
      :key="tableKey"
      :urlAPI="employeesAPI"
      :tableHeaders="tableEmployeeHeader"
      :newTableContents="tableContents"
      :isUpdate="isUpdate"
      @editEmployee="editEmployee"

    />
    <the-paging />
  </div>
</template>

<style scoped>
@import url("../../css/page/employee-list.css");
</style>

<script>
import TheControl from "../common/TheControl.vue";
import BaseTable from "../../components/base/BaseTable.vue";
import ThePaging from "../common/ThePaging.vue";
export default {
  name: "EmployeeList",
  components: {
    TheControl,
    BaseTable,
    ThePaging,
  },
  data() {
    return {
      employeesAPI: "http://cukcuk.manhnv.net/v1/Employees",
      tableKey: false,
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
      tableContents: [],
      isUpdate: true,
    };
  },
  methods: {
    /**
     * Hiển thị form khi ấn nút thêm mới nhân viên
     */
    addEmployee(employeeCode) {
      this.$emit("addEmployee", employeeCode);
    },

    /**
     * hiển thị form chỉnh sửa nhân viên
     * @param {Object} employeeData chứa thông tin nhân viên
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    editEmployee(employeeData) {
      this.$emit("editEmployee", employeeData);
    },

    loadTable() {
      this.tableKey = !this.tableKey;
    },
    showPopup() {
      this.$emit("showPopup");
    },
    changeTableData(newTableContents){
      this.isUpdate = !this.isUpdate;
      this.tableContents = newTableContents;
    }
  },
};
</script>