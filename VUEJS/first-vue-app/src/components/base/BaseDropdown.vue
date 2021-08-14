<template>
  <div class="dropdown" @click="toogleDropdownList" :class="{ show: isShow }">
    <div class="dropdown-value">{{dropdownValue}}</div>
    <i class="fas fa-chevron-down"></i>
    <ul class="dropdown-list">
      <li
        v-for="(item, index) in dropdownListData"
        class="dropdown-item"
        :key="index"
        @click="chooseItem(index)"
        :class="{ active: currentIndex == index }"
      >
        <i class="fas fa-check checkmark"></i>
        <div class="dropdown-item-text">{{ Object.values(item)[0] }}</div>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "BaseDropdown",
  data() {
    return {
      isShow: false,
      dropdownListData: [
        { 0: "Nhà hàng biển Đông" },
        { 1: "Nhà hàng biển Tây" },
        { 2: "Nhà hàng biển Bắc" },
        { 3: "Nhà hàng biển Nam" },
      ],
      dropdownValue: "Nhà hàng biển Đông",
      currentIndex:0,
    };
  },
  methods: {
    toogleDropdownList() {
      this.isShow = !this.isShow;
    },

    chooseItem(index){
        this.currentIndex = index;
        this.dropdownValue = Object.values(this.dropdownListData[index])[0];
    }
  },
};
</script>

<style scoped>
@import url("../../css/base/dropdown.css");
</style>