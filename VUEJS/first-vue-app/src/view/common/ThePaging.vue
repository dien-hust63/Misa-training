<template>
    <div class="content-paging">
      <div class="content-paging__info">
        <p>Hiển thị <b>1-10/1000</b> nhân viên</p>
      </div>
      <div class="content-paging__pagination">
        <div class="navigation" id="btn-firstpage"></div>
        <div class="navigation" id="btn-prev-page"></div>
        <div class="pagination-number pagination-number--active">
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
      <div class="content-paging__filter">
        <p><b>10</b> nhân viên/trang</p>
        <div class="paging-filter__icon">
          <i class="fa fa-chevron-up" aria-hidden="true"></i>
          <i class="fa fa-chevron-down" aria-hidden="true"></i>
        </div>
      </div>
    </div>
</template>

<style scoped>
    @import url("../../css/components/paging.css");
</style>

<script>
export default {
    name: 'ThePaging',
}
</script>