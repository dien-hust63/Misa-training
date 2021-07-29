<template>
  <div class="content">
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
        <button class="button button-employee">
          <div class="btn-icon btn-employee"></div>
          <div class="btn-text">Thêm nhân viên</div>
        </button>
        <div class="controls-right-refresh"></div>
      </div>
    </div>
    <div class="content-stafftable">
      <table class="table-employee">
        <thead>
          <tr>
            <th></th>
            <th>#</th>
            <th>Mã nhân viên</th>
            <th>Họ và tên</th>
            <th>Giới tính</th>
            <th>Ngày sinh</th>
            <th>Điện thoại</th>
            <th>Email</th>
            <th>Chức vụ</th>
            <th>Phòng ban</th>
            <th>Mức lương cơ bản</th>
            <th>Tình trạng công việc</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(employee, index) in employees" :key="employee.EmployeeId">
            <td>
              <div class="checkbox">
                <i class="fas fa-check"></i>
              </div>
            </td>
               
            <td>
                {{index + 1}}
            </td>
            <td>
              {{ employee.EmployeeCode }}
            </td>
            <td>
              {{ employee.FullName }}
            </td>
            <td>
              {{ employee.GenderName }}
            </td>
            <td class="text-align-center">
              {{formatDate(employee.DateOfBirth, "/")}}
            </td>
            <td>
              {{ employee.PhoneNumber }}
            </td>
            <td>
              {{ employee.Email }}
            </td>
            <td>
              {{ employee.PositionName }}
            </td>
            <td >
              {{ employee.DepartmentName }}
            </td>
            <td class="text-align-right">
              {{ formatMoney(employee.Salary) }}
            </td>
            
            <td>
              {{ employee.WorkStatus }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="content-footer">
      <div>
        <p>Hiển thị 1-10/1000 nhân viên</p>
      </div>
      <div class="pagination">
        <div class="navigation" id="btn-firstpage"></div>
        <div class="navigation" id="btn-prev-page"></div>
        <div class="pagination-number active">
          <p>1</p>
        </div>
        <div class="pagination-number">
          <p>2</p>
        </div>
        <div class="pagination-number">
          <p>3</p>
        </div>
        <div class="pagination-number">
          <p>4</p>
        </div>
        <div class="navigation" id="btn-next-page"></div>
        <div class="navigation" id="btn-lastpage"></div>
      </div>
      <div>
        <p>10 nhân viên/trang</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/layout/content.css");
</style>

<script>
import axios from "axios";
export default {
  name: "TheContent",
  mounted() {
    var vm = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((response) => (vm.employees = response.data))
      .catch((response) => console.log(response));
  },
  data() {
    return {
      employees: [],
    };
  },
  methods:{
       /**
 * Format dữ liệu ngày tháng sang định dạng khác mong muốn
 * seperator = "-" : year/month/day
 * seperator = "/" : day/month/year
 * @param {string} dateString xâu dạng date
 * @param {string} seperator dấu phân cách để chia theo định dạng
 * @returns xâu rỗng hoặc xâu dạng date theo định dạng 
 * Created by: nvdien (20/7/2021)
 */

    formatDate(dateString, seperator) {
        let dateObj = new Date(dateString);
        if (Number.isNaN(dateObj.getTime())) {
            return "";
        }
        else {
            let month = dateObj.getUTCMonth() + 1;
            let day = dateObj.getUTCDate() + 1;
            let year = dateObj.getUTCFullYear();
            if (month < 10) {
                month = "0" + month;
            }
            if (day < 10) {
                day = "0" + day;
            }
            let newdate = '';
            if (seperator == "-") {
                newdate = year + seperator + month + seperator + day;
            }
            if (seperator == "/") {
                newdate = day + seperator + month + seperator + year;
            }
            return newdate;
        }

    },

     /**
     * chuyển từ dạng số sang dạng ngăn cách bởi dấu '.'
     * @param {string} money string tiền tệ
     * @returns string tiền tệ theo đúng định dạng
     * Created by nvdien (20/7/2021)
     */
    formatMoney(money) {
        if (money) {
            return money.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ".");
        }
        return '';
    }

  }
};
</script>