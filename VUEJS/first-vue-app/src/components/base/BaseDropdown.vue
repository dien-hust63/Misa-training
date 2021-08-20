<template>
  <div class="dropdown-wrapper">
    <div class="dropdown__lablel" style="margin-bottom: 4px" v-if="label">
      {{ label }}
    </div>
    <div class="dropdown" @click="toogleDropdownList" :class="{ show: isShow }">
      <div class="dropdown-value">{{ dropdownValue }}</div>
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
  </div>
</template>

<script>
export default {
  name: "BaseDropdown",
  props: {
    dropdownListData: {
      type: Array,
      default() {
        return [];
      },
    },
    label: {
      type:String,
      default: "",
    }
  },
  data() {
    return {
      isShow: false,
      dropdownValue: Object.values(this.dropdownListData[0])[0],
      currentIndex: 0,
    };
  },
  methods: {
    toogleDropdownList() {
      this.isShow = !this.isShow;
    },

    chooseItem(index) {
      this.currentIndex = index;
      this.dropdownValue = Object.values(this.dropdownListData[index])[0];
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/dropdown.css");
</style>