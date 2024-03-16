//Home-Slider
$('.home-slider1').slick({
  dots: true,
  infinite: false,
  speed: 300,
  slidesToShow: 1,
  slidesToScroll: 1,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        dots: false,
        slidesToScroll: 1
      }
    }
  ]
});
$('.product-slider').slick({
  dots: false,
  infinite: false,
  speed: 300,
  arrows: true,
  slidesToShow: 4,
  slidesToScroll: 1,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        dots: false,
        arrows: true,
        slidesToScroll: 1
      }
    }

  ]
});


//Shop Product
$('.main-img').slick({
  slidesToShow: 1,
  slidesToScroll: 1,
  speed: 500,
  arrows: false,
  fade: true,
  // Infinity:true,
  asNavFor: '.addition-img'
});
$('.addition-img').slick({
  slidesToShow: 2,
  slidesToScroll: 1,
  speed: 500,
  asNavFor: '.main-img',
  dots: true,
  arrows: false,
  centerMode: true,
  focusOnSelect: true,
  // slide: 'div'
});


//TabMneu
let tabbuttons = document.querySelectorAll('#Tabmenu .container .tabmenu .links button')
console.log(tabbuttons);
console.log("salam");
for (let tbtn of tabbuttons) {
  tbtn.onclick = function () {
    let x = document.querySelector('.tabactive')
    x.classList.remove('tabactive')
    tbtn.classList.add('tabactive')
    let data_id = this.getAttribute('data-id')
    let items = document.querySelectorAll('#Tabmenu .container .tabmenu  .contents div')
    for (let div of items) {
      if (div.getAttribute('id') === data_id) {
        div.classList.remove('d-none')
      }
      else {
        div.classList.add('d-none')
      }
    }
  }
}

// Up button
let up = document.getElementById('Up')
window.onscroll = function () {
  if (document.documentElement.scrollTop > 200) {
    up.style.opacity = '1'
  }
  else {
    up.style.opacity = '0'
  }
}
//Category Dropdown

// var dbtn = document.querySelector("header .header-nav .container .all-categories .collapsed ");
// // // console.log(dbtn);
// // var dropdown=document.querySelector("header .header-nav .container .all-categories .dropdown-category");
// // dropdown.classList


// var dropdown = document.querySelector("header .header-nav .container .all-categories .dropdown-category");
// console.log(dropdown);
// // dropdown.classList.toggle("show");


// var dbtn = document.querySelector("header .header-nav .container .all-categories .collapsed ");
// function togglevisible(dbtn) {
//   var element = document.querySelector("header .header-nav .container .all-categories .dropdown-category");
//   if (element.style.display === "none") {
//     element.style.display = "block";
//   } else {
//     element.style.display = "none";
//   }
// }


function toggleDiv() {
  var dropdown = document.querySelector("header .header-nav .container .all-categories .dropdown-category ")
  if (dropdown.style.display === "none") {
    dropdown.style.display = "block";
  } else {
    dropdown.style.display = "none";
  }
}


// $(document).ready(function () {
//   console.log(tgglebtn)
//   $('header .header-nav .container .toggle-btn').click(function () {
//     $('header .header-nav .container .all-categories .dropdown-category').toggleClass('opened closed');
//   });
// });

// const listItems = document.querySelectorAll("header .header-nav .container .all-categories .dropdown-category .subc");

// console.log(listItems)

// Add event listeners to each < li > element
// listItems.forEach(item => {
//   item.addEventListener('mouseover', () => {
//     // Add the 'hovered' class when mouse enters
//     item.classList.add('hovered');
//   });

//   item.addEventListener('mouseout', () => {
//     // Remove the 'hovered' class when mouse leaves
//     item.classList.remove('hovered');
//   });
// });


//Cartd button HomeSlider
const allAddToCart = document.querySelectorAll("#Hometop .container .row .right-part .best-seller .best-body .product-slider .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter = document.querySelectorAll("#Hometop .container .row .right-part .best-seller .best-body .product-slider .product .product-card .card-content .product-fade-block .product-button-group .quantity");
for (let i = 0; i < allAddToCart.length; i++) {
  allAddToCart[i].addEventListener("click", function () {
    allAddToCart[i].style.display = "none";
    allQuantityCounter[i].style.display = "flex";
  });
}

//Cartd button Homebottom
const allAddToCart2 = document.querySelectorAll("#Hometop .container .row .right-part .new-product .container .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter2 = document.querySelectorAll("#Hometop .container .row .right-part .new-product .container .product .product-card .card-content .product-fade-block .product-button-group .quantity ");
// console.log(allAddToCart2)
// console.log(allQuantityCounter2)
for (let i = 0; i < allAddToCart2.length; i++) {
  allAddToCart2[i].addEventListener("click", function () {
    allAddToCart2[i].style.display = "none";
    allQuantityCounter2[i].style.display = "flex";
  });
}

//Cartd button Homebottom
const allAddToCart3 = document.querySelectorAll("#Shop-part .container .row .shop-primary .container .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter3 = document.querySelectorAll("#Shop-part .container .row .shop-primary .container .product .product-card .card-content .product-fade-block .product-button-group .quantity ");
//quantity and addToCart buttons display values
for (let i = 0; i < allAddToCart3.length; i++) {
  allAddToCart3[i].addEventListener("click", function () {
    allAddToCart3[i].style.display = "none";
    allQuantityCounter3[i].style.display = "flex";
  });
}

//Cartd button Homebottom
const allAddToCart4 = document.querySelectorAll("#Shop-bottom .container .recently .rproduct-body .products .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter4 = document.querySelectorAll("#Shop-bottom .container .recently .rproduct-body .products .product .product-card .card-content .product-fade-block .product-button-group .quantity ");
for (let i = 0; i < allAddToCart4.length; i++) {
  allAddToCart4[i].addEventListener("click", function () {
    allAddToCart4[i].style.display = "none";
    allQuantityCounter4[i].style.display = "flex";
  });
}



//Modal
let locationbtn = document.querySelectorAll('.show-detail-btn')
let modal = document.querySelector(".modal")
let close = document.querySelector('.modal .modal-content .modal-header button')

for (let i = 0; i < locationbtn.length; i++) {
  locationbtn[i].onclick = (e) => {
    modal.style.display = "block";
    modal.classList.add('modal-open')

  }
}

close.onclick = (e) => {
  modal.style.display = "none";
}

//Category filter
function toggleDiv2() {
  var dropdown = document.querySelector("header .header-nav .container .all-categories .dropdown-category ")
  if (dropdown.style.display === "none") {
    dropdown.style.display = "block";
  } else {
    dropdown.style.display = "none";
  }
}






//Product AddedBasket Alert
const addBaskets = document.querySelectorAll('.btn-add-basket');
//console.log(addBaskets);
addBaskets.forEach(basket => basket.addEventListener('click', function (e) {
    e.preventDefault();
    var url = basket.getAttribute('href');
    //console.log(url);
    fetch(`${url}`).then(response => {
        if (response.status == 200) {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                iconColor: "#007bff",
                title: "The product is added successfully to the Basket!"
            });
        }
        else {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "warning",
                iconColor: "#007bff",
                title: "the product can not added to The Basket! Some problem occured!"
            });
        }
    })
}))


//Product PlusMinus Buttons
const plusButtons = document.querySelectorAll('.btn-plus');
const minusButtons = document.querySelectorAll('.btn-minus');
const counterInputs = document.querySelectorAll('.quantity-counter');

minusButtons.forEach((plusButton, index) => {
    plusButton.addEventListener('click', () => {
        let count = parseInt(counterInputs[index].value) + 1;
        count = Math.min(count, parseInt(counterInputs[index].max));
        counterInputs[index].value = count;
    });
});

plusButtons.forEach((minusButton, index) => {
    minusButton.addEventListener('click', () => {
        let count = parseInt(counterInputs[index].value) - 1;
        count = Math.max(count, parseInt(counterInputs[index].min));
        counterInputs[index].value = count;
    });
});






//WishlistAlert
const adwl = document.querySelectorAll('.whislist-btn');
//console.log(adwl);
adwl.forEach(wl => wl.addEventListener('click', function (e) {
    e.preventDefault();
    var url = wl.getAttribute('href');
    //console.log(url);
    fetch(`${url}`).then(response => {
        if (response.status == 200) {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                iconColor: "#007bff",
                title: "The product is added successfully to the Wishlist!"
            });
        }
        else {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "warning",
                iconColor: "#007bff",
                title: "This Product is already exist in your Wishlist!"
            });
        }
    })
}))

//Product RemoveBasket Alert
const removeBaskets = document.querySelectorAll('.btn-plus');
console.log(removeBaskets);
console.log("zahra");
removeBaskets.forEach(basket1 => basket1.addEventListener('click', function (e) {
    e.preventDefault();
    var url = basket1.getAttribute('href');
    //console.log(url);
    fetch(`${url}`).then(response => {
        if (response.status == 200) {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "success",
                iconColor: "red",
                title: "The product is deleted successfully from Basket!"
            });
        }
        else {
            //console.log(response);
            const Toast = Swal.mixin({
                toast: true,
                position: "top-right",
                showConfirmButton: false,
                timer: 10000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.onmouseenter = Swal.stopTimer;
                    toast.onmouseleave = Swal.resumeTimer;
                }
            });
            Toast.fire({
                icon: "warning",
                iconColor: "red",
                title: "the product can not removed! Some problem occured!"
            });
        }
    })
}))



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

//SubcategoryShopPage
var icon = document.getElementById('toggleIcon');
var subUl = document.querySelector('.children');
icon.addEventListener('click', function () {
    subUl.style.display = subUl.style.display === 'none' ? 'block' : 'none';
});
