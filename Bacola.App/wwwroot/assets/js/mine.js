console.log("salamjs");
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
//--------------
//Categoryplus
var toggleIcons = document.querySelectorAll('.toggleIcon');
toggleIcons.forEach(function (icon) {
    icon.addEventListener('click', function () {
        var targetId = icon.getAttribute('data-target');
        var targetUl = document.getElementById(targetId);
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

//--------------
//SerachJsDynamiclyHome
document.getElementById('searchInput').addEventListener('input', performSearch);
const shopCards = document.getElementById('salam');
const searchbutton = document.getElementById('searchButton');
const searchResultsList = document.getElementById('searchResultsList');
async function performSearch() {
    const response = await fetch(`/Shop/SearchProduct?search=${encodeURIComponent(searchTerm)}`);
    var searchTerm = document.getElementById('searchInput').value.trim();
    if (searchTerm.length === 0) {
        console.log("Pis Zhr");
        searchResultsList.innerHTML = ``;

        if (shopCards !== null) {
            location.reload();
           
        }
    }
    try {
        searchButton.style.display = 'none';
        const data = await response.json();
        searchResultsList.innerHTML = ``;
        if (shopCards !== null) {
            shopCards.innerHTML = ` `;
        }
        data.data.forEach(item => {
            if (shopCards !== null) {
                shopCards.innerHTML += `
                     
                            <div  class="product">
                                <div class="product-card">
                                    <div class="card-thumbnail">
                                        <div class="card-badges">
                                            <span class="badge on-sale">${item.discountPercent}</span>
                                            <span class="badge recommend">recommended</span>
                                        </div>
                                       <a asp-controller="shop" asp-action="detail" asp-route-id="${item.id}" class="img-box">
        <img src="./assets/img/product/${item.productImages.find(x => x.isMain && !x.isDeleted)?.image || ''}"
             alt="card-img" />
    </a>
                                         <div class="card-buttons">
                                            <a asp-controller="shop" asp-action="AddWishlist" asp-route-id="${item.id}" class="whislist-btn top-btn">
                                                <i class="fa-solid fa-heart"></i>
                                            </a>
                                        </div>
                                    </div>
                                      <div class="card-content">
                                        <h3 class="card-title">
                                            <a href="" class="title-1">
                                               ${item.title}
                                            </a>
                                        </h3>    
                                        <span class="stock-state">
                                         ${item.inStock ? "In Stock" : "Not In Stock"}
                                        </span>            
                                        <div class="card-rating">
                                            <i class="fa-solid fa-star rating-star"></i>
                                            <i class="fa-solid fa-star rating-star"></i>
                                            <i class="fa-solid fa-star rating-star"></i>
                                            <i class="fa-solid fa-star rating-star"></i>
                                            <i class="fa-solid fa-star rating-star"></i>
                                        </div>
                                        <div class="card-price">
                                            <del>
                                                <bdi>
                                                    <span class="price-amount old-price">${item.price}</span>
                                                </bdi>
                                            </del>
                                            <ins>
                                                <bdi><span class="price-amount new-price">${item.discountPrice}</span></bdi>
                                            </ins>
                                        </div>
                                       <div class="product-fade-block">
                                            <div class="product-button-group">
                                                <div class="quantity">
                                                    <div class="quantity-btn minus">
                                                        <a asp-controller="shop" asp-action="RemoveBasketItem" asp-route-id="${item.id}" class="btn-remove-basket  btn-plus">
                                                            <i style="color:black"
                                                               class="fa-solid fa-minus"></i>
                                                        </a>
                                                    </div>
                                                    <input style="border-left: 0px;border-right:0px" type="text"
                                                           class="quantity-counter" name="quantity" min="0"
                                                           max="57" step="1" value="1" />
                                                    <div class="quantity-btn plus">
                                                        <a asp-controller="shop" asp-action="Addbasket" asp-route-id="${item.id}" class="btn-add-basket  btn-minus">
                                                            <i style="color: black;"
                                                               class="fa-solid fa-plus"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="add-to-cart">
                                                    <a asp-controller="shop" asp-action="AddBasket" asp-route-id="${item.id}" class="btn btn-add-basket btn-primary">Add To Card</a>
                                                </div>
                                            </div>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
            `
            }
            searchResultsList.innerHTML += `
       <li>
            <div class="search-img">
        <img width="90" height="90" src="./assets/img/product/${item.productImages && item.productImages.find(x => x.isMain && !x.isDeleted)?.image || ''}" class="attachment-woocommerce_thumbnail size-woocommerce_thumbnail" alt="" decoding="async" sizes="(max-width: 90px) 100vw, 90px">
            </div>
            <div class="search-content">
                <h1 class="product-title"><a href="">${item.title}</a></h1>
                <span class="price">
                    <del aria-hidden="true"><span class="woocommerce-Price-amount amount"><bdi><span class="woocommerce-Price-currencySymbol">$</span>${item.price}</bdi></span></del>
                    <ins>
                        <span class="woocommerce-Price-amount amount">
                            <bdi>
                                <span class="woocommerce-Price-currencySymbol">$</span>${item.discountPrice}
                            </bdi>
                        </span>
                    </ins>
                </span>
            </div>
       </li>
            `
        })

    }
    catch (error) {
        console.error(error);
        searchResultsList.innerHTML = ``;
        if (shopCards !== null) {
            shopCards.innerHTML = ``;
        }
        
    }
}
//--------------

//CommnetReply
    var replyLinks = document.querySelectorAll('.comment-reply-link');
replyLinks.forEach(function (replyLink) {
    replyLink.addEventListener('click', function (event) {
        event.preventDefault();
        var commentId = this.getAttribute('data-commentid');
        console.log(commentId);
        var commentFormContainer = document.getElementById('commentFormContainer-' + commentId);
        if (commentFormContainer) {
            document.querySelectorAll('.post-comments-form').forEach(function (form) {
                if (form.id !== 'commentFormContainer-' + commentId) {
                    form.style.display = 'none';
                }
            });
            commentFormContainer.style.display = (commentFormContainer.style.display === 'none' || commentFormContainer.style.display === '') ? 'flex' : 'none';
        }
    });
});
//--------------

