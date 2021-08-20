<template>
  <div class="header">
    <div class="header-left">
      <div class="toggle"></div>
      <div class="logo"></div>
    </div>
    <div class="header-right">
      <base-dropdown :dropdownListData="dropdownListData" />
      <div class="user">
        <div class="user-avatar"></div>
        <div class="user-name">Nguyễn Văn Diện</div>
        <div class="user-option"></div>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../css/layout/header.css");
@import url("../../css/base/dropdown.css");
</style>

<script>
import BaseDropdown from "../base/BaseDropdown.vue";
export default {
  name: "TheHeader",
  components: {
    BaseDropdown,
  },
  data() {
    return {
      dropdownListData: [
        { 0: "Nhà hàng biển Đông" },
        { 1: "Nhà hàng biển Tây" },
        { 2: "Nhà hàng biển Bắc" },
        { 3: "Nhà hàng biển Nam" },
      ],
    };
  },
};
</script>