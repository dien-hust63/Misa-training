// show list when click to combobox

var comboboxDepartment = document.querySelector(".combobox-department");
var comboboxPosition = document.querySelector(".combobox-position");
var comboboxSex = document.querySelector(".combobox-sex");
var comboboxPosition2 = document.querySelector('.combobox-position-2');
var comboboxDepartment2 = document.querySelector(".combobox-department-2");
var comboboxWorkState = document.querySelector(".combobox-workstate");
var comboboxDepartmentData = [];
var comboboxPositionData = [];
var comboboxSexData = [
    "Nam", 
    "Nữ",
    "Không xác định"
]
var comboboxWorkStateData = [
    "0",
    "1",
    "2",
    "3"
]
var currentIndex = 0;
var indexListItem = -1;
$(document).ready(function () {
    getData();
})

function getData() {
    $.ajax({
        url: 'http://cukcuk.manhnv.net/api/Department',
        method: 'GET'
    })
        .done(function (res) {
            res.forEach(element => {
                comboboxDepartmentData.push(element["DepartmentName"]);
            })
            console.log(comboboxDepartmentData);
            renderCombobox(comboboxDepartmentData, comboboxDepartment);
            renderCombobox(comboboxDepartmentData, comboboxDepartment2);
        })
        .fail(function () {
            alert("Get api department fail");
        })

    $.ajax({
        url: 'http://cukcuk.manhnv.net/v1/Positions',
        method: 'GET'
    })
        .done(function (res) {
            res.forEach(element => {
                comboboxPositionData.push(element["PositionName"]);
            })
            console.log(comboboxPositionData);
            renderCombobox(comboboxPositionData, comboboxPosition);
            renderCombobox(comboboxPositionData, comboboxPosition2);
        })
        .fail(function () {
            alert("get API position fail");
        })
    renderCombobox(comboboxSexData, comboboxSex);
    renderCombobox(comboboxWorkStateData, comboboxWorkState);
}

/**
 * gọi đến các sự kiện trên combobox
 * @param {string array} comboboxData là mảng chứa dữ liệu đổ ra combobox
 * @param {} combobox
 * author: nvdien (24/7/2021)
 */
function renderCombobox(comboboxData, combobox){
    renderComboboxData(comboboxData, combobox);
    let comboboxDropdownIcon = combobox.querySelector(".combobox__dropdown");
    comboboxDropdownIcon.addEventListener('click', function () {
        console.log("test");
        combobox.classList.toggle("show");
    });
    let comboboxList = combobox.querySelector(".combobox__list");
    

     //search combobox khi nhập vào ô input
    let comboboxInput = combobox.querySelector("input");
    comboboxInput.addEventListener('input', function () {
        let itemData = '';
        let comboboxItemData = '';
        for (let i = 0; i < comboboxData.length; i++) {
            comboboxItemData = comboboxData[i].toLowerCase();
            if (comboboxItemData.includes(this.value.toLowerCase())) {
                itemData += `<li data-id=${i} class="combobox__item"><i class="fas fa-check checkmark"></i> <div class="combobox-item-text">${comboboxData[i]}</div></li>`;
            }
        }
        let comboboxList = combobox.querySelector(".combobox__list");
        comboboxList.innerHTML = itemData;
        comboboxItems = comboboxList.querySelectorAll("li");
        comboboxItems.forEach(comboboxItem => {
            comboboxItem.addEventListener('click', function () {
                currentIndex = comboboxItem.getAttribute('data-id');
                comboboxInput.value = comboboxData[currentIndex];
                // combobox.classList.add("hide");
                combobox.classList.remove("show");
                renderCombobox(comboboxData, combobox);
            })
        })
    })

    //mở combobox list khi focus vào ô input
    comboboxInput.addEventListener('focus', function () {
        combobox.classList.add("show");
    })
    
    //xóa dữ liệu trong input khi ấn vào nút cancel trong input
    let comboboxInputCancel = combobox.querySelector(".combobox__input-cancel");
    comboboxInputCancel.addEventListener('click', function () {
        comboboxInput.value = '';
    })
}

/**
 * đổ dữ liệu lên combobox list, hiển thị kết quả lên input khi người dùng chọn 1 item
 * @param {string array} comboboxData là mảng chứa dữ liệu đổ ra combobox
 * @param {} combobox
 * author: nvdien(24/7/2021)
 */
function renderComboboxData(comboboxData, combobox) {
    let itemData = '';
    let comboboxList = combobox.querySelector(".combobox__list");
    let comboboxInput = combobox.querySelector("input");
    for (var i = 0; i < comboboxData.length; ++i) {
        if (i == currentIndex) {
            itemData += `<li data-id=${i} class="combobox__item active"><i class="fas fa-check checkmark"></i> <div class="combobox-item-text">${comboboxData[i]}</div></li>`;
        } else {
            itemData += `<li data-id=${i} class="combobox__item"><i class="fas fa-check checkmark"></i> <div class="combobox-item-text">${comboboxData[i]}</div></li>`;
        }
    }
    comboboxList.innerHTML = itemData;
    let comboboxItems = comboboxList.querySelectorAll("li");
    comboboxItems.forEach(comboboxItem => {
        comboboxItem.addEventListener('click', function () {
            currentIndex = comboboxItem.getAttribute('data-id');
            comboboxInput.value = comboboxData[currentIndex];
            // combobox.classList.add("hide");
            combobox.classList.remove("show");
            renderComboboxData(comboboxData, combobox);
        })
    })
}
