<template>
  <div class="content-paging">
    <div class="content-paging__info">
      <p>Hiển thị <b>{{beginPage}}-{{endPage}}/{{totalRecord}}</b> nhân viên</p>
    </div>
    <div class="content-paging__pagination">
      <div class="navigation" id="btn-firstpage" @click="returnFirstPage"></div>
      <div class="navigation" id="btn-prev-page" @click="backPage"></div>

      <div
        class="pagination-number"
        v-for="index of pageArray"
        :key="index"
        :class="{ 'pagination-number--active': offset == index }"
        @click="choosePage(index)"
      >
        <p>{{ index }}</p>
      </div>
      <div class="navigation" id="btn-next-page" @click="nextPage"></div>
      <div class="navigation" id="btn-lastpage" @click="jumpLastPage"></div>
    </div>
    <div
      class="content-paging__filter"
      @click="toogleFilterList"
      :class="{ show: isShow }"
    >
      <div class="paging-filter__value">
        <p>
          <b>{{ pageSize }}</b> nhân viên/trang
        </p>
      </div>
      <div class="paging-filter__icon">
        <i class="fa fa-chevron-up" aria-hidden="true"></i>
        <i class="fa fa-chevron-down" aria-hidden="true"></i>
      </div>
      <ul class="paging-filter__list">
        <li
          v-for="(number, index) in numberItemsPerPage"
          :key="index"
          class="paging-filter__item"
          @click="chooseNumber(index)"
          :class="{ active: currentIndex == index }"
        >
          <b>{{ number }}</b> nhân viên/trang
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
export default {
  name: "ThePaging",
  data() {
    return {
      numberItemsPerPage: [10, 20, 30, 40, 50],
      isShow: false,
      currentIndex: 0,
      offset: 1,
      pageSize: 10,
      maxPageNumber: 5,
      pageArray: [],
      totalPage: 0,
      totalRecord: 0,
    };
  },
  mounted() {
    
    this.loadPaging();
    
  },
  methods: {
    /**
     * call api paging
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    loadPaging(){
      var vm = this;
       axios
        .get( `https://localhost:44350/api/v1/Employees/Filter?searchData=&departmentId&positionId&pageIndex=${vm.offset}&pageSize=${vm.pageSize}`)
        .then((response) => (vm.totalPage = response.data["TotalPage"], vm.totalRecord = response.data["TotalRecord"] ))
        .catch((response) => console.log(response));
    },
    /**
     * Ản hiện chọn số bản ghi trên trang
     * author: nvdien(21/8/2021)
     * modifiedby: nvdien(21/8/2021)
     */
    toogleFilterList() {
      this.isShow = !this.isShow;
    },

    /**
     * cập nhật số bản ghi trên trang
     * @param index: chỉ số trong list danh sách số bản ghi trên trang
     * author: nvdien(21/8/2021)
     * modifiedby: nvdien(21/8/2021)
     */
    chooseNumber(index) {
      this.currentIndex = index;
      this.pageSize = this.numberItemsPerPage[index];
      this.$emit("getPageInfo", this.offset, this.pageSize);
      this.loadPaging();
    },
    /**
     * sinh ra mảng các số
     * @param begin:số bắt đầu
     * @param end: số kết thúc
     * @param step: khoảng cách giữa các số
     * @return mảng các số
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    renderArrayPage(begin, end, step) {
      return Array.from(
        { length: (end - begin) / step + 1 },
        (_, i) => begin + i * step
      );
    },
    /**
     * xử lí khi ấn vào nút next page
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    nextPage() {
      this.handleChangePage(1);
      this.$emit("getPageInfo", this.offset, this.pageSize);
    },
    /**
     * hàm xử lí khi ấn vào nút next page hoặc back page
     * @param type: type = 1 (next page), type = 2(back page)
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    handleChangePage(type) {
      let distance = Math.floor(this.maxPageNumber / 2);
      let minChangeValue;
      let maxchangeValue;
      let begin;
      let end;
      let condition;
      //next page
      if (type == "1") {
        condition = this.offset - this.totalPage;
      }
      //prev page
      if (type == "2") {
        condition = 1 - this.offset;
      }
      if (condition < 0) {
        if (this.maxPageNumber % 2) {
          minChangeValue = distance + 1;
          maxchangeValue = this.totalPage - distance;
        } else {
          if (type == 1) {
            minChangeValue = distance + 2;
          } else minChangeValue = distance + 1;
          maxchangeValue = this.totalPage - distance + 1;
        }

        if (type == 1) this.offset++;
        if (type == 2) this.offset--;
        if (
          this.offset >= minChangeValue &&
          this.offset <= maxchangeValue &&
          this.totalPage > this.maxPageNumber
        ) {
          if (this.maxPageNumber % 2) {
            begin = this.offset - distance;
            end = this.offset + distance;
          } else {
            begin = this.offset - distance;
            end = this.offset + distance - 1;
          }
          console.log("begin " + begin + " end " + end);
          this.pageArray = this.renderArrayPage(begin, end, 1);
          
        }
      }
    },
    /**
     * xử lí khi ấn vào nút back page
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    backPage() {
      this.handleChangePage(2);
      this.$emit("getPageInfo", this.offset, this.pageSize);
    },
    /**
     * quay về trang đầu tiên
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    returnFirstPage() {
      this.offset = 1;
      if (this.totalPage > this.maxPageNumber) {
        this.pageArray = this.renderArrayPage(1, this.maxPageNumber, 1);
      } else {
        this.pageArray = this.renderArrayPage(1, this.totalPage, 1);
      }
      this.$emit("getPageInfo", this.offset, this.pageSize);
    },
    /**
     * nhảy đến trang cuối cùng
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    jumpLastPage() {
      this.offset = this.totalPage;
      if (this.totalPage > this.maxPageNumber) {
        this.pageArray = this.renderArrayPage(
          this.totalPage - this.maxPageNumber + 1,
          this.totalPage,
          1
        );
      } else {
        this.pageArray = this.renderArrayPage(1, this.totalPage, 1);
      }
      this.$emit("getPageInfo", this.offset, this.pageSize);
    },
    /**
     * chọn 1 page
     * @param value: giá trị pageIndex
     * author: nvdien(21/8/2021)
     * modified: nvdien(21/8/2021)
     */
    choosePage(value) {
      this.offset = value;
      this.$emit("getPageInfo", this.offset, this.pageSize);
      let distance = Math.floor(this.maxPageNumber / 2);
      let minChangeValue;
      let maxchangeValue;
      //Update UI
      if (this.maxPageNumber % 2) {
        minChangeValue = distance + 1;
        maxchangeValue = this.totalPage - distance;
        if(value < minChangeValue && value != 1){
          this.pageArray = this.renderArrayPage(1, this.maxPageNumber, 1);
        }
        if(value > maxchangeValue & value != this.totalPage){
          let beginVal = this.totalPage > this.maxPageNumber ? this.totalPage - this.maxPageNumber : 1;
          this.pageArray = this.renderArrayPage(beginVal, this.totalPage, 1);
        }
        if(value >= minChangeValue && value <= maxchangeValue){
          this.pageArray =  this.renderArrayPage(value - distance, value + distance,1);
        }
      } else {
        minChangeValue = distance + 1;
        maxchangeValue = this.totalPage - distance + 1;
        if(value >= minChangeValue && value <= maxchangeValue){
          this.pageArray = this.renderArrayPage(value - distance, value + distance -1,1);
        }
      }
    },
  },
  watch: {
    totalPage: function(){
      if (this.totalPage < this.maxPageNumber) {
      this.pageArray = this.renderArrayPage(1, this.totalPage, 1);
    } else {
      this.pageArray = this.renderArrayPage(1, this.maxPageNumber, 1);
    }
    }
  },
  computed:{
    beginPage: function(){
      return (this.offset - 1)*this.pageSize +1;
    },

    endPage: function(){
      if(this.offset*this.pageSize > this.totalRecord){
        return this.totalRecord;
      }
      return this.offset*this.pageSize;
    }
  }
};
</script>

<style scoped>
@import url("../../css/components/paging.css");
</style>

