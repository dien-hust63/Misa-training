<template>
  <div class="content">
    <employee-list
      @addEmployee="addEmployee"
      @editEmployee="editEmployee"
      ref="employeeList"
      @showPopup="showPopup"
    />
    <employee-detail
      :key="focusKey"
      :class="{ 'formstaff--hide': isHide }"
      @closeFormStaff="closeFormStaff"
      :employeeData="employeeData"
      :mode="mode"
      @loadTable="loadTable"
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
      employeeData: {},
      focusKey: false,
      mode: "1",
      listSelectedEmployees: [],
      baseEmployeeApi: "http://cukcuk.manhnv.net/v1/Employees/",
      popupShow: false,
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