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
        />

        <div class="combobox combobox-department">
          <input
            type="text"
            class="combobox__input"
            placeholder="Chọn/Nhập thông tin"
          />
          <div class="combobox__input-cancel">
            <i class="fas fa-times-circle"></i>
          </div>
          <div class="combobox__dropdown">
            <i class="fas fa-chevron-down combobox__icon"></i>
          </div>
          <ul class="combobox__list"></ul>
        </div>

        <div class="combobox combobox-position">
          <input
            type="text"
            class="combobox__input"
            placeholder="Chọn/Nhập thông tin"
          />
          <div class="combobox__input-cancel">
            <i class="fas fa-times-circle"></i>
          </div>
          <div class="combobox__dropdown">
            <i class="fas fa-chevron-down combobox__icon"></i>
          </div>
          <ul class="combobox__list"></ul>
        </div>
      </div>
    </div>
    <div class="content-controls-right">
      <button class="button button-employee" @click="addEmployee">
        <div class="btn-icon btn-employee"></div>
        <div class="btn-text">Thêm nhân viên</div>
      </button>
      <div class="controls-right-refresh" @click="loadTable"></div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/components/control.css");
</style>

<script>
import axios from "axios";
export default {
  name: "TheControl",
  data() {
    return {
      employeeCode: "NV001",
    };
  },
  methods: {
    callAPINewEmployee: async () => {
      const response = await axios.get(
        `http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode`
      );
      return response.data;
    },

    async addEmployee() {
      let newEmployeeCode;
      newEmployeeCode = await this.callAPINewEmployee();
      this.$emit("addEmployee", newEmployeeCode);
    },

    /**
     * load lại dữ liệu trên bảng
     */
    loadTable(){
      this.$emit("loadTable");
    }
  },
};
</script>