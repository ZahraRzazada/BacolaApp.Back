const Categoryinputs = document.querySelectorAll("#checkbox");
const Brendinputs = document.querySelectorAll("#checkboxBrend");



const CategoriesKey = 'CategoryIds';
const BrendsKey = 'BrendIds';
const FromPriceKey = 'fromPrice';
const ToPriceKey = 'toPrice';
const IsinStockKey = 'IsInStock';


console.log("OK2");
localStorage.removeItem(CategoriesKey);
localStorage.removeItem(BrendsKey);

let CategorysIds = [];
let BrendsIds = [];

Categoryinputs.forEach(input => {
    input.addEventListener('click', async (e) => {
        console.log("OK");

        CategorysIds = JSON.parse(localStorage.getItem(CategoriesKey)) || [];
        BrendsIds = JSON.parse(localStorage.getItem(BrendsKey)) || [];

        if (CategorysIds == null) {
            CategorysIds.push(e.target.className);
        }


        if (CategorysIds.includes(e.target.className)) {
            CategorysIds = CategorysIds.filter(item => item != e.target.className)
        }
        else {
            CategorysIds.push(e.target.className)
        }

        localStorage.setItem(CategoriesKey, JSON.stringify(CategorysIds));

        console.log(CategorysIds);

        let models = {
            categoryIds: CategorysIds,
            brandIds: BrendsIds
        }


        $.ajax({
            method: "POST",
            url: "/Shop/Filter",
            data: {
                model: models
            },
            success: function (result) {
                $("#Allproducts").empty();
                $("#Allproducts").append(result);
            }
            , error: function () {
                alert("error");
            }
        })
    });
});

Brendinputs.forEach(inputs => {
    inputs.addEventListener('click', async (e) => {

        CategorysIds = JSON.parse(localStorage.getItem(CategoriesKey)) || [];
        BrendsIds = JSON.parse(localStorage.getItem(BrendsKey)) || [];

        if (BrendsIds == null) {
            BrendsIds.push(e.target.className);
        }


        if (BrendsIds.includes(e.target.className)) {
            BrendsIds = BrendsIds.filter(item => item != e.target.className)
        }
        else {
            BrendsIds.push(e.target.className)
        }

        localStorage.setItem(BrendsKey, JSON.stringify(BrendsIds));

        console.log("brend " + BrendsIds);

        let models = {
            categoryIds: CategorysIds,
            brandIds: BrendsIds
        }

        $.ajax({
            method: "POST",
            url: "/Shop/Filter",
            data: {
                model: models
            },
            success: function (result) {
                $("#Allproducts").empty();
                $("#Allproducts").append(result);
            }
        })
    });
});

const rangeInputMin = document.querySelector('.range-input #MinPrice');
console.log(rangeInputMin);
const rangeInputMax = document.querySelector('.range-input #MaxPrice');

rangeInputMin.addEventListener("input", function (e) {
    var value = document.querySelector(".input-min").textContent;
    console.log(value);


    CategorysIds = JSON.parse(localStorage.getItem(CategoriesKey)) || [];
    BrendsIds = JSON.parse(localStorage.getItem(BrendsKey)) || [];

    let models = {
        categoryIds: CategorysIds,
        brandIds: BrendsIds,
        fromPrice: value
    }

    $.ajax({
        method: "POST",
        url: "/Shop/Filter",
        data: {
            model: models
        },
        success: function (result) {

            $("#Allproducts").empty();
            $("#Allproducts").append(result);
        }
    })

});

rangeInputMax.addEventListener('input', function (e) {
    var value = document.querySelector(".input-max").textContent;
    console.log(value);


    CategorysIds = JSON.parse(localStorage.getItem(CategoriesKey)) || [];
    BrendsIds = JSON.parse(localStorage.getItem(BrendsKey)) || [];

    let models = {
        categoryIds: CategorysIds,
        brandIds: BrendsIds,
        toPrice: value
    }

    $.ajax({
        method: "POST",
        url: "/Shop/Filter",
        data: {
            model: models
        },
        success: function (result) {
      
            $("#Allproducts").empty();
            $("#Allproducts").append(result);
        }
    })

});