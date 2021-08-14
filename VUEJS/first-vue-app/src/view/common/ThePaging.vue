<template>
  <div class="content-paging">
    <div class="content-paging__info">
      <p>Hiển thị <b>1-10/1000</b> nhân viên</p>
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
export default {
  name: "ThePaging",
  data() {
    return {
      numberItemsPerPage: [10, 20, 30, 40, 50],
      isShow: false,
      currentIndex: 0,
      totalPage: 11,
      offset: 1,
      pageSize: 10,
      maxPageNumber: 5,
      pageArray: [],
    };
  },
  mounted() {
    if (this.totalPage < this.maxPageNumber) {
      this.pageArray = this.renderArrayPage(1, this.totalPage, 1);
    } else {
      this.pageArray = this.renderArrayPage(1, this.maxPageNumber, 1);
    }
  },
  methods: {
    toogleFilterList() {
      this.isShow = !this.isShow;
    },

    chooseNumber(index) {
      this.currentIndex = index;
      this.pageSize = this.numberItemsPerPage[index];
    },
    renderArrayPage(begin, end, step) {
      return Array.from(
        { length: (end - begin) / step + 1 },
        (_, i) => begin + i * step
      );
    },
    nextPage() {
      let distance = Math.floor(this.maxPageNumber / 2);
      if (this.offset < this.totalPage) {
        this.offset++;
        if (
          this.offset >= distance + 1 &&
          this.offset <= this.totalPage - distance &&
          this.totalPage > this.maxPageNumber
        ) {
          this.pageArray = this.renderArrayPage(
            this.offset - distance,
            this.offset + distance,
            1
          );
        }
      }
    },
    backPage() {
      let distance = Math.floor(this.maxPageNumber / 2);
      if (this.offset > 1) {
        this.offset--;
        if (
          this.offset >= distance + 1 &&
          this.offset <= this.totalPage - distance &&
          this.totalPage > this.maxPageNumber
        ) {
          this.pageArray = this.renderArrayPage(
            this.offset - distance,
            this.offset + distance,
            1
          );
        }
      }
    },
    returnFirstPage() {
      this.offset = 1;
      if (this.totalPage > this.maxPageNumber) {
        this.pageArray = this.renderArrayPage(1, this.maxPageNumber, 1);
      } else {
        this.pageArray = this.renderArrayPage(1, this.totalPage, 1);
      }
    },
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
    },
    choosePage(index) {
      this.offset = index;
      let distance = Math.floor(this.maxPageNumber / 2);
      if (this.offset < this.totalPage) {
        this.offset++;
        if (
          this.offset >= distance + 1 &&
          this.offset <= this.totalPage - distance &&
          this.totalPage > this.maxPageNumber
        ) {
          this.pageArray = this.renderArrayPage(
            this.offset - distance,
            this.offset + distance,
            1
          );
        }
      }
    },
  },
};
</script>

<style scoped>
@import url("../../css/components/paging.css");
</style>

