<template>
  <div class="formwarning-overlay">
    <div class="warning-del">
      <div class="close" @click="closePopup"></div>
      <div class="warning-title">
        <p>Xóa bản ghi</p>
      </div>
      <div class="warning-content">
        <div class="warning-content-icon"></div>
        <div class="warning-content-text">
          <p v-html="contentText" style="margin:0"></p>
        </div>
      </div>
      <div class="warning-footer">
        <button class="button button-short button-cancel" @click="closePopup">
          Hủy
        </button>
        <button
          class="button button-short button-del"
          @click="deleteData(listData, apiUrl)"
        >
          Xóa
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BasePopup",
  props: {
    listData: {
      type: Array,
      default() {
        return [];
      },
    },
    apiUrl: {
      type: String,
      default() {
        return "";
      },
    },
  },

  data() {
    return {};
  },
  methods: {
    async callApi(listData, apiUrl) {
      try {
        for (let i = 0; i < listData.length; ++i) {
          await axios.delete(apiUrl + listData[i].EmployeeId);
        }
      } catch (error) {
        console.log(error);
      }
    },

    async deleteData(listData, apiUrl) {
      await this.callApi(listData, apiUrl);
      //close popup
      this.$emit("closePopup");
      //loadTable
      this.$emit("loadTable");
    },

    closePopup() {
      this.$emit("closePopup");
    },
  },
  computed: {
    contentText() {
      let listData = this.listData;
      let listEmployeeCode;
      if (listData.length == 1) {
        listEmployeeCode = listData[0].EmployeeCode;
      }
      if (listData.length == 2) {
        listEmployeeCode =
          listData[0].EmployeeCode + " và " + listData[1].EmployeeCode;
      }
      let contentText = `Bạn có chắc chắn muốn xóa nhân viên <b>${listEmployeeCode}</b> không ?`;
      if (listData.length > 2) {
        contentText =
          "Bạn có chắc chắn muốn xóa những nhân viên đã chọn hay không ?";
      }
      return contentText;
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/popup.css");
</style>