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
          v-for="(item, index) in listData"
          class="dropdown-item"
          :key="index"
          @click="chooseItem(index)"
          :class="{ active: currentIndex == index }"
        >
          <i class="fas fa-check checkmark"></i>
          <div class="dropdown-item-text">
            {{ listData[index][fieldValue] }}
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BaseDropdown",
  props: {
    dropdownListData: {
      // cho chế độ manual
      type: Array,
      default() {
        return [];
      },
    },
    label: {
      type: String,
      default: "",
    },
    mode: {
      type: String,
      default() {
        return "1";
      },
    },
    apiUrl: {
      type: String,
      default() {
        return "";
      },
    },
    value: {
      type: [String, Number],
      default() {
        return "";
      },
    },
    fieldData: {
      type: String,
      default() {
        return "";
      },
    },
    fieldValue: {
      type: String,
      default() {
        return "";
      },
    },
    defaultDropdownValue: {
      type: String,
      default() {
        return "";
      },
    },
  },
  data() {
    return {
      isShow: false,
      dropdownValue: "",
      dropdownData: "",
      currentIndex: 0,
      listData: [],
    };
  },
  methods: {
    /**
     * load dữ liệu và hiển thị
     * author: nvdien(23/8/2021)
     * modifiedBy: nvdien(23/8/2021)
     */
    async getData(apiUrl) {
      const response = await axios.get(apiUrl);
      return response.data;
    },

    /**
     * Ẩn hiện danh sách item trong dropdown
     * author: nvdien(23/8/2021)
     * modifiedBy: nvdien(23/8/2021)
     */
    async toogleDropdownList() {
      try {
        if (this.isShow) {
          this.isShow = false;
        } else {
          if (this.mode == "manual") {
            this.listData = this.dropdownListData;
          } else {
            this.listData = await this.getData(this.apiUrl);
          }
          this.isShow = true;
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Chọn item
     * @param index : chỉ số item được chọn
     */
    chooseItem(index) {
      this.currentIndex = index;
      this.dropdownValue = this.listData[index][this.fieldValue];
    },

    /**
     * So sánh giá trị của property object với một giá trị khác
     * @param item: object
     * @param newValue: giá trị để so sánh
     * author: nvdien(23/8/2021)
     * modifiedBy: nvdien(23/8/2021)
     */
    compareValue(item, newValue) {
      return item[this.fieldData] === newValue;
    },
  },
  watch: {
    currentIndex: function (newValue) {
      this.dropdownData = this.listData[newValue][this.fieldData];
      this.$emit("changeDropdownData", this.dropdownData, this.fieldData);
    },

    value: function (newValue) {
      let obj;
      if (this.mode === "manual") {
        obj = this.dropdownListData.find(
          (item) => item[this.fieldData] == newValue
        );
        this.dropdownValue = obj[this.fieldValue];
      } else {
        
        axios
          .get(this.apiUrl)
          .then(
            (response) => (
              (obj = response.data.find(
                (item) => item[this.fieldData] === newValue
              )),
              (this.dropdownValue = obj[this.fieldValue])
            )
          )
          .catch((error) => console.log(error));
      }
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/dropdown.css");
</style>