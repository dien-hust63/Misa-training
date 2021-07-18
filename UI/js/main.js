// show form staff when click button add staff
var formstaff = document.querySelector(".formstaff-overlay");
formstaff.style.display = "none";
var buttonEmployee = document.querySelector(".button-employee");

buttonEmployee.addEventListener('click', function(){
    formstaff.style.display = "";
});

var buttonCloseForm = document.querySelector(".formstaff-header .cancel");
buttonCloseForm.addEventListener('click', function(){
    formstaff.style.display = "none";
})


