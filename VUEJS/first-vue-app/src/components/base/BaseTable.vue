<template>
  <div class="content__stafftable">
    <table class="table-employee">
      <thead>
        <tr>
          <th></th>
          <th>#</th>
          <th v-for="(tableHeader, index) in tableHeaders" :key="index">
            {{ Object.values(tableHeader)[0] }}
          </th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(tableContent, index) in tableContents"
          :key="tableContent.EmployeeId"
          @click="oneClick(index)"
          :class="{ 'row--selected': isSelectedRow(index) }"
        >
          <td>
            <div
              class="checkbox"
              :class="{ 'checkbox--active': isSelectedRow(index) }"
            >
              <i class="fas fa-check"></i>
            </div>
          </td>
          <td>
            {{ index + 1 }}
          </td>
          <td
            v-for="(tableHeader, index) in tableHeaders"
            :key="index"
            :class="textAlignObject(index)"
          >
            {{ formatTableContent(tableContent, tableHeader) }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
@import url("../../css/components/employee-table.css");
</style>

<script>
import axios from "axios";
import CommonMethods from '../../mixins/CommonMethods.js'
import { eventBus } from '../../main.js';
export default {
  name: "BaseTable",
  mixins: [CommonMethods],
  props: {
    urlAPI: {
      type:String,
      default(){
        return "";
      }
    },
    tableHeaders: {
      type:Array,
      default(){
        return [];
      }
    },
    newTableContents:{
      type:Array,
      default(){
        return [];
      }
    },
    isUpdate:{
      type:Boolean,
    }

  },
  mounted() {
    var vm = this;
    axios
      .get(this.urlAPI)
      .then((response) => (vm.tableContents = response.data))
      .catch((response) =>(
        console.log(response)
      )
      );
  },
  data() {
    return {
      tableContents: [],
      isActive: false,
      listSelectedRow: [],
      listSelectedEmployees: [],
      delay: 300,
      clicks: 0,
      timer: null,
    };
  },
  methods: {
    /**
     * xác định xem người dùng click hay double click vào đòng trên bảng
     *@param {Int} index: chỉ sổ của dòng trong bảng
     * author: nvdien(2/8/2021)
     */
    oneClick(index) {
      var self = this;
      this.clicks++;
      if (this.clicks === 1) {
        this.timer = setTimeout(function () {
          self.chooseTableRow(index);
          eventBus.$emit("showSelectedEmployees", self.listSelectedEmployees);
          self.clicks = 0;
        }, this.delay);
      } else {
        clearTimeout(this.timer);
        this.clicks = 0;
        //show form staff
        self.editEmployee(self, self.tableContents[index]);
      }
    },
    /**
     * @param {}  self: component hiện tại
     * @param {Object} employeeData chứa dữ liệu nhân viên
     * author: nvdien(5/8/2021)
     * modified: nvdien(5/8/2021)
     */
    editEmployee(self, employeeData) {
      self.$emit("editEmployee", employeeData);
    },
    /**
     * định dạng dữ liệu trong ô của table bên trái, giữa hay phải
     *  @param {Int} index : chỉ số ứng với header của table
     *  return chuỗi class định dạng
     * author: nvdien(2/8/2021)
     */
    textAlignObject(index) {
      let typeFormat = this.tableHeaders[index].type;
      switch (typeFormat) {
        case "0":
          return "text-align-left";
        case "1":
          return "text-align-center";
        case "2":
          return "text-align-right";
        default:
          return "";
      }
    },

    /**
     * thêm hoặc bớt chỉ số của hàng vào trong danh sách các hàng được chọn
     * @param {Int} index chỉ số của hàng chọn
     * author: nvdien(4/8/2021)
     */
    chooseTableRow(index) {
      const position = this.listSelectedRow.indexOf(index);
      if (position == -1) {
        this.listSelectedRow.push(index);
        this.listSelectedEmployees.push(this.tableContents[index]);
      } else {
        this.listSelectedRow.splice(position, 1);
        this.listSelectedEmployees.splice(this.tableContents[index], 1);
      }
    },

    /**
     * Kiểm tra xem index đã có trong danh sách hàng được chọn chưa
     * @param {Int} index index của hàng được chọn
     * return {boolean} đúng nếu đã có trong đanh sách, sai nếu chưa co
     * author: nvdien(4/8/2021)
     */
    isSelectedRow(index) {
      return this.listSelectedRow.includes(index);
    },

    /**
     * Lấy giá trị của ô trong table và định dạng theo convention ngày, tiền lương
     * @param {Object} tableContent : chứa thông tin api trả về
     * @param {Object} tableHeader: chứa thông tin  Header của bảng
     * return chuỗi chứa giá trị được đổ lên bảng theo đúng định dạng
     * Author: nvdien(30/7/2021)
     */
    formatTableContent(tableContent, tableHeader) {
      let cellData = tableContent[Object.keys(tableHeader)[0]];
      let typeFormat = Object.values(tableHeader)[1];
      if (typeFormat === "1") {
        // định dạng ngày
        return this.formatDate(cellData, "/");
      }
      if (typeFormat === "2") {
        // định dạng tiền lương
        return this.formatMoney(cellData);
      }
      return cellData;
    },

    
  },
  watch:{
    isUpdate: function(){
      this.tableContents = this.newTableContents;
    }
  }
};
</script>
