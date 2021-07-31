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
              <!-- <label>Mã nhân viên(<span class="text-red">*</span>)</label>
                            <input id="employeeId" type="text" class="textbox" tabindex="1" required> -->
              <base-input
                label="Mã nhân viên"
                tabIndex="1"
                :inputValue="employeeCode"
              />
            </div>
            <div class="form-block">
              <!-- <label>Họ và tên(<span class="text-red">*</span>)</label>
              <input
                id="employeeFullName"
                type="text"
                class="textbox"
                tabindex="2"
                required
              /> -->
              <base-input label="Họ và tên" tabIndex="2" />
            </div>
          </div>

          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Ngày sinh</label>
              <input
                id="employeeDateOfBirth"
                type="date"
                class="textbox"
                tabindex="3"
              /> -->
              <base-input label="Ngày sinh" tabIndex="3" type="date" />
            </div>
            <div class="form-block">
              <!-- <label>Giới tính</label>
              <div class="combobox combobox-sex">
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
              </div> -->
              <base-input label="Giới tính" tabIndex="4" />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Số CMTND/Căn cước(<span class="text-red">*</span>)</label>
              <input
                id="employeeIdentityNumber"
                type="text"
                class="textbox"
                tabindex="5"
                required
              /> -->
              <base-input
                label="Số CMTND/Căn cước"
                type="text"
                tabIndex="5"
                required
              />
            </div>
            <div class="form-block">
              <!-- <label>Ngày cấp</label>
              <input
                id="employeeIdentityDate"
                type="date"
                class="textbox"
                tabindex="6"
              /> -->
              <base-input label="Ngày cấp" type="date" tabIndex="6" />
            </div>
          </div>
          <div id="staff-place">
            <!-- <label>Nơi cấp</label>
            <input
              id="employeeIdentityPlace"
              type="text"
              class="textbox"
              tabindex="7"
            /> -->
            <base-input label="Nơi cấp" type="text" tabIndex="7" />
          </div>
          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Email(<span class="text-red">*</span>)</label>
              <input
                id="employeeEmail"
                type="text"
                class="textbox"
                tabindex="8"
                required
              /> -->
              <base-input label="Email" type="text" tabIndex="8" required />
            </div>
            <div class="form-block">
              <!-- <label>Số điện thoại(<span class="text-red">*</span>)</label>
              <input
                id="employeePhone"
                type="text"
                class="textbox"
                tabindex="9"
                required
              /> -->
              <base-input
                label="Điện thoại"
                type="text"
                tabIndex="9"
                required
              />
            </div>
          </div>
          <div class="formstaff-infor__title">
            <div>B. THÔNG TIN CÔNG VIỆC:</div>
            <div class="under-line"></div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Vị trí</label>
              <div class="combobox combobox-position-2">
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
              </div> -->
              <base-input label="Vị trí" type="text" tabIndex="10" />
            </div>
            <div class="form-block">
              <!-- <label>Phòng ban</label>
              <div class="combobox combobox-department-2">
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
              </div> -->
              <base-input label="Phòng ban" type="text" tabIndex="11" />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Mã số thuế cá nhân</label>
              <input
                id="employeeTaxCode"
                type="text"
                class="textbox"
                tabindex="12"
              /> -->
              <base-input
                label="Mã số thuế cá nhân"
                type="text"
                tabIndex="12"
              />
            </div>
            <div class="form-block block-salary">
              <!-- <label>Mức lương cơ bản</label>
              <input
                id="employeeSalary"
                type="text"
                class="textbox salary-value"
                tabindex="13"
              />
              <div class="currencyUnit">
                <p>(VNĐ)</p>
              </div> -->
              <base-input label="Mức lương cơ bản" type="text" tabIndex="13" />
            </div>
          </div>
          <div class="inline-block">
            <div class="form-block">
              <!-- <label>Ngày gia nhập công ty</label>
              <input
                id="employeeJoinDate"
                type="date"
                class="textbox"
                tabindex="14"
              /> -->
              <base-input
                label="Ngày gia nhập công ty"
                type="date"
                tabIndex="14"
              />
            </div>
            <div class="form-block">
              <!-- <label>Tình trạng công việc</label>
              <div class="combobox combobox-workstate">
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
              </div> -->
              <base-input
                label="Tình trạng công việc"
                type="text"
                tabIndex="15"
              />
            </div>
          </div>
        </div>
      </div>
      <div class="formstaff-footer">
        <button class="button-cancel button button-short">Hủy</button>
        <button class="button button-save">
          <div class="btn-icon btn-save"></div>
          <div class="btn-text">Lưu</div>
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/components/employee-detail.css");
@import url("../../css/base/button.css");
</style>

<script>
import BaseInput from "../../components/base/BaseInput.vue";
export default {
  name: "EmployeeDetail",
  components: {
    BaseInput,
  },
  props:{
    employeeCode:{
      type:String,
      default(){
        return "";
      }
    }
  },
  data() {
    return {
      // employeeCode: "",
    };
  },
  methods: {
    closeFormStaff() {
      this.$emit("closeFormStaff");
    },
  },
 
};
</script>