<template>
  <div class="formstaff-overlay">
    <div class="formstaff">
      <div class="formstaff-header">
        <div class="formstaff-header__label">THÔNG TIN NHÂN VIÊN</div>
        <div class="close" @click="closeFormStaff"></div>
      </div>
      <div class="formstaff-body">
        <div class="formstaff-avatar">
          <div class="formstaff-avatar__img"></div>
          <span class="formstaff-avatar__required"
            >(Vui lòng chọn ảnh có định dạng <br />
            <b>.jpg, .jpeg, .png, .gif.</b>)
          </span>
        </div>
        <div class="formstaff-infor">
          <div class="formstaff-infor__title">
            <div>A. THÔNG TIN CHUNG:</div>
            <div class="under-line"></div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Mã nhân viên"
                tabIndex="1"
                ref="employeeCodeInput"
                :required="true"
                v-model="employeeDetailData['EmployeeCode']"
                :inputCheck="inputCheck"
              />
            </div>
            <div class="form-block">
              <base-input
                label="Họ và tên"
                tabIndex="2"
                v-model="employeeDetailData['FullName']"
                :required="true"
                :inputCheck="inputCheck"
              />
            </div>
          </div>

          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Ngày sinh"
                tabIndex="3"
                type="date"
                v-model="employeeDetailData['DateOfBirth']"
              />
            </div>
            <div class="form-block">
              <base-dropdown
                label="Giới tính"
                :dropdownListData="dropdownGenderData"
                v-model="employeeDetailData['Gender']"
                @changeDropdownData="changeDropdownData"
                fieldData="Gender"
                fieldValue="GenderName"
                defaultDropdownValue="Chọn giới tính"
                mode="manual"
              />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Số CMTND/Căn cước"
                type="text"
                tabIndex="5"
                :required="true"
                v-model="employeeDetailData['IdentityNumber']"
                :inputCheck="inputCheck"
              />
            </div>
            <div class="form-block">
              <base-input label="Ngày cấp" type="date" tabIndex="6" />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block form-block__place">
              <base-input
                label="Nơi cấp"
                type="text"
                tabIndex="7"
                v-model="employeeDetailData['IdentityPlace']"
              />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Email"
                type="email"
                tabIndex="8"
                :required="true"
                v-model="employeeDetailData['Email']"
                :inputCheck="inputCheck"
              />
            </div>
            <div class="form-block">
              <base-input
                label="Điện thoại"
                type="text"
                tabIndex="9"
                :required="true"
                v-model="employeeDetailData['PhoneNumber']"
                :inputCheck="inputCheck"
              />
            </div>
          </div>
          <div class="formstaff-infor__title2">
            <div>B. THÔNG TIN CÔNG VIỆC:</div>
            <div class="under-line"></div>
          </div>
          <div class="inline-block">
            <div class="form-block">
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
                label="Vị trí"
              />
            </div>
            <div class="form-block">
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
                label="Phòng ban"
              />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Mã số thuế cá nhân"
                type="text"
                tabIndex="12"
              />
            </div>
            <div class="form-block block-salary">
              <base-input
                label="Mức lương cơ bản"
                type="text"
                tabIndex="13"
                v-model="employeeDetailData['Salary']"
                ref="input-salary"
              />
              <div class="currencyUnit">(VNĐ)</div>
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <base-input
                label="Ngày gia nhập công ty"
                type="date"
                tabIndex="14"
              />
            </div>
            <div class="form-block"></div>
          </div>
        </div>
      </div>
      <div class="formstaff-footer">
        <button class="button-cancel button button-short">Hủy</button>
        <button class="button button-save" @click="saveEmployeeData">
          <div class="btn-icon btn-save"></div>
          <div class="btn-text">Lưu</div>
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/components/employee-detail.css");
</style>

<script>
import BaseInput from "../../components/base/BaseInput.vue";
import BaseCombobox from "../../components/base/BaseCombobox.vue";
import axios from "axios";
import BaseDropdown from "../../components/base/BaseDropdown.vue";

export default {
  name: "EmployeeDetail",
  components: {
    BaseInput,
    BaseCombobox,
    BaseDropdown,
  },
  props: {
    employeeId: {
      type: String,
      default() {
        return "";
      },
    },
    mode: {
      type: String,
      default() {
        return "1";
      },
    },
    isHide: {
      type: Boolean,
      default() {
        return true;
      },
    },
    employeeNewCodeData: {
      type: Object,
      default(){
        return {};
      }
    }
  },
  data() {
    return {
      employeeDetailData: {},
      inputCheck: false,
      comboboxPositionValue: "",
      comboboxPositionData: "",
      comboboxDepartmentValue: "",
      comboboxDepartmentData: "",
      dropdownGenderData: [
        { Gender: "0", GenderName: "Nam" },
        { Gender: "1", GenderName: "Nữ" },
        { Gender: "2", GenderName: "Khác" },
      ],
      departmentListApi: "https://localhost:44350/api/v1/Departments",
      positionListApi: "https://localhost:44350/api/v1/Positions",
    };
  },
  methods: {
    async postEmployee(requestMode, employeeData) {
      try {
        let response;
        if (requestMode == "POST") {
          response = await axios.post(
            "https://localhost:44350/api/v1/Employees/",
            employeeData
          );
        }

        if (requestMode == "PUT") {
          response = await axios.put(
            `https://localhost:44350/api/v1/Employees/` +
              employeeData["EmployeeId"],
            employeeData
          );
        }
        console.log(response);
        return response;
      } catch (error) {
        if (error.response.status == "404") {
          console.log("Not found API url");
        } else {
          console.log(error);
        }
      }
    },

    /**
     *load lại dữ liệu trong bảng
     */
    loadTable() {
      this.$emit("loadTable");
    },

    /**
     * đóng form nhân viên
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    closeFormStaff() {
      this.$emit("closeFormStaff");
    },

    /**
     * thêm mới hoặc sửa thông tin nhân viên
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    async saveEmployeeData() {
      //check validate form
      let isValid = this.validateBeforeSave();
      if (isValid) {
        if (this.mode == "1") {
          //thêm mới nhân viên
          await this.postEmployee("POST", this.employeeDetailData);
        }

        if (this.mode == "2") {
          //sửa nhân viên
          await this.postEmployee("PUT", this.employeeDetailData);
        }
        //Hiện thông báo thêm thành công
        //Close form
        this.closeFormStaff();
        //load lại table
        this.loadTable();
      } else {
        console.log("can't save");
      }
    },
    /**
     * validate email đúng định dạng
     * @param {String} email xâu email người dùng nhập vào
     * @returns {Boolean} true nếu đúng định dạng
     * author: nvdien(8/8/2021)
     * modified: nvdien(8/8/2021)
     */
    validEmail: function (email) {
      var re =
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },

    /**
     * validate trước khi thêm mới hoặc chỉnh sửa thông tin nhân viên
     * author: nvdien(23/8/2021)
     * modifiedBy: nvdien(23/8/2021)
     */
    validateBeforeSave() {
      if (
        this.employeeDetailData["FullName"] == null ||
        this.employeeDetailData["EmployeeCode"] == null ||
        this.employeeDetailData["Email"] == null ||
        this.validEmail(this.employeeDetailData["Email"]) == false ||
        this.employeeDetailData["PhoneNumber"] == null ||
        this.employeeDetailData["IdentityNumber"] == null
      ) {
        this.inputCheck = !this.inputCheck;
        return false;
      }
      return true;
    },

    /**
     * thay đổi giá trị dropdown
     * @param dropdownData :giá trị dropdown
     * @param fieldData: trường thay đổi
     * author: nvdien(23/8/2021)
     * modifiedBy: nvdien(23/8/2021)
     */
    changeDropdownData(dropdownData, fieldData) {
      this.$set(this.employeeDetailData, fieldData, dropdownData);
    },
    /**
     * xóa dữ liệu ở ô input của combobox
     * @param {Int} type xác định loại combobox 1(deparment), 2(position)
     * author: nvdien(7/8/2021)
     * modified: nvdien(7/8/2021)
     */
    clearComboboxValue(type) {
      switch (type) {
        case "department":
          this.comboboxDepartmentValue = "";
          break;
        case "position":
          this.comboboxPositionValue = "";
          break;
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
      switch (type) {
        case "department":
          this.comboboxDepartmentValue = selectedValue;
          break;
        case "position":
          this.comboboxPositionValue = selectedValue;
          break;
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
      switch (type) {
        case "department":
          this.comboboxDepartmentData = comboboxData;
          this.employeeDetailData["DepartmentId"] = comboboxData;
          break;
        case "position":
          this.comboboxPositionData = comboboxData;
          this.employeeDetailData["PositionId"] = comboboxData;
          break;
      }
    },
  },
  watch: {
    employeeId: function (newValue) {
      axios
        .get(`https://localhost:44350/api/v1/Employees/${newValue}`)
        .then((response) => {
          this.employeeDetailData = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    isHide: function (newValue) {
      if (newValue == false) {
        this.$refs.employeeCodeInput.focusInput();
      }
    },
    employeeNewCodeData: function(newValue){
      this.employeeDetailData = newValue;
    }
  },
};
</script>