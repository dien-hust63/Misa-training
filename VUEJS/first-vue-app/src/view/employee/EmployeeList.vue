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
    <the-paging @getPageInfo="getPageInfo" />
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
      tableKey: false,
      tableEmployeeHeader: [
        { EmployeeCode: "Mã nhân viên", type: "0" },//type = 0: dữ liệu căn trái
        { FullName: "Họ và tên", type: "0" },
        { GenderName: "Giới tính", type: "0" },
        { DateOfBirth: "Ngày sinh", type: "1" }, //type = 1: dữ liệu căn giữa
        { PhoneNumber: "Điện thoại", type: "0" },
        { Email: "Email", type: "0" },
        { PositionName: "Chức vụ", type: "0" },
        { DepartmentName: "Phòng ban", type: "0" },
        { Salary: "Mức lương cơ bản", type: "2" }, //type = 2 : dữ liệu căn phải
        { WorkStatus: "Tình trạng công việc", type: "0" },
      ],
      tableContents: [],
      isUpdate: true,
      pageIndex: 1,
      pageSize: 10,
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

    /**
     * Load bảng
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    loadTable() {
      this.tableKey = !this.tableKey;
    },

    /**
     * hiển thị popup 
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    showPopup() {
      this.$emit("showPopup");
    },

    /**
     * thay đỏi dữ liệu bảng
     * @param {Object} newTableContents thông tin mới
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    changeTableData(newTableContents){
      this.isUpdate = !this.isUpdate;
      this.tableContents = newTableContents;
    },
    /**
     * lấy và cập nhật giá trị page Index và page Size
     * @param pageIndex: index trang
     * @param pageSize: số bản ghi trên trang
     * author: nvdien(21/8/2021)
     * modifed: nvdien(21/8/2021)
     */
    getPageInfo(pageIndex, pageSize){
       this.pageIndex = pageIndex;
       this.pageSize = pageSize;
    },

  },
  computed: {
    employeesAPI: function(){
      return `https://localhost:44350/api/v1/Employees/Filter?searchData=&departmentId&positionId&pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`;
    }
  }
};
</script>