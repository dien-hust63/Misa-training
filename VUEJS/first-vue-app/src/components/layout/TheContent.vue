<template>
  <div class="content">
    <employee-list
      @addEmployee="addEmployee"
      @editEmployee="editEmployee"
      ref="employeeList"
      @showPopup="showPopup"
    />
    <employee-detail
      :class="{ 'formstaff--hide': isHide }"
      @closeFormStaff="closeFormStaff"
      :employeeId="employeeId"
      :mode="mode"
      @loadTable="loadTable"
      :isHide="isHide"
      :employeeNewCodeData ="employeeNewCodeData"
    />
    <base-popup
      :apiUrl="baseEmployeeApi"
      :listData="listSelectedEmployees"
      :class="{ 'popup--show': popupShow }"
      @loadTable="loadTable"
      @closePopup="closePopup"
    />
  </div>
</template>

<style scoped>
@import url("../../css/layout/content.css");
</style>

<script>
import EmployeeList from "../../view/employee/EmployeeList.vue";
import EmployeeDetail from "../../view/employee/EmployeeDetail.vue";
import BasePopup from "../../components/base/BasePopup.vue";
import { eventBus } from "../../main.js";
export default {
  name: "TheContent",
  components: {
    EmployeeList,
    EmployeeDetail,
    BasePopup,
  },
  created() {
    eventBus.$on("deleteEmployees", (listSelectedEmployees) => {
      this.listSelectedEmployees = listSelectedEmployees;
    });
  },
  data() {
    return {
      isHide: true,
      show: "",
      employeeId: "",
      mode: "1",
      listSelectedEmployees: [],
      baseEmployeeApi: "https://localhost:44350/api/v1/Employees/",
      popupShow: false,
      employeeNewCodeData: {}
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
      this.employeeNewCodeData = { EmployeeCode: employeeCode };
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
    editEmployee(employeeId) {
      this.isHide = false;
      this.mode = "2";
      this.employeeId  =employeeId;
    },

    /**
     * load lại dữ liệu trong bảng
     * author: nvdien(6/8/2021)
     * modified: nvdien(6/8/2021)
     */
    loadTable() {
      this.$refs.employeeList.loadTable();
    },

    /**
     * đóng popup
     * author: nvdien(6/8/2021)
     * modified: nvdien(6/8/2021)
     */
    closePopup(){
      this.popupShow = false;
    },

    /**
     * hiển thị popup
     * author: nvdien(6/8/2021)
     * modified: nvdien(6/8/2021)
     */
    showPopup(){
      this.popupShow = true;
    }
  },
};
</script>