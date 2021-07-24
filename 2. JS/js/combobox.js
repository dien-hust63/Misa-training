// show list when click to combobox

var comboboxDepartment = document.querySelector(".combobox-department");
var comboboxPosition = document.querySelector(".combobox-position");
var comboboxDepartmentData = [];
var comboboxPositionData = [];
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

        })
        .fail(function () {
            alert("get API position fail");
        })
}

function renderCombobox(comboboxData, combobox) {
    let itemData = '';
    let comboboxList = combobox.querySelector(".combobox__list");
    var comboboxInput = combobox.querySelector("input");
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
            combobox.classList.add("hide");
            combobox.classList.remove("show");
            renderCombobox(comboboxData, combobox);
        })
    })
    comboboxInput.addEventListener('input', function () {
        console.log("alo");
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
                combobox.classList.add("hide");
                combobox.classList.remove("show");
                renderCombobox(comboboxData, combobox);
            })
        })
    })
    comboboxInput.addEventListener('focus', function () {
        combobox.classList.add("show");
    })
    let comboboxDropdownIcon = combobox.querySelector(".combobox__dropdown");
    comboboxDropdownIcon.addEventListener('click', function () {
        if(combobox.classList.contains("show")){
            combobox.classList.remove("show");
        }
        else{
            combobox.classList.add("show");
        }
    });


    let comboboxInputCancel = combobox.querySelector(".combobox__input-cancel");

    comboboxInputCancel.addEventListener('click', function () {
        comboboxInput.value = '';
    })

}
