//PriceFilter
const rangeInput = document.querySelectorAll(".range-input input");
const priceValue = document.querySelectorAll(".price-value");
const range = document.querySelector(".progress");
let priceGap = 10;

function validateRange(e) {
    let minVal = parseInt(rangeInput[0].value);
    let maxVal = parseInt(rangeInput[1].value);

    if (maxVal - minVal < priceGap) {
        if (e.target.className === "range-min") {
            rangeInput[0].value = maxVal - priceGap;
        } else {
            rangeInput[1].value = minVal + priceGap;
        }
    } else {
        priceValue[0].textContent = `$${minVal}`;
        priceValue[1].textContent = `$${maxVal}`;
        range.style.left = `${(minVal / rangeInput[0].max) * 100}%`;
        range.style.right = `${100 - (maxVal / rangeInput[1].max) * 100}%`;
    }
}

rangeInput.forEach((input) => {
    input.addEventListener("input", validateRange);
});

validateRange();

//categoryplus
var toggleIcons = document.querySelectorAll('.toggleIcon');
toggleIcons.forEach(function (icon) {
    icon.addEventListener('click', function () {
        // Get the target ul element
        var targetId = icon.getAttribute('data-target');
        var targetUl = document.getElementById(targetId);

        // Toggle the display style of the target ul
        if (targetUl.style.display === 'none') {
            targetUl.style.display = 'block';
            icon.classList.remove('fa-plus');
            icon.classList.add('fa-minus');
        } else {
            targetUl.style.display = 'none';
            icon.classList.remove('fa-minus');
            icon.classList.add('fa-plus');
        }
    });
});