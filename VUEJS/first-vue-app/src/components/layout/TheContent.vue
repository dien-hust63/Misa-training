<template>
  <div class="content">
    <employee-list @addEmployee="addEmployee" @editEmployee="editEmployee" ref = "employeeList"/>
    <employee-detail
      :key="focusKey"
      :class="{ isHide: isHide }"
      @closeFormStaff="closeFormStaff"
      :employeeData="employeeData"
      :mode="mode"
      @loadTable = "loadTable"
    />
  </div>
</template>

<style scoped>
@import url("../../css/layout/content.css");
</style>

<script>
import EmployeeList from "../../view/employee/EmployeeList.vue";
import EmployeeDetail from "../../view/employee/EmployeeDetail.vue";
export default {
  name: "TheContent",
  components: {
    EmployeeList,
    EmployeeDetail,
  },
  data() {
    return {
      isHide: true,
      show: "",
      employeeData: {},
      focusKey: false,
      mode: "1",
    };
  },
  methods: {
    /**
     * hiển thị form thêm nhân viên
     * @param {Object} employeeCode chứa mã code nhân viên mới
     * author: nvdien(5/8/2021)
     * modified: 5/8/2021
     */
    addEmployee(employeeCode) {
      this.isHide = false;
      this.focusKey = !this.focusKey;
      this.employeeData = { EmployeeCode: employeeCode };
      this.mode = "1";
    },

    /**
     * đóng form nhân viên
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    closeFormStaff() {
      this.isHide = true;
    },

    /**
     * hiển thị form thông tin nhân viên và cho phép chỉnh sửa
     * @param {Object} employeeData chứa thông tin nhân viên hiển thị
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    editEmployee(employeeData) {
      this.isHide = false;
      this.mode = "2";
      this.focusKey = !this.focusKey;
      this.employeeData = employeeData;
    },

    loadTable(){
      this.$refs.employeeList.loadTable();
    }
  },
};
</script>