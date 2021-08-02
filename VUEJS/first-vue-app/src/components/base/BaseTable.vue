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
export default {
  name: "BaseTable",
  props: {
    urlAPI: String,
    tableHeaders: Array,
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
      delay: 300,
      clicks: 0,
      timer: null,
    };
  },
  methods: {
    oneClick(index) {
      var self = this;
      this.clicks++;
      if (this.clicks === 1) {
        this.timer = setTimeout(function () {
          self.chooseTableRow(index);
          self.clicks = 0;
        }, this.delay);
      } else {
        clearTimeout(this.timer);
        this.clicks = 0;
        //show form staff
        self.showEditForm(self);
      }
    },
    showEditForm(self) {
      self.$emit("showEditForm");
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
    chooseTableRow(index) {
      const position = this.listSelectedRow.indexOf(index);
      if (position == -1) {
        this.listSelectedRow.push(index);
      } else {
        this.listSelectedRow.splice(position, 1);
      }
    },
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
      } else {
        let month = dateObj.getUTCMonth() + 1;
        let day = dateObj.getUTCDate() + 1;
        let year = dateObj.getUTCFullYear();
        if (month < 10) {
          month = "0" + month;
        }
        if (day < 10) {
          day = "0" + day;
        }
        let newdate = "";
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
      return "";
    },
  },
};
</script>
