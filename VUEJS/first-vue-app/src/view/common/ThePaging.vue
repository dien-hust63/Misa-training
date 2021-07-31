<template>
    <div class="content-paging">
      <div>
        <p>Hiển thị 1-10/1000 nhân viên</p>
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
      <div>
        <p>10 nhân viên/trang</p>
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