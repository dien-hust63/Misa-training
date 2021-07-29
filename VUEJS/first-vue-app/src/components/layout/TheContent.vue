<template>
    <div class="content">
        <div class="content-controls">
            <div class="content-controls-left">
                <div class="controls-left-lable">
                    <p>Danh sách nhân viên</p>
                </div>
                <div class="controls-left-filter">
                    <input class="search-input" type="text" placeholder="Tìm kiếm theo Mã, Tên hoặc Số điện thoại">

                    <div class="combobox combobox-department">
                        <input type="text" class="combobox__input" placeholder="Chọn/Nhập thông tin">
                        <div class="combobox__input-cancel">
                            <i class="fas fa-times-circle"></i>
                        </div>
                        <div class="combobox__dropdown">
                            <i class="fas fa-chevron-down combobox__icon"></i>
                        </div>
                        <ul class="combobox__list">
                        </ul>
                    </div>

                    <div class="combobox combobox-position">
                        <input type="text" class="combobox__input" placeholder="Chọn/Nhập thông tin">
                        <div class="combobox__input-cancel">
                            <i class="fas fa-times-circle"></i>
                        </div>
                        <div class="combobox__dropdown">
                            <i class="fas fa-chevron-down combobox__icon"></i>
                        </div>
                        <ul class="combobox__list">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="content-controls-right">
                <button class="button button-employee">
                    <div class="btn-icon btn-employee"></div>
                    <div class="btn-text">Thêm nhân viên</div>
                </button>
                <div class="controls-right-refresh"></div>
            </div>
        </div>
        <div class="content-stafftable">
            <table class="table-employee">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th>#</th>
                        <th>Mã nhân viên</th>
                        <th>Họ và tên</th>
                        <th>Giới tính</th>
                        <th>Ngày sinh</th>
                        <th>Điện thoại</th>
                        <th>Email</th>
                        <th>Chức vụ</th>
                        <th>Phòng ban</th>
                        <th>Mức lương cơ bản</th>
                        <th>Tình trạng công việc</th>
                    </tr>
                </thead>
                <tbody>

                </tbody>
            </table>

        </div>

        <div class="content-footer">
            <div>
                <p>Hiển thị 1-10/1000 nhân viên</p>
            </div>
            <div class="pagination">
                <div class="navigation" id="btn-firstpage"></div>
                <div class="navigation" id="btn-prev-page"></div>
                <div class="pagination-number active">
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

    </div>
</template>

<style scoped>
    @import url('../../css/layout/content.css');
</style>