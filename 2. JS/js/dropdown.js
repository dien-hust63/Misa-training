{/* <li class="dropdown-item">
    <div class="dropdown-icon checkmark"></div>
    <div class="dropdown-item-text">
        Nhà hàng Biển Đông
    </div>
</li> */}
var dropdownRestaurantData = [
    "Nhà hàng Biển Đông",
    "Nhà hàng Biển Tây",
    "Nhà hàng biển Bắc",
    "Nhà hàng Biển Nam"
];

var dropdownDepartmentData = [
    "Phòng kĩ thuật",
    "Phòng nhân sự",
    "Phòng quản lí"
];

var dropdownPositionData = [
    "Nhân viên",
    "Quản lí nhân sự",
    "Trưởng phòng"
]

var dropdownSexData = [
    "Nam",
    "Nữ",
    "Khác"
]

var dropdownWorkStateData = [
    "Đang làm việc",
    "Xin nghỉ phép"
]


var currentIndex = 0;
var dropdownRestaurant= document.querySelector(".dropdown-restaurant");
var dropdownDepartment = document.querySelector(".dropdown-department");
var dropdownPosition = document.querySelector(".dropdown-position");
var dropdownDepartmentForm = document.querySelector(".formstaff .dropdown-department");
var dropdownPositionForm = document.querySelector(".formstaff .dropdown-position");
var dropdownSex = document.querySelector(".dropdown-sex");
var dropdownWorkState = document.querySelector(".dropdown-workstate");
var dropdowns = document.querySelectorAll(".dropdown");
function renderDropDown(dropdownData,dropdown) {
    var itemData = '';
    
    var listData = dropdown.querySelector(".dropdown-list");
    var currentData = dropdown.querySelector(".dropdown-value");
    currentData.textContent = dropdownData[currentIndex];
    for (var i = 0; i < dropdownData.length; i++) {
        if (i == currentIndex) {
            itemData += `<li data-id=${i} class="dropdown-item"><i class="fas fa-check checkmark"></i><div class="dropdown-item-text">${dropdownData[i]}</div></li>`;
        }
        else {
            itemData += `<li data-id=${i} class="dropdown-item"><i class="fas fa-check"></i><div class="dropdown-item-text">${dropdownData[i]}</div></li>`;
        }
    }

    listData.innerHTML = itemData;
    var itemDataList = listData.querySelectorAll(".dropdown-item");
    itemDataList[currentIndex].style.backgroundColor = "#019160";
    itemDataList[currentIndex].style.color = "#fff";
    var i = 0;
    itemDataList.forEach(element => {
        element.addEventListener('click', function () {
            currentData.textContent = element.querySelector(".dropdown-item-text").textContent;
            currentIndex = element.getAttribute("data-id");
            renderDropDown(dropdownData,dropdown);
        });
    });
}
renderDropDown(dropdownRestaurantData, dropdownRestaurant);
renderDropDown(dropdownDepartmentData, dropdownDepartment);
renderDropDown(dropdownPositionData, dropdownPosition);
renderDropDown(dropdownSexData, dropdownSex);
renderDropDown(dropdownWorkStateData, dropdownWorkState);
renderDropDown(dropdownDepartmentData, dropdownDepartmentForm);
renderDropDown(dropdownPositionData, dropdownPositionForm);



dropdowns.forEach(dropdown => {
    
    dropdown.addEventListener('click', function(){
        dropdown.classList.toggle('show');
        if(dropdown.classList.contains('show')){
            dropdown.querySelector(".fa-chevron-down").style.transform = 'rotate(180deg)';
        }
        else{
            dropdown.querySelector(".fa-chevron-down").style.transform = 'rotate(0deg)';
        }
    })
})


