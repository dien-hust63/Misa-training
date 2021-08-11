<template>
  <div class="content-controls">
    <div class="content-controls-left">
      <div class="controls-left-lable">
        <p>Danh sách nhân viên</p>
      </div>
      <div class="controls-left-filter">
        <input
          class="search-input"
          type="text"
          placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại"
          ref="search-input"
          @keyup="searchInput(searchInputValue, comboboxDepartmentData, comboboxPositionData)"
          v-model="searchInputValue"
        />

        <base-combobox
          ref="combobox-department"
          v-model="comboboxDepartmentValue"
          @clearComboboxValue="clearComboboxValue"
          @updateComboboxValue="updateComboboxValue"
          @updateComboboxData="updateComboboxData"
          typeCombobox="department"
          :apiUrl="departmentListApi"
          keyValue="DepartmentName"
          keyData="DepartmentId"
        />

        <base-combobox
          ref="combobox-position"
          v-model="comboboxPositionValue"
          @clearComboboxValue="clearComboboxValue"
          @updateComboboxValue="updateComboboxValue"
          @updateComboboxData="updateComboboxData"
          typeCombobox="position"
          :apiUrl="positionListApi"
          keyValue="PositionName"
          keyData="PositionId"
        />
      </div>
    </div>
    <div class="content-controls-right">
      <div class="wrap-button">
        <button
          class="button button-delete"
          @click="deleteEmployees(listSelectedEmployees)"
          v-show="isShow"
        >
          <div class="btn-icon">
            <i class="far fa-trash-alt"></i>
          </div>
          <div class="btn-text">Xóa nhân viên</div>
        </button>
        <button class="button button-employee" @click="addEmployee">
          <div class="btn-icon btn-employee"></div>
          <div class="btn-text">Thêm nhân viên</div>
        </button>
      </div>

      <div class="controls-right-refresh" @click="loadTable"></div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/components/control.css");
</style>

<script>
import axios from "axios";
import { eventBus } from "../../main.js";
import BaseCombobox from "../../components/base/BaseCombobox.vue";

export default {
  name: "TheControl",
  components: {
    BaseCombobox,
  },
  created() {
    eventBus.$on("showSelectedEmployees", (listSelectedEmployees) => {
      this.listSelectedEmployees = listSelectedEmployees;
      if (this.listSelectedEmployees.length > 0) {
        this.isShow = true;
      } else {
        this.isShow = false;
      }
    });
  },
  data() {
    return {
      employeeCode: "NV001",
      listSelectedEmployees: [],
      isShow: false,
      comboboxDepartmentValue: "",
      comboboxDepartmentData: "",
      comboboxPositionValue: "",
      comboboxPositionData: "",
      departmentListApi: "http://cukcuk.manhnv.net/api/Department",
      positionListApi: "http://cukcuk.manhnv.net/v1/Positions",
      searchInputValue: "",
      timeDelayFilter: null,
    };
  },
  methods: {
    callAPINewEmployee: async () => {
      const response = await axios.get(
        `http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode`
      );
      return response.data;
    },

    callAPIEmployeeFilter: async (pageSize, pageNumber, employeeFilter, departmentId, positionId) => {
      try {
        if(departmentId == undefined) departmentId = "";
        if(positionId == undefined) positionId = "";
        
        const response = await axios.get(
          `http://cukcuk.manhnv.net/v1/Employees/employeeFilter?pageSize=${pageSize}&pageNumber=${pageNumber}&employeeFilter=${employeeFilter}&departmentId=${departmentId}&positionId=${positionId}`
        );
        return response.data;
      } catch (e) {
        console.log(e);
      }
    },

    async addEmployee() {
      let newEmployeeCode;
      newEmployeeCode = await this.callAPINewEmployee();
      this.$emit("addEmployee", newEmployeeCode);
    },

    /**
     * load lại dữ liệu trên bảng
     */
    loadTable() {
      this.$emit("loadTable");
    },

    /**
     * truyền dữ liệu chứa các khóa chính của nhân viên được xóa
     * @param {array} listSelectedEmployees : chứa các khóa chính của nhân viện được chọn
     * author: nvdien(6/8/2021)
     * modified: (6/8/2021)
     */
    deleteEmployees(listSelectedEmployees) {
      console.log(listSelectedEmployees);
      eventBus.$emit("deleteEmployees", listSelectedEmployees);
      this.$emit("showPopup");
    },
    /**
     * xóa dữ liệu ở ô input của combobox
     * @param {Int} type xác định loại combobox 1(deparment), 2(position)
     * author: nvdien(7/8/2021)
     * modified: nvdien(7/8/2021)
     */
    clearComboboxValue(type) {
      if (type == "department") {
        this.comboboxDepartmentValue = "";
      }
      if (type == "position") {
        this.comboboxPositionValue = "";
      }
    },

    /**
     * Cập nhật dữ liệu người dùng chọn từ combobox
     * @param {String} type: loại combobox
     * @param {String} selectedValue: giá trị người dùng chọn
     * author: nvdien(8/8/2021)
     * modified: nvdien(8/8/2021)
     */
    updateComboboxValue(type, selectedValue) {
      if (type == "department") {
        this.comboboxDepartmentValue = selectedValue;
      }
      if (type == "position") {
        this.comboboxPositionValue = selectedValue;
      }
    },

    /**
     * Cập nhật dữ liệu người dùng chọn từ combobox để sử dụng cho filter
     * @param {String} type: loại combobox
     * @param {String} comboboxData: key ứng với giá trị người dùng chọn
     * author: nvdien(8/8/2021)
     * modified: nvdien(8/8/2021)
     */
    updateComboboxData(type, comboboxData) {
      if (type == "department") {
        this.comboboxDepartmentData = comboboxData;
      }
      if (type == "position") {
        this.comboboxPositionData = comboboxData;
      }
      //gọi API filter
      this.doSearch(this.searchInputValue, this.comboboxDepartmentData, this.comboboxPositionData);
    },

    searchInput(searchVal, departmentVal, positionVal) {
      if (searchVal == "") {
        this.loadTable();
        return;
      }
      if (this.timeDelayFilter) {
        clearTimeout(this.timeDelayFilter);
      }

      this.timeDelayFilter = setTimeout(() => {
        this.doSearch(searchVal, departmentVal, positionVal);
      }, 500);
    },

    async doSearch(searchVal, departmentVal, positionVal) {
      console.log("dosearch " + searchVal);
      //gọi đến api filter
      let response = await this.callAPIEmployeeFilter(
        20,
        0,
        searchVal,
        departmentVal,
        positionVal
      );
      //trả về dữ liệu
      console.log(response);
      //gán lại data
      this.$emit("changeTableData", response.Data);
    },
  },
};
</script>