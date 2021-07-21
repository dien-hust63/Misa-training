// show form staff when click button add staff
var formstaff = document.querySelector(".formstaff-overlay");
formstaff.style.display = "none";
var buttonEmployee = document.querySelector(".button-employee");

//Hiển thị form nhân viên khi ấn nút thêm nhân viên
buttonEmployee.addEventListener('click', function(){
    formstaff.style.display = "";
});

//Ẩn form nhân viên khi bấm cancel
var buttonCloseForm = document.querySelector(".formstaff-header .cancel");
buttonCloseForm.addEventListener('click', function(){
    formstaff.style.display = "none";
})


