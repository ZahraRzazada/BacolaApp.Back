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
});
//--------------
//TabMneu
let tabbuttons = document.querySelectorAll('#Tabmenu .container .tabmenu .links button')
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
//--------------
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
function toggleDiv() {
  var dropdown = document.querySelector("header .header-nav .container .all-categories .dropdown-category ")
  if (dropdown.style.display === "none") {
    dropdown.style.display = "block";
  } else {
    dropdown.style.display = "none";
  }
}
//--------------
//Cartd button HomeSlider
const allAddToCart = document.querySelectorAll("#Hometop .container .row .right-part .best-seller .best-body .product-slider .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter = document.querySelectorAll("#Hometop .container .row .right-part .best-seller .best-body .product-slider .product .product-card .card-content .product-fade-block .product-button-group .quantity");
for (let i = 0; i < allAddToCart.length; i++) {
  allAddToCart[i].addEventListener("click", function () {
    allAddToCart[i].style.display = "none";
    allQuantityCounter[i].style.display = "flex";
  });
}
//--------------
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
//--------------
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
//--------------
//Cartd button Homebottom
const allAddToCart4 = document.querySelectorAll("#Shop-bottom .container .recently .rproduct-body .products .product .product-card .card-content .product-fade-block .product-button-group .add-to-cart");
const allQuantityCounter4 = document.querySelectorAll("#Shop-bottom .container .recently .rproduct-body .products .product .product-card .card-content .product-fade-block .product-button-group .quantity ");
for (let i = 0; i < allAddToCart4.length; i++) {
  allAddToCart4[i].addEventListener("click", function () {
    allAddToCart4[i].style.display = "none";
    allQuantityCounter4[i].style.display = "flex";
  });
}
//--------------
//Category filter
function toggleDiv2() {
  var dropdown = document.querySelector("header .header-nav .container .all-categories .dropdown-category ")
  if (dropdown.style.display === "none") {
    dropdown.style.display = "block";
  } else {
    dropdown.style.display = "none";
  }
}
//--------------

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
//--------------
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
//--------------

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
//--------------

//Product RemoveBasket Alert
const removeBaskets = document.querySelectorAll('.btn-plus');
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
//--------------






